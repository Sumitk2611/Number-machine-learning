namespace Assignment_3_Kemp_Sumit
{
    using MathNet.Numerics;
    using MathNet.Numerics.LinearAlgebra;
    using Neural_Net;
    using NumSharp;
    using System;
    using System.Diagnostics.Eventing.Reader;
    using System.Drawing.Drawing2D;

    public partial class Form1 : Form
    {
        private Point previousPoint;
        bool isMouseDown = false;

        byte[][] DataSetInputs;
        byte[] DataSetLables;
        byte[][] TestSetInputs;
        byte[] TestSetLables;

        mnist_loader ml = new mnist_loader();
        NeuralNetwork N;
        Dictionary<int, float> avgs;

        public List<Tuple<NDArray, NDArray>> training_data;
        public List<Tuple<NDArray, NDArray>> validation_data;
        public List<Tuple<NDArray, NDArray>> test_data;

        private string epochs, miniBatch, learnRate;
        private string layers;

        public Form1()
        {
            InitializeComponent();
            pictureBox1.MouseDown += pictureBox1_MouseDown;
            pictureBox1.MouseMove += pictureBox1_MouseMove;
            pictureBox1.MouseUp += pictureBox1_MouseUp;

            Tuple<List<Tuple<NDArray, NDArray>>, List<Tuple<NDArray, NDArray>>, List<Tuple<NDArray, NDArray>>> tuple = ml.load_data_wrapper(); // testing for now
            Bitmap bmp = new Bitmap(280, 280);
            using (Graphics gfx = Graphics.FromImage(bmp))
            {
                gfx.Clear(Color.White);
            }
            pictureBox1.Image = bmp;
            training_data = tuple.Item1;
            validation_data = tuple.Item2;
            test_data = tuple.Item3;
            LoadDataSet();
            LoadTestSet();

        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            previousPoint = e.Location;
            isMouseDown = true;
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (isMouseDown == true)
            {
                if (previousPoint != null)
                {
                    if (pictureBox1.Image == null)
                    {
                        Bitmap bmp = new Bitmap(280, 280);
                        using (Graphics gfx = Graphics.FromImage(bmp))
                        {
                            gfx.Clear(Color.White);
                        }
                        pictureBox1.Image = bmp;
                    }
                    using (Graphics g = Graphics.FromImage(pictureBox1.Image))
                    {
                        g.DrawLine(new Pen(Color.Black, 30), previousPoint, e.Location);
                        g.SmoothingMode = SmoothingMode.AntiAlias;
                    }
                    pictureBox1.Invalidate();
                    previousPoint = e.Location;
                }
            }
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            isMouseDown = false;
            previousPoint = Point.Empty;
        }

        private void clear_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = null;
        }

        private void predict_Click(object sender, EventArgs e)
        {
            //Bitmap res = new Bitmap(pictureBox1.Image,pictureBox1.Image.Width / 10, pictureBox1.Height / 10);

            Bitmap res = new Bitmap(28, 28);
            using (Graphics g = Graphics.FromImage((Image)res))
            {
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                g.DrawImage(pictureBox1.Image, 0, 0, 28, 28);
            }

            result.Image = res;

            double[] imageArray = new double[28 * 28];
            for (int i = 0; i < 28; i++)
            {
                for (int j = 0; j < 28; j++)
                {
                    Color pixelColor = res.GetPixel(j, i);
                    // Assuming original MNIST images are white on black, invert colors by subtracting from 255
                    // Convert to grayscale (here assuming the drawing is black on white background) and normalize to [0,1]
                    double grayscaleValue = 255 - ((pixelColor.R + pixelColor.G + pixelColor.B) / 3.0);
                    imageArray[i * 28 + j] = grayscaleValue / 255.0;
                }
            }
            Vector<double> Inputs = Vector<double>.Build.DenseOfArray(imageArray);

            int predict = N.FeedForward(Inputs).AbsoluteMaximumIndex();

            MessageBox.Show("Prediction: " + predict.ToString());
        }


        private void train_Click(object sender, EventArgs e)
        {
            long milliseconds = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond; ;

            int eps = 30; int mbs = 10;
            double learn = 3.0;
            int neurons = 30;

            if (int.TryParse(epsLabel.Text, out int epsVal))
            {
                eps = epsVal;
            }
            else
            {
                MessageBox.Show("Epochs must be a integer");
            }

            if (int.TryParse(mbsLabel.Text, out int mbsVal))
            {
                mbs = mbsVal;
            }
            else
            {
                MessageBox.Show("Mini Batch must be an integer");
            }

            if (double.TryParse(learnRateLabel.Text, out double learnRate))
            {
                learn = learnRate;
            }
            else
            {
                MessageBox.Show("Learning Rate has to be a  double");
            }

            if (int.TryParse(layerLabel.Text, out int layerVal))
            {
                neurons = layerVal;
            }
            else
            {
                MessageBox.Show("Invalid Layers");
            }
            N = new NeuralNetwork(new int[] { 784, neurons, 10 });
            Vector<double>[] Inputs = new Vector<double>[DataSetInputs.Length];
            Vector<double>[] Outputs = new Vector<double>[DataSetInputs.Length]; ;
            for (int i = 0; i < DataSetInputs.Length; i++)
            {
                Inputs[i] = Vector<double>.Build.DenseOfArray(DataSetInputs[i].Select(x => Convert.ToDouble(x) / 255).ToArray());
                Outputs[i] = Vector<double>.Build.Dense(10);
                Outputs[i][DataSetLables[i]] = 1;
            }
            N.Train(Inputs, Outputs, learnRate, eps, mbs);
            ValidationTests();
           
        }

        void LoadDataSet()
        {
            byte[] file = System.IO.File.ReadAllBytes("train-images.idx3-ubyte");
            byte[] file2 = System.IO.File.ReadAllBytes("train-labels.idx1-ubyte");
            byte[] sizear = new byte[4];
            Array.Copy(file, 4, sizear, 0, 4);
            Array.Reverse(sizear);
            int DataSetSize = BitConverter.ToInt32(sizear, 0);
            DataSetInputs = new byte[DataSetSize][];
            DataSetLables = new byte[DataSetSize];
            Array.Copy(file2, 8, DataSetLables, 0, DataSetSize);
            for (int y = 0; y < DataSetSize; y++)
            {
                DataSetInputs[y] = new byte[784];
                Array.Copy(file, 16 + 784 * y, DataSetInputs[y], 0, 784);
            }
        }
        void LoadTestSet()
        {
            byte[] file = System.IO.File.ReadAllBytes("t10k-images.idx3-ubyte");
            byte[] file2 = System.IO.File.ReadAllBytes("t10k-labels.idx1-ubyte");
            byte[] sizear = new byte[4];
            Array.Copy(file, 4, sizear, 0, 4);
            Array.Reverse(sizear);
            int DataSetSize = BitConverter.ToInt32(sizear, 0);
            TestSetInputs = new byte[DataSetInputs.Length][];
            TestSetLables = new byte[DataSetSize];
            Array.Copy(file2, 8, TestSetLables, 0, DataSetSize);
            for (int y = 0; y < DataSetSize; y++)
            {
                TestSetInputs[y] = new byte[784];
                Array.Copy(file, 16 + 784 * y, TestSetInputs[y], 0, 784);
            }
        }
        void ValidationTests()
        {
            //Validation Set Test
            double percent = 0;
            for (int i = 0; i < 10000; i++)
            {
                Vector<double> Inputs = Vector<double>.Build.DenseOfArray(TestSetInputs[i].Select(x => Convert.ToDouble(x) / 255).ToArray());
                if (N.FeedForward(Inputs).AbsoluteMaximumIndex() == TestSetLables[i])
                {
                    percent++;
                }
            }
            percent = 100 * percent / 10000;
            MessageBox.Show("Done Training");
            precision.Text = "Precision : " + percent.ToString() + "%";

        }
    }
}