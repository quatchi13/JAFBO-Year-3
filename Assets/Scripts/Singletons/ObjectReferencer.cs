using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectReferencer : MonoBehaviour
{
    public static ObjectReferencer instance;

    [SerializeField]
    GameObject player;

    public GameObject GetPlayer()
    {
        return player;
    }
}
