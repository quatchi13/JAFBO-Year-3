using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JAFnetwork;
using System;
using System.Net;
using System.Net.Sockets;

//kek this isn't really a lobby rn
public class Lobby : MonoBehaviour
{


    public IPEndPoint remoteEP;
    public static Socket clientSock;
    public bool isConnected = false;
    public static byte[] buff = new byte[1024];
    public static bool disabled = false;

    public bool looping = false;
    private float timeOut = 0.8f;
    public void StartClient()
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
                isConnected = true;
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



    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);
    }

    // Update is called once per frame
    void Update()
    {
        if (isConnected && !disabled)
        {
            int receivedBytes = 0;

            if (SockFunctions.HasStuffToRead(clientSock))
            {
                receivedBytes = clientSock.Receive(buff);
                byte[] recBuff = new byte[receivedBytes];
                Buffer.BlockCopy(buff, 0, recBuff, 0, receivedBytes);
                NetworkParser.ParseCommandBlock(recBuff);

                Debug.Log("Message Received");

                looping = true;
                Follow.isShowingOther = true;
            }


            if (NetworkParser.serverCommandQueue.Count > 0)
            {
                NetworkParser.serverCommandQueue.Dequeue().Execute();
            }


            if (looping)
            {
                if (NetworkParser.networkGameplayCommands.Count > 0)
                {

                    timeOut += Time.deltaTime;
                    Debug.Log(timeOut.ToString("0.00"));
                    if (timeOut >= 0.8f)
                    {
                        NetworkParser.networkGameplayCommands.Dequeue().Inverse();
                        timeOut = 0;
                    }
                    
                }
                else
                {
                    looping = false;
                    timeOut = 0.8f;
                    Follow.isShowingOther = false;
                }
            }
        }
    }

    public void SendCharacterSelection()
    {
        short selected = (short)GetComponent<dontDestroyOnLoad>().character;

        ReadyCommand charSelection = new ReadyCommand();
        charSelection.selectedCharacter = selected;


        byte[] comByte = NetComBuilders.ServComToByteBlock(charSelection);
        byte[] outCom = new byte[2 + comByte.Length];
        TheWorldsMostUnnecessaryStructure ind = new TheWorldsMostUnnecessaryStructure();
        ind.Set(2);
        byte[] iBytes = ind.ToBytes();
        Buffer.BlockCopy(iBytes, 0, outCom, 0, 2);
        Buffer.BlockCopy(comByte, 0, outCom, 2, comByte.Length);


        clientSock.Send(outCom);
    }

}
