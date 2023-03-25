using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JAFnetwork;

public class DeathSpecial : MonoBehaviour
{
    public GameObject specialButton;

    public GameObject remotePlayer;

    void Start()
    {
        //Set each of the buttons to a reference of the Ui button that already exists. We need to do this because it is a prefab being instantiated and cant store references. Therefore Awake
        remotePlayer = NetworkParser.playerCharacters[1];

        specialButton = GameObject.Find("SpecialAttackButton");
    }

    private void Update() 
    {
        if(gameObject.GetComponent<ActionPointsManager>().actions > 4)
        {
            specialButton.SetActive(true);
        }    
        else
        {
            specialButton.SetActive(false);
        }
    }

    public void Special()
    {
        remotePlayer.GetComponent<StatHolder>().SetMarkedState(true);
    }
}
