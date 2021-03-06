using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackManager : Singleton<StackManager>
{
    [SerializeField] private List<StackCombinaison> m_Combinaisons;

    // Accessors \\
    public List<StackCombinaison> Combinaisons => m_Combinaisons;

    // Methods \\

    /// <summary>
    /// Apply stack effect on the target
    /// </summary>
    /// <param name="_target">Character who receive the stack</param>
    /// <param name="_type">Type of the stack</param>
    /// <returns>True if stack effect trigger</returns>
    public bool ApplyStack(CharacterObject _user, CharacterObject _target, StackEnum.EStackType _stackType, StackEnum.EEffectType _effectType)
    {
        if (_target.Data.StackType == StackEnum.EStackType.None || _target.Data.StackType == StackEnum.EStackType.Neutral)
            _target.Data.StackType = _stackType;
        else if (_stackType != StackEnum.EStackType.None && _stackType != StackEnum.EStackType.Neutral)
            ApplyStackEffect(_user, _target, _stackType, _effectType);
        return true;
    }

    /// <summary>
    /// Apply specific effect of the combinaison
    /// </summary>
    /// <param name="_target">Character who receive the stack</param>
    /// <param name="_type">Type of stack added</param>
    /// <returns></returns>
    private bool ApplyStackEffect(CharacterObject _user, CharacterObject _target, StackEnum.EStackType _stackType, StackEnum.EEffectType _effectType)
    {
        for (int i = 0; i < m_Combinaisons.Count; i++)
        {
            if (m_Combinaisons[i].HasTypes(_target.Data.StackType, _stackType) && m_Combinaisons[i].EffectType == _effectType)
            {
                StatusEffect _effect = new StatusEffect();
                _effect = m_Combinaisons[i].Effect;
                _effect.ChooseTarget(_user, _target);
                _target.Data.AddStatusEffect(_effect);
                break;
            }
        }

        _target.Data.StackType = StackEnum.EStackType.None;
        return true;
    }
}
