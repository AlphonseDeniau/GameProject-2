using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class SkillEffect
{
    public enum effectType {
        Status,
        Heal,
        Damage,
    };
    [SerializeField]
    effectType type;
    [SerializeField]
    SkillArea area;
    [SerializeField]
    StatusEffect effect;
    [Range(0, 100)] public int pourcentage;

//    use(user, target)
/*
    switch type
        status -> addStatus
        heal -> target.recover
        damage -> target.takeDamage
*/
//    getArea()
}
