using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JAFnetwork;

public class FollowPosition : MonoBehaviour
{
    [SerializeField]
    private Transform target;


    void Start()
    {
       //target = NetworkParser.playerCharacters[0].transform;
       target = GameObject.FindWithTag("Player").transform;
    }
    void Update()
    {
        gameObject.transform.position = target.position;
    }
}
