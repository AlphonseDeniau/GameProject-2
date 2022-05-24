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
    [SerializeField] private StackEnum.EStackType m_StackType;

    [Header("Target")]
    [SerializeField] private SkillEnum.ETargetType m_Type;
    [SerializeField] private bool m_SelfIncluded;

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
    public StackEnum.EStackType StackType => m_StackType;
    public SkillEnum.ETargetType Type => m_Type;
    public bool SelfIncluded => m_SelfIncluded;
    public List<LevelEffect> LevelEffects => m_LevelEffects;

    // Methods \\
}