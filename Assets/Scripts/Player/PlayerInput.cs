using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private bool canMove = true;
    public GameObject enemyPrefab;

    private bool moveUp, moveDown,moveLeft, moveRight, canInteract = false;

    void Start() 
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {

        if(canMove)
        {
            if (Input.GetKeyDown(KeyCode.RightArrow) && moveRight)
            {
                ResetValidMoves();
                transform.eulerAngles = new Vector3(0,0,0);
                transform.position = transform.position + new Vector3(1, 0, 0);
                
            }

            if (Input.GetKeyDown(KeyCode.DownArrow) && moveDown)
            {
                ResetValidMoves();
                transform.eulerAngles = new Vector3(0, 90, 0);
                transform.position = transform.position + new Vector3(0, 0, -1);
                
            }

            if (Input.GetKeyDown(KeyCode.LeftArrow) && moveLeft)
            {
                ResetValidMoves();
                transform.eulerAngles = new Vector3(0, 180, 0);
                transform.position = transform.position + new Vector3(-1, 0, 0);
                
            }

            if (Input.GetKeyDown(KeyCode.UpArrow) && moveUp)
            {
                ResetValidMoves();
                transform.eulerAngles = new Vector3(0, 270, 0);
                transform.position = transform.position + new Vector3(0, 0, 1);
                
            }

            if (Input.GetKeyDown(KeyCode.Space) && canInteract)
            {
                Debug.Log ("Interaction");
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

   
        
        

}
