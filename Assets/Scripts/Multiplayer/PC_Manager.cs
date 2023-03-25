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
/// This needs to be placed on some sort of game manager
/// we will likely have some of this stuff (the socket) carry over from setup
/// </summary>


public class PC_Manager : MonoBehaviour
{
    //reference to the arena manager
    public GameObject ArenaManagerRef;

    //player and remote player game objects
    public List<GameObject> MasterList = new List<GameObject> { };

    //LOCAL CHARACTER PREFABS - CONTAIN ALL SCRIPTS
    public List<GameObject> localPlayerPrefabs = new List<GameObject> { };

    //REMOTE CHARACTER PREFABS - CONTAIN MODEL AND STATS ONLY
    public List<GameObject> remotePlayerPrefabs = new List<GameObject> { };

    //don't touch these
    [System.NonSerialized] public static GameObject[] staticLocalPrefabs;
    [System.NonSerialized] public static GameObject[] staticRemotePrefabs;
    [System.NonSerialized] public static GameObject[] PCList;

    void Start()
    {
        MasterList[0] = (GameObject.FindWithTag("Player"));
        MasterList[1] = (GameObject.FindWithTag("OtherPlayer"));

        PCList = new GameObject[MasterList.Count];
        for (int i = 0; i < PCList.Length; PCList[i] = MasterList[i], i++) ;

        staticLocalPrefabs = new GameObject[localPlayerPrefabs.Count];
        for (int i = 0; i < staticLocalPrefabs.Length; staticLocalPrefabs[i] = localPlayerPrefabs[i], i++) ;

        staticRemotePrefabs = new GameObject[remotePlayerPrefabs.Count];
        for (int i = 0; i < staticRemotePrefabs.Length; staticRemotePrefabs[i] = remotePlayerPrefabs[i], i++) ;

        NetworkParser.SetPCOrder(PCList[0], PCList[1]);
        NetworkParser.aGenRef = ArenaManagerRef;
    }

    // Update is called once per frame
    void Update()
    {


    }


    public void ReorderPlayerCharacters(List<int> order)
    {
        for (int i = 0; i < order.Count; i++)
        {
            PCList[i] = MasterList[order[i]];
        }
    }
}
