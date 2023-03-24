﻿using System;
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
            //Debug.Log("WOAH");
            NetworkParser.playerCharacters[indexMover].transform.eulerAngles = new Vector3(eulerX, eulerY, eulerZ);
            NetworkParser.playerCharacters[indexMover].transform.position += new Vector3(posX, posY, posZ);
        }

        public void Inverse()
        {
            NetworkParser.playerCharacters[1].transform.eulerAngles = new Vector3(-eulerX, -eulerY, -eulerZ);
            NetworkParser.playerCharacters[1].transform.position = NetworkParser.playerCharacters[1].transform.position + new Vector3(-posX, -posY, -posZ);
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
            NetworkParser.playerCharacters[1].transform.eulerAngles = new Vector3(-eulerX, -eulerY, -eulerZ);
            //player.GetComponent<Animator>().SetBool("Attack", true);
            NetworkParser.playerCharacters[0].GetComponent<StatHolder>().InflictDamage(NetworkParser.playerCharacters[1].GetComponent<StatHolder>().stats[1]);
            Debug.Log("slap");
        }

        public short ComIndex() { return 1; }
    }

    [StructLayout(LayoutKind.Explicit, Size = 6)]
    public class ChangeStatChar : NetCommand
    {
        [FieldOffset(0)] short indexTarget;
        [FieldOffset(2)] short indexStat;
        [FieldOffset(4)] short addValue;

        public void Setup(int targIn, int statIn, int val)
        {
            indexTarget = (short)targIn;
            indexStat = (short)statIn;
            addValue = (short)val;
        }

        public void Execute()
        {
            //yeah
        }

        public void Inverse() { }

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
            //do ya thing
        }

        public void Inverse() { }

        public short ComIndex() { return 3; }
    }

    public interface ServerCommand
    {
        void Execute();
        short ComIndex();
    }

    [StructLayout(LayoutKind.Explicit, Size = 8)]
    public class ArenaSetupCommand : ServerCommand
    {
        [FieldOffset(0)] public int rngSeed;
        [FieldOffset(4)] public short arenaType;
        [FieldOffset(6)] public short playerLocRotation;
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

            //if we have multiple players we will do a thing trademark where we determine where 
            //all of the players actually get positioned on the map
        }

        public short ComIndex()
        {
            return 0;
        }
    }

    [StructLayout(LayoutKind.Explicit, Size = 8)]
    public class CharacterOrderCommand : ServerCommand
    {
        [FieldOffset(0)] short ind1;
        [FieldOffset(2)] short char1;
        [FieldOffset(4)] short ind2;
        [FieldOffset(6)] short char2;

        public void Setup(short a = 0, short b = 0, short c = 1, short d = 1)
        {
            ind1 = a;
            ind2 = b;
            char1 = c;
            char2 = d;
        }

        public void Execute()
        {
            NetworkParser.playerCharacters[0] = PC_Manager.PCList[ind1];
            NetworkParser.playerCharacters[1] = PC_Manager.PCList[ind2];
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
            //seethe?
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