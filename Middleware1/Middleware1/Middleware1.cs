using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Middleware1
{
    public partial class Middleware1 : Form
    {
        const int myPort = 8082;
        int Id = 0;
        private int counter = 0;
        private string timeStamp = "";
        private string data = null;
        int[] vector = new int[5];
        int[] flag = new int[5];
        int number = 0;
        ArrayList readyList = new ArrayList();
        ArrayList ports = new ArrayList();
        ArrayList priorityQ = new ArrayList();
        ArrayList localQ = new ArrayList();

        IComparer dataSort = new FilesNameComparerClass();
        // spliter spliter = new spliter();
        public Middleware1()
        {
            InitializeComponent();
            ReceiveMulticast();
        }

        private void Middleware1_Load(object sender, EventArgs e)
        {

        }

        private void Label3_Click(object sender, EventArgs e)
        {

        }

        private void SendButton_Click(object sender, EventArgs e)
        {
            Start();
        }
        public void Start()
        {
            number += 1;
            try
            {
                IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
                IPAddress ipAddress = null;
                foreach (IPAddress ip in ipHostInfo.AddressList)
                {
                    if (ip.AddressFamily == AddressFamily.InterNetwork)
                    {
                        ipAddress = ip;
                        break;
                    }
                }
                IPEndPoint remoteEP = new IPEndPoint(ipAddress, 8081);
                Socket sendSocket;
                try
                {
                    sendSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                    sendSocket.Connect(remoteEP);
                    Random random = new Random();
                    const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
                    string cont = new string(Enumerable.Repeat(chars, 3).Select(s => s[random.Next(s.Length)]).ToArray());
                    byte[] msg = Encoding.ASCII.GetBytes("Msg from," + myPort + ",#" + number + cont +"<EOM>");
                    int bytesSent = sendSocket.Send(msg);
                    sendSocket.Shutdown(SocketShutdown.Both);
                    sendSocket.Close();
                    sentBox.Items.Add("Msg "+cont+" from," + myPort + ",<EOM>");
                }
                catch (ArgumentNullException ane)
                {
                    Console.WriteLine("ArgumentNullException: {0}", ane.ToString());
                }
                catch (SocketException se)
                {
                    Console.WriteLine("SocketException: {0}", se.ToString());
                }
                catch (Exception e)
                {
                    Console.WriteLine("Unexpected exception:{0}", e.ToString());
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
        private async void ReceiveMulticast()
        {
            byte[] bytes = new Byte[1024];
            IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
            IPAddress ipAddress = null;
            foreach (IPAddress ip in ipHostInfo.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    ipAddress = ip;
                    break;
                }
            }
            IPEndPoint localEndPoint = new IPEndPoint(ipAddress, myPort);
            TcpListener listener = new TcpListener(localEndPoint);
            listener.Start(10);
            try
            {
                while (true)
                {
                    TcpClient tcpClient = await listener.AcceptTcpClientAsync();
                    data = null;
                    while (true)
                    {
                        bytes = new byte[1024];
                        NetworkStream readStream = tcpClient.GetStream();
                        int bytesRec = await readStream.ReadAsync(bytes, 0, 1024);
                        data += Encoding.ASCII.GetString(bytes, 0, bytesRec);
                        if (data.IndexOf("<EOM>") > -1)
                        {
                            break;
                        }
                    }
                    if (data.Contains("&"))
                    {
                        string[] tempMsg = data.Split('&');
                        int port = Int32.Parse(tempMsg[1].Substring(tempMsg[1].IndexOf('*') + 1, 4));
                        ports.Add(port);
                        string empty = "";
                        localQ.Add(tempMsg[1]);
                        localQ.Sort(dataSort);
                        string maxOne = "Max:" + localQ[0].ToString();
                        if (localQ.Count == 5)
                        {
                            for (int i = 0; i < 5; i++)
                            {
                                sendBack(maxOne, Int32.Parse(ports[i].ToString()), empty);
                                
                            }
                            localQ.Clear();
                        }
                        
                    }
                    else if (data.Contains("Max:"))
                    {
                        string[] theOne = data.Split(':');
                        string[] maxTimeStampArray = theOne[1].Split('*');
                        if (priorityQ.Count != 0)
                        {
                            for (int i = 0; i < priorityQ.Count; i++)
                            {
                                string[] fromPQ = priorityQ[i].ToString().Split('*');
                                if (fromPQ[2] == maxTimeStampArray[2])
                                {
                                    fromPQ[0] = maxTimeStampArray[0];
                                    fromPQ[1] = maxTimeStampArray[1];
                                    readyBox.Items.Add(priorityQ[i].ToString().Substring(priorityQ[i].ToString().LastIndexOf('*') + 1));
                                    priorityQ.Remove(priorityQ[i]);
                                }
                            }
                        }
                    }
                    else
                    {
                        receivedBox.Items.Add("msg received: " + data);
                        counter += 1;
                        string[] tempData = data.Split(',');
                        int portToSend = Int32.Parse(tempData[1]);
                        string finalData = "";
                        for (int i = 0; i < tempData.Length; i++)
                        {
                            finalData += tempData[i] + ",";
                        }
                        timeStamp = counter + "*" + myPort;
                        string priorityData = timeStamp + "*" + finalData;
                        priorityQ.Add(priorityData);
                        DoWork(priorityData, portToSend);
                    }

                }
            }
            catch (Exception ee)
            {
                Console.WriteLine(ee.ToString());
            }
        }

        public void DoWork(string data, int port)
        {
            try
            {
                IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
                IPAddress ipAddress = null;
                foreach (IPAddress ip in ipHostInfo.AddressList)
                {
                    if (ip.AddressFamily == AddressFamily.InterNetwork)
                    {
                        ipAddress = ip;
                        break;
                    }
                }
                IPEndPoint remoteEP = new IPEndPoint(ipAddress, port);
                Socket sendSocket;
                try
                {
                    sendSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                    sendSocket.Connect(remoteEP);

                    byte[] msg = Encoding.ASCII.GetBytes("&" + data);

                    int bytesSent = sendSocket.Send(msg);

                    sendSocket.Shutdown(SocketShutdown.Both);
                    sendSocket.Close();
                }
                catch (ArgumentNullException ane)
                {
                    Console.WriteLine("ArgumentNullException: {0}", ane.ToString());
                }
                catch (SocketException se)
                {
                    Console.WriteLine("SocketException : {0}", se.ToString());
                }
                catch (Exception e)
                {
                    Console.WriteLine("Unexpected exception : {0}", e.ToString());
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

        }


        public void sendBack(string maxOne, int port, string empty)
        {
            try
            {
                IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
                IPAddress ipAddress = null;
                foreach (IPAddress ip in ipHostInfo.AddressList)
                {
                    if (ip.AddressFamily == AddressFamily.InterNetwork)
                    {
                        ipAddress = ip;
                        break;
                    }
                }
                IPEndPoint remoteEP = new IPEndPoint(ipAddress, port);
                Socket sendSocket;
                try
                {
                    sendSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    sendSocket.Connect(remoteEP);
                    byte[] msg = Encoding.ASCII.GetBytes(maxOne);
                    int byteSent = sendSocket.Send(msg);
                    sendSocket.Shutdown(SocketShutdown.Both);
                    sendSocket.Close();
                }
                catch (ArgumentNullException ane)
                {
                    Console.WriteLine("ArgumentNullException : {0}", ane.ToString());
                }
                catch (SocketException se)
                {
                    Console.WriteLine("SocketException : {0}", se.ToString());
                }
                catch (Exception e)
                {
                    Console.WriteLine("Unexpected exception : {0}", e.ToString());
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }



    }

    internal class FilesNameComparerClass : IComparer
    {
        int IComparer.Compare(object x, object y)
        {
            if (x==null || y == null)
            {
                throw new ArgumentException("Parameters can not be null");
            }
            string fileA = x as string;
            string fileB = y as string;
            char[] arr1 = fileA.ToCharArray();
            char[] arr2 = fileB.ToCharArray();
            int i = 0, j = 0;
            while(i<arr1.Length && j < arr2.Length)
            {
                if (char.IsDigit(arr1[i]) && char.IsDigit(arr2[j]))
                {
                    string s1 = "", s2 = "";
                    while (i< arr1.Length && char.IsDigit(arr1[i]))
                    {
                        s1 += arr1[i];
                        i++;
                    }
                    while (j < arr2.Length && char.IsDigit(arr2[j]))
                    {
                        s2 += arr2[j];
                        j++;
                    }
                    if (int.Parse(s1)< int.Parse(s2))
                    {
                        return 1;
                    }else if(int.Parse(s1)> int.Parse(s2))
                    {
                        return -1;
                    }
                }
                else
                {
                    if (arr1[i] < arr2[j])
                    {
                        return 1;
                    }if (arr1[i] > arr2[j])
                    {
                        return -1;
                    }
                    i++;
                    j++;
                }
            }if (arr1.Length == arr2.Length)
            {
                return 0;
            }
            else
            {
                return arr1.Length > arr2.Length ? 1 : -1;
            }
        }
    }
}
