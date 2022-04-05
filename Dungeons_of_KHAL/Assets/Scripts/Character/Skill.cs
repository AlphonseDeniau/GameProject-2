using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class Skill
{
    enum targetTypes {
        ally,
        enemy,
        self,
    };

    Functions.areaTypes areaType;
    targetTypes target;
    List<SkillEffect> skillEffects;
}
