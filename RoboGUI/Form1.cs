using System.Buffers.Binary;
using System.Net;
using System.Net.Sockets;
using System.Diagnostics;
using System.ComponentModel;

namespace RoboGUI
{
    public partial class Form1 : Form
    {
        class myProcess
        {
            public void openWithArguments()
            {
                Process.Start("D:\\Global Env\\openCV\\python.exe", "D:\\openCVLab2.py");
            }
        }

        public Form1()
        {
            InitializeComponent();
            TcpListener server = null;
            try
            {
               


                Int32 port = 13000;
                IPAddress localAddr = IPAddress.Parse("127.0.0.1");

                server = new TcpListener(localAddr, port);

                server.Start();

                myProcess myProcess = new myProcess();
                myProcess.openWithArguments();

                using TcpClient client = server.AcceptTcpClient();

                var stream = client.GetStream();
                byte[] sizeBytes = new byte[4];

                while (true) {
                    stream.ReadExactly(sizeBytes);

                    int frameSize = BinaryPrimitives.ReadInt32BigEndian(sizeBytes);

                    if (frameSize == 0)
                    {
                        break;
                    }

                    byte[] frameBytes = new byte[frameSize];

                    stream.ReadExactly(frameBytes);

                    Bitmap bmp;
                    using (var ms = new MemoryStream(frameBytes))
                    {
                        bmp = new Bitmap(ms);
                    }

                    Bitmap bmpCopy = new Bitmap(bmp);


                    videoView.Image = bmpCopy;
                }

                server.Stop();

            }
            catch (SocketException e)
            {

            }
            finally
            {
                server.Stop();
            }
        }
    }
}
