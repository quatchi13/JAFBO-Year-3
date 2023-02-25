using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveButtonManager : MonoBehaviour
{

    private bool up = false,down =  false,left = false,right = false;
    public GameObject[] buttons;
    void Update()
    {
        buttons[0].SetActive(up);
        buttons[1].SetActive(down);
        buttons[2].SetActive(left);
        buttons[3].SetActive(right);
    }

    public void SetUpButton(bool activeState)
    {
        up = activeState;
    }

    public void SetDownButton(bool activeState)
    {
        down = activeState;
    }

    public void SetLeftButton(bool activeState)
    {
        left = activeState;
    }

    public void SetRightButton(bool activeState)
    {
        right = activeState;
    }
}
