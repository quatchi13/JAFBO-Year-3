using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatHolder : MonoBehaviour
{


    //health,damage,speed
    public int[] stats;

    //cloaked,marked
    public bool[] statuses;


    public void InitStats(int health, int damage, int speed,bool cloaked,bool marked)
    {
        stats[0] = health;
        stats[1] = damage;
        stats[2] = speed;
        statuses[0] = cloaked;
        statuses[1] = marked;


    }

    public void InflictDamage(int value)
    {
        int tempHealth = value;
        if(statuses[1] == true)
        {
            tempHealth *= 2;
        }
        if (statuses[0] == true)
        {
            tempHealth /= 4;
        }
        stats[0] -= tempHealth;
        
    }

    public void Heal(int value)
    {
        stats[0] += value;
    }

    void Update()
    {
        if(stats[0] < 0)
        {
            stats[0] = 0;
            Debug.Log("Dead");
        }
    }


    public void SetCloakedState(bool status)
    {
        statuses[0] = status;
    }

    public void SetMarkedState(bool status)
    {
        statuses[1] = status;
    }
}
