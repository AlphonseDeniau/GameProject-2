using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnIcon : MonoBehaviour
{
    [SerializeField] private FightCharacter m_FightCharacter;
    public FightCharacter FightCharacter => m_FightCharacter;
}
