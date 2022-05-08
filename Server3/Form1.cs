using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
namespace Server3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void ConnectBTN_Click(object sender, EventArgs e)
        {
            ConnectBTN.Enabled = false;
            string data;
            IPEndPoint ipep = new IPEndPoint(IPAddress.Any, 9050);
            Socket newsock = new Socket(AddressFamily.InterNetwork,
            SocketType.Stream, ProtocolType.Tcp);
            newsock.Bind(ipep);
            newsock.Listen(10);
            InfoTxt.Text += "Waiting for a client...\n";
            Socket client = newsock.Accept();
            IPEndPoint newclient = (IPEndPoint)client.RemoteEndPoint;
            InfoTxt.Text += "Connected with " + newclient.Address + " at port " + newclient.Port + "\n";
            NetworkStream ns = new NetworkStream(client); StreamReader sr = new StreamReader(ns);
            StreamWriter sw = new StreamWriter(ns);
            string welcome = "Welcome to my test server";
            sw.WriteLine(welcome);
            IPTxt.Text = "127.0.0.1";
            sw.Flush();
            while (true)
            {
                try { data = sr.ReadLine(); }
                catch (IOException) { break; }
                InfoTxt.Text += data + "\n";
                sw.WriteLine(data);
                sw.Flush();
            }
            InfoTxt.Text += "Disconnected from " + newclient.Address;
            sw.Close();
            sr.Close();
            ns.Close();
            ConnectBTN.Enabled = true;
        }
    }
}