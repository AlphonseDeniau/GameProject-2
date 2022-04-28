using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Character", menuName = "ScriptableObjects/Character", order = 1)]
public class Character : ScriptableObject
{
    public enum ETeam {
        Ally,
        Enemy
    };

    [Header("Team Selection")]
    [SerializeField] private ETeam m_Team;
    public ETeam Team => m_Team;
    [Header("Basics Statistics")]
    [SerializeField] private int m_MaxHP;
    [SerializeField] private int m_MaxMP;
    [SerializeField] private int m_Strength;
    [SerializeField] private int m_Defense;
    [SerializeField] private int m_Magic;
    [SerializeField] private int m_Speed;
    [SerializeField] private int m_Dodge;
    [SerializeField] private int m_Critical;
    [SerializeField] private int m_Parry;

    [Header("Skills List")]
    [SerializeField] private List<Skill> m_Skills;

    // Accessors \\
    public int MaxHP => m_MaxHP;
    public int MaxMP => m_MaxMP;
    public int Strength => m_Strength;
    public int Defense => m_Defense;
    public int Magic => m_Magic;
    public int Speed => m_Speed;
    public int Dodge => m_Dodge;
    public int Critical => m_Critical;
    public int Parry => m_Parry;
    public List<Skill> Skills => m_Skills;
}
