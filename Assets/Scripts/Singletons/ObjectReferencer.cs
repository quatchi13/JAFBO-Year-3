using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectReferencer : MonoBehaviour
{
    public static ObjectReferencer instance;

    [SerializeField]
    GameObject player;

<<<<<<< HEAD
=======
    private void Start()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

>>>>>>> StateFarm
    public GameObject GetPlayer()
    {
        return player;
    }
}
