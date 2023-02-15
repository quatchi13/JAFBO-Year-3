using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    [SerializeField]
    private int maxHealth;

    [SerializeField]
    private int maxDefence;

    [SerializeField]
    private int currentHealth;

    [SerializeField]
    private int currentDefense;

    public void Damage(int damage)
    {
        currentHealth -= damage-currentDefense;
    }

    private void Start() 
    {
        currentHealth = maxHealth;    
    }
    private void Update() 
    {
        {
            if(currentHealth <= 0)
            {
                Debug.Log("They Died");
                Destroy(gameObject);
            }
            
        }
    }
}
