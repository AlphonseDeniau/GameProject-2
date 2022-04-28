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
    [SerializeField] private GameObject m_MouseOverObject;
    [SerializeField] private Text m_DescriptionText;
    [SerializeField] private Text m_StackTypeText;

    void OnEnable()
    {
        if (FightManager.Instance != null && FightManager.Instance.CurrentTurn != null)
        {
            if (m_Index < FightManager.Instance.CurrentTurn.ScriptableObject.Skills.Count) {
                m_NameText.text = FightManager.Instance.CurrentTurn.ScriptableObject.Skills[m_Index].SkillName;
                m_MPText.text = "MP: " + FightManager.Instance.CurrentTurn.ScriptableObject.Skills[m_Index].GetLevelEffect().Cost;
            }
            else
            {
                gameObject.SetActive(false);
            }
        }       
    }

    public void SelectSkill()
    {
        if (FightManager.Instance.CurrentTurn.ScriptableObject.Skills[m_Index].IsUsable(FightManager.Instance.CurrentTurn))
            FightManager.Instance.SelectSkill(FightManager.Instance.CurrentTurn.ScriptableObject.Skills[m_Index]);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        m_MouseOverObject.SetActive(true);
        if (m_Index < FightManager.Instance.CurrentTurn.ScriptableObject.Skills.Count) {
            m_DescriptionText.text = FightManager.Instance.CurrentTurn.ScriptableObject.Skills[m_Index].Description;
            m_StackTypeText.text = "Stack type:\n" + FightManager.Instance.CurrentTurn.ScriptableObject.Skills[m_Index].StackType.ToString();
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        m_MouseOverObject.SetActive(false);
    }
}
