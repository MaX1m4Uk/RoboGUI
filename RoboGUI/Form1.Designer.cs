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
            ((System.ComponentModel.ISupportInitialize)videoView).BeginInit();
            SuspendLayout();
            // 
            // startBroadcast
            // 
            startBroadcast.Location = new Point(70, 320);
            startBroadcast.Name = "startBroadcast";
            startBroadcast.Size = new Size(150, 46);
            startBroadcast.TabIndex = 0;
            startBroadcast.Text = "Start broadcast";
            startBroadcast.UseVisualStyleBackColor = true;
            startBroadcast.Click += startBroadcast_Click;
            // 
            // stopBroadcast
            // 
            stopBroadcast.Location = new Point(325, 320);
            stopBroadcast.Name = "stopBroadcast";
            stopBroadcast.Size = new Size(150, 46);
            stopBroadcast.TabIndex = 1;
            stopBroadcast.Text = "Stop broadcast";
            stopBroadcast.UseVisualStyleBackColor = true;
            stopBroadcast.Click += stopBroadcast_Click;
            // 
            // modeView
            // 
            modeView.Location = new Point(600, 320);
            modeView.Name = "modeView";
            modeView.Size = new Size(150, 46);
            modeView.TabIndex = 2;
            modeView.Text = "button3";
            modeView.UseVisualStyleBackColor = true;
            // 
            // videoView
            // 
            videoView.Location = new Point(12, 12);
            videoView.Name = "videoView";
            videoView.Size = new Size(780, 302);
            videoView.SizeMode = PictureBoxSizeMode.Zoom;
            videoView.TabIndex = 3;
            videoView.TabStop = false;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(804, 461);
            Controls.Add(videoView);
            Controls.Add(modeView);
            Controls.Add(stopBroadcast);
            Controls.Add(startBroadcast);
            Name = "Form1";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)videoView).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Button startBroadcast;
        private Button stopBroadcast;
        private Button modeView;
        private PictureBox videoView;
    }
}
