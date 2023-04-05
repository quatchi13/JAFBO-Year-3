using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using JAFnetwork;

public class PlayerInput : MonoBehaviour
{
    private bool canMove = true;

    private bool moveUp, moveDown, moveLeft, moveRight, canInteract = false;

    public static float elevation = 0f;
    public float visibleElevation = 0f;
    private float[] nextEls = new float[]{ 0f, 0f, 0f, 0f };

    public List<GameObject> MoveButtons;
    public GameObject upButton;
    public GameObject downButton;
    public GameObject leftButton;
    public GameObject rightButton;

    public GameObject buttonParent;

    public GameObject endTurnButton;

    private Animator anim;

    private int cloakedCount;
    private int markedCount;

    public bool isMyTurn;
    private bool flipped = false;

    void Start()
    {
        //Set each of the buttons to a reference of the Ui button that already exists. We need to do this because it is a prefab being instantiated and cant store references. Therefore Awake
        upButton = GameObject.Find("MoveUp");
        downButton = GameObject.Find("MoveDown");
        leftButton = GameObject.Find("MoveLeft");
        rightButton = GameObject.Find("MoveRight");
        buttonParent = GameObject.Find("MoveButtons");
        endTurnButton = GameObject.Find("EndTurn");
        anim = GetComponent<Animator>();

        //In list cause easier to reference all the buttons at once
        MoveButtons[0] = upButton;
        MoveButtons[1] = downButton;
        MoveButtons[2] = leftButton;
        MoveButtons[3] = rightButton;


        if (Follow.isP2)
        {
            //FollowPosition.SetRot();
            flipped = true;

            upButton.GetComponent<Button>().onClick.AddListener(MoveDown);
            downButton.GetComponent<Button>().onClick.AddListener(MoveUp);
            leftButton.GetComponent<Button>().onClick.AddListener(MoveRight);
            rightButton.GetComponent<Button>().onClick.AddListener(MoveLeft);
        }
        else
        {
            upButton.GetComponent<Button>().onClick.AddListener(MoveUp);
            downButton.GetComponent<Button>().onClick.AddListener(MoveDown);
            leftButton.GetComponent<Button>().onClick.AddListener(MoveLeft);
            rightButton.GetComponent<Button>().onClick.AddListener(MoveRight);
        }




        endTurnButton.GetComponent<Button>().onClick.AddListener(EndTurn);

        upButton.SetActive(false);
        downButton.SetActive(false);
        leftButton.SetActive(false);
        rightButton.SetActive(false);

        isMyTurn = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(isMyTurn)
        {
            buttonParent.SetActive(true);
            visibleElevation = elevation;
       
            if(canMove && gameObject.GetComponent<ActionPointsManager>().actions > 0)
            {
                if (moveRight && IsAppropriateElevation(nextEls[/**/3])) 
                {
                    MoveButtons[(flipped)?2:3].SetActive(true);
                }

                if (moveDown && IsAppropriateElevation(nextEls[/**/1]))
                {
                    MoveButtons[(flipped) ? 0 : 1].SetActive(true);
                }

                if (moveLeft && IsAppropriateElevation(nextEls[/**/2]))
                {
                    MoveButtons[(flipped) ? 3 : 2].SetActive(true);
                }

                if (moveUp && IsAppropriateElevation(nextEls[/**/0]))
                {
                    MoveButtons[(flipped) ? 1 : 0].SetActive(true);
                }

                if (Input.GetKeyDown(KeyCode.Space) && canInteract)
                {
                    Debug.Log ("Interaction");
                }
            }
            else
            {
                for(int i = 0; i<MoveButtons.Count; i++)
                {
                    MoveButtons[i].SetActive(false);
                }
            }
        
        
        
        }
        else
        {
            buttonParent.SetActive(false);
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
        for(int i=0; i < MoveButtons.Count; i++)
        {
            MoveButtons[i].SetActive(false);
        }
    }

    public void ResetElevations()
    {
        for (int i = 0; i < 4; nextEls[i] = 2, i++) ;
    }

    public void DisableButtons()
    {
        for(int i=0; i < MoveButtons.Count; i++)
        {
            MoveButtons[i].SetActive(false);
        }
        
    }

    public void EndTurn()
    {
        GetComponent<ActionPointsManager>().AlterActionNumber(-100);
        DisableButtons();
        isMyTurn = false;
        if(SockFunctions.CanSend(Lobby.clientSock))
        {
            NetworkParser.SendGameplayQueueToBuffer();
            Lobby.clientSock.Send(NetworkParser.outBuffer);
            NetworkParser.outBuffer = new byte[1024];
            Debug.Log("sent");
        }
        if(gameObject.GetComponent<FlorenceSpecials>() != null)
        {
            gameObject.GetComponent<FlorenceSpecials>().cooldown--;
        } 
        else if(gameObject.GetComponent<DeathSpecial>() != null)
        {
            gameObject.GetComponent<DeathSpecial>().cooldown--;
        }
        else if(gameObject.GetComponent<LifeSpecial>() != null)
        {
            gameObject.GetComponent<LifeSpecial>().cooldown--;
        }  

        if(gameObject.GetComponent<StatHolder>().statuses[0])
        {
            if(cloakedCount > 0)
            {
                gameObject.GetComponent<StatHolder>().statuses[0] = false;
                cloakedCount = 0;
            }
            else
            {
                cloakedCount++;
            }
        }

        if(gameObject.GetComponent<StatHolder>().statuses[1])
        {
            if(markedCount > 0)
            {
                gameObject.GetComponent<StatHolder>().statuses[1] = false;
                markedCount = 0;
            }
            else
            {
                markedCount++;
            }
        }
        
    }

    public void StartTurn()
    {
        isMyTurn = true;
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

    public static bool IsAppropriateElevation(float next_elevation)
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
        anim.Play("Layer 1.Walk", 0, 0);
        anim.Play("Layer 2.Walk 1", 0, 0);
        anim.Play("Layer 3.Walk 2", 0, 0);
        anim.Play("Layer 4.Walk 3", 0, 0);

        ResetValidMoves();
        MoveChar moveRight = new MoveChar();
        moveRight.Setup(NetworkParser.GetPCIndex(gameObject), new Vector3(1, 0, 0), new Vector3(0, 90, 0));

        int ind = (flipped)?2:3;
        elevation = nextEls[3];
        Debug.Log("Player Elevation: " + elevation.ToString());
        //ResetElevations();

        moveRight.Execute();
        NetworkParser.localGameplayCommands.Enqueue(moveRight);
    }

    public void MoveDown()
    {
        anim.Play("Layer 1.Walk", 0, 0);
        anim.Play("Layer 2.Walk 1", 0, 0);
        anim.Play("Layer 3.Walk 2", 0, 0);
        anim.Play("Layer 4.Walk 3", 0, 0);

        ResetValidMoves();
        MoveChar moveDown = new MoveChar();
        moveDown.Setup(NetworkParser.GetPCIndex(gameObject), new Vector3(0, 0, -1), new Vector3(0, 180, 0));

        int ind = (flipped)?0:1;
        elevation = nextEls[ind];
        Debug.Log("Player Elevation: " + elevation.ToString());
        //ResetElevations();

        moveDown.Execute();
        NetworkParser.localGameplayCommands.Enqueue(moveDown);
    }

    public void MoveLeft()
    {
        anim.Play("Layer 1.Walk", 0, 0);
        anim.Play("Layer 2.Walk 1", 0, 0);
        anim.Play("Layer 3.Walk 2", 0, 0);
        anim.Play("Layer 4.Walk 3", 0, 0);

        ResetValidMoves();
        MoveChar moveLeft = new MoveChar();
        moveLeft.Setup(NetworkParser.GetPCIndex(gameObject), new Vector3(-1, 0, 0), new Vector3(0, 270, 0));

        int ind = (flipped)?3:2;
        elevation = nextEls[ind];
        Debug.Log("Player Elevation: " + elevation.ToString());
        //ResetElevations();

        moveLeft.Execute();
        NetworkParser.localGameplayCommands.Enqueue(moveLeft);
    }

    public void MoveUp()
    {
        anim.Play("Layer 1.Walk", 0, 0);
        anim.Play("Layer 2.Walk 1", 0, 0);
        anim.Play("Layer 3.Walk 2", 0, 0);
        anim.Play("Layer 4.Walk 3", 0, 0);
        
        ResetValidMoves();
        MoveChar moveUp = new MoveChar();
        moveUp.Setup(NetworkParser.GetPCIndex(gameObject), new Vector3(0, 0, 1), new Vector3(0, 0, 0));
        
        int ind = (flipped)?1:0;
        elevation = nextEls[ind];
        Debug.Log("Player Elevation: " + elevation.ToString());
        //ResetElevations();
        
        moveUp.Execute();
        NetworkParser.localGameplayCommands.Enqueue(moveUp);
    }
}
