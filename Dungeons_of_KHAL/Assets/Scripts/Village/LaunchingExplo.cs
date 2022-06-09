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

    List<CharacterObject> availableCharacters;
    CharacterManager team;
    int maxMembers = 4;
    List<int> tmpIndexBackup;
    List<CharacterObject> objRef = new List<CharacterObject>();

    void Start()
    {
        availableCharacters = GameManager.Instance.GlobalCharacterManager.Allies;
        team = GameManager.Instance.ExplorationCharacterManager;
        tmpIndexBackup = new List<int>();

        Priest_BTN.onClick.AddListener(priestManagement);
        Ranger_BTN.onClick.AddListener(rangerManagement);
        Templar_BTN.onClick.AddListener(templarManagement);
        Thief_BTN.onClick.AddListener(thiefManagement);
        Warrior_BTN.onClick.AddListener(warriorManagement);
        Wizard_BTN.onClick.AddListener(wizardManagement);
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

    void priestManagement()
    {
        addCharacterToTeam(0);
        Priest_BTN.interactable = false;
    }

    void rangerManagement()
    {
        addCharacterToTeam(1);
        Priest_BTN.interactable = false;
    }

    void templarManagement()
    {
        addCharacterToTeam(2);
        Priest_BTN.interactable = false;
    }

    void thiefManagement()
    {
        addCharacterToTeam(3);
        Priest_BTN.interactable = false;
    }

    void warriorManagement()
    {
        addCharacterToTeam(4);
        Priest_BTN.interactable = false;
    }

    void wizardManagement()
    {
        addCharacterToTeam(5);
        Priest_BTN.interactable = false;
    }
}