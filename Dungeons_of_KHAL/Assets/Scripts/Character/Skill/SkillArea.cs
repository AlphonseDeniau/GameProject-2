using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class SkillArea
{

    [Header("Area Type")]
    [SerializeField] private SkillEnum.EAreaTypes m_Type;

    // Accessors \\
    public SkillEnum.EAreaTypes Type => m_Type;

    // Methods \\

    public List<Character> GetTargetedCharacters(Character _target, List<Character> _targetTeam)
    {
        return (
            m_Type switch {
                SkillEnum.EAreaTypes.Single => singleTarget(_target),
                SkillEnum.EAreaTypes.Adjacent => adjacentTarget(_target, _targetTeam),
                SkillEnum.EAreaTypes.Cross => crossTarget(_target, _targetTeam),
                SkillEnum.EAreaTypes.Row => rowTarget(_target, _targetTeam),
                SkillEnum.EAreaTypes.Column => columnTarget(_target, _targetTeam),
                SkillEnum.EAreaTypes.Square => squareTarget(_target, _targetTeam),
                SkillEnum.EAreaTypes.All => allTarget(_targetTeam),
                _ => throw new ArgumentOutOfRangeException(nameof(m_Type), $"Not expected area value: {m_Type}"),
            }
        );
    }

    List<Character> singleTarget(Character _target) {
        return (new List<Character>{_target});
    }

    List<Character> adjacentTarget(Character _target, List<Character> _targetTeam) {
        if (_target.Team == Character.ETeam.Ally) {
            Character character1 = getCharacterByPosition(_targetTeam, _target.Position - 1);
            Character character2 = _target;
            Character character3 = getCharacterByPosition(_targetTeam, _target.Position + 1);
            List<Character> targets = new List<Character>{
                character1,
                character2,
                character3
            };
            targets.RemoveAll(character => character == null);
            return (targets);
        } else {
            List<Character> targets = new List<Character>();
            int targetPlace = _target.Position / 3;

            for (int i = 0; i < 3; i++) {
                Character tmp = getCharacterByPosition(_targetTeam, i + (targetPlace * 3));
                
                if (tmp != null)
                    targets.Add(tmp);
            }
            for (int i = 0; i < 3; i++) {
                Character tmp = getCharacterByPosition(_targetTeam, targetPlace + (i * 3));
                
                if (tmp != null)
                    targets.Add(tmp);
            }
            return (targets);
        }
    }

    List<Character> crossTarget(Character _target, List<Character> _targetTeam) {
        return (rowTarget(_target, _targetTeam).Union(columnTarget(_target, _targetTeam)).ToList());
    }
    List<Character> rowTarget(Character _target, List<Character> _targetTeam) {
        if (_target.Team == Character.ETeam.Ally)
            return (_targetTeam);
        else {
            List<Character> targets = new List<Character>();
            int targetPlace = _target.Position / 3;

            for (int i = 0; i < 3; i++) {
                Character tmp = getCharacterByPosition(_targetTeam, i + (targetPlace * 3));
                
                if (tmp != null)
                    targets.Add(tmp);
            }
            return (targets);
        }
    }

    List<Character> columnTarget(Character _target, List<Character> _targetTeam) {
        if (_target.Team == Character.ETeam.Ally)
            return (new List<Character>{ _target });
        else {
            List<Character> targets = new List<Character>();
            int targetPlace = _target.Position / 3;

            for (int i = 0; i < 3; i++) {
                Character tmp = getCharacterByPosition(_targetTeam, targetPlace + (i * 3));
                
                if (tmp != null)
                    targets.Add(tmp);
            }
            return (targets);
        }
    }

    List<Character> squareTarget(Character _target, List<Character> _targetTeam) {
        if (_target.Team == Character.ETeam.Ally) {
            Character character1 = getCharacterByPosition(_targetTeam, _target.Position - 1);
            Character character2 = _target;
            Character character3 = getCharacterByPosition(_targetTeam, _target.Position + 1);
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
                    int targetPlace = _target.Position + (i * 3) + j;
                    Character tmp = getCharacterByPosition(_targetTeam, targetPlace);

                    if (tmp != null)
                        targets.Add(tmp);
                }
            }
            return (targets);
        }
    }

    List<Character> allTarget(List<Character> _targetTeam) {
        return (_targetTeam);
    }
    
    Character getCharacterByPosition(List<Character> _targetTeam, int position) {
        Character returnValue = null;
        foreach (Character character in _targetTeam) {
            if (character.Position == position)
                returnValue = character;
        }
        return (returnValue);
    }
}
