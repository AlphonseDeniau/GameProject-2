using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class StatusEffect
{
    public enum EStatusType {
        regeneration,
        concentration,
        strength,
        resistance,
        haste,
        power,
        accuracy,
        ghost,
        vigilance,
        detoxification,
        stun,
        freeze,
        burn,
        paralysis,
        poison,
        blind,
        confusion,
        exhaustion,
        weakness,
        slowness,
        provocation,
        guard,
    };

    public enum EStatusActionTime {
        startOfTurn,
        endOfTurn,
        immediate,
        fight
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
    public int Power => m_Power;
    public Character Target { get { return m_Target; } set { m_Target = value; } }
}
