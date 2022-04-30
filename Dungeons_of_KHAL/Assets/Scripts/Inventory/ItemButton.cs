using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemButton : MonoBehaviour
{
    private int m_Index;
    private Button m_Button;
    private Image m_Image;
    private RectTransform m_RectTransform;

    [Header("Button Information")]
    [SerializeField] private Color m_ColorActive;
    [SerializeField] private Color m_ColorDeactive;

    [Header("Text Information")]
    [SerializeField] private Text m_Text;

    [Header("Item Information")]
    [SerializeField] private Vector2 m_ItemSize;
    [SerializeField] private Image m_ItemImage;
    [SerializeField] private int m_Number;
    [SerializeField] private bool m_IsSelected = false;

    public Button Button => m_Button;
    public bool IsSelected { get { return m_IsSelected; } set { m_IsSelected = value; } }
    public int Index { get { return m_Index; } }

    // Start is called before the first frame update
    void Start()
    {
        m_Image = GetComponent<Image>();
        m_Button = GetComponent<Button>();
        m_RectTransform = GetComponent<RectTransform>();
    }

    public void InitButton(Vector2 position, int index, Sprite itemSprite, int number)
    {
        Start();
        m_RectTransform.localScale = Vector3.one;
        m_RectTransform.localPosition = position;
        m_RectTransform.sizeDelta = m_ItemSize;

        m_Index = index;
        m_Number = number;

        m_ItemImage.sprite = itemSprite;
        m_Text.text = m_Number.ToString();

        UpdateButton();
    }

    public void UpdateButton()
    {
        m_Text.text = m_Number.ToString();
        m_Image.color = m_IsSelected ? m_ColorActive : m_ColorDeactive;
    }
}
