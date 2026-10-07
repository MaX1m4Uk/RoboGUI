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
        byte[] controlBytes = new byte[13];
        Int32 port = 13000;
        IPAddress localAddr = IPAddress.Parse("127.0.0.1");
        Process pythonProcess;
        TcpClient client;
        Int16 mode = 1, /* base view */ willContinue = 1 /* continue */, baseRotate = 0 /* base rotate */,
            flipHorVar = 0 /* base flip hor */, flipVerVar = 0 /* base flip ver */, baseSize = 0 /* base size */,
            baseDenoise = 0 /* base denoise*/, baseColor = 0 /* base color */, baseGray = 0 /* base gray*/, isBinaryPic = 0 /* is binary pic */,
            threshlord = 0 /* theshlord level */, isMorfolofy = 0 /* base morfology*/, baseMorfology = 0 /* base morfology */;
        NetworkStream stream;
        List<string> resizeList = new List<string> {
            "100%",
            "75%",
            "50%",
            "25%"
        };
        List<string> noiseFilterList = new List<string> {
            "None",
            "Gausian noise filter",
            "Median noise filter",
            "Bilateral noise filter"
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
            pythonProcess = Process.Start("D:\\Global Env\\openCV\\python.exe", "D:\\C#\\RoboGUI\\openCVLab2.py");
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

        void sendByte()
        {
            controlBytes[0] = Convert.ToByte(mode);
            controlBytes[1] = Convert.ToByte(willContinue);
            controlBytes[2] = Convert.ToByte(baseRotate);
            controlBytes[3] = Convert.ToByte(flipHorVar);
            controlBytes[4] = Convert.ToByte(flipVerVar);
            controlBytes[5] = Convert.ToByte(baseSize);
            controlBytes[6] = Convert.ToByte(baseDenoise);
            controlBytes[7] = Convert.ToByte(baseColor);
            controlBytes[8] = Convert.ToByte(baseGray);
            controlBytes[9] = Convert.ToByte(isBinaryPic);
            controlBytes[10] = Convert.ToByte(threshlord);
            controlBytes[11] = Convert.ToByte(isMorfolofy);
            controlBytes[12] = Convert.ToByte(baseMorfology);
            stream.Write(controlBytes);
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
            sendByte();
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
            sendByte();
        }

        private void rotateView_Click(object sender, EventArgs e)
        {
            baseRotate++;
            if (baseRotate == 4)
            {
                baseRotate = 0;
            }
            sendByte();
        }

        private void flipHor_CheckedChanged(object sender, EventArgs e)
        {
            if (flipHorVar == 0)
            {
                flipHorVar = 1;
            }
            else if (flipHorVar == 1)
            {
                flipHorVar = 0;
            }
            sendByte();
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
            sendByte();
        }

        private void resizeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (resizeComboBox.Text == "100%")
            {
                baseSize = 0;
            }
            else if (resizeComboBox.Text == "75%")
            {
                baseSize = 1;
            }
            else if (resizeComboBox.Text == "50%")
            {
                baseSize = 2;
            }
            else if (resizeComboBox.Text == "25%")
            {
                baseSize = 3;
            }
            sendByte();
        }

        private void noiseFilterComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (noiseFilterComboBox.Text == "None")
            {
                baseDenoise = 0;
            }
            else if (noiseFilterComboBox.Text == "Gausian noise filter")
            {
                baseDenoise = 1;
            }
            else if (noiseFilterComboBox.Text == "Median noise filter")
            {
                baseDenoise = 2;
            }
            else if (noiseFilterComboBox.Text == "Bilateral noise filter")
            {
                baseDenoise = 3;
            }
            sendByte();
        }

        private void paletteComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (paletteComboBox.Text == "Original")
            {
                baseColor = 0;
            }
            else if (paletteComboBox.Text == "RGB")
            {
                baseColor = 1;
            }
            else if (paletteComboBox.Text == "HSV")
            {
                baseColor = 2;
            }
            else if (paletteComboBox.Text == "JET")
            {
                baseColor = 3;
            }
            sendByte();
        }

        private void grayscaleCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (baseGray == 0)
            {
                baseGray = 1;
            }
            else if (baseGray == 1)
            {
                baseGray = 0;
            }
            sendByte();
        }

        private void binarizationPic_Scroll(object sender, EventArgs e)
        {
            threshlord = (short)binarizationPic.Value;
            sendByte();
        }

        private void binaryCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (isBinaryPic == 0)
            {
                isBinaryPic = 1;
            }
            else if (isBinaryPic == 1)
            {
                isBinaryPic = 0;
            }
            sendByte();
        }

        private void morfologyCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (isMorfolofy == 0)
            {
                isMorfolofy = 1;
            }
            else if (isMorfolofy == 1) {
                isMorfolofy = 0;
            }
            sendByte();
        }

        private void morfologyComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (morfologyComboBox.Text == "Erosion")
            {
                baseMorfology = 0;
            }
            else if (morfologyComboBox.Text == "Dilation")
            {
                baseMorfology = 1;
            }
            else if (morfologyComboBox.Text == "Opening")
            {
                baseMorfology = 2;
            }
            else if (morfologyComboBox.Text == "Closing") {
                baseMorfology = 3;
            }
            sendByte();
        }
    }
}
