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
    [SerializeField] private StatusEnum.EStatusDurationType m_DurationType;
    [SerializeField] private float m_Duration; // -1 = infinite // Depends of the effect (ex: Burn = number of turn, Freeze = number of seconds)
    [SerializeField] private int m_StaticPower; // Depends of the effect (ex: Burn = damage, Regeneration = heal)

    [SerializeField] private StatusCharacterPower m_UserPower;
    [SerializeField] private StatusCharacterPower m_TargetPower;

    private CharacterObject m_User;
    private CharacterObject m_Target;

    // Accessors \\
    public StatusEnum.EStatusType Type => m_Type;
    public StatusEnum.EStatusActionTime ActionTime => m_ActionTime;
    public StatusEnum.EStatusDurationType DurationType => m_DurationType;
    public float Duration => m_Duration;
    public int StaticPower => m_StaticPower;
    public CharacterObject User { get { return m_User; } set { m_User = value; } }
    public CharacterObject Target { get { return m_Target; } set { m_Target = value; } }

    // Methods \\

    public float CalcPower(CharacterObject obj, StatusCharacterPower power)
    {
        float value = 0;
        value += (power.GetPower(StatusEnum.EStatusStatistics.ActualHP) / 100.0f) * obj.Data.GetStatWithModifier(StatusEnum.EStatusStatistics.ActualHP);
        value += (power.GetPower(StatusEnum.EStatusStatistics.ActualMP) / 100.0f) * obj.Data.GetStatWithModifier(StatusEnum.EStatusStatistics.ActualMP);
        value += (power.GetPower(StatusEnum.EStatusStatistics.Critical) / 100.0f) * obj.Data.GetStatWithModifier(StatusEnum.EStatusStatistics.Critical);
        value += (power.GetPower(StatusEnum.EStatusStatistics.Defense) / 100.0f) * obj.Data.GetStatWithModifier(StatusEnum.EStatusStatistics.Defense);
        value += (power.GetPower(StatusEnum.EStatusStatistics.Dodge) / 100.0f) * obj.Data.GetStatWithModifier(StatusEnum.EStatusStatistics.Dodge);
        value += (power.GetPower(StatusEnum.EStatusStatistics.Magic) / 100.0f) * obj.Data.GetStatWithModifier(StatusEnum.EStatusStatistics.Magic);
        value += (power.GetPower(StatusEnum.EStatusStatistics.MaxHP) / 100.0f) * obj.Data.GetStatWithModifier(StatusEnum.EStatusStatistics.MaxHP);
        value += (power.GetPower(StatusEnum.EStatusStatistics.MaxMP) / 100.0f) * obj.Data.GetStatWithModifier(StatusEnum.EStatusStatistics.MaxMP);
        value += (power.GetPower(StatusEnum.EStatusStatistics.Parry) / 100.0f) * obj.Data.GetStatWithModifier(StatusEnum.EStatusStatistics.Parry);
        value += (power.GetPower(StatusEnum.EStatusStatistics.Speed) / 100.0f) * obj.Data.GetStatWithModifier(StatusEnum.EStatusStatistics.Speed);
        value += (power.GetPower(StatusEnum.EStatusStatistics.Strength) / 100.0f) * obj.Data.GetStatWithModifier(StatusEnum.EStatusStatistics.Strength);
        return (value);
    }

    /// <summary>
    /// If actionTime is immediate do the status
    /// Else add it in the CharacterObject
    /// </summary>
    /// <param name="_user">characterObject who use the skill</param>
    /// <param name="_target">characterObject targeted by the skill</param>
    /// <returns>True if the status was immediate</returns>
    public bool ApplyImmediateStatus(CharacterObject _user, CharacterObject _target)
    {
        if (m_ActionTime != StatusEnum.EStatusActionTime.Immediate)
        {
            _target.Data.AddStatusEffect(this);
            return false;
        }
        // Add immediate effect and process of them here
        return (ApplyStatus(_user, _target));
    }

    /// <summary>
    /// Choose the Target of effect
    /// </summary>
    /// <param name="_user">characterObject who use the skill</param>
    /// <param name="_target">characterObject targeted by the skill</param>
    /// <returns>True if the Target his the same</returns>
    public bool ChooseTarget(CharacterObject _user, CharacterObject _target)
    {
        m_User = _user;
        m_Target = _target;
        return true;
    }

    /// <summary>
    /// Apply effect
    /// </summary>
    /// <param name="_user">characterObject who use the skill</param>
    /// <param name="_target">characterObject targeted by the skill</param>
    /// <returns></returns>
    public bool ApplyStatus(CharacterObject _user, CharacterObject _target)
    {
        switch (this.m_Type)
        {
            case StatusEnum.EStatusType.Regeneration:
            case StatusEnum.EStatusType.Concentration:
            case StatusEnum.EStatusType.Detoxification:
            case StatusEnum.EStatusType.Burn:
            case StatusEnum.EStatusType.Poison:
            case StatusEnum.EStatusType.Heal:
                float heal = m_StaticPower;
                heal += CalcPower(m_User, m_UserPower);
                heal += CalcPower(m_Target, m_TargetPower);
                _target.Data.TakeHeal((int)heal);
                break;
            case StatusEnum.EStatusType.Damage:
                float dmg = m_StaticPower;
                dmg += CalcPower(m_User, m_UserPower);
                dmg += CalcPower(m_Target, m_TargetPower);
                _target.Data.TakeDamage((int)dmg);
                break;
            case StatusEnum.EStatusType.Stun:
                FightManager.Instance.TurnManager.ResetCharacter(FightManager.Instance.TurnManager.GetCharacter(_target));
                break;
            default:
                break;
        }
        return true;
    }

    /// <summary>
    /// Update Duration
    /// </summary>
    /// <param name="_time">time passed</param>
    /// <returns>True if the effect is finished</returns>
    public bool UpdateDuration(float _time)
    {
        this.m_Duration -= _time;
        if (this.m_Duration <= 0)
            return true;
        return false;
    }
}
