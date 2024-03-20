namespace Assignment_3_Kemp_Sumit
{
    public partial class Form1 : Form
    {
        private Point? previousPoint;
        readonly Pen pen = new Pen(Color.Black, 5);

        public Form1()
        {
            InitializeComponent();
            pictureBox1.MouseDown += pictureBox1_MouseDown;
            pictureBox1.MouseMove += pictureBox1_MouseMove;
            pictureBox1.MouseUp += pictureBox1_MouseUp;
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            previousPoint = e.Location;
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && previousPoint.HasValue)
            {
                using (Graphics g = pictureBox1.CreateGraphics())
                {
                    g.DrawLine(pen, previousPoint.Value, e.Location);
                }
                previousPoint = e.Location;
            }
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            previousPoint = null;
        }
    }
}