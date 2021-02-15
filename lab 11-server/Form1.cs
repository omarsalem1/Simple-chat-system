using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Net;
using System.IO;

namespace lab_11_server
{
  
    
    public partial class Form1 : Form
    {
        TcpListener server;
        byte[] add;
        IPAddress localadd;
        
        int port;
        Socket connection;
        NetworkStream nstream;
        BinaryReader bR;
        BinaryWriter bW;
       
        public Form1()
        {
            InitializeComponent();
            add = new byte[] { 127, 0, 0, 1 };
            localadd = new IPAddress(add);
            port = 1090;
            server = new TcpListener(localadd, port);
            button2.Enabled = false;
            //button3.Enabled = false;
            button4.Enabled = false;
            


        }

        private void button2_Click(object sender, EventArgs e)
        {
            bR.Close();
            bW.Close();
            nstream.Close();
           connection.Shutdown(SocketShutdown.Both);
            connection.Close();
            this.Close();



        }

        private void button1_Click(object sender, EventArgs e)
        {

            server.Start();
            connection = server.AcceptSocket();
            nstream = new NetworkStream(connection);
            bW = new BinaryWriter(nstream);
            bR = new BinaryReader(nstream);
            button2.Enabled = true;
            //button3.Enabled = true;
            button4.Enabled = true;
           
            backgroundWorker1.RunWorkerAsync();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            //string input;
            //input = bR.ReadString();
            //listBox1.Items.Add("client : " + input);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            listBox1.Items.Add("Server  :" + textBox1.Text);
            bW.Write(textBox1.Text);
            textBox1.Clear();
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            while (connection.Connected )
            {
                if (bR != null)
                {
                    string input;
                    input = bR.ReadString();
                    AddToListBox("Client : " + input);
                }
            }
        }
        private void AddToListBox(object oo)
        {
            Invoke(new MethodInvoker(
                           delegate { listBox1.Items.Add(oo); }
                           ));
        }
    }
}
