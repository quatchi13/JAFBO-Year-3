using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyHitbox : MonoBehaviour
{

    [SerializeField]
    private EnemySubject subject;
    void Awake()
    {
        EnemyManager.instance.AddEnemy(gameObject);
        subject = EnemyManager.instance.GetComponent<EnemySubject>();
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
            subject.RemoveObserver(gameObject.GetComponent<Observer>());
            Destroy(gameObject);
        }
    }
}
