using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JAFprocedural;
using System.Text;
using System.Net;
using System.Net.Sockets;
using JAFnetwork;





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
                Debug.Log("Hacking into the mainframe...");
                clientSock.Connect(remoteEP);
                Debug.Log("Successfully breached the mainframe at IP: " + clientSock.RemoteEndPoint.ToString());
            }
            catch (SocketException e)
            {
                Debug.Log("YOU FUCKED UP!!! " + e.ToString());
            }
        }
        catch (SocketException e)
        {
            Debug.Log("YOU FUCKED UP!!! " + e.ToString());
        }
    }

    void Start()
    {
        StartClient();
        NetworkParser.SetPCOrder(PCList[0], PCList[1]);
    }

    // Update is called once per frame
    void Update()
    {
        if (!quitCommandReceived)
        {
            byte[] buffer = new byte[1024];
            int receivedBytes = 0;
            //string receivedMessage;

            if (HasStuffToRead(clientSock))
            {
                receivedBytes = clientSock.Receive(buffer);
                NetworkParser.ProcessUnparsedByteBuffer(buffer);
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

                    clientSock.SendTo(NetworkParser.outBuffer, remoteEP);
                }
            }



            if (looping){
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





    public void ReorderPlayerCharacters(List<int> order)
    {
        for(int i = 0; i < order.Count; i++)
        {
            PCList[i] = MasterList[order[i]];
        }
    }
}
