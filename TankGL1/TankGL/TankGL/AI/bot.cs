using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TankGL.Client;
using TankGL.GUI;

namespace TankGL.AI
{
    class bot
    {
        static int numOfPlayers;
        static Tank [] tanks;
        static MyClient client = new MyClient();

        internal static void playersFull()
        {
            Console.WriteLine("Players full.");
        }

        internal static void alreadyAdded()
        {
            Console.WriteLine("Already Added.");
        }

        internal static void globalUpdate()
        {
            Random rand = new Random();
            int option = rand.Next(5);
            switch (option){
                case 1:
                    Console.WriteLine("Turn RIGHT#");
                    client.sendMessage("RIGHT#");
                    break;
                case 2:
                    Console.WriteLine("LEFT#");
                    client.sendMessage("LEFT#");
                    break;
                case 3:
                    Console.WriteLine("SHOOT#");
                    client.sendMessage("SHOOT#");
                    break;
                case 4:
                    Console.WriteLine("Turn UP#");
                    client.sendMessage("UP#");
                    break;
                case 5:
                    Console.WriteLine("Turn DOWN#");
                    client.sendMessage("DOWN#");
                    break;
            }
            Console.WriteLine("Already Begun.");
        }

        internal static void intiatePlayer(string msg)
        {
            String[] s = msg.Split(';');
            tanks = new Tank[s.Length - 1];
            numOfPlayers = s.Length - 1;
            for (int i = 0; i < numOfPlayers ; i++)
            {
                tanks[i] = new Tank(s[ i + 1 ]);
            }
        }

        public static void initiateGame(String message)
        {
            Console.WriteLine("Setting Game...");
        }

        public static void handleException()
        {
            Console.WriteLine("Erroneous Situation. :'(");
        }

    }
}