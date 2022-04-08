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

    [Header("Current Statistics")]
    [SerializeField] private int m_ActualHP;
    [SerializeField] private int m_ActualMP;
    [SerializeField] private int m_Position;
    [SerializeField] private bool m_IsDead;

    [Header("Skills List")]
    [SerializeField] private List<Skill> m_Skills;

    // Statuses Effects
    [Header("Dont Touch")]
    [SerializeField] private List<StatusEffect> m_Statuses;
    [SerializeField] private StackEnum.EStackType m_StackType;

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
    public int ActualHP => m_ActualHP;
    public int ActualMP => m_ActualMP;
    public int Position => m_Position;
    public bool IsDead => m_IsDead;

    public List<Skill> Skills => m_Skills;
    public StackEnum.EStackType StackType { get { return m_StackType; } set { m_StackType = value; } }


    /// <summary>
    /// Initialize character when we create it
    /// </summary>
    /// <returns></returns>
    public bool Initialize()
    {
        m_ActualHP = m_MaxHP;
        m_ActualMP = m_MaxMP;
        m_Position = 0;
        m_IsDead = false;
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
    public bool UseSkill(Skill _skill, Character _target, List<Character> _targetTeam)
    {
        if (!m_Skills.Contains(_skill)) return false;
        if (!_skill.IsUsable(this)) return false;
        return _skill.UseSkill(this, _target, _targetTeam);
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
        if (m_ActualHP > m_MaxHP) m_ActualHP = m_MaxHP;
        return true;
    }

    /// <summary>
    /// Add a status effect on the character
    /// </summary>
    /// <param name="_effect">Adding effect</param>
    /// <returns></returns>
    public bool AddStatusEffect(StatusEffect _effect)
    {
        foreach (StatusEffect status in m_Statuses)
        {
            // Check if effect is already make
            // if yes change by the new effect
            // if not add the effect to list
        }
        return true;
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
}
