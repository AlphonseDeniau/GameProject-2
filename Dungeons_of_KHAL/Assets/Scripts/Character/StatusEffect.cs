using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class StatusEffect
{
    enum statusTypes {
        regeneration,
        concentration,
        strength,
        resistance,
        haste,
        power,
        accuracy,
        ghost,
        vigilance,
        detoxification,
        stun,
        freeze,
        burn,
        paralysis,
        poison,
        blind,
        confusion,
        exhaustion,
        weakness,
        slowness,
        provocation,
        guard,
    };
    enum statusActionTime {
        startOfTurn,
        endOfTurn,
        immediate,
        fight
    }

    [SerializeField]
    statusActionTime actionTime;
    [SerializeField]
    statusTypes statusType;
    [SerializeField]
    int duration; // -1 = infinite
    [SerializeField]
    int value;

    Character target;
}
