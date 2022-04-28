using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Item", menuName = "ScriptableObjects/Items", order = 3)]
public class Item : ScriptableObject
{
    [Header("Graphical Parameters")]
    [SerializeField] private string name;
    [SerializeField] private string m_Description;
    [SerializeField] private Sprite m_Sprite;

    [Header("Logical Parameters")]
    [SerializeField] private EItem.EItemEffect m_Effect;
    [SerializeField] private EItem.EItemTarget m_Target;
    [SerializeField] private EItem.EItemType m_Type;
    [SerializeField] private EItem.EItemUsage m_Usage;
    [SerializeField] private int m_Power;

    [Header("Shop Parameters")]
    [SerializeField] private int m_Cost;

    // Accessors \\
    public string Name => name;
    public string Description => m_Description;
    public Sprite Sprite => m_Sprite;

    public EItem.EItemEffect Effect => m_Effect;
    public EItem.EItemTarget Target => m_Target;
    public EItem.EItemType Type => m_Type;
    public EItem.EItemUsage Usage => m_Usage;
    public int Power => m_Power;

    public int Cost => m_Cost;
}
