using System;
using System.IO;
using System.Net.Sockets;
using System.Net;
using System.Collections;

namespace TankGL.Network
{
    class Client
    {
        static TcpClient sender;
        static String message;
        public Client()
        {
            IPAddress ip = new IPAddress(2130706433);
            message = "JOIN#";
        }

        public bool sendMessage(String msg)
        {
            Console.WriteLine("Server says " + msg);
            message = msg;
            return true;
        }

        internal void sendMessage()
        {
            long time = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
            String msg = message;
            Stream s;
            sender = new TcpClient();
            try
            {
                sender.Connect("127.0.0.1", 6000);
                Console.WriteLine("Client connected " + sender.Connected + " : " + msg);
                s = sender.GetStream();
                //Console.WriteLine("Stream can be written " + s.CanWrite);
                StreamWriter sw = new StreamWriter(s);
                sw.AutoFlush = true;
                sw.Write(msg);
                s.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                sender.Close();
            }
            message = "SHOOT#";
            Console.WriteLine("Message Sending Time = " + (DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond - time));
        }
    }
}