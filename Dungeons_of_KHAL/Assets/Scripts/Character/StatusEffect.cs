using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class StatusEffect
{
    public enum EStatusType {
        // GOOD EFFECT \\
        //   Duration  \\
        regeneration,
        concentration,
        provocation,
        guard,
        //   Instant   \\
        detoxification,
        //  Statistics \\
        strength,
        resistance,
        haste,
        power,
        accuracy,
        ghost,
        vigilance,

        //  BAD EFFECT \\
        //   Duration  \\
        burn,
        poison,
        freeze,
        paralysis,
        blind,
        confusion,
        //   Instant   \\
        stun,
        //  Statistics \\
        exhaustion,
        weakness,
        slowness,
        //-------------\\
    };

    public enum EStatusActionTime {
        startOfTurn, // Apply at the beginning of the character turn
        endOfTurn,   // Apply at the end of the character turn
        immediate,   // Apply at the moment you receive the effect
        fight        // Apply at the moment you make a fighting action
    }

    [Header("Effect Description")]
    [SerializeField] private EStatusType m_Type;
    [SerializeField] private EStatusActionTime m_ActionTime;
    [SerializeField] private int m_Duration; // -1 = infinite // Depends of the effect (ex: Burn = number of turn, Freeze = number of seconds)
    [SerializeField] private int m_Power; // Depends of the effect (ex: Burn = damage, Regeneration = heal)
    private Character m_Target;

    // Accessors \\
    public EStatusActionTime ActionTime => m_ActionTime;
    public EStatusType Type => m_Type;
    public int Duration => m_Duration;
    public int Power { get { return m_Power; } set { m_Power = value; } }
    public Character Target { get { return m_Target; } set { m_Target = value; } }

    // Methods \\

    /// <summary>
    /// If actionTime is immediate do the status
    /// Else add it in the Character
    /// </summary>
    /// <param name="_user">character who use the skill</param>
    /// <param name="_target">character targeted by the skill</param>
    /// <returns>True if the status was immediate</returns>
    public bool ApplyStatus(Character _user, Character _target)
    {
        if (m_ActionTime != EStatusActionTime.immediate)
            return false;
        // Make the immediate Effect do something
        // And add other effect in the Character
        return true;
    }


    /// <summary>
    /// Choose the Target of effect
    /// </summary>
    /// <param name="_user">character who use the skill</param>
    /// <param name="_target">character targeted by the skill</param>
    /// <returns>True if the Target his the same</returns>
    public bool ChooseTarget(Character _user, Character _target)
    {
        if (m_Type == EStatusType.provocation || m_Type == EStatusType.guard)
        {
            m_Target = _user;
            return false;
        }
        m_Target = _target;
        return true;
    }
}
