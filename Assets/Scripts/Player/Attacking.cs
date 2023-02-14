using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerWrapper;

public class Attacking : MonoBehaviour
{
   [SerializeField]
    private bool canAttack;
    [SerializeField]
    private bool myTurn;

    private bool attackUp, attackDown,attackLeft, attackRight;


    [SerializeField]
    private int maxDamage;

    [SerializeField]
    private int currentDamage;


    public GameObject[] currentTargets;

    
    void Start() 
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {

        if(myTurn)
        {

        
        if(canAttack)
        {
            if (Input.GetKeyDown(KeyCode.D) && attackRight)
            {
                ResetValidMoves();
                PlayerUpdater.AttackPlayer(gameObject, currentTargets[3], new Vector3 (0,0,0), currentDamage);
            }

            if (Input.GetKeyDown(KeyCode.S) && attackDown)
            {
                ResetValidMoves();
                PlayerUpdater.AttackPlayer(gameObject, currentTargets[1], new Vector3 (0,90,0), currentDamage);
    
                
            }

            if (Input.GetKeyDown(KeyCode.A) && attackLeft)
            {
                ResetValidMoves();
                PlayerUpdater.AttackPlayer(gameObject, currentTargets[2], new Vector3 (0,180,0), currentDamage);
          
                
            }

            if (Input.GetKeyDown(KeyCode.W) && attackUp)
            {
                ResetValidMoves();
                PlayerUpdater.AttackPlayer(gameObject, currentTargets[0], new Vector3 (0,270,0), currentDamage);
                
                
            }
        }   
        }
    }

    

    public void SetCanAttackState(bool state)
    {
        canAttack = state;
    }

    public bool GetCanAttackState(bool state)
    {
        return canAttack;
    }

    public void SetCanAttackInDir(int direction) 
    {
        switch (direction)
        {
            case 0:
                attackUp = true;
           
                break;

            case 1:
                attackDown = true;
              
                break;

            case 2:
                attackLeft = true;
               
                break;

            case 3:
                attackRight = true;
               
                break;

        }
    }

    public void StoreTargetUp(GameObject target)
    {
        currentTargets[0] = target;
    }

    public void StoreTargetDown(GameObject target)
    {
        currentTargets[1] = target;
    }

    public void StoreTargetLeft(GameObject target)
    {
        currentTargets[2] = target;
    }
    public void StoreTargetRight(GameObject target)
    {
        currentTargets[3] = target;
        
    }

    public void ResetValidMoves()
    {
        attackDown = false;
        attackLeft = false;
        attackRight = false;
        attackUp = false;
    }

    public void ChangeTurn()
    {
        myTurn = !myTurn;
        //update player values
    }
}
