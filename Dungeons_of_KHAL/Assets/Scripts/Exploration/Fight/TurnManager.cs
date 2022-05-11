using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    [SerializeField] private GameObject StartWaypoint;
    [SerializeField] private GameObject EndWaypoint;
    [SerializeField] private float SpeedConstant = 1;

    private List<TurnIcon> m_Icons;
    private List<FightCharacter> m_CharactersTurn = new List<FightCharacter>();
    public List<FightCharacter> CharactersTurn => m_CharactersTurn;

    void Start() {
        m_Icons = new List<TurnIcon>(FindObjectsOfType<TurnIcon>());
        m_Icons.ForEach((icon) => {
            this.ResetCharacter(icon.FightCharacter);
        });
    }

    public void FightUpdate() {
        if (m_CharactersTurn.Count == 0) {
            TurnIcon farest = null;
            m_Icons.ForEach(x => {
                if ((farest == null || NextPos(x, 1) > NextPos(farest, 1)) && !x.FightCharacter.CharacterObject.Data.CheckStatusEffect(StatusEnum.EStatusType.Freeze)) farest = x;
            });
            if (farest != null)
            {
                float percentage = 1.0f;
                if (NextPos(farest, 1) >= EndWaypoint.transform.position.x)
                    percentage = Mathf.Abs(EndWaypoint.transform.position.x - farest.transform.position.x) / Mathf.Abs(NextPos(farest, 1) - farest.transform.position.x);
                m_Icons.ForEach(x => {
                    if (!x.FightCharacter.CharacterObject.Data.CheckStatusEffect(StatusEnum.EStatusType.Freeze)) MoveIcon(x, percentage);
                });
            }
        }
    }

    private float NextPos(TurnIcon icon, float percentage)
    {
        return (icon.transform.position.x + icon.FightCharacter.CharacterObject.ScriptableObject.Speed * SpeedConstant * percentage);
    }

    private void MoveIcon(TurnIcon icon, float percentage) {
        icon.transform.position = new Vector3(NextPos(icon, percentage), icon.transform.position.y, icon.transform.position.z);
        if (icon.transform.position.x >= EndWaypoint.transform.position.x) {
            m_CharactersTurn.Add(icon.FightCharacter);
            icon.transform.position = EndWaypoint.transform.position;
        }
    }

    private TurnIcon GetIcon(FightCharacter character) {
        List<TurnIcon> relatedIcons = m_Icons.FindAll(x => x.FightCharacter == character);
        relatedIcons.Sort((x, y) => x.transform.position.x.CompareTo(y.transform.position.x));
        if (relatedIcons.Count == 0)
            return null;
        return relatedIcons[relatedIcons.Count - 1];
    }

    public void ResetCharacter(FightCharacter character) {
        GetIcon(character).transform.position = StartWaypoint.transform.position;
    }

    public void TurnEnd() {
        ResetCharacter(m_CharactersTurn[0]);
        m_CharactersTurn.RemoveAt(0);
    }

    public FightCharacter GetCharacter(CharacterObject character)
    {
        return (m_Icons.Find((x) => (x.FightCharacter.CharacterObject == character)).FightCharacter);
    }
}
