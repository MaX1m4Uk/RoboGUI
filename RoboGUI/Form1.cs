using System.Buffers.Binary;
using System.Net;
using System.Net.Sockets;
using System.Diagnostics;
using System.ComponentModel;

namespace RoboGUI
{
    public partial class Form1 : Form
    {
        TcpListener server = null;
        byte[] sizeBytes = new byte[4];
        Int32 port = 13000;
        IPAddress localAddr = IPAddress.Parse("127.0.0.1");
        Process pythonProcess;
        TcpClient client;
        async Task GetStreamAsync()
        {
            
            server.Start();
            pythonProcess = Process.Start("D:\\Global Env\\openCV\\python.exe", "D:\\openCVLab2.py");
            client = await server.AcceptTcpClientAsync();

            var stream = client.GetStream();

            while (true)
            {
                await stream.ReadExactlyAsync(sizeBytes);

                int frameSize = BinaryPrimitives.ReadInt32BigEndian(sizeBytes);

                if (frameSize == 0)
                {
                    break;
                }

                byte[] frameBytes = new byte[frameSize];

                await stream.ReadExactlyAsync(frameBytes);

                Bitmap bmp;
                using (var ms = new MemoryStream(frameBytes))
                {
                    bmp = new Bitmap(ms);
                    Bitmap bmpCopy = new Bitmap(bmp);
                    bmp.Dispose();
                    var oldImage = videoView.Image;
                    videoView.Image = bmpCopy;
                    oldImage?.Dispose();
                }





            }
        }

        public Form1()
        {
            InitializeComponent();
            server = new TcpListener(localAddr, port);
            
        }

        private async void startBroadcast_Click(object sender, EventArgs e)
        {
            
            await GetStreamAsync();
        }

        private async void stopBroadcast_Click(object sender, EventArgs e)
        {
            pythonProcess.Kill();
            server.Stop();
            client.Dispose();
        }
    }
}
