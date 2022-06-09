using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class StatusEnum : MonoBehaviour
{
    public enum EStatusType
    {
        // GOOD EFFECT \\
        Heal,
        Regeneration,
        Concentration,
        Thorn,
        Provocation,
        Guard,
        Detoxification,
        Strength,
        Resistance,
        Haste,
        Power,
        Accuracy,
        Ghost,
        Vigilance,

        //  BAD EFFECT \\
        Damage,
        Burn,
        Poison,
        Freeze,
        Paralysis,
        Blind,
        Confusion,
        Stun,
        Exhaustion,
        Weakness,
        Slowness,
        Mana,
    };

    public enum EStatusActionTime
    {
        StartOfTurn, // Apply at the beginning of the character turn
        EndOfTurn,   // Apply at the end of the character turn
        Immediate,   // Apply at the moment you receive the effect
        Fight,       // Apply at the moment you make a fighting action
        Continual,   // Apply continuously (freeze)
    }

    public enum EStatusDurationType
    {
        Turn,
        Second
    }

    public enum EStatusStatistics
    {
        MaxHP,
        MaxMP,
        ActualHP,
        ActualMP,
        Strength,
        Magic,
        Defense,
        Speed,
        Dodge,
        Critical,
        Parry,
    }
}
