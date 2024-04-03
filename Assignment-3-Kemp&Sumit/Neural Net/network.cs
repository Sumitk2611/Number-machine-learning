using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NumSharp;

namespace Assignment_3_Kemp_Sumit.Neural_Net
{
     class network1
    {
        private int num_layers;
        private NDArray sizes;
        private List<NDArray> biases;
        private List<NDArray> weights;

        private static Random rng = new Random();

        private List<int> epochResults = new List<int>();

        public List<int> getResults()
        {
            return epochResults;
        }

        public static void shuffle<T>(List<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        public network1(int[] size)
        {
            num_layers = size.Length;
            sizes = size;
            biases = new List<NDArray>();
            weights = new List<NDArray>();
            for (int y = 1; y < size.Length; y++)
            {
                biases.Add(np.random.randn(size[y], 1));
            }
            for (int x = 0, y = 1; y < size.Length; x++, y++)
            {
                weights.Add(np.random.randn(size[y], size[x]));
            }
        }

        public NDArray feedforward(NDArray a)
        {
            for (int i = 0; i < biases.Count; i++)
            {
                a = sigmoid(np.dot(weights[i], a) + biases[i]);
            }
            return a;
        }

        public void ParallelSGD(List<Tuple<NDArray, NDArray>> training_data, int epochs, int mini_batch_size, double eta, List<Tuple<NDArray, NDArray>> test_data)
        {
            int n_test = test_data?.Count ?? 0;
            int n = training_data.Count;

            for (int j = 0; j < epochs; j++)
            {
                shuffle(training_data);

                List<List<Tuple<NDArray, NDArray>>> mini_batches = new List<List<Tuple<NDArray, NDArray>>>();
                for (int k = 0; k < n; k += mini_batch_size)
                {
                    mini_batches.Add(training_data.GetRange(k, Math.Min(mini_batch_size, n - k)));
                }

                Parallel.ForEach(mini_batches, mini_batch =>
                {
                    update_mini_batch(mini_batch, eta);
                });

                if (test_data != null)
                {
                    int res = evaluate(test_data);
                    Console.WriteLine($"Epoch {j}: {res} / {n_test}");
                    epochResults.Add(res);
                }
                else
                {
                    Console.WriteLine($"Epoch {j} complete");
                }
            }
        }


        //not finished translating
        public void SGD(List<Tuple<NDArray, NDArray>> training_data, int epochs, int mini_batch_size, double eta, List<Tuple<NDArray, NDArray>> test_data)
        {
            int n_test = 0;
            if (test_data != null) n_test = test_data.Count;
            int n = training_data.Count;

            for (int j = 0; j < epochs; j++)
            {
                shuffle(training_data);

                //not sure if this is a list of list of tuples
                List<List<Tuple<NDArray, NDArray>>> mini_batches = new List<List<Tuple<NDArray, NDArray>>>();

                for (int k = 0; k < n; k += mini_batch_size)
                {
                    mini_batches.Add(training_data.GetRange(k, mini_batch_size));
                }

                foreach (List<Tuple<NDArray, NDArray>> mini_batch in mini_batches)
                {
                    update_mini_batch(mini_batch, eta);
                }

                if (test_data != null)
                {
                    int res = evaluate(test_data);
                    Console.WriteLine("Epoch {0}: {1} / {2}", j, res, n_test);
                    epochResults.Add(res); // add epoch to results
                }
                else
                {
                    Console.WriteLine("Epoch {0} complete", j);
                }
            }
        }

        public void update_mini_batch(List<Tuple<NDArray, NDArray>> mini_batch, double eta)
        {
            List<NDArray> nabla_b = new List<NDArray>();
            for (int i = 0; i < biases.Count; i++) { nabla_b.Add(np.zeros(biases[i].shape)); }

            List<NDArray> nabla_w = new List<NDArray>();
            for (int i = 0; i < weights.Count; i++) { nabla_w.Add(np.zeros(weights[i].shape)); }

            for (int i = 0; i < mini_batch.Count; i++)
            {
                Tuple<List<NDArray>, List<NDArray>> back = backprop(mini_batch[i].Item1, mini_batch[i].Item2);
                List<NDArray> delta_nabla_b = back.Item1;
                List<NDArray> delta_nabla_w = back.Item2;
                for (int j = 0; j < nabla_b.Count; j++)
                {
                    nabla_b[j] = nabla_b[j] + delta_nabla_b[j];
                }

                for (int j = 0; j < nabla_w.Count; j++)
                {
                    nabla_w[j] = nabla_w[j] + delta_nabla_w[j];
                }

            }

            for (int i = 0; i < weights.Count; i++)
            {
                weights[i] = weights[i] - (eta / mini_batch.Count) * nabla_w[i];
            }

            for (int i = 0; i < biases.Count; i++)
            {
                biases[i] = biases[i] - (eta / mini_batch.Count) * nabla_b[i];
            }
        }

        public Tuple<List<NDArray>, List<NDArray>> backprop(NDArray x, NDArray y)
        {

            List<NDArray> nabla_b = new List<NDArray>();
            for (int i = 0; i < biases.Count; i++) { nabla_b.Add(np.zeros(biases[i].shape)); }

            List<NDArray> nabla_w = new List<NDArray>();
            for (int i = 0; i < weights.Count; i++) { nabla_w.Add(np.zeros(weights[i].shape)); }

            //feedforward
            NDArray activation = x;
            List<NDArray> activations = new List<NDArray>();
            activations.Add(x);
            List<NDArray> zs = new List<NDArray>();
            for (int i = 0; i < biases.Count; i++)
            {
                NDArray z = np.dot(weights[i], activation) + biases[i];
                zs.Add(z);
                activation = sigmoid(z);
                activations.Add(activation);
            }

            //backward pass
            NDArray delta = cost_derivative(activations.Last(), y) * sigmoid_prime(zs.Last());
            nabla_b[nabla_b.Count - 1] = delta;
            nabla_w[nabla_w.Count - 1] = np.dot(delta, activations[activations.Count - 2].transpose());

            for (int l = num_layers - 1; l > 2; l--)
            {
                NDArray z = zs[l];
                NDArray sp = sigmoid_prime(z);
                delta = np.dot(weights[l + 1].transpose(), delta) * sp;
                nabla_b[l] = delta;
                nabla_w[l] = np.dot(delta, activations[l - 1].transpose());
            }

            Tuple<List<NDArray>, List<NDArray>> result = new Tuple<List<NDArray>, List<NDArray>>(nabla_b, nabla_w);
            return result;
        }

        public int evaluate(List<Tuple<NDArray, NDArray>> test_data)
        {
            List<Tuple<NDArray, NDArray>> test_results = new List<Tuple<NDArray, NDArray>>();

            for (int i = 0; i < test_data.Count; i++)
            {
                NDArray x = np.argmax(feedforward(test_data[i].Item1));
                NDArray y = test_data[i].Item2;
                test_results.Add(Tuple.Create<NDArray, NDArray>(x, y));
            }

            int sum = 0;
            for (int i = 0; i < test_results.Count; i++)
            {
                if (np.array_equal(np.atleast_1d(test_results[i].Item1), test_results[i].Item2))
                {
                    sum++;
                }
            }
            return sum;
        }

        public int evaluateSample(NDArray image)
        {
            image = image.astype(np.float32);
            image /= 255.0;
            image = image.reshape(784, 1);
            int x = np.asscalar<int>(np.argmax(feedforward(image)));
            Console.WriteLine(x);
            return x;
        }

        public NDArray cost_derivative(NDArray output_activations, NDArray y)
        {
            return (output_activations - y);
        }

      

        public NDArray sigmoid(NDArray z)
        {
            NDArray expZ = z * (-1);
            expZ = np.exp(expZ);

            // Calculate the sigmoid function using element-wise arithmetic
            NDArray one = np.ones_like(z); // Create an NDArray with all elements as 1
            NDArray sigmoidZ = 1.0 / (one + expZ);

            return sigmoidZ;

        }

        public NDArray sigmoid_prime(NDArray z)
        {
            return sigmoid(z) * (1 - sigmoid(z));
        }
    }
}
