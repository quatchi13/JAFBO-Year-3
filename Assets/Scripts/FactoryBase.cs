using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FactoryBase<T> : MonoBehaviour where T: MonoBehaviour
{
    [SerializeField]
   private T prefab;

   public T CreateInstance()
   {
        return Instantiate(prefab);
   }
}
