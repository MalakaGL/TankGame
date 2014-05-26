using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TankGL.Network;
using TankGL.AI;
using TankGL.GUI;
using System.Threading;

namespace TankGL
{
    class Game1
    {
        public static void Main(String[] args)
        {
            Server myServer = new Server();
            Client myClient = new Client();

            while (true)
            {
                myClient.sendMessage();
                long time = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
                while ((DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond - time) < 1000)
                {
                    myServer.listen();
                }
            }
        }
    }
}