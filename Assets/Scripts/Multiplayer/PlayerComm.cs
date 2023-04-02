using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using System.Runtime.InteropServices;


/// <summary>
/// functions and shit! this is where all the parsing and data organization happens
/// we'll need to do some cleanup in here
/// not a script either, just the poorgaming backend
/// </summary>
namespace JAFnetwork
{
    public static class NetworkParser
    {
        public static byte[] outBuffer = new byte[1024];

        public static Queue<NetCommand> localGameplayCommands = new Queue<NetCommand>();
        public static Queue<NetCommand> networkGameplayCommands = new Queue<NetCommand>();
        public static Queue<ServerCommand> serverCommandQueue = new Queue<ServerCommand>();

        public static GameObject[] playerCharacters = new GameObject[2];
        public static GameObject aGenRef = new GameObject();
        public static void SetPCOrder(GameObject g1, GameObject g2)
        {
            playerCharacters[0] = g1;
            playerCharacters[1] = g2;
        }
        public static short GetPCIndex(GameObject pc)
        {
            return ((pc == playerCharacters[0]) ? (short)0 : (short)1);
        }




        private static NetCommand[] gameplayCommandTypes = new NetCommand[] {
            new MoveChar(),
            new BasicAttackChar(),
            new ChangeStatChar(),
            new ChangeFlagChar(), 
            new EliminateChar()
        };
        private static ServerCommand[] serverCommandTypes = new ServerCommand[]
        {
            new ArenaSetupCommand(),//received
            new CharSelectionsAndPlayerIndex(),//received
            new ReadyCommand(),//sent
            new StartTurnCommand(),//received
            new StartGameCommand(),//received
            new SetCharacterOrder(),//received
            new MarkPlayerAsEliminated()//sent
        };





        
        public static void SendServerCommand(ServerCommand sCom)
        {
            byte[] commandBlock = NetComBuilders.ServComToByteBlock(sCom);
            byte[] outBlock = new byte[2 + commandBlock.Length];
            Buffer.BlockCopy(commandBlock, 0, outBlock, 2, commandBlock.Length);
            TheWorldsMostUnnecessaryStructure ind = new TheWorldsMostUnnecessaryStructure();
            ind.Set(2);
            byte[] indBytes = ind.ToBytes();
            Buffer.BlockCopy(indBytes, 0, outBlock, 0, 2);

            if (SockFunctions.CanSend(Lobby.clientSock)) Lobby.clientSock.Send(outBlock);
        }

        public static void SendGameplayQueueToBuffer()
        {
            TheWorldsMostUnnecessaryStructure str = new TheWorldsMostUnnecessaryStructure();
            str.Set(1);
            byte[] tempOBuff = new byte[1024];
            Array.Copy(str.ToBytes(), 0, tempOBuff, 0, 2);
            TheWorldsMostUnnecessaryStructure y = new TheWorldsMostUnnecessaryStructure();
            y.Set((short)localGameplayCommands.Count);
            Buffer.BlockCopy(y.ToBytes(), 0, tempOBuff, 2, 2);
            int offset = 4;
            byte[] tBuff = new byte[1];
            for (; localGameplayCommands.Count > 0;
                tBuff = NetComBuilders.CommandToByteBlock(localGameplayCommands.Dequeue()),
                Buffer.BlockCopy(tBuff, 0, tempOBuff, offset, tBuff.Length),
                offset += tBuff.Length) { }
            outBuffer = new byte[offset];
            Buffer.BlockCopy(tempOBuff, 0, outBuffer, 0, offset);

        }

        public static void ProcessGameplayCommandBlock(byte[] buff)
        {
            byte[] comBuff;


            Stack<NetCommand> tempComList = new Stack<NetCommand> { };

            int offset = 0;
            byte[] queueSize = new byte[2];
            Array.Copy(buff, offset, queueSize, 0, 2);
            offset += 2;
            TheWorldsMostUnnecessaryStructure qCount = NetComBuilders.BytesToShortStuff(queueSize);
            for (; tempComList.Count < qCount.val;)
            {
                byte[] cT = new byte[2];
                Array.Copy(buff, offset, cT, 0, 2);
                offset += 2;
                TheWorldsMostUnnecessaryStructure curType = NetComBuilders.BytesToShortStuff(cT);
                comBuff = new byte[Marshal.SizeOf(gameplayCommandTypes[curType.val])];
                Array.Copy(buff, offset, comBuff, 0, comBuff.Length);

                switch (curType.val)
                {
                    case (0):
                        Debug.Log("Queueing up some movement");
                        tempComList.Push(NetComBuilders.BytesToNetCom(comBuff, new MoveChar()));
                        networkGameplayCommands.Enqueue(tempComList.Peek());
                        break;
                    case (1):
                        Debug.Log("Queueing up some attacking");
                        tempComList.Push(NetComBuilders.BytesToNetCom(comBuff, new BasicAttackChar()));
                        networkGameplayCommands.Enqueue(tempComList.Peek());
                        break;
                    case (2):
                        Debug.Log("Queueing up a stat change");
                        tempComList.Push(NetComBuilders.BytesToNetCom(comBuff, new ChangeStatChar()));
                        networkGameplayCommands.Enqueue(tempComList.Peek());
                        break;
                    case (3):
                        Debug.Log("Queueing up a flag change");
                        tempComList.Push(NetComBuilders.BytesToNetCom(comBuff, new ChangeFlagChar()));
                        networkGameplayCommands.Enqueue(tempComList.Peek());
                        break;
                    case (4):
                        Debug.Log("Queueing up a CHARACTER ELIMINATION :O");
                        tempComList.Push(NetComBuilders.BytesToNetCom(comBuff, new EliminateChar()));
                        networkGameplayCommands.Enqueue(tempComList.Peek());
                        break;
                    default:
                        break;

                }
                offset += comBuff.Length;
                //networkGameplayCommands.Enqueue(tempComList.Peek());

            }
            Debug.Log("finished parsing external gameplay commands");
        }

