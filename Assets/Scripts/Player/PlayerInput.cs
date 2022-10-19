using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{

    private int lookDir=0;
    private bool canMove = false;
    private bool canAttack = false;

    private int currentState = 0;
    private int changedState = 0;

    private Transform[] playerChildren;

    public GameObject enemyPrefab;

    List<GameObject> scanners = new List<GameObject>();

    void Start() 
    {
        playerChildren = GetComponentsInChildren<Transform>();
       
        
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
            //confirm action
            if (Input.GetKeyDown(KeyCode.Space) && canMove)
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


            if (currentState != changedState)
        {
            currentState = changedState;
            switch(currentState)
            {
                case 1:
                    if(scanners.Count > 0)
                    {
                        for(int i = 0;  i < scanners.Count; i++)
                            {
                                if(scanners[i].GetComponent<TileReader>() != null || scanners[i].GetComponent<BaddieReader>() != null)
                                {
                                    scanners[i].SetActive(false);
                                }

                            }
                    }
                    transform.Find("MoveScanner").gameObject.SetActive(true);  
                break;
                case 2:
                    if(scanners.Count > 0)
                    {
                        for(int i = 0;  i < scanners.Count; i++)
                            {
                                if(scanners[i].GetComponent<TileReader>() != null || scanners[i].GetComponent<BaddieReader>() != null)
                                {
                                    scanners[i].SetActive(false);
                                }

                            }
                    }
                    transform.Find("AttackScanner").gameObject.SetActive(true);  
                break;
                default:
                break;
                    
            }
        }

            if(Input.GetKeyDown(KeyCode.Alpha1))
            {
                changedState = 1;
            }
            else if(Input.GetKeyDown(KeyCode.Alpha2))
            {
                changedState = 2;
            }
    }

    public void AddScanner(GameObject child)
    {
        scanners.Add(child);
    }

    public void SetCanMoveState(bool state)
    {
        canMove = state;
    }

    public bool GetCanMoveState(bool state)
    {
        return canMove;
    }

    public void SetCanAttackState(bool state)
    {
        canAttack = state;
    }

    public bool GetCanAttackState()
    {
        return canAttack;
    }
}
