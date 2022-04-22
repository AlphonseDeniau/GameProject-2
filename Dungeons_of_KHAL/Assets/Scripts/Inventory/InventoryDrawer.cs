using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryDrawer : MonoBehaviour
{
    [Header("Element To Draw")]
    [SerializeField] private Inventory m_Inventory;

    [Header("Graphical Parameters")]
    [SerializeField] private RectTransform m_ParentButton;
    [SerializeField] private GameObject m_PrefabButton;

    [SerializeField] private float m_Width;
    [SerializeField] private float m_Height;
    [SerializeField] private Vector2 m_PaddingVertical;
    [SerializeField] private Vector2 m_PaddingHorizontal;

    [SerializeField] private float m_WidthItem;
    [SerializeField] private float m_HeightItem;

    [SerializeField] private float m_WidthSeparator;

    [SerializeField] private List<Button> m_Buttons = new List<Button>();

    [Header("Logical Parameters")]
    [SerializeField] private List<Button> m_ItemButtons = new List<Button>();

    private int m_IndexShowing = 0;

    // Accessors \\
    public Inventory Inventory { get { return m_Inventory; } set { m_Inventory = value; } }

    // Methods \\
    public void Start()
    {
        m_Inventory = DungeonManager.Instance.Inventory;
        CreateInventory();
        UpdateButton();
    }

    private void CreateInventory()
    {
        for (int i = 0; i < 5 && i < m_Inventory.Items.Count; i++)
        {
            CreateButton(m_Inventory.Items[i + m_IndexShowing], i);
        }
    }

    private void CreateButton(Inventory.InventoryItem item, int index)
    {
        GameObject newButtonObject = Instantiate(m_PrefabButton, Vector3.zero, Quaternion.identity);
        
        RectTransform newButtonRect = newButtonObject.GetComponent<RectTransform>();
        newButtonRect.SetParent(m_ParentButton.transform);
        newButtonRect.transform.localScale = Vector3.one;
        newButtonRect.transform.localPosition = new Vector3(m_PaddingHorizontal.x + ((m_WidthItem + m_WidthSeparator) * index) - (m_ParentButton.sizeDelta.x / 2), m_ParentButton.sizeDelta.y / 2, 0);

        Button newButton = newButtonObject.GetComponent<Button>();
        newButton.onClick.AddListener(() => SelectItem(item.m_Item));
        newButton.onClick.AddListener(() => Debug.Log("CLICKED"));

        Image newButtonImage = newButtonObject.GetComponent<Image>();
        newButtonImage.color = Color.white;
        newButtonImage.sprite = item.m_Item.Sprite;

        Text newButtonText = newButtonObject.GetComponentInChildren<Text>();
        newButtonText.text = item.m_Number.ToString();

        m_ItemButtons.Add(newButton);
    }

    public void UpdateVisual()
    {
        m_ItemButtons.ForEach(x => Destroy(x.gameObject));
        CreateInventory();
    }

    public void SelectItem(Item item)
    {
        m_Inventory.SelectItem(item);
    }

    public void MoveDirection(int direction)
    {
        if (direction == 0 && m_IndexShowing != 0) // Left
        {
            m_IndexShowing -= 1;
        } 
        else if (direction == 1 && m_IndexShowing + 5 != m_ItemButtons.Count) // Right
        {
            m_IndexShowing += 1;
        }
        UpdateVisual();
        UpdateButton();
    }

    private void UpdateButton()
    {
        Button leftButton = m_Buttons[0];
        Button rightButton = m_Buttons[1];

        leftButton.interactable = true;
        rightButton.interactable = true;

        if (m_ItemButtons.Count <= 5) {
            leftButton.interactable = false;
            rightButton.interactable = false;
            return;
        }
        if (m_IndexShowing == 0)
            leftButton.interactable = false;
        if (m_IndexShowing + 5 == m_ItemButtons.Count)
            rightButton.interactable = false;
    }
}
