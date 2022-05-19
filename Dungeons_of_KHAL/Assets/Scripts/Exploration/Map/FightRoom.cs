using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightRoom : ARoom
{
    [SerializeField] private List<Character> m_Characters = new List<Character>();

    private CharacterManager m_CharacterManager;
    private DungeonManager m_Dungeon;
    private BestiaryManager m_BestiaryManager;


    public override void InitRoom()
    {
        m_Dungeon = DungeonManager.Instance;
        m_CharacterManager = m_Dungeon.CharacterManager;
        m_BestiaryManager = m_Dungeon.Bestiary;

        if (m_Characters.Count == 0)
        {
            int numberEnemies = Random.Range(8, 10);
            List<Character> characters = m_BestiaryManager.CreateMonster(numberEnemies);
            m_Characters = characters;
        }
    }

    public override void EnterRoom()
    {
        Debug.Log("Enter in room: " + m_Index + ": FIGHT");
        UpdateImage(true);

        if (!m_Explored)
        {
            m_Characters.ForEach(x => m_CharacterManager.AddCharacter(x, Character.ETeam.Enemy));
            m_Dungeon.StartFight();
        }
        m_Explored = true;
    }

    public override void LeaveRoom()
    {
        Debug.Log("Leave this room: " + m_Index);
        UpdateImage(false);
    }
}
