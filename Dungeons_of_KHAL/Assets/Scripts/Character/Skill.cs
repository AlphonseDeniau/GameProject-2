using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Skill", menuName = "ScriptableObjects/Skill", order = 2)]
public class Skill : ScriptableObject
{
    public enum targetTypes {
        ally,
        enemy,
        self,
    };
    
    public targetTypes target;

    string skillName;
    int level;
    int cost;

    [SerializeField]
    Combination.stackTypes stack;

    public List<SkillList> Levels;

    [Serializable]
    public class SkillList
    {
        public List<SkillEffect> skillEffects;
    }

//    use(user, target)
    /*
    for effects
        effect.use
    stack.use
    */
}