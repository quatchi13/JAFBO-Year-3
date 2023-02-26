using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialAttack : MonoBehaviour
{
    public GameObject remotePlayer;

    public GameObject specialButton;

    public int damage = 5;

    private void Update() 
    {
        if(ActionPointsManager.instance.actions > 4)
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
        remotePlayer.GetComponent<HealthManager>().AlterHealth(-damage);
    }
}
