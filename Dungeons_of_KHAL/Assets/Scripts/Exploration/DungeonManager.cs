using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonManager : Singleton<DungeonManager>
{
    [Header("Game Datas")]
    [SerializeField] private Inventory m_Inventory;
    [SerializeField] private Map m_Map;

    // Private Variables
    private GameManager m_GameManager;

    // Accessors \\
    public Inventory Inventory => m_Inventory;
    public Map Map => m_Map;

    // Methods \\
    protected void Awake()
    {
        base.Awake();
        m_GameManager = GameManager.Instance;
        m_Inventory = m_GameManager.ExplorationInventory;
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
