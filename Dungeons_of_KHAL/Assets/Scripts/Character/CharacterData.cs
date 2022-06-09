using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class CharacterData
{
    [SerializeField] private CharacterObject m_CharacterObject;

    [Header("Current Statistics")]
    [SerializeField] private int m_ActualHP;
    [SerializeField] private int m_ActualMP;
    [SerializeField] private int m_Position;
    [SerializeField] private bool m_IsDead;
    [SerializeField] private List<StatusEffect> m_Statuses;
    [SerializeField] private StackEnum.EStackType m_StackType;

    [Header("Skills Values")]
    [SerializeField] private List<SkillData> m_Skills = new List<SkillData>();

    public int ActualHP => m_ActualHP;
    public int ActualMP => m_ActualMP;
    public int Position { get { return m_Position; } set { m_Position = value; } }
    public bool IsDead => m_IsDead;
    public StackEnum.EStackType StackType { get { return m_StackType; } set { m_StackType = value; } }
    public List<SkillData> Skills => m_Skills;
    public List<StatusEffect> Statuses => m_Statuses;

    /// <summary>
    /// Initialize character when we create it
    /// </summary>
    /// <returns></returns>
    public bool Initialize(CharacterObject character)
    {
        m_ActualHP = character.ScriptableObject.MaxHP;
        m_ActualMP = character.ScriptableObject.MaxMP;
//        m_Position = 0;
        m_IsDead = false;
        m_CharacterObject = character;
        character.ScriptableObject.Skills.ForEach(x => m_Skills.Add(new SkillData(x, 0)));
        return true;
    }

    /// <summary>
    /// Take the character for an expedition
    /// </summary>
    /// <param name="_position">Position in the team of expedition</param>
    /// <returns>True if character alive</returns>
    public bool ChooseExpedition(int _position)
    {
        if (m_IsDead) return false;
        m_Position = _position;
        return true;
    }

    /// <summary>
    /// Use a skill to hit the target
    /// </summary>
    /// <param name="_skill">Skill of the character</param>
    /// <param name="_target">character who will take the skill</param>
    /// <param name="_targetTeam">target's team</param>
    /// <returns>True if the skill is used</returns>
    public bool UseSkill(SkillData _skill, CharacterObject _target, List<CharacterObject> _targetTeam)
    {
        if (!m_CharacterObject.ScriptableObject.Skills.Contains(_skill.Skill)) return false;
        if (!this.m_Skills.Contains(_skill)) return false;
        if (!_skill.IsUsable(m_CharacterObject)) return false;
        return _skill.UseSkill(m_CharacterObject, _target, _targetTeam);
    }

    /// <summary>
    /// Decrease current HP of the character
    /// </summary>
    /// <param name="_damage">value of damage</param>
    /// <param name="_attacker">attacker</param>
    /// <returns>True: if the character is still alivet</returns>
    public bool TakeDamage(int _damage, CharacterObject _attacker)
    {
        float tmpDamage = _damage;
        bool crit = UnityEngine.Random.Range(0, 100) < _attacker.ScriptableObject.Critical;
        if (crit)
        {
            tmpDamage *= 1.5f;
        }
        tmpDamage = _damage * (40.0f / (40.0f + (float)_attacker.ScriptableObject.Defense));
        if (UnityEngine.Random.Range(0, 100) < m_CharacterObject.ScriptableObject.Dodge)
        {
            DungeonManager.Instance.UIManager.FeedbackManager.DodgeText(m_Position, m_CharacterObject.ScriptableObject.Team);
            return true;
        }
        if (UnityEngine.Random.Range(0, 100) < m_CharacterObject.ScriptableObject.Parry)
        {
            DungeonManager.Instance.UIManager.FeedbackManager.ParryText(m_Position, m_CharacterObject.ScriptableObject.Team);
            return true;
        }
        return TakeFlatDamage((int)tmpDamage, _attacker);
    }

    /// <summary>
    /// Decrease current HP of the character
    /// </summary>
    /// <param name="_damage">value of damage</param>
    /// <param name="_attacker">attacker</param>
    /// <returns>True: if the character is still alivet</returns>
    public bool TakeFlatDamage(int _damage, CharacterObject _attacker)
    {
        // MAKE A BETTER CALCULATION BITCHES
        DungeonManager.Instance.UIManager.FeedbackManager.DamageText(m_Position, m_CharacterObject.ScriptableObject.Team, _damage);
        if (_damage < 0) return true;
        m_ActualHP -= _damage;
        if (m_ActualHP < 0)
        {
            m_ActualHP = 0;
            m_IsDead = true;
            m_Statuses.Clear();
            m_StackType = StackEnum.EStackType.Neutral;
        }
        if (!m_IsDead && CheckStatusEffect(StatusEnum.EStatusType.Thorn))
        {
            _attacker.Data.TakeFlatDamage(_damage, m_CharacterObject);
        }
        return m_IsDead;
    }

    /// <summary>
    /// Increase current HP of the character 
    /// </summary>
    /// <param name="_heal">value of heal</param>
    /// <returns>True: if the character is still alive</returns>
    public bool TakeHeal(int _heal)
    {
        if (m_ActualHP <= 0) return false;
        DungeonManager.Instance.UIManager.FeedbackManager.HealText(m_Position, m_CharacterObject.ScriptableObject.Team, _heal);
        m_ActualHP += _heal;
        if (m_ActualHP > m_CharacterObject.ScriptableObject.MaxHP) m_ActualHP = m_CharacterObject.ScriptableObject.MaxHP;
        return true;
    }

    /// <summary>
    /// Decrease current MP of the character
    /// </summary>
    /// <param name="_mp">value of mp used</param>
    /// <returns>True: if the character still has MP</returns>
    public bool LoseMP(int _mp)
    {
        DungeonManager.Instance.UIManager.FeedbackManager.LoseMPText(m_Position, m_CharacterObject.ScriptableObject.Team, _mp);
        m_ActualMP -= _mp;
        if (m_ActualMP < 0)
        {
            m_ActualMP = 0;
        }
        return m_ActualMP > 0;
    }

    /// <summary>
    /// Increase current MP of the character 
    /// </summary>
    /// <param name="_mp">value of heal</param>
    /// <returns>True: if the character has mp</returns>
    public bool GainMP(int _mp)
    {
        DungeonManager.Instance.UIManager.FeedbackManager.GainMPText(m_Position, m_CharacterObject.ScriptableObject.Team, _mp);
        m_ActualMP += _mp;
        if (m_ActualMP > m_CharacterObject.ScriptableObject.MaxMP) m_ActualMP = m_CharacterObject.ScriptableObject.MaxMP;
        return true;
    }

    /// <summary>
    /// Add a status effect on the character
    /// </summary>
    /// <param name="_effect">Adding effect</param>
    /// <returns></returns>
    public bool AddStatusEffect(StatusEffect _effect)
    {
        // Check if effect is already make
        // if yes change by the new effect
        // if not add the effect to list
        if (CheckStatusEffect(_effect.Type))
        {
            StatusEffect currentEffect = m_Statuses.Find(x => x.Type == _effect.Type);
            if (currentEffect != null)
            {
                if (_effect.Duration > currentEffect.Duration)
                {
                    m_Statuses.Remove(currentEffect);
                    m_Statuses.Add(new StatusEffect(_effect));
                }
                return true;
            }
            else
                return false;
        }
        else
        {
            m_Statuses.Add(new StatusEffect(_effect));
            return true;
        }
    }

    /// <summary>
    /// Clear Status's Effects
    /// If parameter Count is equal to 0 => Remove all Effects
    /// </summary>
    /// <param name="_statusToRemove">List of effects to remove</param>
    /// <returns></returns>
    public bool ClearStatusEffect(List<StatusEffect> _effectsToRemove)
    {
        if (_effectsToRemove.Count != 0)
        {
            _effectsToRemove.ForEach(x => {
                m_Statuses.Remove(x);
            });
            return true;
        }
        m_Statuses.Clear();
        return true;
    }

    /// <summary>
    /// Check if the character has a status of a certain type
    /// </summary>
    /// <param name="_effectToCheck">List of effect to check</param>
    /// <returns>True if the character has the status type</returns>
    public bool CheckStatusEffect(StatusEnum.EStatusType _effectToCheck)
    {
        return m_Statuses.Exists(x => x.Type == _effectToCheck);
    }

    /// <summary>
    /// Update the duration of the statuses of the character and remove them if they finished
    /// </summary>
    /// <param name="_time">Time passed</param>
    /// <param name="_type">Type of duration (turn or second)</param>
    /// <returns></returns>
    public bool UpdateStatus(float _time, StatusEnum.EStatusDurationType _type)
    {
        List<StatusEffect> l_EffectToRemove = new List<StatusEffect>();

        foreach (StatusEffect l_Status in this.m_Statuses)
        {
            if (l_Status.DurationType == _type)
            {
                if (l_Status.UpdateDuration(_time))
                {
                    l_EffectToRemove.Add(l_Status);
                }
            }
        }
        if (l_EffectToRemove.Count > 0)
        {
            this.ClearStatusEffect(l_EffectToRemove);
        }
        return true;
    }

    /// <summary>
    /// Do Status
    /// </summary>
    /// <param name="_time">Action time of status to do</param>
    /// <returns></returns>
    public void DoStatus(StatusEnum.EStatusActionTime _time)
    {
        this.m_Statuses.FindAll(x => x.ActionTime == _time).ForEach(x => x.ApplyStatus(x.User, x.Target));
    }

    /// <summary>
    /// Get the stat value with effects modifiers
    /// </summary>
    /// <param name="_stat">Stat to get</param>
    /// <returns></returns>
    public float GetStatWithModifier(StatusEnum.EStatusStatistics _stat)
    {
        float stat = 0;
        switch (_stat)
        {
            case StatusEnum.EStatusStatistics.ActualHP:
                stat = this.m_ActualHP;
                break;
            case StatusEnum.EStatusStatistics.ActualMP:
                stat = this.m_ActualMP;
                break;
            case StatusEnum.EStatusStatistics.MaxHP:
                stat = this.m_CharacterObject.ScriptableObject.MaxHP;
                break;
            case StatusEnum.EStatusStatistics.MaxMP:
                stat = this.m_CharacterObject.ScriptableObject.MaxMP;
                break;
            case StatusEnum.EStatusStatistics.Critical:
                stat = this.m_CharacterObject.ScriptableObject.Critical;
                this.m_Statuses.FindAll(x => x.Type == StatusEnum.EStatusType.Accuracy).ForEach(x => stat += stat * x.StaticPower / 100.0f);
                this.m_Statuses.FindAll(x => x.Type == StatusEnum.EStatusType.Slowness).ForEach(x => stat -= stat * x.StaticPower / 100.0f);
                break;
            case StatusEnum.EStatusStatistics.Defense:
                stat = this.m_CharacterObject.ScriptableObject.Defense;
                this.m_Statuses.FindAll(x => x.Type == StatusEnum.EStatusType.Resistance).ForEach(x => stat += stat * x.StaticPower / 100.0f);
                this.m_Statuses.FindAll(x => x.Type == StatusEnum.EStatusType.Weakness).ForEach(x => stat -= stat * x.StaticPower / 100.0f);
                break;
            case StatusEnum.EStatusStatistics.Dodge:
                stat = this.m_CharacterObject.ScriptableObject.Dodge;
                this.m_Statuses.FindAll(x => x.Type == StatusEnum.EStatusType.Ghost).ForEach(x => stat += stat * x.StaticPower / 100.0f);
                this.m_Statuses.FindAll(x => x.Type == StatusEnum.EStatusType.Weakness).ForEach(x => stat -= stat * x.StaticPower / 100.0f);
                break;
            case StatusEnum.EStatusStatistics.Magic:
                stat = this.m_CharacterObject.ScriptableObject.Magic;
                this.m_Statuses.FindAll(x => x.Type == StatusEnum.EStatusType.Power).ForEach(x => stat += stat * x.StaticPower / 100.0f);
                this.m_Statuses.FindAll(x => x.Type == StatusEnum.EStatusType.Exhaustion).ForEach(x => stat -= stat * x.StaticPower / 100.0f);
                break;
            case StatusEnum.EStatusStatistics.Parry:
                stat = this.m_CharacterObject.ScriptableObject.Parry;
                this.m_Statuses.FindAll(x => x.Type == StatusEnum.EStatusType.Vigilance).ForEach(x => stat += stat * x.StaticPower / 100.0f);
                this.m_Statuses.FindAll(x => x.Type == StatusEnum.EStatusType.Weakness).ForEach(x => stat -= stat * x.StaticPower / 100.0f);
                break;
            case StatusEnum.EStatusStatistics.Speed:
                stat = this.m_CharacterObject.ScriptableObject.Speed;
                this.m_Statuses.FindAll(x => x.Type == StatusEnum.EStatusType.Haste).ForEach(x => stat += stat * x.StaticPower / 100.0f);
                this.m_Statuses.FindAll(x => x.Type == StatusEnum.EStatusType.Slowness).ForEach(x => stat -= stat * x.StaticPower / 100.0f);
                break;
            case StatusEnum.EStatusStatistics.Strength:
                stat = this.m_CharacterObject.ScriptableObject.Strength;
                this.m_Statuses.FindAll(x => x.Type == StatusEnum.EStatusType.Strength).ForEach(x => stat += stat * x.StaticPower / 100.0f);
                this.m_Statuses.FindAll(x => x.Type == StatusEnum.EStatusType.Exhaustion).ForEach(x => stat -= stat * x.StaticPower / 100.0f);
                break;
            default:
                return (-1);
        }
        return (stat);
    }

};