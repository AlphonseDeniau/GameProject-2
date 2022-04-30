using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectorButtons : MonoBehaviour
{
    [SerializeField] private SelectorsManager m_SelectorManager;
    [SerializeField] private Image m_Image;
    private bool m_Activated;
    [SerializeField] private SelectorsManager.E_SelectedPart m_SelectValue;
    [SerializeField] private Color m_ColorOn = new Color(0.5f, 0.5f, 0.5f, 1.0f);
    [SerializeField] private Color m_ColorOff = new Color(1.0f, 1.0f, 1.0f, 1.0f);

    void Start()
    {
        if (m_SelectorManager.SelectedPart != m_SelectValue)
        {
            ChangeColor(m_ColorOff);
            m_Activated = false;
        }
        if (m_SelectorManager.SelectedPart == m_SelectValue)
        {
            ChangeColor(m_ColorOn);
            m_Activated = true;
        }
    }

    public void NewValue()
    {
        if (m_Activated && m_SelectorManager.SelectedPart != m_SelectValue)
        {
            ChangeColor(m_ColorOff);
            m_Activated = false;
        }
        if (!m_Activated && m_SelectorManager.SelectedPart == m_SelectValue)
        {
            ChangeColor(m_ColorOn);
            m_Activated = true;
        }
    }

    void ChangeColor(Color color)
    {
        m_Image.color = color;
    }

    public void Clicked()
    {
        m_SelectorManager.ChangeValue(m_SelectValue);
    }
}
