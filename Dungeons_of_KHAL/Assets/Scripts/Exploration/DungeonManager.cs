using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonManager : Singleton<DungeonManager>
{
    [Header("Managers")]
    [SerializeField] private DungeonUIManager m_UIManager;
    [SerializeField] private FightManager m_FightManager;

    [Header("Game Datas")]
    [SerializeField] private Inventory m_Inventory;
    [SerializeField] private Map m_Map;

    // Private Variables
    private GameManager m_GameManager;

    // Accessors \\
    public Inventory Inventory => m_Inventory;
    public Map Map => m_Map;

    // Methods \\
    void Start()
    {
        m_GameManager = GameManager.Instance;
        m_Inventory = m_GameManager.ExplorationInventory;
        m_Map = Map.Instance;
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