        public static void ProcessServerCommandBlock(byte[] buff)
        {
            byte[] comBuff;


            Stack<ServerCommand> tempComList = new Stack<ServerCommand> { };

            int offset = 0;
            byte[] queueSize = new byte[2];
            Array.Copy(buff, offset, queueSize, 0, 2);
            offset += 2;
            TheWorldsMostUnnecessaryStructure qCount = NetComBuilders.BytesToShortStuff(queueSize);
            Debug.Log("Commands received from the server: " + qCount.val.ToString());
            for (; tempComList.Count < qCount.val;)
            {
                byte[] cT = new byte[2];
                Array.Copy(buff, offset, cT, 0, 2);
                offset += 2;
                TheWorldsMostUnnecessaryStructure curType = NetComBuilders.BytesToShortStuff(cT);
                Debug.Log("Server command type: " + curType.val.ToString());
                comBuff = new byte[Marshal.SizeOf(serverCommandTypes[curType.val])];
                Debug.Log("Command size: " + comBuff.Length);
                if (comBuff.Length == 0) comBuff = new byte[2];
                Array.Copy(buff, offset, comBuff, 0, comBuff.Length);

                switch (curType.val)
                {
                    case (0):
                        Debug.Log("Server command: Make arena");
                        tempComList.Push(NetComBuilders.BytesToServerCom(comBuff, new ArenaSetupCommand()));
                        serverCommandQueue.Enqueue(tempComList.Peek());
                        break;
                    case (1):
                        Debug.Log("Server command: Receive selected Characters");
                        tempComList.Push(NetComBuilders.BytesToServerCom(comBuff, new CharSelectionsAndPlayerIndex()));
                        serverCommandQueue.Enqueue(tempComList.Peek());
                        break;
                    case (3):
                        Debug.Log("Server command: Start");
                        tempComList.Push(NetComBuilders.BytesToServerCom(comBuff, new StartTurnCommand()));
                        serverCommandQueue.Enqueue(tempComList.Peek());
                        break;
                    case (4):
                        Debug.Log("Server command: Load Main Scene");
                        tempComList.Push(NetComBuilders.BytesToServerCom(comBuff, new StartGameCommand()));
                        serverCommandQueue.Enqueue(tempComList.Peek());
                        break;
                    case (5):
                        Debug.Log("Server command: Reorder characters");
                        tempComList.Push(NetComBuilders.BytesToServerCom(comBuff, new SetCharacterOrder()));
                        serverCommandQueue.Enqueue(tempComList.Peek());
                        break;
                    default:
                        break;

                }
                offset += comBuff.Length;
            }
            Debug.Log("finished parsing server commands");
        }

        public static void ParseCommandBlock(byte[] rawBytes)
        {
            int receivedByteCount = rawBytes.Length;
            Debug.Log("Received Bytes: " + receivedByteCount.ToString());
            int offset = 0;
            byte[] sBuff = new byte[2];
            byte[] comBlock;

            Array.Copy(rawBytes, offset, sBuff, 0, 2);
            TheWorldsMostUnnecessaryStructure str = NetComBuilders.BytesToShortStuff(sBuff);
            Debug.Log(str.val.ToString());

            comBlock = new byte[receivedByteCount - 2];

            Debug.Log("comblock size: " + comBlock.Length.ToString());
            Array.Copy(rawBytes, 2, comBlock, 0, comBlock.Length);
            if (str.val == 1)
            {
                ProcessGameplayCommandBlock(comBlock);
            }
            else if (str.val == 3)
            {
                Debug.Log("server commands");
                ProcessServerCommandBlock(comBlock);
            }
            else
            {
                Console.WriteLine("BRUH");
            }
        }

        public static void SetLocalCharacter(GameObject replace, short index)
        {
            playerCharacters[GetPCIndex(replace)] = GameObject.Instantiate(GameObject_Manager.staticLocalPrefabs[index], replace.transform);

        }

        public static void SetRemoteCharacter(GameObject replace, short index)
        {
            playerCharacters[GetPCIndex(replace)] = GameObject.Instantiate(GameObject_Manager.staticRemotePrefabs[index], replace.transform);
        }


        public static void SetCharacter(GameObject replace, short index)
        {
            GameObject template = (GetPCIndex(replace) == GameObject_Manager.localPlayerIndex) ? GameObject_Manager.staticLocalPrefabs[index] : GameObject_Manager.staticRemotePrefabs[index];
            playerCharacters[GetPCIndex(replace)] = GameObject.Instantiate(template, replace.transform.position, Quaternion.identity);
        }
    }
}
