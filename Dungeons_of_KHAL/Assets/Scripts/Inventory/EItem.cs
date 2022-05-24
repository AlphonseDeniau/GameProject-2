using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EItem
{
    public enum EItemEffect
    {
        Heal,
        Mana,
        Regeneration,
        Concentration,
        Detoxification,
    }

    public enum EItemType
    {
        Consommable,
        Permanent,
    }

    public enum EItemUsage
    {
        Pourcentage,
        Constant,
    }

    public enum EItemTarget
    {
        Ally,
        Enemy,
    }
}