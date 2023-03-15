using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerWrapper;
using JAFnetwork;

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
    public int currentDamage;

    public GameObject a;

    private GameObject[] currentTargets = new GameObject[4];

    
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
                        //ResetValidMoves();
                    BasicAttackChar atk = new BasicAttackChar();
                    atk.Setup(NetworkParser.GetPCIndex(gameObject), NetworkParser.GetPCIndex(currentTargets[3]), new Vector3(0, 0, 0));
                    atk.Execute();
                    NetworkParser.localGameplayCommands.Enqueue(atk);
                }

                if (Input.GetKeyDown(KeyCode.S) && attackDown)
                {
                    BasicAttackChar atk = new BasicAttackChar();
                    atk.Setup(NetworkParser.GetPCIndex(gameObject), NetworkParser.GetPCIndex(currentTargets[1]), new Vector3(0, 90, 0));

                    atk.Execute();
                    NetworkParser.localGameplayCommands.Enqueue(atk);

                }

                if (Input.GetKeyDown(KeyCode.A) && attackLeft)
                {
                    BasicAttackChar atk = new BasicAttackChar();
                    atk.Setup(NetworkParser.GetPCIndex(gameObject), NetworkParser.GetPCIndex(currentTargets[2]), new Vector3(0, 180, 0));

                    atk.Execute();
                    NetworkParser.localGameplayCommands.Enqueue(atk);
          
                
                }

                if (Input.GetKeyDown(KeyCode.W) && attackUp)
                {
                    BasicAttackChar atk = new BasicAttackChar();
                    atk.Setup(NetworkParser.GetPCIndex(gameObject), NetworkParser.GetPCIndex(currentTargets[0]), new Vector3(0, 270, 0));

                    atk.Execute();
                    NetworkParser.localGameplayCommands.Enqueue(atk);
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


    public void StoreTargetUp(GameObject target)
    {
        currentTargets[0] = target;
        attackUp = true;
    }

    public void StoreTargetDown(GameObject target)
    {
        currentTargets[1] = target;
        attackDown = true;
    }

    public void StoreTargetLeft(GameObject target)
    {
        currentTargets[2] = target;
        attackLeft = true;
    }
    public void StoreTargetRight(GameObject target)
    {
        currentTargets[3] = target;
        attackRight = true;
        
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
