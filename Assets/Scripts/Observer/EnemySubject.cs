using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySubject : Subject
{

    public override void Notify()
    {
        for (int i = 0; i < observers.Count; i++)
        {
            observers[i].OnNotify();
        }
    }

    public override void AddObserver(Observer observer)
    {
        observers.Add(observer);
    }

    public override void RemoveObserver(Observer observerToRemove)
    {
        observers.Remove(observerToRemove);
    }

    
}
