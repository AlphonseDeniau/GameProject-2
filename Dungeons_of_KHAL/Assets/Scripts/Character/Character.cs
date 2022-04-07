using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Character", menuName = "ScriptableObjects/Character", order = 1)]
public class Character : ScriptableObject
{
    public enum teamList {
        ally,
        enemy
    };

    public int position;
    teamList team;
    int maxPV;
    int actualPV;
    int maxMP;
    int actualMP;
    int strength;
    int defense;
    int magic;
    int speed;
    int dodge;
    int crit;
    int parry;

    [SerializeField]
    List<Skill> skills;
    List<StatusEffect> statuses;
    Combination.stackTypes stackOnHim;

//    useSkill(index);
    
}
