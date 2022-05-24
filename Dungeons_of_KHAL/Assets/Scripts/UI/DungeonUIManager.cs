using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonUIManager : MonoBehaviour
{
    [SerializeField] private DungeonMiddlePanel m_MiddlePanel;
    [SerializeField] private TurnManager m_TurnManager;
    [SerializeField] private GameObject m_MapButton;
    

    // Accessors \\
    public DungeonMiddlePanel MiddlePanel => m_MiddlePanel;
    public TurnManager TurnManager => m_TurnManager;
    public GameObject MapButton => m_MapButton;
}
