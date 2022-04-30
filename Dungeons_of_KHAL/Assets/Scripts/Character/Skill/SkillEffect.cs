using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class SkillEffect
{
    [Header("Properties")]
    [SerializeField] private SkillArea m_Area;
    [SerializeField] private StatusEffect m_Effect;


    // Accessors \\
    public SkillArea Area => m_Area;
    public StatusEffect Effect => m_Effect;


    // Methods \\

    /// <summary>
    /// Apply effect on target
    /// </summary>
    /// <param name="_user">character who use the skill</param>
    /// <param name="_target">character targeted by the skill</param>
    /// <returns></returns>
    public bool ApplyEffect(CharacterObject _user, CharacterObject _target, List<CharacterObject> _targetTeam)
    {
        List<CharacterObject> _targets = m_Area.GetTargetedCharacters(_target, _targetTeam);
        _targets.ForEach(x => ApplyStatus(_user, x));
        return true;
    }

    /// <summary>
    /// Apply status to target
    /// </summary>
    /// <param name="_user">character who use the skill</param>
    /// <param name="_target">character targeted by the skill</param>
    /// <returns></returns>
    private bool ApplyStatus(CharacterObject _user, CharacterObject _target)
    {
        if (m_Effect == null) return false;
        // Choose if who will have the effect link
        m_Effect.ChooseTarget(_user, _target);
        // Try to apply the Immediate Status effect
        if (!m_Effect.ApplyImmediateStatus(_user, _target))
        {
            // False add the effect in the character targeted
            _target.Data.AddStatusEffect(m_Effect);
        }
        return true;
    }
}
