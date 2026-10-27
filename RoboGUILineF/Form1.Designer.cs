namespace RoboGUILineF
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
            videoView = new PictureBox();
            broadcast = new Button();
            startBroadcast = new Button();
            ((System.ComponentModel.ISupportInitialize)videoView).BeginInit();
            SuspendLayout();
            // 
            // videoView
            // 
            videoView.Location = new Point(12, 12);
            videoView.Name = "videoView";
            videoView.Size = new Size(1240, 380);
            videoView.SizeMode = PictureBoxSizeMode.Zoom;
            videoView.TabIndex = 4;
            videoView.TabStop = false;
            // 
            // broadcast
            // 
            broadcast.Location = new Point(1102, 398);
            broadcast.Name = "broadcast";
            broadcast.Size = new Size(150, 46);
            broadcast.TabIndex = 5;
            broadcast.Text = "Stop broadcast";
            broadcast.UseVisualStyleBackColor = true;
            broadcast.Click += broadcast_Click;
            // 
            // startBroadcast
            // 
            startBroadcast.Location = new Point(12, 398);
            startBroadcast.Name = "startBroadcast";
            startBroadcast.Size = new Size(150, 46);
            startBroadcast.TabIndex = 6;
            startBroadcast.Text = "Start broadcast";
            startBroadcast.UseVisualStyleBackColor = true;
            startBroadcast.Click += startBroadcast_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1264, 681);
            Controls.Add(startBroadcast);
            Controls.Add(broadcast);
            Controls.Add(videoView);
            Name = "Form1";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)videoView).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private PictureBox videoView;
        private Button broadcast;
        private Button startBroadcast;
    }
}
