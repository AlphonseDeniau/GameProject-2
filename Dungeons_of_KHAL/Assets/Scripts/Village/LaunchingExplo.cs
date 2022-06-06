using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LaunchingExplo : MonoBehaviour
{
    public Button Priest_BTN;
    public Button Ranger_BTN;
    public Button Templar_BTN;
    public Button Thief_BTN;
    public Button Warrior_BTN;
    public Button Wizard_BTN;

    var selected_character = new List<string>();

    void Start()
    {
        Priest_BTN.onClick.AddListener(priestManagement);
        Ranger_BTN.onClick.AddListener(rangerManagement);
        Templar_BTN.onClick.AddListener(templarManagement);
        Thief_BTN.onClick.AddListener(thiefManagement);
        Warrior_BTN.onClick.AddListener(warriorManagement);
        Wizard_BTN.onClick.AddListener(wizardManagement);
    }

    void priestManagement()
    {
        Debug.Log("Priest !!!");
    }

    void rangerManagement()
    {
        Debug.Log("Ranger !!!");
    }

    void templarManagement()
    {
        Debug.Log("Templar !!!");
    }

    void thiefManagement()
    {
        Debug.Log("Thief !!!");
    }

    void warriorManagement()
    {
        Debug.Log("Warrior !!!");
    }

    void wizardManagement()
    {
        Debug.Log("Wizard !!!");
    }
}