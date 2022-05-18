using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedParts : MonoBehaviour
{
    [SerializeField] private SelectorsManager m_SelectorManager;
    [SerializeField] private SelectorsManager.E_SelectedPart m_SelectValue;

    void Start()
    {
        if (m_SelectorManager.SelectedPart != m_SelectValue)
        {
            gameObject.SetActive(false);
        }
        if (m_SelectorManager.SelectedPart == m_SelectValue)
        {
            gameObject.SetActive(true);
        }
    }

    public void NewValue()
    {
        if (m_SelectorManager.SelectedPart != m_SelectValue)
        {
            gameObject.SetActive(false);
        }
        if (m_SelectorManager.SelectedPart == m_SelectValue)
        {
            gameObject.SetActive(true);
        }
    }
}
