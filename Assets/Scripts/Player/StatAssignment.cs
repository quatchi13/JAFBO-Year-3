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

    [SerializeField]
    private int attackStat =0;
    [SerializeField]
    private int ultAttackStat=0;
    [SerializeField]
    private int magicStat=0;

    [SerializeField]
    private int ultMagicStat=0;

    [SerializeField]
    private int DefenseStat=0;

    [SerializeField]
    private int magicDefenseStat=0;

    [SerializeField]
    private int speedStat=0;

    [SerializeField]
    private int healthStat=0;

    
    [DllImport("FlorenceStatBlock")] public static extern int GetFlorenceStat(Stat s);

    public void LoadStats()
    {
        attackStat = GetFlorenceStat(Stat.ATTACK);
        ultAttackStat = GetFlorenceStat(Stat.ULT_ATTACK);
        magicStat = GetFlorenceStat(Stat.MAGIC);
        ultMagicStat = GetFlorenceStat(Stat.ULT_MAGIC);
        DefenseStat = GetFlorenceStat(Stat.DEFENSE_ATTACK);
        magicDefenseStat = GetFlorenceStat(Stat.DEFENSE_MAGIC);
        speedStat = GetFlorenceStat(Stat.SPEED);
        healthStat = GetFlorenceStat(Stat.HEALTH);
    }
}
