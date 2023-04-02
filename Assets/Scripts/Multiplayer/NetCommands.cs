using System;
using System.Runtime.InteropServices;
using UnityEngine;
using JAFprocedural;
/// <summary>
/// we don't need to attach this anywhere, it's backend code
/// in a non-dummy visualizer experience, inverse should be applied to the original gameobjects
/// also need to actually fill out stuff for stats, will require overhauls we will work on together
/// </summary>

namespace JAFnetwork
{
   
    public interface NetCommand
    {
        void Execute();
        void Inverse();
        short ComIndex();
    }

    [StructLayout(LayoutKind.Explicit, Size = 26)]
    public class MoveChar : NetCommand
    {
        [FieldOffset(0)] short indexMover;

        [FieldOffset(2)] float posX;
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
            Debug.Log("HAOW");

            NetworkParser.playerCharacters[indexMover].transform.eulerAngles = new Vector3(-eulerX, -eulerY, -eulerZ);
            NetworkParser.playerCharacters[indexMover].transform.position += new Vector3(posX, posY, posZ);
        }

        public short ComIndex() { return 0; }
    }

    [StructLayout(LayoutKind.Explicit, Size = 16)]
    public class BasicAttackChar : NetCommand
    {
        [FieldOffset(0)] short indexAttacker;
        [FieldOffset(2)] short indexReceiver;

        [FieldOffset(4)] float eulerX;
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
            NetworkParser.playerCharacters[indexReceiver].GetComponent<StatHolder>().InflictDamage(NetworkParser.playerCharacters[indexAttacker].GetComponent<StatHolder>().stats[1]);
            Debug.Log("slap");
        }

        public void Inverse() {
            NetworkParser.playerCharacters[indexAttacker].transform.eulerAngles = new Vector3(-eulerX, -eulerY, -eulerZ);
            //player.GetComponent<Animator>().SetBool("Attack", true);
            NetworkParser.playerCharacters[indexReceiver].GetComponent<StatHolder>().InflictDamage(NetworkParser.playerCharacters[indexAttacker].GetComponent<StatHolder>().stats[1]);
            Debug.Log("slap");
        }

        public short ComIndex() { return 1; }
    }

    [StructLayout(LayoutKind.Explicit, Size = 6)]
    public class ChangeStatChar : NetCommand
    {
        [FieldOffset(0)] short indexTarget;
        [FieldOffset(2)] short indexStat;
        [FieldOffset(4)] short newValue;

        public void Setup(int targIn, int statIn, int val)
        {
            indexTarget = (short)targIn;
            indexStat = (short)statIn;
            newValue = (short)val;
        }

        public void Execute()
        {
            NetworkParser.playerCharacters[indexTarget].GetComponent<StatHolder>().stats[indexStat] = newValue;
        }

        public void Inverse() {
            NetworkParser.playerCharacters[indexTarget].GetComponent<StatHolder>().stats[indexStat] = newValue;
        }

        public short ComIndex() { return 2; }
    }

    [StructLayout(LayoutKind.Explicit, Size = 8)]
    public class ChangeFlagChar : NetCommand
    {
        [FieldOffset(0)] bool flagValue;
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
            NetworkParser.playerCharacters[indexChar].GetComponent<StatHolder>().statuses[indexFlag] = flagValue;
        }

        public void Inverse() {
            NetworkParser.playerCharacters[indexChar].GetComponent<StatHolder>().statuses[indexFlag] = flagValue;
        }

        public short ComIndex() { return 3; }
    }
    [StructLayout(LayoutKind.Explicit, Size = 2)]
    public class EliminateChar : NetCommand
    {
        [FieldOffset(0)] public short elimIndex;
        
        public void Setup(int eIndex = 0)
        {
            elimIndex = (short)eIndex;
        }
        
        public void Execute()
        {
            //do like. a death animation here or something. idk. 

            NetworkParser.playerCharacters[elimIndex].SetActive(false);
        }
        public void Inverse()
        {
            //do exactly what you did above

            NetworkParser.playerCharacters[elimIndex].SetActive(false);
        }
        public short ComIndex()
        {
            return 4;
        }
    }


    public interface ServerCommand
    {
        void Execute();
        short ComIndex();
    }

    [StructLayout(LayoutKind.Explicit, Size = 6)]
    public class ArenaSetupCommand : ServerCommand
    {
        [FieldOffset(0)] public int rngSeed;
        [FieldOffset(4)] public short arenaType;
        public void Execute()
        {
            RNG.SetSeed(rngSeed);
            switch (arenaType)
            {
                case (1):
                    NetworkParser.aGenRef.GetComponent<MakeArenaArray>().SetToWestern();
                    break;
                default:
                    NetworkParser.aGenRef.GetComponent<MakeArenaArray>().SetToDefault();
                    break;
            }

            NetworkParser.aGenRef.GetComponent<MakeArenaArray>().GenerateNewTerrain();

        }

        public short ComIndex()
        {
            return 0;
        }
    }

    [StructLayout(LayoutKind.Explicit, Size = 6)]
    public class CharSelectionsAndPlayerIndex : ServerCommand
    {
        [FieldOffset(0)] public short playerIndex;
        [FieldOffset(2)] public short localC;
        [FieldOffset(4)] public short remoteC;

        public void Execute()
        {
            GameObject_Manager.localPlayerIndex = playerIndex;
            Follow.isP2= (playerIndex == 1);
            GameObject_Manager.selectedCharacters = new short[2] { localC, remoteC };
            Debug.Log("character selection indexes: "+ GameObject_Manager.selectedCharacters[0].ToString() +", " + GameObject_Manager.selectedCharacters[1].ToString());
        }

        public short ComIndex()
        {
            return 1;
        }
    }


    [StructLayout(LayoutKind.Explicit, Size = 2)]
    public class ReadyCommand : ServerCommand
    {
        [FieldOffset(0)] public short selectedCharacter;

        public void Execute()
        {
            //cope
        }

        public short ComIndex()
        {
            return 2;
        }
    }

    [StructLayout(LayoutKind.Explicit, Size = 2)]
    public class StartTurnCommand : ServerCommand
    {
        public void Execute()
        {
            NetworkParser.playerCharacters[GameObject_Manager.localPlayerIndex].GetComponent<ActionPointsManager>().StartTurn();
        }

        public short ComIndex()
        {
            return 3;
        }
    }

    [StructLayout(LayoutKind.Explicit, Size = 2)]
    public class StartGameCommand : ServerCommand
    {
        public void Execute()
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("Game");
        }

        public short ComIndex()
        {
            return 4;
        }
    }

    [StructLayout(LayoutKind.Explicit, Size = 4)]
    public class SetCharacterOrder : ServerCommand
    {
        [FieldOffset(0)] public short index1;
        [FieldOffset(2)] public short index2;

        public void Execute()
        {
            NetworkParser.playerCharacters[0] = GameObject_Manager.PCList[index1];
            NetworkParser.playerCharacters[1] = GameObject_Manager.PCList[index2];
        }

        public short ComIndex()
        {
            return 5;
        }
    }

    [StructLayout(LayoutKind.Explicit, Size = 2)]
    public class MarkPlayerAsEliminated : ServerCommand
    {
        [FieldOffset(0)] public short elimIndex;
        public void Setup(int e)
        {
            elimIndex = (short)e;
        }

        public void Execute()
        {
            //cry yourself to sleep
        }

        public short ComIndex()
        {
            return 6;
        }
    }


    [StructLayout(LayoutKind.Explicit, Size = 2)]
    public class TheWorldsMostUnnecessaryStructure
    {
        [FieldOffset(0)] public short val;

        public void Set(short x)
        {
            val = x;
        }

        public byte[] ToBytes()
        {
            int obSize = Marshal.SizeOf(this);

            byte[] bytes = new byte[obSize];
            IntPtr memPoint = Marshal.AllocHGlobal(obSize);
            Marshal.StructureToPtr(this, memPoint, false);
            Marshal.Copy(memPoint, bytes, 0, obSize);
            Marshal.FreeHGlobal(memPoint);

            return bytes;
        }
    }


    public static class NetComBuilders
    {
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

        public static byte[] ServComToByteBlock(ServerCommand s)
        {
            int comSize = Marshal.SizeOf(s);
            byte[] byteBlock = new byte[comSize + 2];

            TheWorldsMostUnnecessaryStructure ind = new TheWorldsMostUnnecessaryStructure();
            ind.Set(s.ComIndex());
            byte[] iBytes = ind.ToBytes();
            Buffer.BlockCopy(iBytes, 0, byteBlock, 0, 2);

            IntPtr memPoint = Marshal.AllocHGlobal(comSize);
            Marshal.StructureToPtr(s, memPoint, false);
            Marshal.Copy(memPoint, byteBlock, 2, comSize);
            Marshal.FreeHGlobal(memPoint);

            return byteBlock;
        }

        public static NetCommand BytesToNetCom(byte[] bytes, NetCommand nCom)
        {
            int obSize = Marshal.SizeOf(nCom);
            IntPtr memPointer = Marshal.AllocHGlobal(obSize);
            Marshal.Copy(bytes, 0, memPointer, obSize);
            Marshal.PtrToStructure(memPointer, nCom);
            Marshal.FreeHGlobal(memPointer);

            return nCom;
        }

        public static ServerCommand BytesToServerCom(byte[] bytes, ServerCommand sCom)
        {
            int obSize = Marshal.SizeOf(sCom);
            IntPtr memPointer = Marshal.AllocHGlobal(obSize);
            Marshal.Copy(bytes, 0, memPointer, obSize);
            Marshal.PtrToStructure(memPointer, sCom);
            Marshal.FreeHGlobal(memPointer);

            return sCom;
        }

        public static TheWorldsMostUnnecessaryStructure BytesToShortStuff(byte[] bytes)
        {
            TheWorldsMostUnnecessaryStructure str = new TheWorldsMostUnnecessaryStructure();
            int obSize = Marshal.SizeOf(str);

            IntPtr memPointer = Marshal.AllocHGlobal(obSize);
            Marshal.Copy(bytes, 0, memPointer, obSize);
            Marshal.PtrToStructure(memPointer, str);
            Marshal.FreeHGlobal(memPointer);

            return str;
        }
    }




}
