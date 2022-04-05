using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class Character : MonoBehaviour
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

    List<Skill> skills;
}
