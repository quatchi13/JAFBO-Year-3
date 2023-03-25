using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JAFnetwork;

public class Player : MonoBehaviour
{
    //parent class for all active characters (players AND enemies)

    //create child class for player controlled things
    //create child class for all enemies

    //create more children for individual characters (should only have attributes with getter setters!)


    public GameObject make;

    void Awake() {

    Instantiate(make, new Vector3(1f,0.75f,-25f), Quaternion.identity);
    }
}
