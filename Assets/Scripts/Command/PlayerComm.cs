using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using System.Runtime.InteropServices;



namespace JAFnetwork
{
    
    public interface NetCommand
    {
        void Execute();
        void Inverse();
    }

    [StructLayout(LayoutKind.Explicit, Size = 26)]
    public class MoveChar : NetCommand
    {
        [FieldOffset(0)]  short indexMover;

        [FieldOffset(2)]  float posX;
        [FieldOffset(6)] float posY;
        [FieldOffset(10)] float posZ;

        [FieldOffset(14)] float eulerX;
        [FieldOffset(18)] float eulerY;
        [FieldOffset(22)] float eulerZ;

        public void Setup(int ind, Vector3 pos, Vector3 euler)
        {
            indexMover = (short)ind;

            posX = pos.x;
            posY = pos.y;
            posZ = pos.z;

            eulerX = euler.x;
            eulerY = euler.y;
            eulerZ = euler.z;
        }

        public void Execute()
        {
            Debug.Log("WOAH");
            NetworkParser.playerCharacters[indexMover].transform.eulerAngles = new Vector3(eulerX, eulerY, eulerZ);
            NetworkParser.playerCharacters[indexMover].transform.position += new Vector3(posX, posY, posZ);
        }

        public void Inverse()
        {
            Debug.Log("SICK");
            NetworkParser.playerCharacters[1].transform.eulerAngles = new Vector3(-eulerX, -eulerY, -eulerZ);
            NetworkParser.playerCharacters[1].transform.position = NetworkParser.playerCharacters[1].transform.position + new Vector3(-posX, -posY, -posZ);
        }
    }

    [StructLayout(LayoutKind.Explicit, Size = 16)]
    public class BasicAttackChar : NetCommand
    {
        [FieldOffset(0)]  short indexAttacker;
        [FieldOffset(2)]  short indexReceiver;

        [FieldOffset(4)]  float eulerX;
        [FieldOffset(8)] float eulerY;
        [FieldOffset(12)] float eulerZ;

        public void Setup(int iA, int iR, Vector3 rot)
        {
            indexAttacker = (short)iA;
            indexReceiver = (short)iR;

            eulerX = rot.x;
            eulerY = rot.y;
            eulerZ = rot.z;
        }

        public void Execute()
        {
            NetworkParser.playerCharacters[indexAttacker].transform.eulerAngles = new Vector3(eulerX, eulerY, eulerZ);
            //player.GetComponent<Animator>().SetBool("Attack", true);
            NetworkParser.playerCharacters[indexReceiver].GetComponent<HealthSystem>().Damage(NetworkParser.playerCharacters[indexAttacker].GetComponent<Attacking>().currentDamage);
            Debug.Log("slap");
        }

        public void Inverse() { }
    }

    [StructLayout(LayoutKind.Explicit, Size = 6)]
    public class ChangeStatChar : NetCommand
    {
        [FieldOffset(0)]short indexTarget;
        [FieldOffset(2)]short indexStat;
        [FieldOffset(4)]short addValue;

        public void Setup(int targIn, int statIn, int val)
        {
            indexTarget = (short)targIn;
            indexStat   = (short)statIn;
            addValue    = (short)val;
        }

        public void Execute()
        {
            //yeah
        }

        public void Inverse() { }
    }

    [StructLayout(LayoutKind.Explicit, Size = 8)]
    public class ChangeFlagChar : NetCommand
    {
        [FieldOffset(0)] bool  flagValue;
        [FieldOffset(4)] short indexChar;
        [FieldOffset(6)] short indexFlag;

        public void Setup(int charInd, int flagInd, bool fV)
        {
            indexChar = (short)charInd;
            indexFlag = (short)flagInd;
            flagValue = fV;
        }

        public void Execute()
        {
            //do ya thing
        }

        public void Inverse() { }
    }

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
        

        

        
        public static void SendGameplayQueueToBuffer()
        {
            outBuffer = new byte[654];
            Buffer.BlockCopy(BitConverter.GetBytes((short)0), 0, outBuffer, 0, 2);
            NetInstructBlock nob = new NetInstructBlock();
            nob.Init();
            for(; localGameplayCommands.Count > 0; nob.AddToList(localGameplayCommands.Peek()), localGameplayCommands.Dequeue()) { }
            Buffer.BlockCopy(nob.ConvertToBytes(), 0, outBuffer, 2, 652); 
        }

        public static void ProcessGameplayCommandBlock(byte[] buff)
        {
            Debug.Log("I was activated!");
            NetInstructBlock nob = new NetInstructBlock();

            int oSize = buff.Length;

            IntPtr memAlloc = Marshal.AllocHGlobal(oSize);
            Marshal.Copy(buff, 0, memAlloc, oSize);
            nob = Marshal.PtrToStructure<NetInstructBlock>(memAlloc);
            Marshal.FreeHGlobal(memAlloc);

            nob.CopyActiveToQueue(networkGameplayCommands);
        }

        public static void TempProcessor(byte[] buff)
        {

        }

        public static void ParseCommandBlock(byte[] rawBytes)
        {
            int receivedByteCount = rawBytes.Length;
            Debug.Log("Received Bytes: " + receivedByteCount.ToString());
            int offset = 0;
            short sBuff;
            byte[] comBlock;

            short blockType = BitConverter.ToInt16(rawBytes, 0);

            for (; offset < (receivedByteCount - 1);)
            {
                sBuff = BitConverter.ToInt16(rawBytes, offset);
                comBlock = new byte[652];
                offset += 2;
                Debug.Log("comblock size: " + comBlock.Length.ToString());
                Buffer.BlockCopy(rawBytes, offset, comBlock, 0, comBlock.Length);
                if (comBlock.Length == 652)
                {
                    ProcessGameplayCommandBlock(comBlock);
                }
                else
                {
                    TempProcessor(comBlock);
                }
                offset += comBlock.Length;
            }
        }
    }
}
