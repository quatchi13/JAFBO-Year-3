using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlorenceSpecials : MonoBehaviour
{
    public GameObject specialButton;
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
        //florence special  anim
        animator.Play("Special 1");
        gameObject.GetComponent<StatHolder>().SetCloakedState(true);
    }
}
