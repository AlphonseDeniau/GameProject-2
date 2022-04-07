using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class SkillArea
{
    public enum areaTypes {
        Single,
        Adjacent,
        Cross,
        Row,
        Column,
        Square,
        All
    };

    areaTypes area;

    public List<Character> getTargetedCharacters(List<Character> teamCharacters, Character selectedTarget, Character.teamList team)
    {
        return (
            area switch {
                areaTypes.Single => singleTarget(selectedTarget),
                areaTypes.Adjacent => adjacentTarget(teamCharacters, selectedTarget, team),
                areaTypes.Cross => crossTarget(teamCharacters, selectedTarget, team),
                areaTypes.Row => rowTarget(teamCharacters, selectedTarget, team),
                areaTypes.Column => columnTarget(teamCharacters, selectedTarget, team),
                areaTypes.Square => squareTarget(teamCharacters, selectedTarget, team),
                areaTypes.All => allTarget(teamCharacters),
                _ => throw new ArgumentOutOfRangeException(nameof(area), $"Not expected area value: {area}"),
            }
        );
    }

    List<Character> singleTarget(Character selectedTarget) {
        return (new List<Character>{selectedTarget});
    }

    List<Character> adjacentTarget(List<Character> teamCharacters, Character selectedTarget, Character.teamList team) {
        if (team == Character.teamList.ally) {
            Character character1 = getCharacterByPosition(teamCharacters, selectedTarget.position - 1);
            Character character2 = selectedTarget;
            Character character3 = getCharacterByPosition(teamCharacters, selectedTarget.position + 1);
            List<Character> targets = new List<Character>{
                character1,
                character2,
                character3
            };
            targets.RemoveAll(character => character == null);
            return (targets);
        } else {
            List<Character> targets = new List<Character>();
            int targetPlace = selectedTarget.position / 3;

            for (int i = 0; i < 3; i++) {
                Character tmp = getCharacterByPosition(teamCharacters, i + (targetPlace * 3));
                
                if (tmp != null)
                    targets.Add(tmp);
            }
            for (int i = 0; i < 3; i++) {
                Character tmp = getCharacterByPosition(teamCharacters, targetPlace + (i * 3));
                
                if (tmp != null)
                    targets.Add(tmp);
            }
            return (targets);
        }
    }

    List<Character> crossTarget(List<Character> teamCharacters, Character selectedTarget, Character.teamList team) {
        return (rowTarget(teamCharacters, selectedTarget, team).Union(columnTarget(teamCharacters, selectedTarget, team)).ToList());
    }
    List<Character> rowTarget(List<Character> teamCharacters, Character selectedTarget, Character.teamList team) {
        if (team == Character.teamList.ally)
            return (teamCharacters);
        else {
            List<Character> targets = new List<Character>();
            int targetPlace = selectedTarget.position / 3;

            for (int i = 0; i < 3; i++) {
                Character tmp = getCharacterByPosition(teamCharacters, i + (targetPlace * 3));
                
                if (tmp != null)
                    targets.Add(tmp);
            }
            return (targets);
        }
    }

    List<Character> columnTarget(List<Character> teamCharacters, Character selectedTarget, Character.teamList team) {
        if (team == Character.teamList.ally)
            return (new List<Character>{selectedTarget});
        else {
            List<Character> targets = new List<Character>();
            int targetPlace = selectedTarget.position / 3;

            for (int i = 0; i < 3; i++) {
                Character tmp = getCharacterByPosition(teamCharacters, targetPlace + (i * 3));
                
                if (tmp != null)
                    targets.Add(tmp);
            }
            return (targets);
        }
    }

    List<Character> squareTarget(List<Character> teamCharacters, Character selectedTarget, Character.teamList team) {
        if (team == Character.teamList.ally) {
            Character character1 = getCharacterByPosition(teamCharacters, selectedTarget.position - 1);
            Character character2 = selectedTarget;
            Character character3 = getCharacterByPosition(teamCharacters, selectedTarget.position + 1);
            List<Character> targets = new List<Character>{
                character1,
                character2,
                character3
            };
            targets.RemoveAll(character => character == null);
            return (targets);
        } else {
            List<Character> targets = new List<Character>();

            for (int i = -1; i < 2; i++) {
                for (int j = -1; j < 2; j++) {
                    int targetPlace = selectedTarget.position + (i * 3) + j;
                    Character tmp = getCharacterByPosition(teamCharacters, targetPlace);

                    if (tmp != null)
                        targets.Add(tmp);
                }
            }
            return (targets);
        }
    }

    List<Character> allTarget(List<Character> teamCharacters) {
        return (teamCharacters);
    }
    
    Character getCharacterByPosition(List<Character> teamCharacters, int position) {
        Character returnValue = null;
        foreach (Character character in teamCharacters) {
            if (character.position == position)
                returnValue = character;
        }
        return (returnValue);
    }
}
