using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightManager : Singleton<FightManager>
{
    [SerializeField] private TurnManager m_TurnManager;
    [SerializeField] private GameObject m_UIActionPart;
    [SerializeField] private List<GameObject> m_UISkillButtons;
    [SerializeField] private List<CharacterObject> m_Allies;
    [SerializeField] private List<CharacterObject> m_Enemies;
    [SerializeField] private CharacterObject m_CurrentTurn = null;
    private SkillData m_SelectedSkill = null;
    [SerializeField] private bool m_InFight = true;
    public TurnManager TurnManager => m_TurnManager;
    public List<CharacterObject> Allies => m_Allies;
    public List<CharacterObject> Enemies => m_Enemies;
    public CharacterObject CurrentTurn => m_CurrentTurn;
    public SkillData SelectedSkill => m_SelectedSkill;

    public void SetAllies(List<CharacterObject> allies)
    {
        m_Allies = allies;
        m_Allies.ForEach((ally) => {
            new List<FightCharacter>(FindObjectsOfType<FightCharacter>()).Find(x => x.Position == ally.Data.Position && x.Team == ally.ScriptableObject.Team).SetCharacter(ally);
        });
    }

    public void CreateFight(List<CharacterObject> enemies)
    {
        m_Enemies = enemies;
        m_InFight = true;
        m_Enemies.ForEach((enemy) => {
            new List<FightCharacter>(FindObjectsOfType<FightCharacter>()).Find(x => x.Position == enemy.Data.Position && x.Team == enemy.ScriptableObject.Team).SetCharacter(enemy);
        });
        m_UIActionPart.SetActive(false);
    }

    void Start()
    {
        m_UIActionPart.SetActive(false);
    }

    void FixedUpdate()
    {
        if (m_InFight)
        {
            if (m_CurrentTurn == null)
            {
                if (m_TurnManager.CharactersTurn.Count != 0)
                {
                    m_CurrentTurn = m_TurnManager.CharactersTurn[0].CharacterObject;
                    DoTurn();
                }
                else
                {
                    m_TurnManager.FightUpdate();
                    DoContinuousStatus();
                }
            }
        }
    }


    void DoTurn() {
        m_CurrentTurn.Data.DoStatus(StatusEnum.EStatusActionTime.StartOfTurn);
        if (m_CurrentTurn.Data.CheckStatusEffect(StatusEnum.EStatusType.Paralysis))
        {
            m_TurnManager.TurnEnd();
            m_CurrentTurn = null;
        }
        else
        {
            if (m_CurrentTurn.ScriptableObject.Team == Character.ETeam.Ally)
            {
                m_UIActionPart.SetActive(true);
                m_UISkillButtons.ForEach(x => x.SetActive(true));
            }
            else
            {
                IA();
            }
        }
    }

    void IA()
    {
        //call doaction
    }

    public void SelectSkill(SkillData skill)
    {
        m_SelectedSkill = skill;
        new List<FightCharacter>(FindObjectsOfType<FightCharacter>()).ForEach(x => x.SkillSelected(skill, m_CurrentTurn));
    }

    public void DoAction(SkillData skill, CharacterObject target)
    {
        //Rand confusion modify skill and target
        if (target.ScriptableObject.Team == Character.ETeam.Ally)
            m_CurrentTurn.Data.UseSkill(skill, target, m_Allies);
        else
            m_CurrentTurn.Data.UseSkill(skill, target, m_Enemies);
        m_CurrentTurn.Data.DoStatus(StatusEnum.EStatusActionTime.EndOfTurn);
        m_CurrentTurn.Data.UpdateStatus(1, StatusEnum.EStatusDurationType.Turn);
        m_UIActionPart.SetActive(false);
        m_CurrentTurn = null;
        m_SelectedSkill = null;
        new List<FightCharacter>(FindObjectsOfType<FightCharacter>()).ForEach(x => x.CancelTarget());
        m_TurnManager.TurnEnd();
    }

    private void DoContinuousStatus() {
        m_Allies.ForEach(x => x.Data.UpdateStatus(Time.deltaTime, StatusEnum.EStatusDurationType.Second));
        m_Enemies.ForEach(x => x.Data.UpdateStatus(Time.deltaTime, StatusEnum.EStatusDurationType.Second));
    }
}
