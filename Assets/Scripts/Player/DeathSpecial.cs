using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathSpecial : MonoBehaviour
{
    public GameObject specialButton;

    public GameObject remotePlayer;
    private Animator animator;

    void Awake() 
    {
        animator = GetComponent<Animator>();
        
    }

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
        //death special anim
        animator.Play("Special 1");
        remotePlayer.GetComponent<StatHolder>().SetMarkedState(true);
    }
}
