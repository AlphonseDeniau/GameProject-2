using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SkillButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private int m_Index;
    [SerializeField] private Text m_NameText;
    [SerializeField] private Text m_MPText;
    [SerializeField] private SkillDescription m_Description;
    
    private Button m_Button;
    private FightManager m_FightManager;

    void Awake()
    {
        m_FightManager = FightManager.Instance;
        m_Button = GetComponent<Button>();
    }

    void OnEnable()
    {
        if (m_FightManager != null && m_FightManager.CurrentTurn != null)
        {
            CharacterObject _currentFighter = m_FightManager.CurrentTurn;

            if (m_Index < _currentFighter.ScriptableObject.Skills.Count) {
                m_NameText.text = _currentFighter.ScriptableObject.Skills[m_Index].SkillName;
                m_MPText.text = "MP: " + _currentFighter.Data.Skills[m_Index].GetLevelEffect().Cost;
                if (_currentFighter.Data.Skills[m_Index].IsUsable(_currentFighter))
                    m_Button.interactable = true;
                else
                    m_Button.interactable = false;
            }
            else
            {
                gameObject.SetActive(false);
            }
        }       
    }

    public void SelectSkill()
    {
        if (m_FightManager != null && m_FightManager.CurrentTurn != null)
        {
            CharacterObject _currentFighter = m_FightManager.CurrentTurn;

            if (_currentFighter.Data.Skills[m_Index].IsUsable(_currentFighter))
            {
                m_FightManager.SelectSkill(_currentFighter.Data.Skills[m_Index]);
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (m_FightManager != null && m_FightManager.CurrentTurn != null)
        {
            CharacterObject _currentFighter = m_FightManager.CurrentTurn;
            m_Description.ActiveText(true);

            if (m_Index < _currentFighter.ScriptableObject.Skills.Count)
            {
                Skill _skill = _currentFighter.ScriptableObject.Skills[m_Index];
                m_Description.ModifyInformation(_skill.Description, "Stack type: \n" + _skill.StackType.ToString());
            }
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        m_Description.ActiveText(false);
    }
}
