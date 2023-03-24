using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathSpecial : MonoBehaviour
{
    public GameObject specialButton;

    public GameObject remotePlayer;

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
