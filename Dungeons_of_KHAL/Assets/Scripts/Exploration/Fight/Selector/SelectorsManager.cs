using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SelectorsManager : MonoBehaviour
{
    [Serializable]
    public enum E_SelectedPart {
        Inventory,
        Skills
    };
    [SerializeField] private E_SelectedPart m_SelectedPart;
    [SerializeField] private List<SelectorButtons> m_Buttons;
    [SerializeField] private List<SelectedParts> m_Parts;
    public E_SelectedPart SelectedPart => m_SelectedPart;

    public void ChangeValue(E_SelectedPart selectedPart)
    {
        m_SelectedPart = selectedPart;
        m_Buttons.ForEach((button) => button.NewValue());
        m_Parts.ForEach((part) => part.NewValue());
    }
}
