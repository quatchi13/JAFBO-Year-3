using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JAFnetwork;

public class PlayerInput : MonoBehaviour
{
    private bool canMove = true;
    public GameObject enemyPrefab;

    private bool moveUp, moveDown, moveLeft, moveRight, canInteract = false;

    private float elevation = 0f;
    private float[] nextEls = new float[]{ 0f, 0f, 0f, 0f };
    
    [SerializeField]
    private GameObject[] MoveButtons;

    void Start() 
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {
        if(canMove && ActionPointsManager.instance.actions > 0)
        {
            if (moveRight)
            {
                if (IsAppropriateElevation(nextEls[3]))
                {
                    MoveButtons[3].SetActive(true);
                    //MoveRight();
                }
                
                
            }

            if (moveDown)
            {
                if (IsAppropriateElevation(nextEls[1]))
                {
                    MoveButtons[1].SetActive(true);
                    //MoveDown();
                }
            }

            if (moveLeft)
            {
                if (IsAppropriateElevation(nextEls[2]))
                {
                    MoveButtons[2].SetActive(true);
                   //MoveLeft();
                }
            }

            if (moveUp)
            {
                if (IsAppropriateElevation(nextEls[0]))
                {
                    MoveButtons[0].SetActive(true);
                    //MoveUp();
                }
            }

            if (Input.GetKeyDown(KeyCode.Space) && canInteract)
            {
                Debug.Log ("Interaction");
            }
        }
        else
        {
            for(int i = 0; i<MoveButtons.Length; i++)
            {
                MoveButtons[i].SetActive(false);
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
        for(int i=0; i < MoveButtons.Length; i++)
        {
            MoveButtons[i].SetActive(false);
        }
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


    public void MoveRight()
    {
        ResetValidMoves();
        MoveChar moveRight = new MoveChar();
        moveRight.Setup(NetworkParser.GetPCIndex(gameObject), new Vector3(1, 0, 0), new Vector3(0, 0, 0));
        moveRight.Execute();
        NetworkParser.localGameplayCommands.Enqueue(moveRight);
        elevation = nextEls[3];
    }

    public void MoveDown()
    {
        ResetValidMoves();
        MoveChar moveDown = new MoveChar();
        moveDown.Setup(NetworkParser.GetPCIndex(gameObject), new Vector3(0, 0, -1), new Vector3(0, 90, 0));
        moveDown.Execute();
        NetworkParser.localGameplayCommands.Enqueue(moveDown);
        elevation = nextEls[1];
    }

    public void MoveLeft()
    {
        ResetValidMoves();
        MoveChar moveLeft = new MoveChar();
        moveLeft.Setup(NetworkParser.GetPCIndex(gameObject), new Vector3(-1, 0, 0), new Vector3(0, 180, 0));
        moveLeft.Execute();
        NetworkParser.localGameplayCommands.Enqueue(moveLeft);
        elevation = nextEls[2];
    }

    public void MoveUp()
    {
        ResetValidMoves();
        MoveChar moveUp = new MoveChar();
        moveUp.Setup(NetworkParser.GetPCIndex(gameObject), new Vector3(0, 0, 1), new Vector3(0, 270, 0));
        moveUp.Execute();
        NetworkParser.localGameplayCommands.Enqueue(moveUp);
        elevation = nextEls[0];
    }
}
