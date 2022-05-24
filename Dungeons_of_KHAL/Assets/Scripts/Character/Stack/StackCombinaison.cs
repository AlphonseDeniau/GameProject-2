using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class StackCombinaison
{
    [SerializeField] private StackEnum.EEffectType m_EffectType;
    [SerializeField] private StackEnum.EStackType m_StackType1;
    [SerializeField] private StackEnum.EStackType m_StackType2;
    [SerializeField] private StatusEffect m_Effect;

    // Accessors \\
    public StackEnum.EEffectType EffectType => m_EffectType;
    public StackEnum.EStackType StackType1 => m_StackType1;
    public StackEnum.EStackType StackType2 => m_StackType2;
    public StatusEffect Effect => m_Effect;

    // Methods \\

    /// <summary>
    /// Check if the combinaison has this Type
    /// </summary>
    /// <param name="_type">Type of Stack</param>
    /// <returns>True if the combinaison contains this type</returns>
    public bool HasType(StackEnum.EStackType _type)
    {
        return (m_StackType1 == _type || m_StackType2 == _type);
    }

    /// <summary>
    /// Check if the combinaison has the two types
    /// </summary>
    /// <param name="_type1">First type of Stack</param>
    /// <param name="_type2">Second type of Stack</param>
    /// <returns>True if Combinaison types equal to parameters</returns>
    public bool HasTypes(StackEnum.EStackType _type1, StackEnum.EStackType _type2)
    {
        return ((m_StackType1 == _type1 && m_StackType2 == _type2) || (m_StackType1 == _type2 && m_StackType2 == _type1));
    }
}
