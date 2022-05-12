using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightCharacter : MonoBehaviour
{
    [SerializeField] private int m_Position;
    [SerializeField] private Character.ETeam m_Team;
    [SerializeField] private CharacterObject m_CharacterObject;
    [SerializeField] private GameObject m_Selected;
    private FightManager m_FightManager;
    private bool m_CanBeTargeted = false;
    public int Position => m_Position;
    public Character.ETeam Team => m_Team;
    public CharacterObject CharacterObject => m_CharacterObject;

    void Start()
    {
        m_FightManager = FightManager.Instance;
        m_Selected.SetActive(false);
    }

    public void SkillSelected(SkillData skill, CharacterObject user)
    {
        if (skill.Skill.Type == SkillEnum.ETargetType.Self)
        {
            if (user == m_CharacterObject)
            {
                CanTarget();
            }
            else
            {
                CancelTarget();
            }
        }
        if (skill.Skill.Type == SkillEnum.ETargetType.Ally)
        {
            if (m_CharacterObject.ScriptableObject.Team == Character.ETeam.Ally)
            {
                CanTarget();
            }
            else
            {
                m_Selected.SetActive(false);
            }
        }
        if (skill.Skill.Type == SkillEnum.ETargetType.Enemy)
        {
            if (m_CharacterObject.ScriptableObject.Team == Character.ETeam.Enemy)
            {
                CanTarget();
            }
            else
            {
                m_Selected.SetActive(false);
            }
        }
    }

    public void CanTarget()
    {
        m_CanBeTargeted = true;
        m_Selected.SetActive(true);
    }

    public void CancelTarget()
    {
        m_CanBeTargeted = false;
        m_Selected.SetActive(false);
    }

    public void SetCharacter(CharacterObject character)
    {
        m_CharacterObject = character;
    }

    void ShowAffected()
    {
        List<CharacterObject> targets = new List<CharacterObject>();
        if (Team == Character.ETeam.Ally)
            targets = m_FightManager.SelectedSkill.GetTargetedCharacters(CharacterObject, m_FightManager.AlliesObject);
        if (Team == Character.ETeam.Enemy)
            targets = m_FightManager.SelectedSkill.GetTargetedCharacters(CharacterObject, m_FightManager.EnemiesObject);
        targets.ForEach(x => {
            m_FightManager.FightCharacters.Find(y => x.Data.Position == y.Position && x.ScriptableObject.Team == y.Team).StartSelected();
        });
    }
    
    void OnMouseEnter()
    {
        if (m_FightManager.SelectedSkill != null && m_CanBeTargeted)
        {
            m_FightManager.FightCharacters.ForEach(x => x.StopSelected());
            ShowAffected();
        }
    }

    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0) && m_CanBeTargeted)
        {
            m_FightManager.DoAction(m_FightManager.SelectedSkill, m_CharacterObject);
        }
    }

    void OnMouseExit()
    {
        if (m_CanBeTargeted)
        {
            m_FightManager.FightCharacters.ForEach(x => x.StopSelected());
            m_FightManager.FightCharacters.ForEach(x => x.SkillSelected(m_FightManager.SelectedSkill, m_FightManager.CurrentTurn));
        }
    }

    public void StartSelected()
    {
        m_Selected.SetActive(true);
    }

    public void StopSelected()
    {
        m_Selected.SetActive(false);
    }
}
