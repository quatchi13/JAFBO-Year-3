using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatHolder : MonoBehaviour
{
    public int health = 10;
    public int damage = 2;

    public void AlterHealth(int difference)
    {
        health += difference;
    }

    void Update()
    {
        if(health < 0)
        {
            health = 0;
            Debug.Log("Dead");
        }
    }
}
