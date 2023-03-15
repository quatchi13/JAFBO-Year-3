using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JAFprocedural;
using System.Text;
using System.Net;
using System.Net.Sockets;
using JAFnetwork;
using System;


/// <summary>
/// This needs to be placed on some sort of game manager
/// we will likely have some of this stuff (the socket) carry over from setup
/// </summary>


public class PC_Manager : MonoBehaviour
{
    public static List<GameObject> MasterList = new List<GameObject> { };
    public static GameObject[] PCList = new GameObject[2];
    public GameObject ArenaManagerRef;

    private Socket client;
    // Start is called before the first frame update

    private static byte[] outBuffer = new byte[512];
    private static IPEndPoint remoteEP;
    private static Socket clientSock;
    public static bool quitCommandReceived = false;
    public static float delay = 2f;
    public float timeOut = 0.8f;

    public bool looping = false;


    public static bool HasStuffToRead(Socket s)
    {
        return s.Poll(1, SelectMode.SelectRead);
    }
    public static bool CanSend(Socket s)
    {
        return s.Poll(1, SelectMode.SelectWrite);
    }

    public static void KillClient()
    {
        clientSock.Shutdown(SocketShutdown.Both);
        clientSock.Close();
        Debug.Log("Successfully disconnected from server");
    }

    public static void StartClient()
    {
        try
        {
            IPAddress ip = IPAddress.Parse("127.0.0.1");
            remoteEP = new IPEndPoint(ip, 9669);

            clientSock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {

                //Attempt a connection
                Debug.Log("Connecting to server...");
                clientSock.Connect(remoteEP);
                Debug.Log("Successfully connected to server at IP: " + clientSock.RemoteEndPoint.ToString());
            }
            catch (SocketException e)
            {
                Debug.Log("something goofed: " + e.ToString());
            }
        }
        catch (SocketException e)
        {
            Debug.Log("something goofed: " + e.ToString());
        }
    }

    void Start()
    {
        //comment this out if you are not testing networking
        StartClient();
        //don't uncomment this tho
        NetworkParser.SetPCOrder(PCList[0], PCList[1]);
        NetworkParser.aGenRef = ArenaManagerRef;
        
    }

    // Update is called once per frame
    void Update()
    {
        //comment everything in these {}'s if you aren't using networking
        {
            //i need to clean this up X(
            if (!quitCommandReceived)
            {
                byte[] buff = new byte[1024];
                int receivedBytes = 0;
                //string receivedMessage;

                if (HasStuffToRead(clientSock))
                {
                    receivedBytes = clientSock.Receive(buff);
                    byte[] recBuff = new byte[receivedBytes];
                    Buffer.BlockCopy(buff, 0, recBuff, 0, receivedBytes);
                    NetworkParser.ParseCommandBlock(recBuff);
                    looping = true;

                    Debug.Log("CRAZY");
                    Debug.Log("Queue size: " + NetworkParser.networkGameplayCommands.Count);
                    
                }

                if (!quitCommandReceived)
                {
                    if (Input.GetKeyDown(KeyCode.Tab) && CanSend(clientSock))
                    {
                        NetworkParser.SendGameplayQueueToBuffer();

                        clientSock.Send(NetworkParser.outBuffer);
                    }
                }

                if(NetworkParser.serverCommandQueue.Count > 0)
                {
                    NetworkParser.serverCommandQueue.Dequeue().Execute();
                }


                if (looping)
                {
                    if (NetworkParser.networkGameplayCommands.Count > 0)
                    {

                        timeOut += Time.deltaTime;
                        if (timeOut >= 0.8f)
                        {
                            NetworkParser.networkGameplayCommands.Peek().Inverse();
                            NetworkParser.networkGameplayCommands.Dequeue();
                            timeOut = 0;
                        }
                        
                    }
                    else
                    {
                        looping = false;
                        timeOut = 0.8f;
                    }
                }

            }

        }

    }





    public static void ReorderPlayerCharacters(List<int> order)
    {
        for(int i = 0; i < order.Count; i++)
        {
            PCList[i] = MasterList[order[i]];
        }
    }
}
