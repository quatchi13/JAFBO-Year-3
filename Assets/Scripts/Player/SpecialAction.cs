using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialAction : MonoBehaviour
{
    public GameObject[] characters;

    private int selection = 0;

    [SerializeField]
    private bool specialAttacking=false;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.RightShift))
        {
            if(!specialAttacking)
            {
                SpecialAttackState(true);
                characters[selection].GetComponent<MeshRenderer>().material.color = new Vector4(0,1,0,1);

            }
            else
            {
                PerformSpecialAttack();
            }
            
        }


        if(specialAttacking)
        {
             if(Input.GetKeyDown(KeyCode.A))
            {  
                characters[selection].GetComponent<MeshRenderer>().material.color = new Vector4(1,1,1,1);
                selection--;
                if(selection < 0)
                {
                    selection = characters.Length-1;   
                }
                characters[selection].GetComponent<MeshRenderer>().material.color = new Vector4(0,1,0,1);
            }

            else if(Input.GetKeyDown(KeyCode.D))
            {
                characters[selection].GetComponent<MeshRenderer>().material.color = new Vector4(1,1,1,1);
                selection++;
                if(selection >= characters.Length)
                {
                    selection = 0;   
                }
                characters[selection].GetComponent<MeshRenderer>().material.color = new Vector4(0,1,0,1);
            }

        }
       

    }

    public void SpecialAttackState(bool state)
    {
        specialAttacking = state;
    }

    public void PerformSpecialAttack()
    {
        characters[selection].GetComponent<MeshRenderer>().material.color = new Vector4(1,0,1,1);
    }
}
