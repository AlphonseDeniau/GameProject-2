using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class StatusEffect
{
    [Header("Effect Description")]
    [SerializeField] private StatusEnum.EStatusType m_Type;
    [SerializeField] private StatusEnum.EStatusActionTime m_ActionTime;
    [SerializeField] private int m_Duration; // -1 = infinite // Depends of the effect (ex: Burn = number of turn, Freeze = number of seconds)
    [SerializeField] private int m_StaticPower; // Depends of the effect (ex: Burn = damage, Regeneration = heal)

    [SerializeField] private StatusCharacterPower m_UserPower;
    [SerializeField] private StatusCharacterPower m_TargetPower;

    private Character m_User;
    private Character m_Target;

    // Accessors \\
    public StatusEnum.EStatusType Type => m_Type;
    public StatusEnum.EStatusActionTime ActionTime => m_ActionTime;
    public int Duration => m_Duration;
    public int StaticPower => m_StaticPower;
    public Character User { get { return m_User; } set { m_User = value; } }
    public Character Target { get { return m_Target; } set { m_Target = value; } }

    // Methods \\

    /// <summary>
    /// If actionTime is immediate do the status
    /// Else add it in the Character
    /// </summary>
    /// <param name="_user">character who use the skill</param>
    /// <param name="_target">character targeted by the skill</param>
    /// <returns>True if the status was immediate</returns>
    public bool ApplyImmediateStatus(Character _user, Character _target)
    {
        if (m_ActionTime != StatusEnum.EStatusActionTime.immediate)
        {
            _target.AddStatusEffect(this);
            return false;
        }
        // Add immediate effect and process of them here
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
        m_User = _user;
        m_Target = _target;
        return true;
    }

    /// <summary>
    /// Apply effect
    /// </summary>
    /// <param name="_user">character who use the skill</param>
    /// <param name="_target">character targeted by the skill</param>
    /// <returns></returns>
    public bool ApplyStatus(Character _user, Character _target)
    {
        // Add non immediate effect and process of them here
        return true;
    }
}
