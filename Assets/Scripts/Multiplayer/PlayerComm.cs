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
            new ChangeFlagChar()
        };
        private static ServerCommand[] serverCommandTypes = new ServerCommand[]
        {
            new ArenaSetupCommand(), new CharacterOrderCommand(), new ReadyCommand(), new StartTurnCommand(), new StartGameCommand()
        };





        public static byte[] CommandToByteBlock(NetCommand netCom)
        {
            int obSize = Marshal.SizeOf(netCom);
            Debug.Log("size of this object: " + obSize.ToString());
            byte[] byteBlock = new byte[obSize + 2];
            TheWorldsMostUnnecessaryStructure str = new TheWorldsMostUnnecessaryStructure();
            str.Set(netCom.ComIndex());
            Buffer.BlockCopy(str.ToBytes(), 0, byteBlock, 0, 2);

            IntPtr memPoint = Marshal.AllocHGlobal(obSize);
            Marshal.StructureToPtr(netCom, memPoint, false);
            Marshal.Copy(memPoint, byteBlock, 2, obSize);
            Marshal.FreeHGlobal(memPoint);

            return byteBlock;
        }


        public static void SendGameplayQueueToBuffer()
        {
            TheWorldsMostUnnecessaryStructure str = new TheWorldsMostUnnecessaryStructure();
            str.Set(1);
            Debug.Log(str.val.ToString());
            Array.Copy(str.ToBytes(), 0, outBuffer, 0, 2);
            TheWorldsMostUnnecessaryStructure y = new TheWorldsMostUnnecessaryStructure();
            y.Set((short)localGameplayCommands.Count);
            Buffer.BlockCopy(y.ToBytes(), 0, outBuffer, 2, 2);
            int offset = 4;
            byte[] tBuff = new byte[1];
            for (; localGameplayCommands.Count > 0;
                tBuff = CommandToByteBlock(localGameplayCommands.Dequeue()),
                Array.Copy(tBuff, 0, outBuffer, offset, tBuff.Length),
                offset += tBuff.Length) { }

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
                        //tempComList.Peek().Inverse();
                        break;
                    case (1):
                        Debug.Log("Queueing up some attacking");
                        tempComList.Push(NetComBuilders.BytesToNetCom(comBuff, new BasicAttackChar()));
                        networkGameplayCommands.Enqueue(tempComList.Peek());
                        break;
                    default:
                        break;

                }
                offset += comBuff.Length;
                //networkGameplayCommands.Enqueue(tempComList.Peek());


            }
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
                if (comBuff.Length == 0) comBuff = new byte[2];
                Array.Copy(buff, offset, comBuff, 0, comBuff.Length);

                switch (curType.val)
                {
                    case (0):
                        Debug.Log("Server command: Make arena");
                        tempComList.Push(NetComBuilders.BytesToServerCom(comBuff, new ArenaSetupCommand()));
                        serverCommandQueue.Enqueue(tempComList.Peek());
                        //tempComList.Peek().Inverse();
                        break;
                    case (1):
                        Debug.Log("Server command: Setup characters");
                        tempComList.Push(NetComBuilders.BytesToServerCom(comBuff, new CharacterOrderCommand()));
                        serverCommandQueue.Enqueue(tempComList.Peek());
                        break;
                    case (3):
                        Debug.Log("Server command: Start");
                        tempComList.Push(NetComBuilders.BytesToServerCom(comBuff, new StartTurnCommand()));
                        serverCommandQueue.Enqueue(tempComList.Peek());
                        break;
                    case (4):
                        Debug.Log("Server command: Init game");
                        tempComList.Push(NetComBuilders.BytesToServerCom(comBuff, new StartGameCommand()));
                        serverCommandQueue.Enqueue(tempComList.Peek());
                        break;
                    default:
                        break;

                }
                offset += comBuff.Length;
            }
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
    }
}
