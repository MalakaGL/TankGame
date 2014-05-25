using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TankGL.Client;
using TankGL.GUI;

namespace TankGL.AI
{
    class Bot
    {
        static int numOfPlayers;
        static Tank[] tanks = new Tank[5];
        static MyClient client = new MyClient();
        static int myId;
        static Cell[,] grid;

        internal static void playersFull()
        {
            Console.WriteLine("Players full. Press Escape to exit.");
            while (!Keyboard.GetState().IsKeyDown(Keys.Escape)) ;
            return;
        }

        internal static void alreadyAdded()
        {
            Console.WriteLine("Already Added.");
        }

        internal static void globalUpdate(String msg)
        {
            msg = msg.Substring(2, msg.Length - 3);
            String[] details = msg.Split(':');
            String message = "";
            for (int i = 0; i < numOfPlayers; i++)
            {
                String[] temp = details[i].Split(';');
                message += temp[0] + ";" + temp[1] + ";" + temp[2] + ":";
                if (int.Parse(temp[4]) == 0)
                {
                    grid[tanks[i].getY(), tanks[i].getX()].setType(" E");
                }
            }
            setPlayers(message.Substring(0, message.Length - 1));
            for (int i = 0; i < numOfPlayers; i++)
            {
                String[] temp = details[i].Split(';');
                if (int.Parse(temp[4]) == 0)
                {
                    grid[tanks[i].getY(), tanks[i].getX()].setType(" E");
                }
            } 
            setBricks(details[numOfPlayers]);
            display();
            if (!shoot(tanks[myId].getDirection()))
            {
                move(tanks[myId].getDirection(), 0);
            }
        }

        internal static bool shoot(int direction)
        {
            bool done = false;
            Tank t = tanks[myId];
            switch (direction)
            {
                case 0:
                    for (int i = t.getY() - 1; i > 0; i--)
                    {
                        String occ = grid[i, t.getX()].getType();
                        if (occ.Equals("B") || occ.Substring(0, 1).Equals("P"))
                        {
                            client.sendMessage("SHOOT#");
                            done = true;
                            break;
                        }
                        if (occ.Equals("S"))
                        {
                            break;
                        }
                    }
                    break;
                case 1:
                    for (int i = tanks[myId].getX() + 1; i < Constants.GRID_SIZE; i++)
                    {
                        String occ = grid[tanks[myId].getY(), i].getType();
                        if (occ.Equals("B") || occ.Substring(0, 1).Equals("P"))
                        {
                            client.sendMessage("SHOOT#");
                            done = true;
                            break;
                        }
                    }
                    break;
                case 2:
                    for (int i = tanks[myId].getY() + 1; i < Constants.GRID_SIZE; i++)
                    {
                        String occ = grid[i, tanks[myId].getX()].getType();
                        if (occ.Equals("B") || occ.Substring(0, 1).Equals("P"))
                        {
                            client.sendMessage("SHOOT#");
                            done = true;
                            break;
                        }
                    }
                    break;
                case 3:
                    for (int i = tanks[myId].getX() - 1; i > 0; i--)
                    {
                        String occ = grid[i, tanks[myId].getX()].getType();
                        if (occ.Equals("B") || occ.Substring(0, 1).Equals("P"))
                        {
                            client.sendMessage("SHOOT#");
                            done = true;
                            break;
                        }
                    }
                    break;
            }
            return done;
        }

        internal static void move(int direction, int call)
        {
            Tank t = tanks[myId];
            String occ;
            call++;
            if (call > 4)
            {
                Console.WriteLine("Pass 1");
                return;
            }

            switch (direction)
            {
                case 0:
                    Console.WriteLine("Checking " + t.getY() + " " +direction);
                    if (t.getY() - 1 < 0)
                    {
                        move(1, call);
                        return;
                    }
                    else
                    {
                        occ = grid[t.getY() - 1, t.getX()].getType();
                        if (occ.Equals(" E"))
                        {
                            client.sendMessage("UP#");
                        }
                        else
                        {
                            move(1, call);
                            return;
                        }
                    }
                    break;
                case 1:
                    Console.WriteLine("Checking " + t.getX() + " " + direction);
                    if (t.getX() + 1 >= Constants.GRID_SIZE)
                    {
                        move(2, call);
                        return;
                    }
                    else
                    {
                        occ = grid[t.getY(), t.getX() + 1].getType();
                        Console.WriteLine(occ);
                        if (occ.Equals(" E"))
                        {
                            client.sendMessage("RIGHT#");
                        }
                        else
                        {
                            move(2, call);
                            return;
                        }
                    }
                    break;
                case 2:
                    Console.WriteLine("Checking " + t.getY() + " " + direction);
                    if (t.getY() + 1 >= Constants.GRID_SIZE)
                    {
                        move(3, call);
                        return;
                    }
                    else
                    {
                        occ = grid[t.getY() + 1, t.getX()].getType();
                        if (occ.Equals(" E"))
                        {
                            client.sendMessage("LEFT#");
                        }
                        else
                        {
                            move(3, call);
                            return;
                        }
                    }
                    break;
                case 3:
                    Console.WriteLine("Checking " + t.getX() + " " + direction);
                    if (t.getX() - 1 < 0)
                    {
                        move(0, call);
                        return;
                    }
                    else
                    {
                        occ = grid[t.getY(), t.getX() - 1].getType();
                        if (occ.Equals(" E"))
                        {
                            client.sendMessage("LEFT#");
                        }
                        else
                        {
                            move(0, call);
                            return;
                        }
                    }
                    break;
            }
        }

