using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillDescription : MonoBehaviour
{

    [SerializeField] private Text m_Description;
    [SerializeField] private Text m_StackType;


    public void ModifyInformation(string description, string stackType)
    {
        m_Description.text = description;
        m_StackType.text = stackType;
    }

    public void ActiveText(bool active)
    {
        m_Description.gameObject.SetActive(active);
        m_StackType.gameObject.SetActive(active);
    }
}
