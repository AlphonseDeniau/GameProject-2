using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

[Serializable]
public class SkillArea
{

    [Header("Area Type")]
    [SerializeField] private SkillEnum.EAreaTypes m_Type;

    // Accessors \\
    public SkillEnum.EAreaTypes Type => m_Type;

    // Methods \\

    public List<CharacterObject> GetTargetedCharacters(CharacterObject _target, List<CharacterObject> _targetTeam)
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

    List<CharacterObject> singleTarget(CharacterObject _target) {
        return (new List<CharacterObject>{_target});
    }

    List<CharacterObject> adjacentTarget(CharacterObject _target, List<CharacterObject> _targetTeam) {
        if (_target.ScriptableObject.Team == Character.ETeam.Ally) {
            CharacterObject character1 = getCharacterByPosition(_targetTeam, _target.Data.Position - 1);
            CharacterObject character2 = _target;
            CharacterObject character3 = getCharacterByPosition(_targetTeam, _target.Data.Position + 1);
            List<CharacterObject> targets = new List<CharacterObject>{
                character1,
                character2,
                character3
            };
            targets.RemoveAll(character => character == null);
            return (targets);
        } else {
            List<CharacterObject> targets = new List<CharacterObject>();
            int targetPlace = _target.Data.Position / 3;

            for (int i = 0; i < 3; i++) {
                CharacterObject tmp = getCharacterByPosition(_targetTeam, i + (targetPlace * 3));
                
                if (tmp != null)
                    targets.Add(tmp);
            }
            for (int i = 0; i < 3; i++) {
                CharacterObject tmp = getCharacterByPosition(_targetTeam, targetPlace + (i * 3));
                
                if (tmp != null)
                    targets.Add(tmp);
            }
            return (targets);
        }
    }

    List<CharacterObject> crossTarget(CharacterObject _target, List<CharacterObject> _targetTeam) {
        return (rowTarget(_target, _targetTeam).Union(columnTarget(_target, _targetTeam)).ToList());
    }
    List<CharacterObject> rowTarget(CharacterObject _target, List<CharacterObject> _targetTeam) {
        if (_target.ScriptableObject.Team == Character.ETeam.Ally)
            return (_targetTeam);
        else {
            List<CharacterObject> targets = new List<CharacterObject>();
            int targetPlace = _target.Data.Position / 3;

            for (int i = 0; i < 3; i++) {
                CharacterObject tmp = getCharacterByPosition(_targetTeam, i + (targetPlace * 3));
                
                if (tmp != null)
                    targets.Add(tmp);
            }
            return (targets);
        }
    }

    List<CharacterObject> columnTarget(CharacterObject _target, List<CharacterObject> _targetTeam) {
        if (_target.ScriptableObject.Team == Character.ETeam.Ally)
            return (new List<CharacterObject>{ _target });
        else {
            List<CharacterObject> targets = new List<CharacterObject>();
            int targetPlace = _target.Data.Position / 3;

            for (int i = 0; i < 3; i++) {
                CharacterObject tmp = getCharacterByPosition(_targetTeam, targetPlace + (i * 3));
                
                if (tmp != null)
                    targets.Add(tmp);
            }
            return (targets);
        }
    }

    List<CharacterObject> squareTarget(CharacterObject _target, List<CharacterObject> _targetTeam) {
        if (_target.ScriptableObject.Team == Character.ETeam.Ally) {
            CharacterObject character1 = getCharacterByPosition(_targetTeam, _target.Data.Position - 1);
            CharacterObject character2 = _target;
            CharacterObject character3 = getCharacterByPosition(_targetTeam, _target.Data.Position + 1);
            List<CharacterObject> targets = new List<CharacterObject>{
                character1,
                character2,
                character3
            };
            targets.RemoveAll(character => character == null);
            return (targets);
        } else {
            List<CharacterObject> targets = new List<CharacterObject>();

            for (int i = -1; i < 2; i++) {
                for (int j = -1; j < 2; j++) {
                    int targetPlace = _target.Data.Position + (i * 3) + j;
                    CharacterObject tmp = getCharacterByPosition(_targetTeam, targetPlace);

                    if (tmp != null)
                        targets.Add(tmp);
                }
            }
            return (targets);
        }
    }

    List<CharacterObject> allTarget(List<CharacterObject> _targetTeam) {
        return (_targetTeam);
    }
    
    CharacterObject getCharacterByPosition(List<CharacterObject> _targetTeam, int position) {
        CharacterObject returnValue = null;
        foreach (CharacterObject character in _targetTeam) {
            if (character.Data.Position == position)
                returnValue = character;
        }
        return (returnValue);
    }
}
