using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectTeam : MonoBehaviour
{
    [SerializeField] private List<SelectButton> m_Buttons;
    [SerializeField] private List<Character> m_Characters = new List<Character>();
    [SerializeField] private GameObject m_Prefab;

    void Start()
    {
        m_Buttons.ForEach(x => x.ResumeAdd());
        m_Buttons.ForEach(x => x.StopMinus());
    }

    public void LaunchExpedition()
    {
        int i = 0;
        m_Characters.ForEach(x => {
            GameObject tmp = Instantiate(m_Prefab);
            tmp.GetComponent<CharacterObject>().Initialize(x);
            tmp.GetComponent<CharacterObject>().Data.Position = i;            
            GameManager.Instance.ExplorationCharacterManager.AddCharacter(tmp.GetComponent<CharacterObject>(), Character.ETeam.Ally);
            tmp.transform.SetParent(GameManager.Instance.ExplorationCharacterManager.transform);
            i++;
        });
        GameManager.Instance.ChangeScene("FightScene");
    }

    public void AddCharacter(Character character)
    {
        m_Characters.Add(character);
        m_Buttons.Find(x => x.Character == character).ResumeMinus();
        if (m_Characters.Count >= 4)
        {
            m_Buttons.ForEach(x => x.StopAdd());
        }
    }

    public void RemoveCharacter(Character character)
    {
        m_Characters.Remove(character);
        if (!m_Characters.Contains(character))
            m_Buttons.Find(x => x.Character == character).StopMinus();
        m_Buttons.ForEach(x => x.ResumeAdd());
    }
}
