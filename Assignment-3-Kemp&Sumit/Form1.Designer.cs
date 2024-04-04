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
            learnRateLabel = new TextBox();
            mbsLabel = new TextBox();
            epsLabel = new TextBox();
            layerLabel = new TextBox();
            train = new Button();
            label4 = new Label();
            label3 = new Label();
            label2 = new Label();
            label1 = new Label();
            label5 = new Label();
            label6 = new Label();
            clear = new Button();
            predict = new Button();
            result = new PictureBox();
            precision = new Label();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)result).BeginInit();
            SuspendLayout();
            // 
            // pictureBox1
            // 
            pictureBox1.BorderStyle = BorderStyle.FixedSingle;
            pictureBox1.Location = new Point(34, 90);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(382, 378);
            pictureBox1.SizeMode = PictureBoxSizeMode.AutoSize;
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // panel1
            // 
            panel1.BackColor = SystemColors.ControlLight;
            panel1.Controls.Add(learnRateLabel);
            panel1.Controls.Add(mbsLabel);
            panel1.Controls.Add(epsLabel);
            panel1.Controls.Add(layerLabel);
            panel1.Controls.Add(train);
            panel1.Controls.Add(label4);
            panel1.Controls.Add(label3);
            panel1.Controls.Add(label2);
            panel1.Controls.Add(label1);
            panel1.Location = new Point(688, 100);
            panel1.Name = "panel1";
            panel1.Size = new Size(369, 368);
            panel1.TabIndex = 1;
            // 
            // learnRateLabel
            // 
            learnRateLabel.Location = new Point(164, 239);
            learnRateLabel.Name = "learnRateLabel";
            learnRateLabel.Size = new Size(173, 31);
            learnRateLabel.TabIndex = 12;
            // 
            // mbsLabel
            // 
            mbsLabel.Location = new Point(161, 166);
            mbsLabel.Name = "mbsLabel";
            mbsLabel.Size = new Size(176, 31);
            mbsLabel.TabIndex = 11;
            // 
            // epsLabel
            // 
            epsLabel.Location = new Point(157, 97);
            epsLabel.Name = "epsLabel";
            epsLabel.Size = new Size(180, 31);
            epsLabel.TabIndex = 10;
            // 
            // layerLabel
            // 
            layerLabel.Location = new Point(157, 28);
            layerLabel.Name = "layerLabel";
            layerLabel.Size = new Size(180, 31);
            layerLabel.TabIndex = 9;
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
            train.Click += train_Click;
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
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(3, 169);
            label3.Name = "label3";
            label3.Size = new Size(139, 25);
            label3.TabIndex = 5;
            label3.Text = "Mini Batch Size :";
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
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(72, 28);
            label1.Name = "label1";
            label1.Size = new Size(70, 25);
            label1.TabIndex = 1;
            label1.Text = "Layers :";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 20F, FontStyle.Bold, GraphicsUnit.Point);
            label5.Location = new Point(752, 32);
            label5.Name = "label5";
            label5.Size = new Size(247, 54);
            label5.TabIndex = 2;
            label5.Text = "Train Model";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 20F, FontStyle.Bold, GraphicsUnit.Point);
            label6.Location = new Point(136, 32);
            label6.Name = "label6";
            label6.Size = new Size(123, 54);
            label6.TabIndex = 3;
            label6.Text = "Draw";
            // 
            // clear
            // 
            clear.Location = new Point(304, 50);
            clear.Name = "clear";
            clear.Size = new Size(112, 34);
            clear.TabIndex = 4;
            clear.Text = "Clear";
            clear.UseVisualStyleBackColor = true;
            clear.Click += clear_Click;
            // 
            // predict
            // 
            predict.Location = new Point(304, 484);
            predict.Name = "predict";
            predict.Size = new Size(112, 34);
            predict.TabIndex = 5;
            predict.Text = "Predict";
            predict.UseVisualStyleBackColor = true;
            predict.Click += predict_Click;
            // 
            // result
            // 
            result.Location = new Point(34, 519);
            result.Name = "result";
            result.Size = new Size(114, 111);
            result.SizeMode = PictureBoxSizeMode.StretchImage;
            result.TabIndex = 6;
            result.TabStop = false;
            // 
            // precision
            // 
            precision.AutoSize = true;
            precision.Location = new Point(691, 506);
            precision.Name = "precision";
            precision.Size = new Size(91, 25);
            precision.TabIndex = 7;
            precision.Text = "Precision: ";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1415, 692);
            Controls.Add(precision);
            Controls.Add(result);
            Controls.Add(predict);
            Controls.Add(clear);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(panel1);
            Controls.Add(pictureBox1);
            Name = "Form1";
            Text = "Assignment 3";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)result).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox pictureBox1;
        private Panel panel1;
        private Label label1;
        private Label label4;
        private Label label3;
        private Label label2;
        private Button train;
        private Label label5;
        private Label label6;
        private Button clear;
        private Button predict;
        private TextBox learnRateLabel;
        private TextBox mbsLabel;
        private TextBox epsLabel;
        private TextBox layerLabel;
        private PictureBox result;
        private Label precision;
    }
}