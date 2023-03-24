using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeSpecial : MonoBehaviour
{
    public GameObject specialButton;

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
        gameObject.GetComponent<StatHolder>().Heal(32);
    }
}
