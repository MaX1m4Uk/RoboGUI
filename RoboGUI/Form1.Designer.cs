namespace RoboGUI
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
            startBroadcast = new Button();
            stopBroadcast = new Button();
            modeView = new Button();
            videoView = new PictureBox();
            rotateView = new Button();
            flipHor = new CheckBox();
            flipVer = new CheckBox();
            resizeComboBox = new ComboBox();
            noiseFilterComboBox = new ComboBox();
            binaryCheckBox = new CheckBox();
            grayscaleCheckBox = new CheckBox();
            paletteComboBox = new ComboBox();
            trackBar1 = new TrackBar();
            morfologyCheckBox = new CheckBox();
            morfologyComboBox = new ComboBox();
            ((System.ComponentModel.ISupportInitialize)videoView).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trackBar1).BeginInit();
            SuspendLayout();
            // 
            // startBroadcast
            // 
            startBroadcast.Location = new Point(12, 398);
            startBroadcast.Name = "startBroadcast";
            startBroadcast.Size = new Size(150, 46);
            startBroadcast.TabIndex = 0;
            startBroadcast.Text = "Start broadcast";
            startBroadcast.UseVisualStyleBackColor = true;
            startBroadcast.Click += startBroadcast_Click;
            // 
            // stopBroadcast
            // 
            stopBroadcast.Location = new Point(12, 450);
            stopBroadcast.Name = "stopBroadcast";
            stopBroadcast.Size = new Size(150, 46);
            stopBroadcast.TabIndex = 1;
            stopBroadcast.Text = "Stop broadcast";
            stopBroadcast.UseVisualStyleBackColor = true;
            stopBroadcast.Click += stopBroadcast_Click;
            // 
            // modeView
            // 
            modeView.Location = new Point(12, 502);
            modeView.Name = "modeView";
            modeView.Size = new Size(150, 46);
            modeView.TabIndex = 2;
            modeView.Text = "Mode";
            modeView.UseVisualStyleBackColor = true;
            modeView.Click += modeView_Click;
            // 
            // videoView
            // 
            videoView.Location = new Point(12, 12);
            videoView.Name = "videoView";
            videoView.Size = new Size(1240, 380);
            videoView.SizeMode = PictureBoxSizeMode.Zoom;
            videoView.TabIndex = 3;
            videoView.TabStop = false;
            // 
            // rotateView
            // 
            rotateView.Location = new Point(12, 554);
            rotateView.Name = "rotateView";
            rotateView.Size = new Size(150, 46);
            rotateView.TabIndex = 4;
            rotateView.Text = "Rotate";
            rotateView.UseVisualStyleBackColor = true;
            rotateView.Click += rotateView_Click;
            // 
            // flipHor
            // 
            flipHor.AutoSize = true;
            flipHor.Location = new Point(12, 606);
            flipHor.Name = "flipHor";
            flipHor.Size = new Size(103, 19);
            flipHor.TabIndex = 5;
            flipHor.Text = "Flip Horizontal";
            flipHor.UseVisualStyleBackColor = true;
            flipHor.CheckedChanged += flipHor_CheckedChanged;
            // 
            // flipVer
            // 
            flipVer.AutoSize = true;
            flipVer.Location = new Point(12, 631);
            flipVer.Name = "flipVer";
            flipVer.Size = new Size(86, 19);
            flipVer.TabIndex = 6;
            flipVer.Text = "Flip Vertical";
            flipVer.UseVisualStyleBackColor = true;
            flipVer.CheckedChanged += flipVer_CheckedChanged;
            // 
            // resizeComboBox
            // 
            resizeComboBox.FormattingEnabled = true;
            resizeComboBox.Location = new Point(168, 398);
            resizeComboBox.Name = "resizeComboBox";
            resizeComboBox.Size = new Size(121, 23);
            resizeComboBox.TabIndex = 7;
            // 
            // noiseFilterComboBox
            // 
            noiseFilterComboBox.FormattingEnabled = true;
            noiseFilterComboBox.Location = new Point(168, 427);
            noiseFilterComboBox.Name = "noiseFilterComboBox";
            noiseFilterComboBox.Size = new Size(121, 23);
            noiseFilterComboBox.TabIndex = 8;
            // 
            // binaryCheckBox
            // 
            binaryCheckBox.AutoSize = true;
            binaryCheckBox.Location = new Point(12, 656);
            binaryCheckBox.Name = "binaryCheckBox";
            binaryCheckBox.Size = new Size(59, 19);
            binaryCheckBox.TabIndex = 9;
            binaryCheckBox.Text = "Binary";
            binaryCheckBox.UseVisualStyleBackColor = true;
            // 
            // grayscaleCheckBox
            // 
            grayscaleCheckBox.AutoSize = true;
            grayscaleCheckBox.Location = new Point(77, 656);
            grayscaleCheckBox.Name = "grayscaleCheckBox";
            grayscaleCheckBox.Size = new Size(76, 19);
            grayscaleCheckBox.TabIndex = 10;
            grayscaleCheckBox.Text = "Grayscale";
            grayscaleCheckBox.UseVisualStyleBackColor = true;
            // 
            // paletteComboBox
            // 
            paletteComboBox.FormattingEnabled = true;
            paletteComboBox.Location = new Point(168, 456);
            paletteComboBox.Name = "paletteComboBox";
            paletteComboBox.Size = new Size(121, 23);
            paletteComboBox.TabIndex = 11;
            // 
            // trackBar1
            // 
            trackBar1.LargeChange = 15;
            trackBar1.Location = new Point(295, 398);
            trackBar1.Maximum = 255;
            trackBar1.Name = "trackBar1";
            trackBar1.Size = new Size(565, 45);
            trackBar1.TabIndex = 13;
            // 
            // morfologyCheckBox
            // 
            morfologyCheckBox.AutoSize = true;
            morfologyCheckBox.Location = new Point(159, 656);
            morfologyCheckBox.Name = "morfologyCheckBox";
            morfologyCheckBox.Size = new Size(82, 19);
            morfologyCheckBox.TabIndex = 14;
            morfologyCheckBox.Text = "Morfology";
            morfologyCheckBox.UseVisualStyleBackColor = true;
            // 
            // morfologyComboBox
            // 
            morfologyComboBox.FormattingEnabled = true;
            morfologyComboBox.Location = new Point(168, 485);
            morfologyComboBox.Name = "morfologyComboBox";
            morfologyComboBox.Size = new Size(121, 23);
            morfologyComboBox.TabIndex = 15;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1264, 681);
            Controls.Add(morfologyComboBox);
            Controls.Add(morfologyCheckBox);
            Controls.Add(trackBar1);
            Controls.Add(paletteComboBox);
            Controls.Add(grayscaleCheckBox);
            Controls.Add(binaryCheckBox);
            Controls.Add(noiseFilterComboBox);
            Controls.Add(resizeComboBox);
            Controls.Add(flipVer);
            Controls.Add(flipHor);
            Controls.Add(rotateView);
            Controls.Add(videoView);
            Controls.Add(modeView);
            Controls.Add(stopBroadcast);
            Controls.Add(startBroadcast);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Name = "Form1";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)videoView).EndInit();
            ((System.ComponentModel.ISupportInitialize)trackBar1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button startBroadcast;
        private Button stopBroadcast;
        private Button modeView;
        private PictureBox videoView;
        private Button rotateView;
        private CheckBox flipHor;
        private CheckBox flipVer;
        private ComboBox resizeComboBox;
        private ComboBox noiseFilterComboBox;
        private CheckBox binaryCheckBox;
        private CheckBox grayscaleCheckBox;
        private ComboBox paletteComboBox;
        private TrackBar trackBar1;
        private CheckBox morfologyCheckBox;
        private ComboBox morfologyComboBox;
    }
}
