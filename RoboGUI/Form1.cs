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
        byte[] controlBytes = new byte[5];
        Int32 port = 13000;
        IPAddress localAddr = IPAddress.Parse("127.0.0.1");
        Process pythonProcess;
        TcpClient client;
        Int16 mode = 1, /* base view */ willContinue = 1 /* continue */, baseRotate = 0 /* base rotate */, 
            flipHorVar = 0 /* base flip hor */, flipVerVar = 0 /* base flip ver */;
        NetworkStream stream;
        List<string> resizeList = new List<string> {
            "100%",
            "75%",
            "50%",
            "25%"
        };
        List<string> noiseFilterList = new List<string> {
            "Gausian noise filter",
            "////"
        };
        List<string> paletteList = new List<string>
        {
            "Original",
            "RGB",
            "HSV",
            "JET"
        };
        List<string> morfologyStyleList = new List<string> {
            "Erosion",
            "Dilation",
            "Opening",
            "Closing"
        };
        async Task GetStreamAsync()
        {
            server.Start();
            pythonProcess = Process.Start("D:\\Global Env\\openCV\\python.exe", "D:\\openCVLab2.py");
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
            server.Stop();
            client.Dispose();
        }

        public Form1()
        {
            InitializeComponent();
            server = new TcpListener(localAddr, port);
            resizeComboBox.DataSource = resizeList;
            noiseFilterComboBox.DataSource = noiseFilterList;
            paletteComboBox.DataSource = paletteList;
            morfologyComboBox.DataSource = morfologyStyleList;
        }

        private async void startBroadcast_Click(object sender, EventArgs e)
        {
            await GetStreamAsync();
        }

        private async void stopBroadcast_Click(object sender, EventArgs e)
        {
            willContinue = 0;
            controlBytes[0] = Convert.ToByte(mode);
            controlBytes[1] = Convert.ToByte(willContinue);
            controlBytes[2] = Convert.ToByte(baseRotate);
            controlBytes[3] = Convert.ToByte(flipHorVar);
            controlBytes[4] = Convert.ToByte(flipVerVar);
            stream.Write(controlBytes);
        }

        private void modeView_Click(object sender, EventArgs e)
        {
            if (mode == 0)
            {
                mode = 1;
            }
            else if (mode == 1)
            {
                mode = 0;
            }
            controlBytes[0] = Convert.ToByte(mode);
            controlBytes[1] = Convert.ToByte(willContinue);
            controlBytes[2] = Convert.ToByte(baseRotate);
            controlBytes[3] = Convert.ToByte(flipHorVar);
            controlBytes[4] = Convert.ToByte(flipVerVar);
            stream.Write(controlBytes);
        }

        private void rotateView_Click(object sender, EventArgs e)
        {
            baseRotate++;
            if (baseRotate == 4)
            {
                baseRotate = 0;
            }
            controlBytes[0] = Convert.ToByte(mode);
            controlBytes[1] = Convert.ToByte(willContinue);
            controlBytes[2] = Convert.ToByte(baseRotate);
            controlBytes[3] = Convert.ToByte(flipHorVar);
            controlBytes[4] = Convert.ToByte(flipVerVar);
            stream.Write(controlBytes);
        }

        private void flipHor_CheckedChanged(object sender, EventArgs e)
        {
            if (flipHorVar == 0)
            {
                flipHorVar = 1;
            }
            else if (flipHorVar == 1) {
                flipHorVar = 0;
            }
            controlBytes[0] = Convert.ToByte(mode);
            controlBytes[1] = Convert.ToByte(willContinue);
            controlBytes[2] = Convert.ToByte(baseRotate);
            controlBytes[3] = Convert.ToByte(flipHorVar);
            controlBytes[4] = Convert.ToByte(flipVerVar);
            stream.Write(controlBytes);
        }

        private void flipVer_CheckedChanged(object sender, EventArgs e)
        {
            if (flipVerVar == 0)
            {
                flipVerVar = 1;
            }
            else if (flipVerVar == 1)
            {
                flipVerVar = 0;
            }
            controlBytes[0] = Convert.ToByte(mode);
            controlBytes[1] = Convert.ToByte(willContinue);
            controlBytes[2] = Convert.ToByte(baseRotate);
            controlBytes[3] = Convert.ToByte(flipHorVar);
            controlBytes[4] = Convert.ToByte(flipVerVar);
            stream.Write(controlBytes);
        }
    }
}
