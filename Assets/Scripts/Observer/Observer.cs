using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Observer : MonoBehaviour
{
    public abstract void OnNotify();
}

public abstract class Subject : MonoBehaviour
{
    public List<Observer> observers = new List<Observer>();

    public abstract void Notify();


    public abstract void AddObserver(Observer observer);

    public abstract void RemoveObserver(Observer observer);
   
}
