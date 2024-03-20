namespace Assignment_3_Kemp_Sumit
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            pictureBox1 = new PictureBox();
            panel1 = new Panel();
            train = new Button();
            label4 = new Label();
            eps = new NumericUpDown();
            label3 = new Label();
            mbs = new NumericUpDown();
            label2 = new Label();
            learnRate = new NumericUpDown();
            label1 = new Label();
            layers = new NumericUpDown();
            label5 = new Label();
            label6 = new Label();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)eps).BeginInit();
            ((System.ComponentModel.ISupportInitialize)mbs).BeginInit();
            ((System.ComponentModel.ISupportInitialize)learnRate).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layers).BeginInit();
            SuspendLayout();
            // 
            // pictureBox1
            // 
            pictureBox1.BorderStyle = BorderStyle.FixedSingle;
            pictureBox1.Location = new Point(12, 139);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(503, 368);
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // panel1
            // 
            panel1.BackColor = SystemColors.ControlLight;
            panel1.Controls.Add(train);
            panel1.Controls.Add(label4);
            panel1.Controls.Add(eps);
            panel1.Controls.Add(label3);
            panel1.Controls.Add(mbs);
            panel1.Controls.Add(label2);
            panel1.Controls.Add(learnRate);
            panel1.Controls.Add(label1);
            panel1.Controls.Add(layers);
            panel1.Location = new Point(609, 139);
            panel1.Name = "panel1";
            panel1.Size = new Size(369, 368);
            panel1.TabIndex = 1;
            // 
            // train
            // 
            train.Cursor = Cursors.Hand;
            train.Location = new Point(219, 305);
            train.Name = "train";
            train.Size = new Size(118, 39);
            train.TabIndex = 8;
            train.Text = "Train";
            train.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(64, 100);
            label4.Name = "label4";
            label4.Size = new Size(78, 25);
            label4.TabIndex = 7;
            label4.Text = "Epochs :";
            // 
            // eps
            // 
            eps.Location = new Point(157, 98);
            eps.Name = "eps";
            eps.Size = new Size(180, 31);
            eps.TabIndex = 6;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(3, 169);
            label3.Name = "label3";
            label3.Size = new Size(139, 25);
            label3.TabIndex = 5;
            label3.Text = "Mini Batch Size :";
            // 
            // mbs
            // 
            mbs.Location = new Point(157, 167);
            mbs.Name = "mbs";
            mbs.Size = new Size(180, 31);
            mbs.TabIndex = 4;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(14, 237);
            label2.Name = "label2";
            label2.Size = new Size(128, 25);
            label2.TabIndex = 3;
            label2.Text = "Learning Rate :";
            // 
            // learnRate
            // 
            learnRate.Location = new Point(157, 235);
            learnRate.Name = "learnRate";
            learnRate.Size = new Size(180, 31);
            learnRate.TabIndex = 2;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(72, 28);
            label1.Name = "label1";
            label1.Size = new Size(70, 25);
            label1.TabIndex = 1;
            label1.Text = "Layers :";
            // 
            // layers
            // 
            layers.Location = new Point(157, 26);
            layers.Name = "layers";
            layers.Size = new Size(180, 31);
            layers.TabIndex = 0;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 20F, FontStyle.Bold, GraphicsUnit.Point);
            label5.Location = new Point(673, 71);
            label5.Name = "label5";
            label5.Size = new Size(247, 54);
            label5.TabIndex = 2;
            label5.Text = "Train Model";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 20F, FontStyle.Bold, GraphicsUnit.Point);
            label6.Location = new Point(12, 71);
            label6.Name = "label6";
            label6.Size = new Size(123, 54);
            label6.TabIndex = 3;
            label6.Text = "Draw";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1025, 674);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(panel1);
            Controls.Add(pictureBox1);
            Name = "Form1";
            Text = "Assignment 3";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)eps).EndInit();
            ((System.ComponentModel.ISupportInitialize)mbs).EndInit();
            ((System.ComponentModel.ISupportInitialize)learnRate).EndInit();
            ((System.ComponentModel.ISupportInitialize)layers).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox pictureBox1;
        private Panel panel1;
        private NumericUpDown layers;
        private Label label1;
        private Label label4;
        private NumericUpDown eps;
        private Label label3;
        private NumericUpDown mbs;
        private Label label2;
        private NumericUpDown learnRate;
        private Button train;
        private Label label5;
        private Label label6;
    }
}