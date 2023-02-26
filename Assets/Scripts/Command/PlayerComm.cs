using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using System.Runtime.InteropServices;



namespace JAFnetwork
{
    
   

    [StructLayout(LayoutKind.Sequential, Size = 652)]
    public class NetInstructBlock
    {
        public short commsActive;
        public NetCommand instr_0;
        public NetCommand instr_1;
        public NetCommand instr_2;
        public NetCommand instr_3;
        public NetCommand instr_4;
        public NetCommand instr_5;
        public NetCommand instr_6;
        public NetCommand instr_7;
        public NetCommand instr_8;
        public NetCommand instr_9;
        public NetCommand instr_10;
        public NetCommand instr_11;
        public NetCommand instr_12;
        public NetCommand instr_13;
        public NetCommand instr_14;
        public NetCommand instr_15;
        public NetCommand instr_16;
        public NetCommand instr_17;
        public NetCommand instr_18;
        public NetCommand instr_19;
        public NetCommand instr_20;
        public NetCommand instr_21;
        public NetCommand instr_22;
        public NetCommand instr_23;
        public NetCommand instr_24;
        

        public void Init(short bt = 0)
        {
            commsActive = 0;
        }

        public void CopyActiveToQueue(Queue<NetCommand> comQueue)
        {
            if(commsActive > 0)
            {
                Debug.Log("commands in queue: " + commsActive.ToString());
                NetCommand[] arr = new NetCommand[]
                {instr_0, instr_1, instr_2, instr_3, instr_4, instr_5, instr_6, instr_7, instr_8, instr_9, instr_10, instr_11, instr_12, instr_13, instr_14, instr_15, instr_16, instr_17, instr_18, instr_19, instr_20, instr_21, instr_22, instr_23, instr_24};
                for (int i = 0; i < commsActive; arr[i].Inverse(), i++) { }
            }
            
        }

        public void AddToList(NetCommand nCom)
        {
            if(commsActive < 25)
            {
                NetCommand[] arr = new NetCommand[]
            {instr_0, instr_1, instr_2, instr_3, instr_4, instr_5, instr_6, instr_7, instr_8, instr_9, instr_10, instr_11, instr_12, instr_13, instr_14, instr_15, instr_16, instr_17, instr_18, instr_19, instr_20, instr_21, instr_22, instr_23, instr_24};
                arr[commsActive] = nCom;
                commsActive++;
            }
        }
        public NetCommand GetValAt(int index)
        {
            NetCommand n = new MoveChar();
            if(index < 25 && index > -1)
            {
                NetCommand[] arr = new NetCommand[]
            {instr_0, instr_1, instr_2, instr_3, instr_4, instr_5, instr_6, instr_7, instr_8, instr_9, instr_10, instr_11, instr_12, instr_13, instr_14, instr_15, instr_16, instr_17, instr_18, instr_19, instr_20, instr_21, instr_22, instr_23, instr_24};
                n = arr[index];
            }
            return n;
        }


        public byte[] ConvertToBytes()
        {
            int spaceToAlloc = Marshal.SizeOf(this);
            byte[] bytes = new byte[spaceToAlloc];

            try
            {
                IntPtr memYeeter = Marshal.AllocHGlobal(spaceToAlloc);
                Marshal.StructureToPtr(this, memYeeter, false);
                Marshal.Copy(memYeeter, bytes, 0, spaceToAlloc);
                Marshal.FreeHGlobal(memYeeter);
            }
            catch (MarshalDirectiveException) {
                //one day i will put something here idk
            }

            return bytes;
        }
    }                  


    public static class NetworkParser
    {
        public static byte[] outBuffer = new byte[1024];

        public static Queue<NetCommand> localGameplayCommands = new Queue<NetCommand>();
        public static Queue<NetCommand> networkGameplayCommands = new Queue<NetCommand>();

        public static GameObject[] playerCharacters = new GameObject[2];
        public static void SetPCOrder(GameObject g1, GameObject g2)
        {
            playerCharacters[0] = g1;
            playerCharacters[1] = g2;
        }
        public static short GetPCIndex(GameObject pc)
        {
            return((pc == playerCharacters[0]) ? (short)0:(short)1);
        }

        

        private static NetCommand[] gameplayCommandTypes = new NetCommand[] { 
            new MoveChar(), 
            new BasicAttackChar(), 
            new ChangeStatChar(), 
            new ChangeFlagChar() 
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
            //byte[] tempBuffer = new byte[1024];
            TheWorldsMostUnnecessaryStructure str = new TheWorldsMostUnnecessaryStructure();
            str.Set(1);
            Debug.Log(str.val.ToString());
            Array.Copy(str.ToBytes(), 0, outBuffer, 0, 2);
            TheWorldsMostUnnecessaryStructure y = new TheWorldsMostUnnecessaryStructure();
            y.Set((short)localGameplayCommands.Count);
            Debug.Log("queue size: " + y.val.ToString());
            Buffer.BlockCopy(y.ToBytes(), 0, outBuffer, 2, 2);
            int offset = 4;
            byte[] tBuff = new byte[1];
            for (; localGameplayCommands.Count > 0;
                tBuff = CommandToByteBlock(localGameplayCommands.Dequeue()),
                Array.Copy(tBuff, 0, outBuffer, offset, tBuff.Length), 
                offset += tBuff.Length,
                Debug.Log(localGameplayCommands.Count.ToString())) { }

        }

        public static void ProcessGameplayCommandBlock(byte[] buff)
        {
            byte[] comBuff;


            Debug.Log("I was activated!");
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

        public static void TempProcessor(byte[] buff)
        {

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

            comBlock = new byte[receivedByteCount - 2];
           
            Debug.Log("comblock size: " + comBlock.Length.ToString());
            Array.Copy(rawBytes, 2, comBlock, 0, comBlock.Length);
            if (str.val == 1)
            {
                ProcessGameplayCommandBlock(comBlock);
            }
            else
            {
                Console.WriteLine("BRUH");
                TempProcessor(comBlock);
            }
        }
    }
}
