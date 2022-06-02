using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonUIManager : MonoBehaviour
{
    [SerializeField] private DungeonMiddlePanel m_MiddlePanel;
    [SerializeField] private TurnManager m_TurnManager;
    [SerializeField] private GameObject m_MapButton;
    [SerializeField] private FeedbackManager m_FeedbackManager;
    

    // Accessors \\
    public DungeonMiddlePanel MiddlePanel => m_MiddlePanel;
    public TurnManager TurnManager => m_TurnManager;
    public GameObject MapButton => m_MapButton;
    public FeedbackManager FeedbackManager => m_FeedbackManager;
}
