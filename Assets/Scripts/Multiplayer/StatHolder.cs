using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatHolder : MonoBehaviour
{
    public int health = 10;
    public int damage = 2;

    public bool cloaked = false;

    public bool marked = false;

    public void InflictDamage(int value)
    {
        int tempHealth = value;
        if(marked == true)
        {
            tempHealth *= 2;
        }
        if (cloaked == true)
        {
            tempHealth /= 4;
        }
        health -= tempHealth;
        
    }

    public void Heal(int value)
    {
        health += value;
    }

    void Update()
    {
        if(health < 0)
        {
            health = 0;
            Debug.Log("Dead");
        }
    }


    public void SetCloakedState(bool status)
    {
        cloaked = status;
    }

    public void SetMarkedState(bool status)
    {
        marked = status;
    }
}
