using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;

public class StatAssignment : MonoBehaviour
{
    public enum Stat : int {
        ATTACK,
        ULT_ATTACK,
        MAGIC,
        ULT_MAGIC,
        DEFENSE_ATTACK,
        DEFENSE_MAGIC,
        SPEED,
        HEALTH
    }

    
    [DllImport("FlorenceStatBlock")] public static extern int GetFlorenceStat(Stat s);
    
    void Start()
    {
        Example();
    }

    
    void Update()
    {
        
    }

    void Example()
    {
        print(GetFlorenceStat(Stat.ATTACK));
        print(GetFlorenceStat(Stat.ULT_ATTACK));
        print(GetFlorenceStat(Stat.MAGIC));
        print(GetFlorenceStat(Stat.ULT_MAGIC));
        print(GetFlorenceStat(Stat.DEFENSE_ATTACK));
        print(GetFlorenceStat(Stat.DEFENSE_MAGIC));
        print(GetFlorenceStat(Stat.SPEED));
        print(GetFlorenceStat(Stat.HEALTH));
    }
}
