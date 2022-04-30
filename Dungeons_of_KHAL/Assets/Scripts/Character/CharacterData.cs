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

    public int ActualHP => m_ActualHP;
    public int ActualMP => m_ActualMP;
    public int Position => m_Position;
    public bool IsDead => m_IsDead;
    public StackEnum.EStackType StackType { get { return m_StackType; } set { m_StackType = value; } }


    /// <summary>
    /// Initialize character when we create it
    /// </summary>
    /// <returns></returns>
    public bool Initialize(CharacterObject character)
    {
        m_ActualHP = character.ScriptableObject.MaxHP;
        m_ActualMP = character.ScriptableObject.MaxMP;
        m_Position = 0;
        m_IsDead = false;
        m_CharacterObject = character;
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
    public bool UseSkill(Skill _skill, CharacterObject _target, List<CharacterObject> _targetTeam)
    {
        if (!m_CharacterObject.ScriptableObject.Skills.Contains(_skill)) return false;
        if (!_skill.IsUsable(m_CharacterObject)) return false;
        return _skill.UseSkill(m_CharacterObject, _target, _targetTeam);
    }

    /// <summary>
    /// Decrease current HP of the character
    /// </summary>
    /// <param name="_damage">value of damage</param>
    /// <returns>True: if the character is still alivet</returns>
    public bool TakeDamage(int _damage)
    {
        // MAKE A BETTER CALCULATION BITCHES
        if (_damage < 0) return true;
        m_ActualHP -= _damage;
        return _damage > 0;
    }

    /// <summary>
    /// Increase current HP of the character 
    /// </summary>
    /// <param name="_heal">value of heal</param>
    /// <returns>True: if the character is still alive</returns>
    public bool TakeHeal(int _heal)
    {
        if (m_ActualHP <= 0) return false;
        m_ActualHP += _heal;
        if (m_ActualHP > m_CharacterObject.ScriptableObject.MaxHP) m_ActualHP = m_CharacterObject.ScriptableObject.MaxHP;
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
                    m_Statuses.Add(_effect);
                }
                return true;
            }
            else
                return false;
        }
        else
        {
            m_Statuses.Add(_effect);
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
            // Only remove the status
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
    /// <returns></returns>
    public bool UpdateStatus(float _time)
    {
        List<StatusEffect> l_EffectToRemove = new List<StatusEffect>();

        foreach (StatusEffect l_Status in this.m_Statuses)
        {
            if (l_Status.DurationType == StatusEnum.EStatusDurationType.Second)
            {
                if (l_Status.UpdateDuration(_time))
                {
                    l_EffectToRemove.Add(l_Status);
                }
            }
        }
        this.ClearStatusEffect(l_EffectToRemove);
        return true;
    }
};