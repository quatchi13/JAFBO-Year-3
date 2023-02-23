using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public void ActivateMoveMode()
    {
        ActiveSelections.instance.ClearSelection();
                for(int i = 0; i < scanners.Count; i++)
                {
                    scanners[i].SetActive(false);
                }

                scanners[0].SetActive(true);
    }

    public void ActivateAttackMode()
    {
        ActiveSelections.instance.ClearSelection();
        
                for(int i = 0; i < scanners.Count; i++)
                {
                    scanners[i].SetActive(false);
                }

                scanners[1].SetActive(true);
            canMove = false;
    }

    public void PerformAction()
    {
        
        if (canMove)
            {
                switch (lookDir)
                {
                    case 0:
                        transform.position = transform.position + new Vector3(1, 0, 0);
                        break;
                    case 1:
                        transform.position = transform.position + new Vector3(0, 0, -1);
                        break;
                    case 2:
                        transform.position = transform.position + new Vector3(-1, 0, 0);
                        break;
                    case 3:
                        transform.position = transform.position + new Vector3(0, 0, 1);
                        break;
                }
                canMove = false;
            }
        else
        {
            for(int i = 0; i < ActiveSelections.instance.GetSelection().Count; i++)
            {
                Destroy(ActiveSelections.instance.GetSelection()[i]);
            }
        }
        
        ActiveSelections.instance.ClearSelection();
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

    private bool IsAppropriateElevation(float next_elevation)
    {
        float diff = (next_elevation - elevation);
        if (diff < 0) diff *= -1;
        Debug.Log(diff);

        if (diff > 0.5f)
        {
            return false;
        }

        return true;
    }

}
