using NumSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_3_Kemp_Sumit.Neural_Net
{
     class mnist_loader
    {
        private const string TrainImages = "\\Data\\train-images-idx3-ubyte\\train-images.idx3-ubyte";
        
        private const string TrainLabels = "\\Data\\train-labels-idx1-ubyte\\train-labels.idx1-ubyte";
        private const string TestImages = "\\Data\\t10k-images-idx3-ubyte\\t10k-images.idx3-ubyte";
        private const string TestLabels = "\\Data\\t10k-labels-idx1-ubyte\\t10k-labels.idx1-ubyte";

        public Tuple<List<Tuple<NDArray, NDArray>>, List<Tuple<NDArray, NDArray>>, List<Tuple<NDArray, NDArray>>> load_data()
        {

            string path = Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName).FullName;
            Console.WriteLine(path);
            List<Tuple<NDArray, NDArray>> training_data = ReadTrainingData(path).ToList(); // 60k
            List<Tuple<NDArray, NDArray>> validation_data = new List<Tuple<NDArray, NDArray>>(); // last 10k elements of original training_data

            validation_data.AddRange(training_data.GetRange(50000, 10000)); // add last 10k training_data to validation_data
            training_data.RemoveRange((training_data.Count() - 1) - 10000, 10000); // remove those 10k from training_data so its 50k

            List<Tuple<NDArray, NDArray>> test_data = ReadTestData(path).ToList();

            return Tuple.Create(training_data, validation_data, test_data);
        }

        /*
         * Support method for reading the training data 
         */
        private IEnumerable<Tuple<NDArray, NDArray>> ReadTrainingData(string path)
        {
            foreach (var item in Read(path + TrainImages, path + TrainLabels))
            {
                yield return item;
            }
        }

        /*
         * Support method for reading the test data
         */
        private IEnumerable<Tuple<NDArray, NDArray>> ReadTestData(string path)
        {
            foreach (var item in Read(path + TestImages, path + TestLabels))
            {
                yield return item;
            }
        }

        /*
         * Support method for reading in the data from the filepath specified
         */
        private IEnumerable<Tuple<NDArray, NDArray>> Read(string imagesPath, string labelsPath)
        {
            BinaryReader images, labels;

            using (var labelStream = new FileStream(labelsPath, FileMode.Open))
            using (var imageStream = new FileStream(imagesPath, FileMode.Open))
            {
                using (labels = new BinaryReader(labelStream))
                using (images = new BinaryReader(imageStream))
                {

                    int magicImageNumber = images.ReadBigInt32(); // discard
                    int numberOfImages = images.ReadBigInt32();
                    int width = images.ReadBigInt32(); // width of image
                    int height = images.ReadBigInt32(); // height of image

                    int magicLabelsNumber = labels.ReadBigInt32(); // discard
                    int numberOfLabels = labels.ReadBigInt32(); // not needed since number of labels is the same as number of images

                    for (int i = 0; i < numberOfImages; i++)
                    {
                        var bytes = images.ReadBytes(width * height);
                        var arr = new byte[height, width];

                        arr.ForEach((j, k) => arr[j, k] = bytes[j * height + k]);

                        var imageData = np.arange(width * height).reshape(28, 28); // first ndarray for image data
                        imageData = arr;

                        byte bLabel = labels.ReadByte();
                        int iLabel = bLabel;
                        var labelNd = np.full(iLabel, 1); // second ndarray for label

                        var result = new Tuple<NDArray, NDArray>(imageData, labelNd);

                        yield return result;
                    }
                }
            }

        }

        public Tuple<List<Tuple<NDArray, NDArray>>, List<Tuple<NDArray, NDArray>>, List<Tuple<NDArray, NDArray>>> load_data_wrapper()
        {

            Tuple<List<Tuple<NDArray, NDArray>>, List<Tuple<NDArray, NDArray>>, List<Tuple<NDArray, NDArray>>> all_data = load_data();
            List<Tuple<NDArray, NDArray>> tr_d = all_data.Item1; // training data
            List<Tuple<NDArray, NDArray>> va_d = all_data.Item2; // validation data
            List<Tuple<NDArray, NDArray>> te_d = all_data.Item3; // test data

            List<Tuple<NDArray, NDArray>> training_data = new List<Tuple<NDArray, NDArray>>();

            foreach (var tup in tr_d)
            {
                var input = tup.Item1; // image data
                var result = tup.Item2; // digit

                var training_input = np.reshape(input, 784, 1);
                var training_result = vectorized_result(result[0]); // vectorize the digit

                training_data.Add(Tuple.Create(training_input, training_result));
            }

            List<Tuple<NDArray, NDArray>> validation_data = new List<Tuple<NDArray, NDArray>>();

            foreach (var tup in va_d)
            {
                var input = tup.Item1;

                var validation_input = np.reshape(input, 784, 1);

                validation_data.Add(Tuple.Create(validation_input, tup.Item2));
            }

            List<Tuple<NDArray, NDArray>> test_data = new List<Tuple<NDArray, NDArray>>();

            foreach (var tup in te_d)
            {
                var input = tup.Item1;

                var test_input = np.reshape(input, 784, 1);

                test_data.Add(Tuple.Create(test_input, tup.Item2));
            }

            return Tuple.Create(training_data, validation_data, test_data);
        }

        public NDArray vectorized_result(int j)
        {
            var e = np.zeros(10, 1);
            e[j] = 1.0;
            return e;
        }
    }
}
