using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net.Sockets;
using System.Net;
using TankGL.AI;

namespace TankGL.Network
{
    class Server
    {
        // Set the TcpListener on port 7000.
        Int32 port = 7000;
        IPAddress localAddr = IPAddress.Parse("127.0.0.1");
        String message = "Failed";
        TcpListener server = null;
        // Buffer for reading data
        Byte[] bytes = new Byte[256];
        String data = null;

        public void listen()
        {
            try
            {
                // TcpListener server = new TcpListener(port);
                server = new TcpListener(localAddr, port);

                // Start listening for client requests.
                server.Start();

                Console.Write("Listening...");

                // Perform a blocking call to accept requests. 
                // You could also user server.AcceptSocket() here.
                TcpClient client = server.AcceptTcpClient();
                Console.WriteLine("Connected!");

                data = null;

                // Get a stream object for reading and writing
                NetworkStream stream = client.GetStream();

                int i = 0;

                // Loop to receive all the data sent by the client. 
                if ((i = stream.Read(bytes, 0, bytes.Length)) != 0)
                {
                    // Translate data bytes to a ASCII string.
                    data = System.Text.Encoding.ASCII.GetString(bytes, 0, i);
                    message = new String(data.ToCharArray());
                    Console.WriteLine(message + "\n\n");
                    switch (message.Substring(0, 2))
                    {
                        case "G:":
                            Bot.globalUpdate(message);
                            break;
                        case "C:":
                            Bot.tryCoins(message);
                            break;
                        case "L:":
                            Bot.tryLifePack(message);
                            break;
                        case "PL": //Players full
                            Bot.playersFull();
                            break;
                        case "AL": //ALready added
                            Bot.alreadyAdded();
                            break;
                        case "GA": //Game already begun, finished or not started yet
                            Bot.gameIssue(message);
                            break;
                        case "S:":
                            Bot.intiatePlayer(message);
                            break;
                        case "I:": //initiation
                            Bot.initiateGame(message);
                            break;
                        default:
                            Bot.handleException(message);
                            break;
                    }
                }
                // Shutdown and end connection
                client.Close();
            }
            catch (SocketException e)
            {
                Console.WriteLine("SocketException: {0}", e);
            }
            finally
            {
                // Stop listening for new responses.
                server.Stop();
            }
        }
    }
}