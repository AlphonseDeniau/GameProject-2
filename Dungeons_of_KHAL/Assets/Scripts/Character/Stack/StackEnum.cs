using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class StackEnum
{
    public enum EStackType
    {
        None,
        Neutral,
        Fire,
        Water,
        Wind,
        Earth
    }

    public enum EEffectType
    {
        Positive,
        Negative,
    }
}
