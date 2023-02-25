using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActionManager : MonoBehaviour
{
   public int actions = 10;

   public void AlterActions (int cost)
   {
        actions += cost;
   }
   private void Update() 
   {
        if(actions <= 0)
        {
            actions = 0;
            Debug.Log("OutOfActions");
        } 
   }

   public int GetRemainingActions()
   {
        return actions;
   }
}
