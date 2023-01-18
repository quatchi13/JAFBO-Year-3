using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionCoroutines : MonoBehaviour
{

    private bool up;
    private bool left;
    private bool down;
    private bool right;
    private bool spotChosen;
    private int choseDirection;

    private bool moving;
    
[SerializeField]
    private int steps;
    IEnumerator Move() 
    {
        moving = true;
        yield return new WaitForSeconds(1);
        for(int i = 0; i < steps; i++)
        {
            while(!spotChosen)
        {
            choseDirection = Random.Range(1,5);
            Debug.Log(choseDirection);
            switch(choseDirection)
            {
                case 1:
                    if(up)
                    {
                        spotChosen = true;
                    }
                    break;
                case 2:
                    if(left)
                    {
                        spotChosen = true;
                    }
                    break;
                case 3:
                    if(down)
                    {
                        spotChosen = true;
                    }
                    break;
                case 4:
                    if(right)
                    {
                        spotChosen = true;
                    }
                    break;
                default:
                    break;
            }


        }

        switch(choseDirection)
        {
            case 1:
                transform.position += new Vector3(0,0,1.5f);
                break;
            case 2:
                transform.position += new Vector3(-1.5f,0,0);
                break;
            case 3:
                transform.position += new Vector3(0,0,-1.5f);
                break;
            case 4:
                transform.position += new Vector3(1.5f,0,0);
                break;
        }
        up = false;
        left = false;
        down = false;
        right = false;
        spotChosen = false;

        yield return new WaitForSeconds(1);
        }
        Debug.Log("Routine finished");
        moving = false;
    }

    public void SetValidDirection(string direction)
    {
        switch(direction)
        {
            case "up":
                up = true;
                break;
            case "left":
            left = true;
                break;
            case "down":
            down = true;
                break;
            case "right":
            right = true;
                break;
                default:
                break;
        }
    }

    public void StartCoroutine()
    {
        if(!moving)
        {
            StartCoroutine(Move());
        }
        
    }

    
}
