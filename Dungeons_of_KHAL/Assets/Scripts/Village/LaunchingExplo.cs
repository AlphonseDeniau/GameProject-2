using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LaunchingExplo : MonoBehaviour
{
    public Button Priest_BTN;
    public Button Priest_minus_BTN;
    public Button Ranger_BTN;
    public Button Ranger_minus_BTN;
    public Button Templar_BTN;
    public Button Templar_minus_BTN;
    public Button Thief_BTN;
    public Button Thief_minus_BTN;
    public Button Warrior_BTN;
    public Button Warrior_minus_BTN;
    public Button Wizard_BTN;
    public Button Wizard_minus_BTN;

    List<CharacterObject> availableCharacters;
    CharacterManager team;
    int maxMembers = 4;
    List<int> tmpIndexBackup;
    List<CharacterObject> objRef = new List<CharacterObject>();
    Character.ETeam eTeam;

    void Start()
    {
        availableCharacters = GameManager.Instance.GlobalCharacterManager.Allies;
        team = GameManager.Instance.ExplorationCharacterManager;
        tmpIndexBackup = new List<int>();

        Priest_BTN.onClick.AddListener(priestManagement);
        Priest_minus_BTN.onClick.AddListener(priestMinusManagement);
        Ranger_BTN.onClick.AddListener(rangerManagement);
        Ranger_minus_BTN.onClick.AddListener(rangerMinusManagement);
        Templar_BTN.onClick.AddListener(templarManagement);
        Templar_minus_BTN.onClick.AddListener(templarMinusManagement);
        Thief_BTN.onClick.AddListener(thiefManagement);
        Thief_minus_BTN.onClick.AddListener(thiefMinusManagement);
        Warrior_BTN.onClick.AddListener(warriorManagement);
        Warrior_minus_BTN.onClick.AddListener(warriorMinusManagement);
        Wizard_BTN.onClick.AddListener(wizardManagement);
        Wizard_minus_BTN.onClick.AddListener(wizardMinusManagement);
    }

    public void MoveCharacters() {
        foreach (CharacterObject obj in objRef) {
            obj.transform.SetParent(team.transform);
            GameManager.Instance.GlobalCharacterManager.RemoveCharacter(obj, Character.ETeam.Ally);
        }
    }

    void addCharacterToTeam(int index) {
        if (tmpIndexBackup.IndexOf(index) != -1)
            return;
        if (team.Allies.Count >= maxMembers) {
            team.RemoveCharacter(availableCharacters[tmpIndexBackup[0]], Character.ETeam.Ally);
            tmpIndexBackup.RemoveAt(0);
            foreach (CharacterObject obj in team.Allies) {
                obj.Data.Position -= 1;
            }
        }
        availableCharacters[index].Data.Position = team.Allies.Count;
        team.AddCharacter(availableCharacters[index], Character.ETeam.Ally);
        tmpIndexBackup.Add(index);
        objRef.Add(availableCharacters[index]);
    }

    void removeCharacterFromTeam(int index)
    {
        team = GameManager.Instance.ExplorationCharacterManager;
        team.RemoveCharacter(objRef[index], eTeam);
    }

    void priestManagement()
    {
        addCharacterToTeam(0);
        if (Priest_BTN.interactable == true && Priest_minus_BTN.interactable == false) {
            Priest_BTN.interactable = false;
            Priest_minus_BTN.interactable = true;
        }

    }

    void priestMinusManagement()
    {
        removeCharacterFromTeam(0);
        if (Priest_BTN.interactable == false && Priest_minus_BTN.interactable == true) {
            Priest_BTN.interactable = true;
            Priest_minus_BTN.interactable = false;
        }
    }

    void rangerManagement()
    {
        addCharacterToTeam(1);
        if (Ranger_BTN.interactable == true && Ranger_minus_BTN.interactable == false)
            Ranger_BTN.interactable = false;
            Ranger_minus_BTN.interactable = true;
    }

    void rangerMinusManagement()
    {
        removeCharacterFromTeam(1);
        if (Ranger_BTN.interactable == false && Ranger_minus_BTN.interactable == true)
            Ranger_BTN.interactable = true;
            Ranger_minus_BTN.interactable = false;
    }

    void templarManagement()
    {
        addCharacterToTeam(2);
        if (Templar_BTN.interactable == true && Templar_minus_BTN.interactable == false)
            Templar_BTN.interactable = false;
            Templar_minus_BTN.interactable = true;
    }
    
    void templarMinusManagement()
    {
        removeCharacterFromTeam(2);
        if (Templar_BTN.interactable == false && Templar_minus_BTN.interactable == true)
            Templar_BTN.interactable = true;
            Templar_minus_BTN.interactable = false;
    }

    void thiefManagement()
    {
        addCharacterToTeam(3);
        if (Thief_BTN.interactable == true && Thief_minus_BTN.interactable == false)
            Thief_BTN.interactable = false;
            Thief_minus_BTN.interactable = true;
    }

    void thiefMinusManagement()
    {
        removeCharacterFromTeam(3);
        if (Thief_BTN.interactable == false && Thief_minus_BTN.interactable == true)
            Thief_BTN.interactable = true;
            Thief_minus_BTN.interactable = false;
    }

    void warriorManagement()
    {
        addCharacterToTeam(4);
        if (Warrior_BTN.interactable == true && Warrior_minus_BTN.interactable == false)
            Warrior_BTN.interactable = false;
            Warrior_minus_BTN.interactable = true;
    }

    void warriorMinusManagement()
    {
        removeCharacterFromTeam(4);
        if (Warrior_BTN.interactable == false && Warrior_minus_BTN.interactable == true)
            Warrior_BTN.interactable = true;
            Warrior_minus_BTN.interactable = false;
    }

    void wizardManagement()
    {
        addCharacterToTeam(5);
        if (Wizard_BTN.interactable == true && Wizard_minus_BTN.interactable == false)
            Wizard_BTN.interactable = false;
            Wizard_minus_BTN.interactable = true;
    }

    void wizardMinusManagement()
    {
        removeCharacterFromTeam(5);
        if (Wizard_BTN.interactable == false && Wizard_minus_BTN.interactable == true)
            Wizard_BTN.interactable = true;
            Wizard_minus_BTN.interactable = false;
    }
}