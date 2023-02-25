using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JAFnetwork;

public class PlayerInput : MonoBehaviour
{
    [SerializeField]
    private bool canMove = true;
    [SerializeField]
    private bool myTurn = true;

    //wow!

    //attacking target
    private GameObject currentTarget;

    //movement variables
    private bool moveUp, moveDown, moveLeft, moveRight, canInteract = false;
    private float elevation = 0f;
    private float[] nextEls = new float[]{ 0f, 0f, 0f, 0f };
    


    // Update is called once per frame
    void Update()
    {
        if (myTurn)
        {
            if (canMove)
            {

                if (Input.GetKeyDown(KeyCode.RightArrow) && moveRight)
                {
                    ResetValidMoves();
                    MoveChar mc = new MoveChar();
                    mc.Setup(NetworkParser.GetPCIndex(gameObject), new Vector3(1, 0, 0), new Vector3(0, 0, 0));
                    mc.Execute();
                    NetworkParser.localGameplayCommands.Enqueue(mc);
                    elevation = nextEls[3];
                }

                if (Input.GetKeyDown(KeyCode.DownArrow) && moveDown)
                {
                    ResetValidMoves();
                    MoveChar mc = new MoveChar();
                    mc.Setup(NetworkParser.GetPCIndex(gameObject), new Vector3(0, 0, -1), new Vector3(0, 90, 0));
                    mc.Execute();
                    NetworkParser.localGameplayCommands.Enqueue(mc);
                    ResetValidMoves();
                    elevation = nextEls[1];
                }

                if (Input.GetKeyDown(KeyCode.LeftArrow) && moveLeft)
                {
                    ResetValidMoves();
                    MoveChar mc = new MoveChar();
                    mc.Setup(NetworkParser.GetPCIndex(gameObject), new Vector3(-1, 0, 0), new Vector3(0, 180, 0));
                    mc.Execute();
                    NetworkParser.localGameplayCommands.Enqueue(mc);
                    elevation = nextEls[2];
                }

                if (Input.GetKeyDown(KeyCode.UpArrow) && moveUp)
                {
                    ResetValidMoves();
                    MoveChar mc = new MoveChar();
                    mc.Setup(NetworkParser.GetPCIndex(gameObject), new Vector3(0, 0, 1), new Vector3(0, 270, 0));
                    mc.Execute();
                    NetworkParser.localGameplayCommands.Enqueue(mc);
                    elevation = nextEls[0];
                }

                
            }

            if (canInteract)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    Debug.Log("AND IT'S FLORENCE WITH THE STEEL CHAIR!!!!");
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
        canInteract = state;
    }


    public void SetNextElevation(int idx, float el)
    {
        nextEls[idx] = el;
        Debug.Log(idx);
    }

    public void SetUpElevation(float e)
    {
        nextEls[0] = e;
    }

    public void SetDownElevation(float e)
    {
        nextEls[1] = e;
    }

    public void SetLeftElevation(float e)
    {
        nextEls[2] = e;
    }

    public void SetRightElevation(float e)
    {
        nextEls[3] = e;
    }

    public bool IsAppropriateElevation(float next_elevation)
    {
        float diff = (next_elevation - elevation);
        if (diff < 0) diff *= -1;

        if (diff > 0.5f)
        {
            return false;
        }

        return true;
    }

}
