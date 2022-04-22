using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Inventory : MonoBehaviour
{
    [Serializable]
    public class InventoryItem
    {
        public InventoryItem(int number, Item item)
        {
            m_Number = number;
            m_Item = item;
        }

        public int m_Number;
        public Item m_Item;
    }

    [SerializeField] private List<InventoryItem> m_Items = new List<InventoryItem>();
    [SerializeField] private Item m_SelectedItem = null;

    // Accessors \\
    public List<InventoryItem> Items => m_Items;
    public Item SelectedItem => m_SelectedItem;
    public bool HasSelectedItem => m_SelectedItem != null;

    public void AddItem(Item item, int number = 1)
    {
        if (m_Items.Exists(x => x.m_Item == item))
        {
            InventoryItem inventory = m_Items.Find(x => x.m_Item == item);
            inventory.m_Number += number;
        }
        else
        {
            m_Items.Add(new InventoryItem(1, item));
        }
    }

    public void RemoveItem(Item item, int number)
    {
        if (m_Items.Exists(x => x.m_Item == item))
        {
            InventoryItem inventory = m_Items.Find(x => x.m_Item == item);
            if (inventory.m_Number < number)
                inventory.m_Number -= 0;
            else
                inventory.m_Number -= number;
            if (inventory.m_Number == 0)
                m_Items.Remove(inventory);
        }
    }

    public int GetItemNumber(Item item)
    {
        if (m_Items.Exists(x => x.m_Item == item))
        {
            InventoryItem inventory = m_Items.Find(x => x.m_Item == item);
            return inventory.m_Number;
        } else
        {
            return 0;
        }
    }

    public void SelectItem(Item item)
    {
        if (m_Items.Exists(x => x.m_Item == item))
            m_SelectedItem = item;
    }
}
