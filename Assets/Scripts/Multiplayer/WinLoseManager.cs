using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class WinLoseManager
{
   public static void YouWin()
   {
    GameObject.Find("Win Screen").SetActive(true);
   }

   public static void YouLose()
   {
    GameObject.Find("Lose Screen").SetActive(true);
   }
}
