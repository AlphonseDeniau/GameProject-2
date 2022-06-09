using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatUI : MonoBehaviour
{
    [SerializeField] private Text Name;

    [SerializeField] private Text m_HP;
    [SerializeField] private Text m_MP;

    [SerializeField] private Text m_STR;
    [SerializeField] private Text m_DEF;
    [SerializeField] private Text m_MAG;
    [SerializeField] private Text m_SPD;


    public void UpdateText(string name, int currentHP, int maxHP, int currentMP, int maxMP, int str, int def, int mag, int spd)
    {
        Name.text = name;
        m_HP.text = "HP: " + currentHP.ToString() + "/" + maxHP.ToString();
        m_MP.text = "MP: " + currentMP.ToString() + "/" + maxMP.ToString();
        m_STR.text = "Strength: " + str.ToString();
        m_DEF.text = "Defence: " + def.ToString();
        m_MAG.text = "Magic: " + mag.ToString();
        m_SPD.text = "Speed: " + spd.ToString();
    }
}
