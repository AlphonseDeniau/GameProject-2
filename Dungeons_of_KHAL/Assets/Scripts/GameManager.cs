using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SingletonDontDestroy<GameManager>
{
    [Header("Managers")]
    [SerializeField] private SceneManager m_SceneManager;
    [SerializeField] private AudioManager m_AudioManager;

    [Header("Game Datas")]
    [SerializeField] private Inventory m_GlobalInventory;
    [SerializeField] private Inventory m_ExplorationInventory;

    // Accessors \\
    public Inventory GlobalInventory => m_GlobalInventory;
    public Inventory ExplorationInventory => m_ExplorationInventory;


    // Methods \\
    public void ChangeScene(string name = "MainMenu")
    {
        m_SceneManager.ChangeScene(name);
    }
}
