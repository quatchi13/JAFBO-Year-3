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

    public GameObject[] chars = new GameObject[3];

    void Awake() {
        Vector3 startPos = (GameObject_Manager.localPlayerIndex == 1) ? new Vector3(28f, 0.75f, -4f) : new Vector3(1f, 0.75f, -25f);
        Instantiate(chars[GameObject_Manager.selectedCharacters[0]], startPos, Quaternion.identity);
    }
}
