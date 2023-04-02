using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JAFnetwork;

public class FollowPosition : MonoBehaviour
{
    [SerializeField]
    private Transform target;
    public static Vector3 rot = Vector3.zero;

    void Start()
    {
       //target = NetworkParser.playerCharacters[0].transform;
       target = GameObject.FindWithTag("Player").transform;
    }
    void Update()
    {
        gameObject.transform.position = target.position;
        gameObject.transform.eulerAngles = rot;
    }
    public static void SetRot()
    {
        Debug.Log("Flipped the triggers");
        rot = new Vector3(180, 0, 180);
    }
}
