namespace Assignment_3_Kemp_Sumit
{
    using Neural_Net;
    using NumSharp;
    using System.Diagnostics.Eventing.Reader;
    using System.Drawing.Drawing2D;

    public partial class Form1 : Form
    {
        private Point previousPoint;
        bool isMouseDown = false;

        mnist_loader ml = new mnist_loader();
        network Network;
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

            training_data = tuple.Item1;
            validation_data = tuple.Item2;
            test_data = tuple.Item3;
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            previousPoint = e.Location;
            isMouseDown =true;
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

            Bitmap res = new Bitmap(pictureBox1.Image.Width / 10, pictureBox1.Height / 10);
            using (Graphics g = Graphics.FromImage((Image)res))
            {
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
                g.DrawImage(pictureBox1.Image, 0, 0, pictureBox1.Image.Width / 10, pictureBox1.Height / 10);
            }

            result.Image = res;

            NDArray n = res.ToNDArray(flat: false, copy: true, discardAlpha: false).reshape(28, 28, 4);

            NDArray rgb = n["0:28,0:28,3"];
            rgb = rgb.reshape(784, 1);

            int predict = Network.evaluateSample(rgb);
            
            MessageBox.Show("Prediction: " + predict.ToString());
        }


        private void train_Click(object sender, EventArgs e)
        {
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

            Network = new network(new int[] { 784, neurons, 10 });
            Network.SGD(training_data, eps, mbs, learn, test_data);

            // show results
            List<int> results = Network.getResults();
            int bestEpoch = results[0];

            for (int i = 0; i < results.Count(); i++)
            {
                if (results[i] > bestEpoch)
                {
                    bestEpoch = results[i];
                }
            }
            MessageBox.Show("Best classification rate is : " + ((double)bestEpoch / 100) + "%");


        }
    }
}