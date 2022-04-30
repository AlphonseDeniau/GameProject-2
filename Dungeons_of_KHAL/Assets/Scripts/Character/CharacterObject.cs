using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterObject : MonoBehaviour
{
    [SerializeField] private Character m_ScriptableObject;
    [SerializeField] private CharacterData m_Data;

    public Character ScriptableObject => m_ScriptableObject;
    public CharacterData Data => m_Data;

    public bool Initialize(Character scriptableObject)
    {
        m_ScriptableObject = scriptableObject;
        m_Data.Initialize(this);
        return true;
    }
};