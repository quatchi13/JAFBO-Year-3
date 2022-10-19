using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager instance;
    List<GameObject> enemies = new List<GameObject>();
    // Update is called once per frame
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
    }

    public void AddEnemy(GameObject newEnemy)
    {
        enemies.Add(newEnemy);
        Debug.Log("EnemySpawn");
    }

    public void RemoveEnemy(GameObject removeEnemy)
    {
        if (enemies.Count > 0)
        {
            if (enemies.Contains(removeEnemy))
            {
                enemies.Remove(removeEnemy);
                Debug.Log("EnemyDied:(");
            }

            gameObject.GetComponent<EnemySubject>().Notify();
        }
    }

        public int NumberOfEnemies()
        {
            return enemies.Count;
        }
    public void Notify()
    {
        
    }
   
}
