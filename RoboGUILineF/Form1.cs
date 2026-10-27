using System.Buffers.Binary;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;

namespace RoboGUILineF
{
    public partial class Form1 : Form
    {
        TcpListener server = null;
        byte[] sizeBytes = new byte[4];
        byte[] controlBytes = new byte[14];
        Int32 port = 13000;
        IPAddress localAddr = IPAddress.Parse("127.0.0.1");
        Process pythonProcess;
        TcpClient client;
        Int16 start = 1;
        NetworkStream stream;
        async Task GetStreamAsync()
        {
            server.Start();
            pythonProcess = Process.Start("D:\\Global Env\\openCV\\python.exe", "D:\\C#\\RoboGUI\\copeliaLab6.py");
            //pythonProcess = Process.Start("cmd.exe","/k \"\"D:\\Global Env\\openCV\\python.exe\" \"D:\\C#\\RoboGUI\\openCVLab2.py\"\"");
            client = await server.AcceptTcpClientAsync();

            stream = client.GetStream();

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
            pythonProcess.Kill();
            //pythonProcess.Kill(true);
            server.Stop();
            client.Dispose();
        }
        void sendByte()
        {
            controlBytes[0] = Convert.ToByte(start);
            stream.Write(controlBytes);
        }
        public Form1()
        {
            InitializeComponent();
            server = new TcpListener(localAddr, port);
        }

        private async void broadcast_Click(object sender, EventArgs e)
        {
            start = 0;
            sendByte();
        }

        private async void startBroadcast_Click(object sender, EventArgs e)
        {
            await GetStreamAsync();
        }
    }
}
