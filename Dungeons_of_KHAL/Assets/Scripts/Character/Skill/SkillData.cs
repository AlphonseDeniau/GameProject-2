using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class SkillData
{
    public SkillData(Skill _skill, int _level)
    {
        m_Skill = _skill;
        m_Level = _level;
    }
    [SerializeField] private int m_Level = 0;
    [SerializeField] private Skill m_Skill;
    public int Level => m_Level;
    public Skill Skill => m_Skill;


    /// <summary>
    /// Use the skill on target
    /// </summary>
    /// <param name="_user">character who use the skill</param>
    /// <param name="_target">character targeted by the skill</param>
    /// <returns></returns>
    public bool UseSkill(CharacterObject _user, CharacterObject _target, List<CharacterObject> _targetTeam)
    {
        Skill.LevelEffect _levelEffects = m_Skill.LevelEffects.Find(x => x.Level == m_Level);
        _levelEffects.SkillEffects.ForEach(x => x.ApplyEffect(_user, _target, _targetTeam));
        GetTargetedCharacters(_target, _targetTeam).ForEach(x => {
            if (x.Data.CheckStatusEffect(StatusEnum.EStatusType.Guard))
            {
                StackManager.Instance.ApplyStack(_user, x.Data.Statuses.Find(x => x.Type == StatusEnum.EStatusType.Guard).User, m_Skill.StackType, _user.ScriptableObject.Team == x.ScriptableObject.Team ? StackEnum.EEffectType.Positive : StackEnum.EEffectType.Negative);
            }
            else
            {
                StackManager.Instance.ApplyStack(_user, x, m_Skill.StackType, _user.ScriptableObject.Team == x.ScriptableObject.Team ? StackEnum.EEffectType.Positive : StackEnum.EEffectType.Negative);
            }
        });
        _user.Data.LoseMP(_levelEffects.Cost);
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
        List<SkillEffect> _effects = m_Skill.LevelEffects.Find(x => x.Level == m_Level).SkillEffects;
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
        Skill.LevelEffect _levelEffects = m_Skill.LevelEffects.Find(x => x.Level == m_Level);
        return _user.Data.ActualMP > _levelEffects.Cost;
    }

    /// <summary>
    /// Get the actual level effect
    /// </summary>
    /// <returns>level effect</returns>
    public Skill.LevelEffect GetLevelEffect()
    {
        Skill.LevelEffect _levelEffects = m_Skill.LevelEffects.Find(x => x.Level == m_Level);
        return _levelEffects;
    }
}
