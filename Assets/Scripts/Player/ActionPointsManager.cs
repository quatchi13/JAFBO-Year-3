using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionPointsManager : MonoBehaviour
{

    public int actions=10;
    private void Start() 
    {
        
    }


    public void AlterActionNumber(int difference)
    {
        actions += difference;
    }

    public void StartTurn()
    {
        actions = gameObject.GetComponent<StatHolder>().stats[2];
    }
}
