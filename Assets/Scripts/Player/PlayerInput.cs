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
                transform.eulerAngles = new Vector3(0,0,0);
                lookDir = 0;
                //change look dir
            }

            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                transform.eulerAngles = new Vector3(0, 90, 0);
                lookDir = 1;
                //change look dir
            }

            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                transform.eulerAngles = new Vector3(0, 180, 0);
                lookDir = 2;
                //change look dir
            }

            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                transform.eulerAngles = new Vector3(0, 270, 0);
                lookDir = 3;
                //change look dir
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
        
        ActiveSelections.instance.ClearSelection();
    }

}
