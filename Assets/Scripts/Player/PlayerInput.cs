using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerWrapper;



public class PlayerInput : MonoBehaviour
{
    [SerializeField]
    private bool canMove;
    [SerializeField]
    private bool myTurn;

    private bool moveUp, moveDown,moveLeft, moveRight, canInteract = false;

    private GameObject currentTarget;
    void Start() 
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {

        if(myTurn)
        {

        
        if(canMove)
        {
            if (Input.GetKeyDown(KeyCode.RightArrow) && moveRight)
            {
                ResetValidMoves();
                PlayerUpdater.UpdatePlayer(gameObject, transform.position + new Vector3(1,0,0), new Vector3 (0,0,0));
            }

            if (Input.GetKeyDown(KeyCode.DownArrow) && moveDown)
            {
                ResetValidMoves();
                PlayerUpdater.UpdatePlayer(gameObject, transform.position + new Vector3(0,0,-1), new Vector3 (0,90,0));
                
            }

            if (Input.GetKeyDown(KeyCode.LeftArrow) && moveLeft)
            {
                ResetValidMoves();
                PlayerUpdater.UpdatePlayer(gameObject, transform.position + new Vector3(-1,0,0), new Vector3 (0,180,0));
                
            }

            if (Input.GetKeyDown(KeyCode.UpArrow) && moveUp)
            {
                ResetValidMoves();
                PlayerUpdater.UpdatePlayer(gameObject, transform.position + new Vector3(0,0,1), new Vector3 (0,270,0));
                
            }

            if (Input.GetKeyDown(KeyCode.Space) && canInteract)
            {
                Debug.Log ("Interaction");
            }
        }

        if(canInteract)
        {
            if(Input.GetKeyDown(KeyCode.LeftShift))
            {
                Attack();
            }
        }
        

            
        }
    }

    

    public void SetCanMoveState(bool state)
    {
        canMove = state;
    }

    public bool GetCanMoveState(bool state)
    {
        return canMove;
    }

    public void SetCanMoveInDir(int direction) 
    {
        switch (direction)
        {
            case 0:
                moveUp = true;
                break;

            case 1:
                moveDown = true;
                break;

            case 2:
                moveLeft = true;
                break;

            case 3:
                moveRight = true;
                break;

        }
    }

    public void ResetValidMoves()
    {
        moveDown = false;
        moveLeft = false;
        moveRight = false;
        moveUp = false;
    }

    public void SetInteractState(bool state)
    {
        canInteract = true;
        
    }

    public void UpdateTarget(GameObject target)
    {
        currentTarget = target;
    }

    public void ChangeTurn()
    {
        myTurn = !myTurn;
        //update player values
    }

    public void Attack()
    {
        Destroy(currentTarget);
    }
    
        

}
