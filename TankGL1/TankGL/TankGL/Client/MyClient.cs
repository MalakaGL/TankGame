using System;
using System.IO;
using System.Net.Sockets;
using System.Net;

namespace TankGL.Client
{
    class MyClient
    {
        static TcpClient sender;

        public MyClient()
        {
            IPAddress ip = new IPAddress(2130706433);
        }

        public bool sendMessage(String msg)
        {
            Stream s;
            sender = new TcpClient();
            bool error = false;
            try
            {
                sender.Connect("127.0.0.1", 6000);
                Console.WriteLine("Client connected " + sender.Connected);
                s = sender.GetStream();
                Console.WriteLine("Stream can be written " + s.CanWrite);
                StreamWriter sw = new StreamWriter(s);
                sw.AutoFlush = true;
                sw.Write(msg);
                s.Close();
                error = true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                sender.Close();
            }
            return error;
        }
    }
}