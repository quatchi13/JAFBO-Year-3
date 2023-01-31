using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private bool canMove = true;
    public GameObject enemyPrefab;

    private bool moveUp, moveDown, moveLeft, moveRight, canInteract = false;

    private float elevation = 0f;
    private float[] nextEls = new float[]{ 0f, 0f, 0f, 0f };
    

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
                if (IsAppropriateElevation(nextEls[3]))
                {
                    ResetValidMoves();
                    transform.eulerAngles = new Vector3(0,0,0);
                    transform.position = transform.position + new Vector3(1, 0, 0);
                    elevation = nextEls[3];
                }
                
                
            }

            if (Input.GetKeyDown(KeyCode.DownArrow) && moveDown)
            {
                if (IsAppropriateElevation(nextEls[1]))
                {
                    ResetValidMoves();
                    transform.eulerAngles = new Vector3(0, 90, 0);
                    transform.position = transform.position + new Vector3(0, 0, -1);
                    elevation = nextEls[1];
                }
            }

            if (Input.GetKeyDown(KeyCode.LeftArrow) && moveLeft)
            {
                if (IsAppropriateElevation(nextEls[2]))
                {
                    ResetValidMoves();
                    transform.eulerAngles = new Vector3(0, 180, 0);
                    transform.position = transform.position + new Vector3(-1, 0, 0);
                    elevation = nextEls[2];
                }
            }

            if (Input.GetKeyDown(KeyCode.UpArrow) && moveUp)
            {
                if (IsAppropriateElevation(nextEls[0]))
                {
                    ResetValidMoves();
                    transform.eulerAngles = new Vector3(0, 270, 0);
                    transform.position = transform.position + new Vector3(0, 0, 1);
                    elevation = nextEls[0];
                }
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

    private bool IsAppropriateElevation(float next_elevation)
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
