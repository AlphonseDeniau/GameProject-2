using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BestiaryManager : MonoBehaviour
{
    public enum MonsterType
    {
        Weak,
        Strong,
        Boss,
    }

    [Serializable]
    public class Monster
    {
        public MonsterType m_Type;
        public Character m_Monster;
    }

    [Header("Monster Available")]
    [SerializeField] private List<Monster> m_Monsters = new List<Monster>();

    public List<Character> CreateMonster(int monsterNumber)
    {
        List<Character> monsters = new List<Character>();
        List<Monster> monsterList = m_Monsters.FindAll(x => x.m_Type != MonsterType.Boss);

        for (int i = 0; i < monsterNumber; i++)
        {
            int random = UnityEngine.Random.Range(0, monsterList.Count);
            monsters.Add(monsterList[random].m_Monster);
        }
        return monsters;
    }
}
