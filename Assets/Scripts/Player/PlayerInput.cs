using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{

    public int lookDir=0;
    private bool canMove = false;
    public GameObject enemyPrefab;
    [SerializeField]
    private List<GameObject> scanners = new List<GameObject>();
    private bool canMove = true;
    public GameObject enemyPrefab;

    private bool moveUp, moveDown, moveLeft, moveRight, canInteract = false;

    private float elevation = 0f;
    private float[] nextEls = new float[]{ 0f, 0f, 0f, 0f };
    

    void Start() 
    {
        for(int i = 0; i < scanners.Count; i++)
        {
            scanners[i].SetActive(false);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        //controls are subject to change/expand
        //Look inputs 
        if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                if (IsAppropriateElevation(nextEls[3]))
                {
                    ResetValidMoves();
                    transform.eulerAngles = new Vector3(0,0,0);
                    transform.position = transform.position + new Vector3(1, 0, 0);
                    elevation = nextEls[3];
                }
                
                
            }

            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                if (IsAppropriateElevation(nextEls[1]))
                {
                    ResetValidMoves();
                    transform.eulerAngles = new Vector3(0, 90, 0);
                    transform.position = transform.position + new Vector3(0, 0, -1);
                    elevation = nextEls[1];
                }
            }

            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                if (IsAppropriateElevation(nextEls[2]))
                {
                    ResetValidMoves();
                    transform.eulerAngles = new Vector3(0, 180, 0);
                    transform.position = transform.position + new Vector3(-1, 0, 0);
                    elevation = nextEls[2];
                }
            }

            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                if (IsAppropriateElevation(nextEls[0]))
                {
                    ResetValidMoves();
                    transform.eulerAngles = new Vector3(0, 270, 0);
                    transform.position = transform.position + new Vector3(0, 0, 1);
                    elevation = nextEls[0];
                }
            }
            

            if (Input.GetKeyDown(KeyCode.H)){

                switch (lookDir)
                {
                     case 0:
                         Instantiate(enemyPrefab, transform.position + new Vector3(1, 0, 0), transform.rotation);
                         break;
                     case 1:
                         Instantiate(enemyPrefab, transform.position + new Vector3(0, 0, -1), transform.rotation);
                         break;
                     case 2:
                         Instantiate(enemyPrefab, transform.position + new Vector3(-1, 0, 0), transform.rotation);
                         break;
                     case 3:
                         Instantiate(enemyPrefab, transform.position + new Vector3(0, 0, 1), transform.rotation);
                         break;
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
