using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class DungeonMiddlePanel : MonoBehaviour
{
    [Serializable]
    public class Panel
    {
        public EUIPanel m_Type;
        public DungeonMiddlePanelButton m_Button;
        public GameObject m_Panel;
    }

    [SerializeField] private EUIPanel m_CurrentType = EUIPanel.Skill;
    [SerializeField] private List<Panel> m_Panels = new List<Panel>();

    private void OnEnable()
    {
        ActivePanel(m_CurrentType);
    }

    public void ActivePanel(EUIPanel type)
    {
        if (m_CurrentType != type)
        {
            FightManager.Instance.SelectSkill(null);
        }

        m_CurrentType = type;
        m_Panels.ForEach(x => {
            x.m_Panel.SetActive(x.m_Type == type);
            x.m_Button.SetButtonActive(x.m_Type == type);
        });
    }

    public void ActivePanel(int enumIndex)
    {
        if (m_CurrentType != (EUIPanel)enumIndex)
        {
            FightManager.Instance.SelectSkill(null);
        }
        EUIPanel type = (EUIPanel)enumIndex;
        m_CurrentType = type;
        m_Panels.ForEach(x => {
            x.m_Panel.SetActive(x.m_Type == type);
            x.m_Button.SetButtonActive(x.m_Type == type);
        });
    }

    public GameObject GetCurrentPanel()
    {
        if (m_Panels.Exists(x => x.m_Type == m_CurrentType))
            return m_Panels.Find(x => x.m_Type == m_CurrentType).m_Panel;
        return null;
    }

    public GameObject GetPanel(EUIPanel type)
    {
        if (m_Panels.Exists(x => x.m_Type == type))
            return m_Panels.Find(x => x.m_Type == type).m_Panel;
        return null;
    }

    public void ActiveMiddlePanel(bool active)
    {
        m_Panels.ForEach(x => {
            x.m_Panel.SetActive(false);
            x.m_Button.m_Button.interactable = false;
        });

        if (active)
        {
            ActivePanel(m_CurrentType);
        }
    }
}
