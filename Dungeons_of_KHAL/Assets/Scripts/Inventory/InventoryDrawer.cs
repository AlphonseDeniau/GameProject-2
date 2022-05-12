using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryDrawer : MonoBehaviour
{
    [Header("Element To Draw")]
    [SerializeField] private Inventory m_Inventory;

    [Header("Graphical Parameters")]
    [SerializeField] private int m_MaxItem = 0;

    [SerializeField] private Vector2 m_PlaceSize = Vector2.zero;
    [SerializeField] private Vector2 m_ItemSize = Vector2.zero;
    [SerializeField] private Vector2 m_PaddingWidth = Vector2.zero;
    [SerializeField] private Vector2 m_PaddingHeight = Vector2.zero;

    [Header("Button for Item")]
    [SerializeField] private RectTransform m_ParentButton;
    [SerializeField] private GameObject m_PrefabButton;

    [Header("Logical Parameters")]
    [SerializeField] private List<Button> m_SelectButtons = new List<Button>();
    [SerializeField] private List<ItemButton> m_ItemButtons = new List<ItemButton>();

    private int m_IndexShowing = 0;

    // Accessors \\
    public Inventory Inventory { get { return m_Inventory; } set { m_Inventory = value; } }

    // Methods \\

    private void Start()
    {
        m_Inventory = DungeonManager.Instance.Inventory;
        m_IndexShowing = 0;
        UpdateVisual();
        UpdateButton();
    }

    private void OnEnable()
    {
        m_IndexShowing = 0;
        UpdateVisual();
        UpdateButton();
    }

    private void OnDisable()
    {
        m_Inventory.SelectItem(null);
    }

    private void CreateInventory()
    {
        for (int i = 0; i < m_MaxItem && i < m_Inventory.Items.Count; i++)
        {
            CreateButton(m_Inventory.Items[i + m_IndexShowing], i);
        }
    }

    private void CreateButton(Inventory.InventoryItem item, int index)
    {
        float separatorWidth = m_MaxItem == 1 ? 0 : (m_PlaceSize.x - m_PaddingWidth.x - m_PaddingWidth.y - (m_ItemSize.x * m_MaxItem)) / (m_MaxItem - 1);

        GameObject newButtonObject = Instantiate(m_PrefabButton, Vector3.zero, Quaternion.identity);
        newButtonObject.transform.SetParent(m_ParentButton);
        Vector2 position = new Vector2(m_PaddingWidth.x + ((m_ItemSize.x + separatorWidth) * index) - (m_PlaceSize.x / 2), m_PlaceSize.y / 2);

        ItemButton newButtonItem = newButtonObject.GetComponent<ItemButton>();
        newButtonItem.InitButton(position, index, item.m_Item.Sprite, item.m_Number);
        
        Button newButton = newButtonItem.GetComponent<Button>();
        newButton.onClick.AddListener(delegate () {
            SelectInventoryItem(item.m_Item);
            SelectGraphicalItem(index);
        });

        m_ItemButtons.Add(newButtonItem);
    }

    public void UpdateVisual()
    {
        if (m_Inventory)
        {
            m_ItemButtons.ForEach(x => Destroy(x.gameObject));
            m_ItemButtons.Clear();
            CreateInventory();
        }
    }

    private void UpdateButton()
    {
        if (m_Inventory)
        {
            Button leftButton = m_SelectButtons[0];
            Button rightButton = m_SelectButtons[1];

            leftButton.interactable = true;
            rightButton.interactable = true;

            if (m_Inventory.Items.Count <= m_MaxItem)
            {
                leftButton.interactable = false;
                rightButton.interactable = false;
            }
            if (m_IndexShowing == 0)
                leftButton.interactable = false;
            if (m_IndexShowing + m_MaxItem == m_Inventory.Items.Count)
                rightButton.interactable = false;
        }
    }

    public void SelectInventoryItem(Item item)
    {
        m_Inventory.SelectItem(item);
    }

    public void SelectGraphicalItem(int index)
    {
        m_ItemButtons.ForEach(x => {
            x.IsSelected = x.Index == index;
            x.UpdateButton();
        });
    }

    public void MoveDirection(int direction)
    {
        if (direction == 0 && m_IndexShowing != 0) // Left
        {
            m_IndexShowing -= 1;
        } 
        else if (direction == 1 && m_IndexShowing + m_MaxItem != m_Inventory.Items.Count) // Right
        {
            m_IndexShowing += 1;
        }
        UpdateVisual();
        UpdateButton();
    }
}
