using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthManager : MonoBehaviour
{
    private int health=10;

    public void AlterHealth(int difference)
    {
        health += difference;
    }

    private void Update() {
        if(health < 0)
        {
            health = 0;
            Debug.Log("Overkill");
        }
        
    }
}
