using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class SkillArea
{
    public enum EAreaTypes {
        Single,
        Adjacent,
        Cross,
        Row,
        Column,
        Square,
        All
    };
    [Header("Area Type")]
    [SerializeField] private EAreaTypes m_Type;

    // Accessors \\
    public EAreaTypes Type => m_Type;

    // Methods \\

    public List<Character> getTargetedCharacters(List<Character> teamCharacters, Character selectedTarget, Character.ETeam team)
    {
        return (
            m_Type switch {
                EAreaTypes.Single => singleTarget(selectedTarget),
                EAreaTypes.Adjacent => adjacentTarget(teamCharacters, selectedTarget, team),
                EAreaTypes.Cross => crossTarget(teamCharacters, selectedTarget, team),
                EAreaTypes.Row => rowTarget(teamCharacters, selectedTarget, team),
                EAreaTypes.Column => columnTarget(teamCharacters, selectedTarget, team),
                EAreaTypes.Square => squareTarget(teamCharacters, selectedTarget, team),
                EAreaTypes.All => allTarget(teamCharacters),
                _ => throw new ArgumentOutOfRangeException(nameof(m_Type), $"Not expected area value: {m_Type}"),
            }
        );
    }

    List<Character> singleTarget(Character selectedTarget) {
        return (new List<Character>{selectedTarget});
    }

    List<Character> adjacentTarget(List<Character> teamCharacters, Character selectedTarget, Character.ETeam team) {
        if (team == Character.ETeam.Ally) {
            Character character1 = getCharacterByPosition(teamCharacters, selectedTarget.Position - 1);
            Character character2 = selectedTarget;
            Character character3 = getCharacterByPosition(teamCharacters, selectedTarget.Position + 1);
            List<Character> targets = new List<Character>{
                character1,
                character2,
                character3
            };
            targets.RemoveAll(character => character == null);
            return (targets);
        } else {
            List<Character> targets = new List<Character>();
            int targetPlace = selectedTarget.Position / 3;

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

    List<Character> crossTarget(List<Character> teamCharacters, Character selectedTarget, Character.ETeam team) {
        return (rowTarget(teamCharacters, selectedTarget, team).Union(columnTarget(teamCharacters, selectedTarget, team)).ToList());
    }
    List<Character> rowTarget(List<Character> teamCharacters, Character selectedTarget, Character.ETeam team) {
        if (team == Character.ETeam.Ally)
            return (teamCharacters);
        else {
            List<Character> targets = new List<Character>();
            int targetPlace = selectedTarget.Position / 3;

            for (int i = 0; i < 3; i++) {
                Character tmp = getCharacterByPosition(teamCharacters, i + (targetPlace * 3));
                
                if (tmp != null)
                    targets.Add(tmp);
            }
            return (targets);
        }
    }

    List<Character> columnTarget(List<Character> teamCharacters, Character selectedTarget, Character.ETeam team) {
        if (team == Character.ETeam.Ally)
            return (new List<Character>{selectedTarget});
        else {
            List<Character> targets = new List<Character>();
            int targetPlace = selectedTarget.Position / 3;

            for (int i = 0; i < 3; i++) {
                Character tmp = getCharacterByPosition(teamCharacters, targetPlace + (i * 3));
                
                if (tmp != null)
                    targets.Add(tmp);
            }
            return (targets);
        }
    }

    List<Character> squareTarget(List<Character> teamCharacters, Character selectedTarget, Character.ETeam team) {
        if (team == Character.ETeam.Ally) {
            Character character1 = getCharacterByPosition(teamCharacters, selectedTarget.Position - 1);
            Character character2 = selectedTarget;
            Character character3 = getCharacterByPosition(teamCharacters, selectedTarget.Position + 1);
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
                    int targetPlace = selectedTarget.Position + (i * 3) + j;
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
            if (character.Position == position)
                returnValue = character;
        }
        return (returnValue);
    }
}
