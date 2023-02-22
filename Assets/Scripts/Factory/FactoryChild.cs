using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Reflection;
using System;
using TMPro;

public class FactoryChild : FactoryParent
{
    
    public GameObject boundaries; //0
    public GameObject ground;     //1
    public GameObject hRail;      //2
    public GameObject vRail;      //3
    public GameObject swRail;     //4
    public GameObject neRail;     //5
    public GameObject nwRail;     //6
    public GameObject seRail;     //7
    public GameObject goldmine;   //8
    public GameObject tree;       //9
    public GameObject cactus;     //10
    public GameObject barrel;     //11
    public GameObject b1Cliff;    //12
    public GameObject b2Cliff;    //13
    public GameObject b6Cliff;    //14
    public GameObject f1Cliff;    //15
    public GameObject f2Cliff;    //16
    public GameObject f3Cliff;    //17
    public GameObject f4Cliff;    //18
    public GameObject f5Cliff;    //19
    public GameObject f6Cliff;    //20
    public GameObject rock;
    public GameObject water;
    public List<FactoryParent> tiles;
    public List<GameObject> tileGOs;

    public override string Name { get { return "FactoryChild"; } }

    public override GameObject Create(GameObject prefab, Vector3 position, Quaternion rotation)
    {
        GameObject tile = Instantiate(prefab, position, rotation);
        return tile;
    }

    // Start is called before the first frame update
    void Start()
    {
        var tileTypes = Assembly.GetAssembly(typeof(FactoryParent)).GetTypes().Where(myType => myType.IsClass && !myType.IsAbstract && myType.IsSubclassOf(typeof(FactoryParent)));

        tiles = new List<FactoryParent>();

        foreach(var type in tileTypes)
        {
            var tempType = Activator.CreateInstance(type) as FactoryParent;
            tiles.Add(tempType);
            Debug.Log(tiles.Last().Name);
        }

        tileGOs = new List<GameObject>
        {
            boundaries,
            ground,
            hRail,
            vRail,
            swRail,
            neRail,
            nwRail,
            seRail,
            goldmine,
            tree,
            cactus,
            barrel,
            b1Cliff,
            b2Cliff,
            b6Cliff,
            f1Cliff,
            f2Cliff,
            f3Cliff,
            f4Cliff,
            f5Cliff,
            f6Cliff,
            rock,
            water
        };
    }


    public FactoryParent GetTile(string tileType)
    {
        foreach(FactoryParent tile in tiles)
        {
            if(tile.Name == tileType)
            {
                //throwing warnings due to 'new' keyword use in monobehaviour, have yet to solve
                var target = Activator.CreateInstance(tile.GetType()) as FactoryParent;

                return target;
            }
        }

        return null;
    }
}
