using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectButton : MonoBehaviour
{
    [SerializeField] private Button m_Add;
    [SerializeField] private Button m_Minus;
    [SerializeField] private Text m_Text;
    [SerializeField] private Character m_Character;
    [SerializeField] private SelectTeam m_SelectTeam;
    public Character Character => m_Character;

    public void ShowNumber(int value)
    {
        m_Text.text = value.ToString();
    }

    public void AddCharacter()
    {
        m_SelectTeam.AddCharacter(m_Character);
    }

    public void RemoveCharacter()
    {
        m_SelectTeam.RemoveCharacter(m_Character);
    }

    public void StopAdd()
    {
        m_Add.interactable = false;
    }

    public void ResumeAdd()
    {
        m_Add.interactable = true;
    }

    public void StopMinus()
    {
        m_Minus.interactable = false;
    }

    public void ResumeMinus()
    {
        m_Minus.interactable = true;
    }

}
