using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonManager : Singleton<DungeonManager>
{
    [Header("Managers")]
    [SerializeField] private DungeonUIManager m_UIManager;
    [SerializeField] private FightManager m_FightManager;
    [SerializeField] private CharacterManager m_CharacterManager;
    [SerializeField] private BestiaryManager m_Bestiary;

    [Header("Game Datas")]
    [SerializeField] private Inventory m_Inventory;
    [SerializeField] private Map m_Map;

    // Private Variables
    private GameManager m_GameManager;

    // Accessors \\
    public FightManager FightManager => m_FightManager;
    public DungeonUIManager UIManager => m_UIManager;
    public CharacterManager CharacterManager => m_CharacterManager;
    public BestiaryManager Bestiary => m_Bestiary;
    public Inventory Inventory => m_Inventory;
    public Map Map => m_Map;

    // Methods \\
    void Start()
    {
        m_GameManager = GameManager.Instance;
        m_Inventory = m_GameManager.ExplorationInventory;
        m_CharacterManager.Allies = m_GameManager.ExplorationCharacterManager.Allies;
        m_FightManager.SetAllies(m_CharacterManager.Allies);
        m_Map = Map.Instance;
        m_Map.MapStart();
        m_UIManager.MiddlePanel.ActiveMiddlePanel(false);
    }

    public void ReturnToVillage()
    {
        SetInventory();
        m_GameManager.ChangeScene("Village");
    }

    private void SetInventory()
    {
        m_Inventory.ClearInventory();
        m_Inventory.Items.ForEach(x => m_GameManager.GlobalInventory.AddItem(x.m_Item, x.m_Number));
    }

    public void StartFight()
    {
        m_UIManager.MapButton.SetActive(false);
        m_FightManager.Initialize();
    }

    public void EndFight()
    {
        m_UIManager.MiddlePanel.ActiveMiddlePanel(false);
        m_UIManager.MapButton.SetActive(true);
        m_FightManager.Uninitialize();
        m_CharacterManager.ClearCharacter();
    }
}
