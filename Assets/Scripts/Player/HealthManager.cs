using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public int health = 10;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(health < 0)
        {
            health = 0;
            Debug.Log("Dead");
        }
    }

    public void AlterHealth(int difference)
    {
        health += difference;
    }

    public int GetCurrentHealth()
    {
        return health;
    }
}
