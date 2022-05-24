using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnIcon : MonoBehaviour
{
    [SerializeField] private FightCharacter m_FightCharacter;
    public FightCharacter FightCharacter => m_FightCharacter;
    private GameObject m_Sprite;

    public void UpdateIcon()
    {
        if (m_FightCharacter.CharacterObject)
        {
            m_Sprite = Instantiate(m_FightCharacter.CharacterObject.ScriptableObject.Icon);
            m_Sprite.transform.SetParent(this.gameObject.transform);
            m_Sprite.transform.localPosition = new Vector3(0, 0, 0);
        }
    }

    public void RemoveIcon()
    {
        if (m_Sprite)
            DestroyImmediate(m_Sprite);
    }

    public void HideIcon()
    {
        if (m_Sprite)
            m_Sprite.SetActive(false);
    }

    public void ShowIcon()
    {
        if (m_Sprite)
            m_Sprite.SetActive(true);
    }
}
