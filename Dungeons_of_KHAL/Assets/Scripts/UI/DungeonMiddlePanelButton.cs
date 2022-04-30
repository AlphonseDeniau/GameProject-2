using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DungeonMiddlePanelButton : MonoBehaviour
{
    [Header("Parameters Image")]
    private Image m_Image;
    [SerializeField] private Color m_ColorActive = Color.white;
    [SerializeField] private Color m_ColorInactive = Color.white;

    [Header("Parameters Text")]
    [SerializeField] private Text m_Text;
    [SerializeField] private Color m_ColorActiveText = Color.white;
    [SerializeField] private Color m_ColorInactiveText = Color.white;

    private bool m_Active = false;

    private void Awake()
    {
        m_Image = GetComponent<Image>();
    }

    public void SetButtonActive(bool active)
    {
        m_Active = active;
        m_Image.color = active ? m_ColorActive : m_ColorInactive;
        m_Text.color = active ? m_ColorActiveText : m_ColorInactiveText;
    }

    public void ChangeText(string text)
    {
        m_Text.text = text;
    }
}
