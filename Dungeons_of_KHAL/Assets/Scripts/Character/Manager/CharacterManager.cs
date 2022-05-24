using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    [SerializeField] private GameObject m_Prefab;
    [SerializeField] private List<CharacterObject> m_Allies = new List<CharacterObject>();
    [SerializeField] private List<CharacterObject> m_Enemies = new List<CharacterObject>();

    public List<CharacterObject> Allies { get { return m_Allies; }  set { m_Allies = value; } }
    public List<CharacterObject> Enemies { get { return m_Enemies; } set { m_Enemies = value; } }

    public void AddCharacter(CharacterObject obj, Character.ETeam team)
    {
        if (team == Character.ETeam.Ally)
            m_Allies.Add(obj);
        else if (team == Character.ETeam.Enemy)
            m_Enemies.Add(obj);
    }

    public void AddCharacter(Character newCharacter, Character.ETeam team)
    {
        GameObject go = Instantiate(m_Prefab, this.transform);
        go.transform.SetParent(this.transform);

        CharacterObject obj = go.GetComponent<CharacterObject>();
        if (obj != null)
        {
            obj.Initialize(newCharacter);
            obj.Data.Position = team == Character.ETeam.Ally ? m_Allies.Count : m_Enemies.Count;
        }

        if (team == Character.ETeam.Ally)
            m_Allies.Add(obj);
        else if (team == Character.ETeam.Enemy)
            m_Enemies.Add(obj);
    }

    public void RemoveCharacter(CharacterObject obj, Character.ETeam team)
    {
        if (team == Character.ETeam.Ally)
            m_Allies.Remove(obj);
        else if (team == Character.ETeam.Enemy)
            m_Enemies.Remove(obj);
    }

    public void ClearCharacter()
    {
        m_Allies.Clear();
        m_Enemies.Clear();
    }
}
