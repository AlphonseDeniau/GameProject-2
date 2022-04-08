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
        heal,
        regeneration,
        concentration,
        thorn,
        provocation,
        guard,
        detoxification,
        strength,
        resistance,
        haste,
        power,
        accuracy,
        ghost,
        vigilance,

        //  BAD EFFECT \\
        damage,
        burn,
        poison,
        freeze,
        paralysis,
        blind,
        confusion,
        stun,
        exhaustion,
        weakness,
        slowness,
    };

    public enum EStatusActionTime
    {
        startOfTurn, // Apply at the beginning of the character turn
        endOfTurn,   // Apply at the end of the character turn
        immediate,   // Apply at the moment you receive the effect
        fight        // Apply at the moment you make a fighting action
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
