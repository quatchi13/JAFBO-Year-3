using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialAction : MonoBehaviour
{
    public GameObject target;

    
    //delete this 
    private int selection = 0;
    

    [SerializeField] public bool specialAttacking = false;


    // Update is called once per frame
    void Update()
    {
        
    }

    public void ActivateSpecial()
    {
        if (specialAttacking) PerformSpecialAction();
    }

    public void SpecialActionA()
    {
        selection = 1;
        Debug.Log(selection);
    }

    public void SpecialActionB()
    {
        selection = 2;
        Debug.Log(selection);
    }


    //kill this
    void PerformSpecialAction()
    {
        Debug.Log("TARGET JUST GOT HIT BY " + ((selection == 1)? "a truck (bAOWW)" : "a smooth criminal"));

    }
}
