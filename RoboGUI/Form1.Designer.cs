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
            binarizationPic = new TrackBar();
            morfologyCheckBox = new CheckBox();
            morfologyComboBox = new ComboBox();
            colorPresetCBox = new ComboBox();
            ((System.ComponentModel.ISupportInitialize)videoView).BeginInit();
            ((System.ComponentModel.ISupportInitialize)binarizationPic).BeginInit();
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
            modeView.Location = new Point(168, 399);
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
            rotateView.Location = new Point(168, 451);
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
            flipHor.Location = new Point(324, 451);
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
            flipVer.Location = new Point(324, 476);
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
            resizeComboBox.Location = new Point(1131, 447);
            resizeComboBox.Name = "resizeComboBox";
            resizeComboBox.Size = new Size(121, 23);
            resizeComboBox.TabIndex = 7;
            resizeComboBox.SelectedIndexChanged += resizeComboBox_SelectedIndexChanged;
            // 
            // noiseFilterComboBox
            // 
            noiseFilterComboBox.FormattingEnabled = true;
            noiseFilterComboBox.Location = new Point(1131, 476);
            noiseFilterComboBox.Name = "noiseFilterComboBox";
            noiseFilterComboBox.Size = new Size(121, 23);
            noiseFilterComboBox.TabIndex = 8;
            noiseFilterComboBox.SelectedIndexChanged += noiseFilterComboBox_SelectedIndexChanged;
            // 
            // binaryCheckBox
            // 
            binaryCheckBox.AutoSize = true;
            binaryCheckBox.Location = new Point(605, 399);
            binaryCheckBox.Name = "binaryCheckBox";
            binaryCheckBox.Size = new Size(59, 19);
            binaryCheckBox.TabIndex = 9;
            binaryCheckBox.Text = "Binary";
            binaryCheckBox.UseVisualStyleBackColor = true;
            binaryCheckBox.CheckedChanged += binaryCheckBox_CheckedChanged;
            // 
            // grayscaleCheckBox
            // 
            grayscaleCheckBox.AutoSize = true;
            grayscaleCheckBox.Location = new Point(605, 424);
            grayscaleCheckBox.Name = "grayscaleCheckBox";
            grayscaleCheckBox.Size = new Size(76, 19);
            grayscaleCheckBox.TabIndex = 10;
            grayscaleCheckBox.Text = "Grayscale";
            grayscaleCheckBox.UseVisualStyleBackColor = true;
            grayscaleCheckBox.CheckedChanged += grayscaleCheckBox_CheckedChanged;
            // 
            // paletteComboBox
            // 
            paletteComboBox.FormattingEnabled = true;
            paletteComboBox.Location = new Point(1131, 505);
            paletteComboBox.Name = "paletteComboBox";
            paletteComboBox.Size = new Size(121, 23);
            paletteComboBox.TabIndex = 11;
            paletteComboBox.SelectedIndexChanged += paletteComboBox_SelectedIndexChanged;
            // 
            // binarizationPic
            // 
            binarizationPic.LargeChange = 15;
            binarizationPic.Location = new Point(687, 399);
            binarizationPic.Maximum = 255;
            binarizationPic.Name = "binarizationPic";
            binarizationPic.Size = new Size(565, 45);
            binarizationPic.TabIndex = 13;
            binarizationPic.Scroll += binarizationPic_Scroll;
            // 
            // morfologyCheckBox
            // 
            morfologyCheckBox.AutoSize = true;
            morfologyCheckBox.Location = new Point(1131, 621);
            morfologyCheckBox.Name = "morfologyCheckBox";
            morfologyCheckBox.Size = new Size(82, 19);
            morfologyCheckBox.TabIndex = 14;
            morfologyCheckBox.Text = "Morfology";
            morfologyCheckBox.UseVisualStyleBackColor = true;
            morfologyCheckBox.CheckedChanged += morfologyCheckBox_CheckedChanged;
            // 
            // morfologyComboBox
            // 
            morfologyComboBox.FormattingEnabled = true;
            morfologyComboBox.Location = new Point(1131, 646);
            morfologyComboBox.Name = "morfologyComboBox";
            morfologyComboBox.Size = new Size(121, 23);
            morfologyComboBox.TabIndex = 15;
            morfologyComboBox.SelectedIndexChanged += morfologyComboBox_SelectedIndexChanged;
            // 
            // colorPresetCBox
            // 
            colorPresetCBox.FormattingEnabled = true;
            colorPresetCBox.Location = new Point(605, 449);
            colorPresetCBox.Name = "colorPresetCBox";
            colorPresetCBox.Size = new Size(121, 23);
            colorPresetCBox.TabIndex = 16;
            colorPresetCBox.SelectedIndexChanged += colorPresetCBox_SelectedIndexChanged;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1264, 681);
            Controls.Add(colorPresetCBox);
            Controls.Add(morfologyComboBox);
            Controls.Add(morfologyCheckBox);
            Controls.Add(binarizationPic);
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
            ((System.ComponentModel.ISupportInitialize)binarizationPic).EndInit();
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
        private TrackBar binarizationPic;
        private CheckBox morfologyCheckBox;
        private ComboBox morfologyComboBox;
        private ComboBox colorPresetCBox;
    }
}