        internal static void intiatePlayer(string msg)
        {
            msg = msg.Substring(2, msg.Length - 3);
            setPlayers(msg);
            display();
        }

        internal static void setBricks(String msg)
        {
            String[] temp = msg.Split(';');
            for (int i = 0; i < temp.Length; i++)
            {
                if(int.Parse(temp[i].Substring(temp[i].Length - 1,1)) == 4)
                {
                    String [] t = temp[i].Split(',');
                    grid[int.Parse(t[1]),int.Parse(t[0])].setType(" E");
                }
            }
        }

        private static void setPlayers(string msg)
        {
            String[] players = msg.Split(':');
            numOfPlayers = players.Length;
            for (int i = 0; i < players.Length; i++)
            {
                String[] details = players[i].Split(';');
                int[] p = getPoint(details[1]);
                if(tanks[i] != null)
                    grid[tanks[i].getY(), tanks[i].getX()].setType(" E"); 
                tanks[i] = new Tank(int.Parse(details[0].Substring(1, 1)), p[0], p[1],
                    int.Parse(details[2]));
                grid[p[1], p[0]].setType(details[0]);
            }
        }

        public static void initiateGame(String message)
        {
            Console.WriteLine("Setting Game...");
            grid = new Cell[Constants.GRID_SIZE, Constants.GRID_SIZE];
            message = message.Substring(2, message.Length - 3);
            String[] temp = message.Split(':');
            myId = int.Parse(temp[0].Substring(1, 1));
            String[] temp2 = temp[1].Split(';');
            for (int i = 0; i < Constants.GRID_SIZE; i++)
            {
                for (int j = 0; j < Constants.GRID_SIZE; j++)
                {
                    grid[i, j] = new Cell(" E");
                }
            }
            for (int i = 0; i < temp2.Length; i++)
            {
                String[] temp3 = temp2[i].Split(',');
                grid[int.Parse(temp3[1]), int.Parse(temp3[0])].setType(" B");
            }
            temp2 = temp[2].Split(';');
            for (int i = 0; i < temp2.Length; i++)
            {
                String[] temp3 = temp2[i].Split(',');
                grid[int.Parse(temp3[1]), int.Parse(temp3[0])].setType(" S");
            }
            temp2 = temp[3].Split(';');
            for (int i = 0; i < temp2.Length; i++)
            {
                String[] temp3 = temp2[i].Split(',');
                grid[int.Parse(temp3[1]), int.Parse(temp3[0])].setType(" W");
            }
            display();
        }

        internal static void display()
        {
            for (int i = 0; i < Constants.GRID_SIZE; i++)
            {
                for (int j = 0; j < Constants.GRID_SIZE; j++)
                {
                    Console.Write(grid[i, j].getType() + " | ");
                }
                Console.WriteLine();
            }
        }

        public static void handleException(String msg)
        {
            switch (msg)
            {
                case "OBSTACLE#":
                    //think update your data
                    Console.WriteLine("Blind bot. You hit on something.");
                    break;
                case "CELL_OCCUPIED#":
                    //update man...
                    Console.WriteLine("Someone in bot.");
                    break;
                case "DEAD#":
                    Console.WriteLine("You are done. Watch genius play.");
                    break;
                case "TOO_QUICK#":
                    // never mind for now :) Consider resending last action
                    break;
                case "INVALID_CELL#":
                    Console.WriteLine("Invalid cell.");
                    break;
                case "NOT_A_VALID_CONTESTANT#":
                    Console.WriteLine("He doesn't know you bot...");
                    break;
                default:
                    Console.WriteLine("Something happened. Carry on.");
                    break;
            }
        }

        internal static void gameIssue(string msg)
        {
            if (msg.Equals("GAME_ALREADY_STARTED#"))
            {
                Console.WriteLine("Already Begun.");
            }
            else if (msg.Equals("GAME_HAS_FINISHED#"))
            {
                Console.WriteLine("Game Has Finished. Press Escape to exit.");
                while (!Keyboard.GetState().IsKeyDown(Keys.Escape)) ;
                return;
            }
            else if (msg.Equals("GAME_NOT_STARTED YET#"))
            {
                Console.WriteLine("Game not started yet.");
            }
        }

        internal static void tryCoins(string message)
        {
            Console.WriteLine("Coin Pile...");
        }

        internal static void tryLifePack(string message)
        {
            Console.WriteLine("Life Pack...");
        }

        internal static int[] getPoint(String p)
        {
            int[] point = new int[2];
            String[] t = p.Split(',');
            point[0] = int.Parse(t[0]);
            point[1] = int.Parse(t[1]);
            return point;
        }
    }
}