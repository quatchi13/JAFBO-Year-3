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

    [StructLayout(LayoutKind.Explicit, Size = 50, CharSet = CharSet.Ansi)]
    public class MoveChar : NetCommand
    {
        [FieldOffset(0)]  short indexMover;

        [FieldOffset(2)]  float posX;
        [FieldOffset(10)] float posY;
        [FieldOffset(18)] float posZ;

        [FieldOffset(26)] float eulerX;
        [FieldOffset(34)] float eulerY;
        [FieldOffset(42)] float eulerZ;

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

    [StructLayout(LayoutKind.Explicit, Size = 28, CharSet = CharSet.Ansi)]
    public class BasicAttackChar : NetCommand
    {
        [FieldOffset(0)]  short indexAttacker;
        [FieldOffset(2)]  short indexReceiver;

        [FieldOffset(4)]  float eulerX;
        [FieldOffset(12)] float eulerY;
        [FieldOffset(20)] float eulerZ;

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

    [StructLayout(LayoutKind.Explicit, Size = 6, CharSet = CharSet.Ansi)]
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

    [StructLayout(LayoutKind.Explicit, Size = 8, CharSet = CharSet.Ansi)]
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

        private static short sizeOfType(int index)
        {
            switch (index) {
                case (0):
                    return 50;
                case (1):
                    return 28;
                case (2):
                    return 6;
                default:
                    return 8;

            }

        }

        private static NetCommand[] gameplayCommandTypes = new NetCommand[] { 
            new MoveChar(), 
            new BasicAttackChar(), 
            new ChangeStatChar(), 
            new ChangeFlagChar() 
        };
        private static NetCommand CommandOfType(int index)
        {
            switch (index)
            {
                case (0):
                    return new MoveChar();
                case (1):
                    return new BasicAttackChar();
                case (2):
                    return new ChangeStatChar();
                default:
                    return new ChangeFlagChar();
            }
        }
        

        public static byte[] ConvertCommandToBytes(NetCommand cmd)
        {
            byte[] bytes = new byte[sizeOfType(GetGameplayCommandType(cmd))];

            try
            {
                //allocate memory
                IntPtr memPointer = Marshal.AllocHGlobal(bytes.Length);
                //point to the command
                Marshal.StructureToPtr(cmd, memPointer, false);
                //copy bytes
                Marshal.Copy(memPointer, bytes, 0, bytes.Length);
                //free memory
                Marshal.FreeHGlobal(memPointer);
            }
            catch (TypeLoadException) {
                //lol you're fucked
                Debug.Log("ERROR CONVERTING COMMAND TO BYTES - SOME DATA HAS BEEN LOST");
                //if you're feeling really extra you can probably try to do some sort of unholy conversion here idk
            }

            return bytes;
        }
        public static MoveChar BytesToCommand(byte[] bytes)
        {
            int objSize = 50;
            bytes = new byte[objSize];
            MoveChar m = new MoveChar();
            try
            {
                IntPtr memPointer = Marshal.AllocHGlobal(objSize);
                Marshal.Copy(bytes, 0, memPointer, objSize);

                m = Marshal.PtrToStructure<MoveChar>(memPointer);

                Marshal.FreeHGlobal(memPointer);
            }
            catch (TypeLoadException)
            {
                Console.WriteLine("oh noes");
            }

            Debug.Log("size: " + objSize);
            return m;
        }

        

        private static short GetGameplayCommandType(NetCommand g)
        {
            short i;
            for (i = 0; i < gameplayCommandTypes.Length - 1 && gameplayCommandTypes[i].GetType() != g.GetType(); i++) { };
            return i;
        }
        public static void SendGameplayQueueToBuffer()
        {
            int off = 0;
            short queueType = 0;
            byte[] shortBuff = new byte[2];

            shortBuff = BitConverter.GetBytes(queueType);

            Buffer.BlockCopy(shortBuff, 0, outBuffer, off, 2);
            off += 2;
            shortBuff = BitConverter.GetBytes(localGameplayCommands.Count);
            Buffer.BlockCopy(shortBuff, 0, outBuffer, off, 2);
            off += 2;
            short tShort;

            int byteSize = 0;
            int queueLength = localGameplayCommands.Count;
            for (; localGameplayCommands.Count != 0;
                tShort = (GetGameplayCommandType(localGameplayCommands.Peek())),
                shortBuff = BitConverter.GetBytes(tShort),
                Buffer.BlockCopy(shortBuff, 0, outBuffer, off, 2),
                off+=2,
                byteSize = Marshal.SizeOf(localGameplayCommands.Peek()), 
                Buffer.BlockCopy(ConvertCommandToBytes(localGameplayCommands.Dequeue()), 0, outBuffer, off, byteSize),
                
                off += byteSize) { }
            
        }

        public static void ProcessGameplayCommandQueue(byte[] inBuffer)
        {
            int offset = 0;
            byte[] shortReader = new byte[2];
            short commType;
            byte[] commandBytes;
            int sizeOfCom;
            short sizeOfQueue;
            
            Buffer.BlockCopy(inBuffer, offset, shortReader, 0, 2);
            offset += 2;
            sizeOfQueue = BitConverter.ToInt16(shortReader);

            for (; networkGameplayCommands.Count < sizeOfQueue;
                Buffer.BlockCopy(inBuffer, offset, shortReader, 0, 2),
                offset += 2,

                commType = BitConverter.ToInt16(shortReader),
                Debug.Log(commType),
                commandBytes = new byte[commType], 
                Buffer.BlockCopy(inBuffer, offset, commandBytes, 0, commandBytes.Length), 
                offset += commType, 
                networkGameplayCommands.Enqueue(BytesToCommand(commandBytes)),
                Debug.Log("yes"))
            {}

        }


        public static void ProcessUnparsedByteBuffer(byte[] rawBytes)
        {
            byte[] shortHold = new byte[2];
            byte[] commands = new byte[rawBytes.Length - 2];

            Buffer.BlockCopy(rawBytes, 0, shortHold, 0, 2);
            Buffer.BlockCopy(rawBytes, 2, commands, 0, commands.Length);

            switch (BitConverter.ToInt16(shortHold))
            {
                default:
                    ProcessGameplayCommandQueue(commands);
                    break;
            }
        }


    }
}
