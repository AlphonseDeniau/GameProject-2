using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillButtonManager : MonoBehaviour
{
    [SerializeField] private int m_Index = -1;
    [SerializeField] private List<SkillButton> m_Buttons = new List<SkillButton>();

    private FightManager m_FightManager;

    private void OnEnable()
    {
        m_FightManager = FightManager.Instance;
        if (m_FightManager != null && m_FightManager.CurrentTurn != null)
        {
            CharacterObject _currentFighter = m_FightManager.CurrentTurn;
            m_Buttons.ForEach(x => x.SetInformation(_currentFighter));
        }
    }

    private void OnDisable()
    {
        ResetButtons();
    }

    public void ResetButtons()
    {
        m_Index = -1;
        m_Buttons.ForEach(x => x.UpdateGraphical(m_Index));
    }

    public void SelectButton(int index)
    {
        m_Index = index;
        m_Buttons.ForEach(x => x.UpdateGraphical(m_Index));
        m_Buttons.Find(x => x.Index == index).SelectSkill();
    }
}
