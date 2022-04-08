using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class SkillEnum
{
    public enum ETargetType
    {
        Ally,
        Enemy,
        Self,
    };

    public enum EAreaTypes
    {
        Single,
        Adjacent,
        Cross,
        Row,
        Column,
        Square,
        All
    };
}
