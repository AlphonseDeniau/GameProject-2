using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class SkillEffect
{
    public enum EEffectType {
        Status,
        Heal,
        Damage,
    };

    [Serializable]
    public class EffectStatistics
    {
        [SerializeField] private int m_MaxHPPourcentage;
        [SerializeField] private int m_MaxMPPourcentage;
        [SerializeField] private int m_ActualHPPourcentage;
        [SerializeField] private int m_ActualMPPourcentage;
        [SerializeField] private int m_StrengthPourcentage;
        [SerializeField] private int m_DefensePourcentage;
        [SerializeField] private int m_MagicPourcentage;
        [SerializeField] private int m_SpeedPourcentage;
        [SerializeField] private int m_DodgePourcentage;
        [SerializeField] private int m_CriticalPourcentage;
        [SerializeField] private int m_ParryPourcentage;

        public int MaxHPPourcentage => m_MaxHPPourcentage;
        public int MaxMPPourcentage => m_MaxMPPourcentage;
        public int ActualHPPourcentage => m_ActualHPPourcentage;
        public int ActualMPPourcentage => m_ActualMPPourcentage;
        public int StrengthPourcentage => m_StrengthPourcentage;
        public int DefensePourcentage => m_DefensePourcentage;
        public int MagicPourcentage => m_MagicPourcentage;
        public int SpeedPourcentage => m_SpeedPourcentage;
        public int DodgePourcentage => m_DodgePourcentage;
        public int CriticalPourcentage => m_CriticalPourcentage;
        public int ParryPourcentage => m_ParryPourcentage;
    }

    [Header("Properties")]
    [SerializeField] private EEffectType m_Type;
    [SerializeField] private int m_StaticPower;
    [SerializeField] private SkillArea m_Area;
    [SerializeField] private StatusEffect m_Effect;

    [Header("Statistics Calculation")]
    [SerializeField] private EffectStatistics m_CharacterStatistics;
    [SerializeField] private EffectStatistics m_TargetStatistics;


    // Accessors \\
    public EEffectType Type => m_Type;
    public SkillArea Area => m_Area;
    public StatusEffect Effect => m_Effect;


    // Methods \\

    /// <summary>
    /// Apply effect on target
    /// </summary>
    /// <param name="_user">character who use the skill</param>
    /// <param name="_target">character targeted by the skill</param>
    /// <returns></returns>
    public bool ApplyEffect(Character _user, Character _target)
    {
        return m_Type switch
        {
            EEffectType.Damage => ApplyDamage(_user, _target),
            EEffectType.Heal => ApplyHeal(_user, _target),
            EEffectType.Status => ApplyStatus(_user, _target),
            _ => throw new ArgumentOutOfRangeException(nameof(m_Type), $"Not expected effect value: {m_Type}")
        };
    }

    public bool ApplyDamage(Character _user, Character _target)
    {
        return true;
    }

    public bool ApplyHeal(Character _user, Character _target)
    {
        return true;
    }

    public bool ApplyStatus(Character _user, Character _target)
    {
        // Choose if who will have the effect link
        m_Effect.ChooseTarget(_user, _target);
        // Calcul power of the effect by using the pourcentage and static power
        m_Effect.Power = m_StaticPower + CalculCharactersPower(_user, m_CharacterStatistics) + CalculCharactersPower(_target, m_TargetStatistics);
        // Try to apply the Status effect
        if (!m_Effect.ApplyStatus(_user, _target))
        {
            // False add the effect in the character targeted
            _target.AddStatusEffect(m_Effect);
        }
        return true;
    }

    private int CalculCharactersPower(Character _character, EffectStatistics _stats)
    {
        int _result = 0;

        _result += _character.MaxHP * _stats.MaxHPPourcentage / 100;
        _result += _character.MaxMP * _stats.MaxMPPourcentage / 100;
        _result += _character.ActualHP * _stats.ActualHPPourcentage / 100;
        _result += _character.ActualMP * _stats.ActualMPPourcentage / 100;
        _result += _character.Strength * _stats.StrengthPourcentage / 100;
        _result += _character.Magic * _stats.MagicPourcentage / 100;
        _result += _character.Defense * _stats.DefensePourcentage / 100;
        _result += _character.Speed * _stats.SpeedPourcentage / 100;
        _result += _character.Dodge * 5 * _stats.DodgePourcentage / 100;
        _result += _character.Critical * 5 * _stats.CriticalPourcentage / 100;
        _result += _character.Parry * 5 * _stats.ParryPourcentage / 100;
        return _result;
    }
}
