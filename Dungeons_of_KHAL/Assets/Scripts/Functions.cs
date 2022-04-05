using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class Functions
{
    public enum areaTypes {
        single,
        cross,
        row,
        column,
        square,
        all
    };

    static List<Character> getTargetedCharacters(List<Character> teamCharacters, Character selectedTarget, areaTypes area, Character.teamList team) => area switch {
        areaTypes.single => singleTarget(selectedTarget),
        areaTypes.cross => crossTarget(teamCharacters, selectedTarget, team),
        areaTypes.row => rowTarget(teamCharacters, selectedTarget, team),
        areaTypes.column => columnTarget(teamCharacters, selectedTarget, team),
        areaTypes.square => squareTarget(teamCharacters, selectedTarget, team),
        areaTypes.all => allTarget(teamCharacters),
        _ => throw new ArgumentOutOfRangeException(nameof(area), $"Not expected area value: {area}"),
    };

    static List<Character> singleTarget(Character selectedTarget) {
        return (new List<Character>{selectedTarget});
    }
    static List<Character> crossTarget(List<Character> teamCharacters, Character selectedTarget, Character.teamList team) {
        return (rowTarget(teamCharacters, selectedTarget, team).Union(columnTarget(teamCharacters, selectedTarget, team)).ToList());
    }
    static List<Character> rowTarget(List<Character> teamCharacters, Character selectedTarget, Character.teamList team) {
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
    static List<Character> columnTarget(List<Character> teamCharacters, Character selectedTarget, Character.teamList team) {
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
    static List<Character> squareTarget(List<Character> teamCharacters, Character selectedTarget, Character.teamList team) {
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
    static List<Character> allTarget(List<Character> teamCharacters) {
        return (teamCharacters);
    }
    
    static Character getCharacterByPosition(List<Character> teamCharacters, int position) {
        Character returnValue = null;
        foreach (Character character in teamCharacters) {
            if (character.position == position)
                returnValue = character;
        }
        return (returnValue);
    }
}
