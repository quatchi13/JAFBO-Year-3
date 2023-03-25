using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlorenceSpecials : MonoBehaviour
{
    public GameObject specialButton;

    void Awake()
    {
        //Set each of the buttons to a reference of the Ui button that already exists. We need to do this because it is a prefab being instantiated and cant store references. Therefore Awake

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
        gameObject.GetComponent<StatHolder>().SetCloakedState(true);
    }
}
