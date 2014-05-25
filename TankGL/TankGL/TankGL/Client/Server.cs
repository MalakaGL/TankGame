using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net.Sockets;
using System.Net;
using TankGL.AI;

namespace TankGL.Client
{
    class Server
    {
        // Set the TcpListener on port 13000.
        Int32 port = 7000;
        IPAddress localAddr = IPAddress.Parse("127.0.0.1");
        String message = "Failed";
        TcpListener server = null;
        // Buffer for reading data
        Byte[] bytes = new Byte[256];
        String data = null;
        bot myBot = new bot();
    
        public void listen()
        {
            try
            {                
                // TcpListener server = new TcpListener(port);
                server = new TcpListener(localAddr, port);

                // Start listening for client requests.
                server.Start();

                // Enter the listening loop. 
                while (true)
                {
                    Console.Write("Waiting for a connection... ");

                    // Perform a blocking call to accept requests. 
                    // You could also user server.AcceptSocket() here.
                    TcpClient client = server.AcceptTcpClient();
                    Console.WriteLine("Connected!");

                    data = null;

                    // Get a stream object for reading and writing
                    NetworkStream stream = client.GetStream();

                    int i, j = 0;

                    // Loop to receive all the data sent by the client. 
                    if ((i = stream.Read(bytes, 0, bytes.Length)) != 0)
                    {
                        // Translate data bytes to a ASCII string.
                        data = System.Text.Encoding.ASCII.GetString(bytes, 0, i);
                        message = new String(data.ToCharArray());
                        Console.WriteLine("\n\n" + message);
                        switch(message.Substring(0,1))
                        {
                            case "P": //Players full
                                bot.playersFull();
                                break;
                            case "A": //ALready added
                                bot.alreadyAdded();
                                break;
                            case "G": //Game already begun, global update, finished or not started yet
                                bot.globalUpdate();
                                break;
                            case "S":
                                bot.intiatePlayer(message);
                                break;
                            case "I": //initiation and invalid cell
                                bot.initiateGame(message);
                                break;
                            default:
                                bot.handleException();
                                break;
                        }
                    }
                    // Shutdown and end connection
                    client.Close();
                }
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