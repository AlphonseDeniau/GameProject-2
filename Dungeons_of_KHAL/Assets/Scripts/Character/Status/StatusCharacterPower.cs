using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class StatusCharacterPower
{
    [Serializable]
    public class StatusSinglePower
    {

        public StatusEnum.EStatusStatistics m_Status;
        public int m_Value;
    }
    [SerializeField] private List<StatusSinglePower> m_Powers;

    /// <summary>
    /// Get value of a Data
    /// </summary>
    /// <param name="_stat">Type of stat we want</param>
    /// <returns>Value of the Data</returns>
    public int GetPower(StatusEnum.EStatusStatistics _stat)
    {
        if (m_Powers.Exists(x => x.m_Status == _stat))
        {
            return m_Powers.Find(x => x.m_Status == _stat).m_Value;
        }
        return 0;
    }
}
