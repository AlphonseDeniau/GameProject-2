using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Combination
{
    public enum stackTypes {
        Neutral,
        Fire,
        Water,
        Wind,
        Earth
    };

    public stackTypes type1;
    public stackTypes type2;
    public StatusEffect statusEffect;
}
