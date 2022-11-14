using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Reflection;
using System;
using TMPro;

public class FactoryChild : FactoryParent
{
    public GameObject boundaries;
    public GameObject ground;
    public GameObject water;
    public GameObject rock;
    public GameObject tree;

    List<FactoryParent> tiles;

    public override string Name { get { return "FactoryChild"; } }

    public override GameObject Create(GameObject prefab, Vector3 position, Quaternion rotation)
    {
        GameObject tile = Instantiate(prefab, position, rotation);
        return tile;
    }

    private void Awake()
    {
        var tileTypes = Assembly.GetAssembly(typeof(FactoryParent)).GetTypes().Where(myType => myType.IsClass && !myType.IsAbstract && myType.IsSubclassOf(typeof(FactoryParent)));

        tiles = new List<FactoryParent>();

        foreach(var type in tileTypes)
        {
            var tempType = Activator.CreateInstance(type) as FactoryParent;
            tiles.Add(tempType);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
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
