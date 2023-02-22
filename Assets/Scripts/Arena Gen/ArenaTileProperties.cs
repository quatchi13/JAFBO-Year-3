using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JAFprocedural;


public class ArenaTileProperties : MonoBehaviour
{
    public ArenaTile properties;
    void Awake()
    {
        properties = new BasicIndestructableTile(ArenaTileType.BOUNDS);
    }

    // Update is called once per frame
    void Update()
    {

    }

}
