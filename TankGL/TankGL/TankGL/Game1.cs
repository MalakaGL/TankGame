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
            //Bot.intiatePlayer("S:P0;0,0;0#");
            //Bot.globalUpdate("G:P0;0,0;0;0;100;0;0:8,4,0;7,6,0;6,8,0;1,3,0;3,2,0;2,1,0;4,8,0;6,3,0#");
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