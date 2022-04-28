using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Skill", menuName = "ScriptableObjects/Skill", order = 2)]
public class Skill : ScriptableObject
{
    [Header("Properties")]
    [SerializeField] private string m_SkillName;
    [SerializeField] private string m_Description;
    [SerializeField] private int m_Level;
    [SerializeField] private StackEnum.EStackType m_StackType;

    [Header("Target")]
    [SerializeField] private SkillEnum.ETargetType m_Type;

    [Header("Effects")]
    [SerializeField] private List<LevelEffect> m_LevelEffects;

    [Serializable]
    public class LevelEffect
    {
        [SerializeField] private int m_Level;
        [SerializeField] private int m_Cost;
        [SerializeField] private List<SkillEffect> m_SkillEffects;

        // Accessors \\
        public int Level => m_Level;
        public List<SkillEffect> SkillEffects => m_SkillEffects;
        public int Cost => m_Cost;
    }

    // Accessors \\
    public string SkillName => m_SkillName;
    public string Description => m_Description;
    public int Level { get { return m_Level; } set { m_Level = value; } }
    public StackEnum.EStackType StackType => m_StackType;
    public SkillEnum.ETargetType Type => m_Type;

    // Methods \\

    /// <summary>
    /// Use the skill on target
    /// </summary>
    /// <param name="_user">character who use the skill</param>
    /// <param name="_target">character targeted by the skill</param>
    /// <returns></returns>
    public bool UseSkill(CharacterObject _user, CharacterObject _target, List<CharacterObject> _targetTeam)
    {
        LevelEffect _levelEffects = m_LevelEffects.Find(x => x.Level == m_Level);
        _levelEffects.SkillEffects.ForEach(x => x.ApplyEffect(_user, _target, _targetTeam));
        return true;
    }

    /// <summary>
    /// Get all the targeted Characters
    /// </summary>
    /// <param name="_user">character who use the skill</param>
    /// <param name="_target">character targeted by the skill</param>
    /// <returns>List of all targets</returns>
    public List<CharacterObject> GetTargetedCharacters(CharacterObject _target, List<CharacterObject> _targetTeam)
    {
        List<CharacterObject> _result = new List<CharacterObject>();
        List<SkillEffect> _effects = m_LevelEffects.Find(x => x.Level == m_Level).SkillEffects;
        for (int i = 0; i < _effects.Count; i++)
        {
            List<CharacterObject> _targets = _effects[i].Area.GetTargetedCharacters(_target, _targetTeam);
            _result.AddRange(_targets.FindAll(x => !_result.Contains(x)));
        }
        return _result;
    }

    /// <summary>
    /// Check if the skill is usable
    /// </summary>
    /// <param name="_user">Main character</param>
    /// <returns>True if he can cast the skill</returns>
    public bool IsUsable(CharacterObject _user)
    {
        LevelEffect _levelEffects = m_LevelEffects.Find(x => x.Level == m_Level);
        return _user.Data.ActualMP < _levelEffects.Cost;
    }

    /// <summary>
    /// Get the actual level effect
    /// </summary>
    /// <returns>level effect</returns>
    public LevelEffect GetLevelEffect()
    {
        LevelEffect _levelEffects = m_LevelEffects.Find(x => x.Level == m_Level);
        return _levelEffects;
    }
}