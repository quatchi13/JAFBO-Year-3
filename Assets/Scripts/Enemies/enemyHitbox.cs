using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyHitbox : MonoBehaviour
{
    void Awake()
    {
        EnemyManager.instance.AddEnemy(gameObject);
    }
    // Update is called once per frame
    void Update()
    {

    }


    void OnCollisionEnter(Collision other)
    {
        if (other.collider.tag == "Player")
        {
            EnemyManager.instance.RemoveEnemy(gameObject);

            Destroy(gameObject);
        }
    }
}
