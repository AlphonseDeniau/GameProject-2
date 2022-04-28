using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    [SerializeField] private GameObject StartWaypoint;
    [SerializeField] private GameObject EndWaypoint;
    [SerializeField] private float SpeedConstant = 1;

    private List<TurnIcon> m_Icons;
    [SerializeField] private List<FightCharacter> m_CharactersTurn = new List<FightCharacter>();
    public List<FightCharacter> CharactersTurn => m_CharactersTurn;

    void Start() {
        m_Icons = new List<TurnIcon>(FindObjectsOfType<TurnIcon>());
        m_Icons.ForEach((icon) => {
            //icon.transform.position = StartWaypoint.transform.position;
        });
    }

    public void FightUpdate() {
        if (m_CharactersTurn.Count == 0) {
            foreach (TurnIcon icon in m_Icons) {
                if (!icon.FightCharacter.CharacterObject.Data.CheckStatusEffect(StatusEnum.EStatusType.Freeze)) MoveIcon(icon);
            }
        }
    }

    private void MoveIcon(TurnIcon icon) {
        icon.transform.position += new Vector3(icon.FightCharacter.CharacterObject.ScriptableObject.Speed * SpeedConstant, 0, 0);
        if (icon.transform.position.x >= EndWaypoint.transform.position.x) {
            m_CharactersTurn.Add(icon.FightCharacter);
            icon.transform.position = EndWaypoint.transform.position;
        }
    }

    private TurnIcon GetIcon(FightCharacter character) {
        foreach (TurnIcon icon in m_Icons) {
            if (icon.FightCharacter == character)
                return (icon);
        }
        return (null);
    }

    public void ResetCharacter(FightCharacter character) {
        GetIcon(character).transform.position = StartWaypoint.transform.position;
    }

    public void TurnEnd() {
        ResetCharacter(m_CharactersTurn[0]);
        m_CharactersTurn.RemoveAt(0);
    }
}
