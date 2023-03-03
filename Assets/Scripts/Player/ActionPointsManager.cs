using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionPointsManager : MonoBehaviour
{
    public static ActionPointsManager instance;

    public int actions=10;
    private void Start() 
    {
        if(!instance)
        {
            instance = this;
        }    
    }

   

    public void AlterActionNumber(int difference)
    {
        actions += difference;
    }

    public void StartTurn()
    {
        actions = 10;
    }
}
