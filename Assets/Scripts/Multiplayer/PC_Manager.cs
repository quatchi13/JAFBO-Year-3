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
/// if any of you bitches touch this script or PlayerComm.cs, i will kill you. not literally, but i will be fucking pissed. don't do it.
/// i didn't even want to push this version at all. fuck this
/// </summary>


public class PC_Manager : MonoBehaviour
{
    public List<GameObject> MasterList = new List<GameObject> { };
    public GameObject[] PCList = new GameObject[2];

    private Socket client;
    // Start is called before the first frame update

    private static byte[] outBuffer = new byte[512];
    private static IPEndPoint remoteEP;
    private static Socket clientSock;
    public static bool quitCommandReceived = false;
    public static float delay = 2f;
    public static float timeOut = 0f;

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
        Debug.Log("Network connections disabled. No attempt made");
        
        StartClient();

        NetworkParser.SetPCOrder(PCList[0], PCList[1]);
        
    }

    // Update is called once per frame
    void Update()
    {
        {

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
                    //if (receivedMessage != "end_9669")
                    //{
                    //    Debug.Log("Message from server: " + receivedMessage);
                    //}
                    //else
                    //{
                    //    Debug.Log("QUIT request accepted by server. Closing connection...");
                    //    quitCommandReceived = true;
                    //    KillClient();
                    //}
                }

                if (!quitCommandReceived)
                {
                    if (Input.GetKeyDown(KeyCode.Tab) && CanSend(clientSock))
                    {
                        NetworkParser.SendGameplayQueueToBuffer();

                        clientSock.Send(NetworkParser.outBuffer);
                    }
                }



                if (looping)
                {
                    if (NetworkParser.networkGameplayCommands.Count > 0)
                    {

                        NetworkParser.networkGameplayCommands.Peek().Inverse();
                        NetworkParser.networkGameplayCommands.Dequeue();

                        timeOut += Time.deltaTime;
                        if (timeOut >= 2f)
                        {
                            //NetworkParser.networkGameplayCommands.Peek().Execute();
                            //NetworkParser.networkGameplayCommands.Dequeue();
                        }
                        timeOut = 0;
                    }
                    else
                    {
                        looping = false;
                        timeOut = 0;
                    }
                }

            }

        }

    }





    public void ReorderPlayerCharacters(List<int> order)
    {
        for(int i = 0; i < order.Count; i++)
        {
            PCList[i] = MasterList[order[i]];
        }
    }
}
