using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace Port_Scanner
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Author: Carver\nGithub: @willsh2");
        }

        int sayi = 1;
        bool islem = false;
        private void portScan(int port, string ip)
        {
            progressBar1.Value += 1;
            label5.Text = port.ToString();

            if (islem)
                return;

            using (var client = new TcpClient())
            {
                try
                {
                    client.Connect(ip, port);
                    listBox1.Items.Add($"{sayi}) IP: {ip}  |  Port: {port}");
                    sayi++;

                }
                catch
                {
                    
                }
            }
        }
        private async void button1_Click(object sender, EventArgs e)
        {
            await Task.Run(basla);
        }
        private async Task basla()
        {
            if (textBox1.Text.Trim() == "")
            {
                MessageBox.Show("IP adresini girmedin");
                return;
            }
                
            if (button1.Text == "Stop")
            {
                islem = true;
                return;
            }

            string ip = textBox1.Text.Trim();
            int baslangic = Convert.ToInt32(numericUpDown1.Value);
            int bitis = Convert.ToInt32(numericUpDown2.Value);

            progressBar1.Value = 0;
            progressBar1.Maximum = bitis + 1;

            button1.Text = "Stop";
            button1.ForeColor = Color.Red;

            Parallel.For(baslangic, bitis + 1, port =>
            {
                portScan(port, ip);
            });


            MessageBox.Show("Port tarama iþlemi bitmiþtir.");

            button1.Text = "Start";
            button1.ForeColor = Color.Black;
            islem = false;
        }
    }
}