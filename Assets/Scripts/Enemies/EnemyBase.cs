using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    public bool isAdded = false;

    void Update()
    {
        if (!isAdded)
        {
            EnemyManager.instance.AddEnemy(gameObject);
            isAdded = true;

        }
        
    }

}
