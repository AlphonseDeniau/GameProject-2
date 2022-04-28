using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightManager : Singleton<FightManager>
{
    [SerializeField] private TurnManager turnManager;
    [SerializeField] private List<Character> allies;
    [SerializeField] private List<Character> enemies;

    void Update() {
        if (turnManager.CharactersTurn.Count != 0) {
            DoTurn();
        } else {
            DoContinuousStatus();
        }
    }

    void DoTurn() {
        if (!turnManager.CharactersTurn[0].Character.CheckStatusEffect(StatusEnum.EStatusType.Paralysis)) {

        }
    }

    void DoContinuousStatus() {
        foreach (Character character in allies) {
            character.UpdateStatus(Time.deltaTime);
        }
        foreach (Character character in enemies) {
            character.UpdateStatus(Time.deltaTime);
        }
    }
}
