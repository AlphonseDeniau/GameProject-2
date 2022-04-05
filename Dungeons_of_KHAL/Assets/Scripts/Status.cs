using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class Status : MonoBehaviour
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
        slowness
    };
    enum statusActionTime {
        startOfTurn,
        endOfTurn,
        immediate
    }
    statusTypes statusType;
    int duration; // -1 = infinite
}
