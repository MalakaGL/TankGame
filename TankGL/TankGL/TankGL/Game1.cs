using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TankGL.Client;
using TankGL.AI;

namespace TankGL
{
    class Game1
    {
        public static void Main(String [] args)
        {
            Game1 game = new Game1();
            game.start();
            //bot.intiatePlayer("S:P0;0,0;0:P1;0,9;0:P2;9,0;0:P3;9,9;0:P4;5,5;0#");
        }

        public void start(){
            MyClient client = new MyClient();
            bool joined = false;
            Server myServer = new Server();

            while(!joined)
            {
                joined = client.sendMessage("JOIN#");
            }
            myServer.listen();
        }
    }
}