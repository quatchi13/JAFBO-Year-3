using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyOnNotify : Observer
{
    // Start is called before the first frame update
    void Start()
    {
        EnemyManager.instance.GetComponent<EnemySubject>().AddObserver(gameObject.GetComponent<EnemyOnNotify>());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void OnNotify()
    {
        gameObject.GetComponent<Renderer>().material.color = new Color(255, 0, 0);
    }
}
