using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightCharacter : MonoBehaviour
{
    [SerializeField] private int m_Position;
    [SerializeField] private Character.ETeam m_Team;
    [SerializeField] private CharacterObject m_CharacterObject;
    [SerializeField] private GameObject m_Selected;
    [SerializeField] private StatBar m_HPBar;
    [SerializeField] private StatBar m_MPBar;
    private FightManager m_FightManager;
    private GameObject m_Sprite;
    private bool m_IsActive = true;
    private bool m_CanBeTargeted = false;
    public int Position => m_Position;
    public Character.ETeam Team => m_Team;
    public CharacterObject CharacterObject => m_CharacterObject;

    void Start()
    {
        m_FightManager = FightManager.Instance;
        m_Selected.SetActive(false);
    }

    void Awake()
    {
        m_FightManager = FightManager.Instance;
        m_Selected.SetActive(false);
    }

    public void SkillSelected(SkillData skill, CharacterObject user)
    {
        if (m_IsActive && !m_CharacterObject.Data.IsDead)
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
    }

    public void CanTarget()
    {
        if (m_IsActive && !m_CharacterObject.Data.IsDead)
        {
            m_CanBeTargeted = true;
            m_Selected.SetActive(true);
        }
    }

    public void CancelTarget()
    {
        m_CanBeTargeted = false;
        m_Selected.SetActive(false);
    }

    public void SetCharacter(CharacterObject character)
    {
        m_IsActive = true;
        GetComponent<BoxCollider2D>().enabled = true;
        m_CharacterObject = character;
        m_Sprite = Instantiate(m_CharacterObject.ScriptableObject.Model);
        m_Sprite.transform.SetParent(this.gameObject.transform);
        m_Sprite.transform.localPosition = new Vector3(0,-0.5f,0);
        m_Sprite.transform.localScale = new Vector3(0.8f,0.8f,0.8f);
    }

    public void NoCharacter()
    {
        m_IsActive = false;
        GetComponent<BoxCollider2D>().enabled = false;
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
            m_FightManager.DoAction(m_FightManager.SelectedSkill, m_CharacterObject, false);
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
        if (m_IsActive && !m_CharacterObject.Data.IsDead)
        {
            m_Selected.SetActive(true);
        }
    }

    public void StopSelected()
    {
        if (m_IsActive && !m_CharacterObject.Data.IsDead)
        {
            m_Selected.SetActive(false);
        }
    }

    public void UpdateStat()
    {
        if (m_CharacterObject)
        {
            m_HPBar.UpdateStat(m_CharacterObject.ScriptableObject.MaxHP, m_CharacterObject.Data.ActualHP);
            m_MPBar.UpdateStat(m_CharacterObject.ScriptableObject.MaxMP, m_CharacterObject.Data.ActualMP);
        }
    }

    public void HideSprite()
    {
        if (m_Sprite)
            m_Sprite.SetActive(false);
        if (m_HPBar)
            m_HPBar.gameObject.SetActive(false);
        if (m_MPBar)
            m_MPBar.gameObject.SetActive(false);
    }

    public void ShowSprite()
    {
        if (m_Sprite)
            m_Sprite.SetActive(true);
        if (m_HPBar)
            m_HPBar.gameObject.SetActive(true);
        if (m_MPBar)
            m_MPBar.gameObject.SetActive(true);
    }
}
