using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class StatusWindow : MonoBehaviour
{
    [Serializable] private class StatusTextCombination
    {
        [SerializeField] private StatusEnum.EStatusType m_StatusType;
        [SerializeField] private String m_Text;
        public StatusEnum.EStatusType StatusType => m_StatusType;
        public String Text => m_Text;
    }
    [SerializeField] private List<StatusTextCombination> m_Combinations;
    [SerializeField] private Text m_Text;

    public void SetStatus(List<StatusEffect> _statuses)
    {
        m_Text.text = "";
        _statuses.ForEach(x => {
            m_Text.text += x.Type + " - ";
            if (m_Combinations.Find(y => y.StatusType == x.Type) != null)
                m_Text.text += m_Combinations.Find(y => y.StatusType == x.Type).Text;
            m_Text.text += "\n";
        });
    }
}
