using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonUIManager : MonoBehaviour
{
    [SerializeField] private DungeonMiddlePanel m_MiddlePanel;
    

    // Accessors \\
    public DungeonMiddlePanel MiddlePanel => m_MiddlePanel;

}
