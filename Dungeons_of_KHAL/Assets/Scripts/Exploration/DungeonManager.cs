using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonManager : Singleton<DungeonManager>
{
    [Header("Managers")]
    [SerializeField] private DungeonUIManager m_UIManager;
    [SerializeField] private FightManager m_FightManager;
    [SerializeField] private CharacterManager m_CharacterManager;

    [Header("Game Datas")]
    [SerializeField] private Inventory m_Inventory;
    [SerializeField] private Map m_Map;

    // Private Variables
    private GameManager m_GameManager;

    // Accessors \\
    public DungeonUIManager UIManager => m_UIManager;
    public Inventory Inventory => m_Inventory;
    public CharacterManager CharacterManager => m_CharacterManager;
    public Map Map => m_Map;

    // Methods \\
    void Start()
    {
        m_GameManager = GameManager.Instance;
        m_Inventory = m_GameManager.ExplorationInventory;
        m_CharacterManager.Allies = m_GameManager.ExplorationCharacterManager.Allies;
        m_Map = Map.Instance;

        m_FightManager.Initialize();
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
}
