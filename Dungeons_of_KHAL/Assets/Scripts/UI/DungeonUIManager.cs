using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DungeonUIManager : MonoBehaviour
{
    [SerializeField] private DungeonMiddlePanel m_MiddlePanel;
    [SerializeField] private TurnManager m_TurnManager;
    [SerializeField] private GameObject m_MapButton;
    [SerializeField] private FeedbackManager m_FeedbackManager;
    [SerializeField] private StatUI m_StatUI;
    [SerializeField] private Text m_InstructionText;
    

    // Accessors \\
    public DungeonMiddlePanel MiddlePanel => m_MiddlePanel;
    public TurnManager TurnManager => m_TurnManager;
    public GameObject MapButton => m_MapButton;
    public FeedbackManager FeedbackManager => m_FeedbackManager;
    public StatUI StatUI => m_StatUI;
    public Text InstructionText => m_InstructionText;
}
