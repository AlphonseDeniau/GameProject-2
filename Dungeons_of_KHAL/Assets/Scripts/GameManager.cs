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

    [SerializeField] private CharacterManager m_GlobalCharacterManager;
    [SerializeField] private CharacterManager m_ExplorationCharacterManager;

    // Accessors \\
    public Inventory GlobalInventory => m_GlobalInventory;
    public Inventory ExplorationInventory => m_ExplorationInventory;

    public CharacterManager GlobalCharacterManager => m_GlobalCharacterManager;
    public CharacterManager ExplorationCharacterManager => m_ExplorationCharacterManager;


    // Methods \\
    public void ChangeScene(string name = "MainMenu")
    {
        m_SceneManager.ChangeScene(name);
    }
}
