using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class FightManager : Singleton<FightManager>
{
    [SerializeField] private TurnManager m_TurnManager;
    [SerializeField] private List<FightCharacter> m_Allies;
    [SerializeField] private List<FightCharacter> m_Enemies;
    [SerializeField] private CharacterObject m_CurrentTurn = null;
    private SkillData m_SelectedSkill = null;
    [SerializeField] private bool m_InFight = true;
    public TurnManager TurnManager => m_TurnManager;
    public List<FightCharacter> FightCharacters => new List<FightCharacter>(Allies).Union<FightCharacter>(new List<FightCharacter>(Enemies)).ToList<FightCharacter>();
    public List<FightCharacter> Allies => m_Allies;
    public List<FightCharacter> Enemies => m_Enemies;
    public List<CharacterObject> CharactersObject => new List<CharacterObject>(AlliesObject).Union<CharacterObject>(new List<CharacterObject>(EnemiesObject)).ToList<CharacterObject>();
    public List<CharacterObject> AlliesObject => m_Allies.Select(x => x.CharacterObject).ToList<CharacterObject>();
    public List<CharacterObject> EnemiesObject => m_Enemies.Select(x => x.CharacterObject).ToList<CharacterObject>();
    public List<CharacterObject> CharactersAlive => new List<CharacterObject>(AlliesAlive).Union<CharacterObject>(new List<CharacterObject>(EnemiesAlive)).ToList<CharacterObject>();
    public List<CharacterObject> AlliesAlive => AlliesObject.FindAll(x => !x.Data.IsDead);
    public List<CharacterObject> EnemiesAlive => EnemiesObject.FindAll(x => !x.Data.IsDead);
    public CharacterObject CurrentTurn => m_CurrentTurn;
    public SkillData SelectedSkill => m_SelectedSkill;

    private DungeonManager m_DungeonManager;

    public void SetAllies(List<CharacterObject> allies)
    {
        m_Allies.ForEach(x => {
            allies.ForEach(y => {
                if (x.Position == y.Data.Position && x.Team == y.ScriptableObject.Team) x.SetCharacter(y);
            });
        });
    }

    public void CreateFight(List<CharacterObject> enemies)
    {
        m_Enemies.ForEach(x => {
            enemies.ForEach(y => {
                if (x.Position == y.Data.Position && x.Team == y.ScriptableObject.Team) x.SetCharacter(y);
            });
        });
        m_InFight = true;
        m_DungeonManager.UIManager.MiddlePanel.ActiveMiddlePanel(false);
    }

    void Start()
    {
        m_DungeonManager = DungeonManager.Instance;
        m_DungeonManager.UIManager.MiddlePanel.ActiveMiddlePanel(false);
        this.SetAllies(new List<CharacterObject>(FindObjectsOfType<CharacterObject>()));
        this.CreateFight(new List<CharacterObject>(FindObjectsOfType<CharacterObject>()));
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
            TurnEnd();
            m_CurrentTurn = null;
        }
        else
        {
            if (m_CurrentTurn.ScriptableObject.Team == Character.ETeam.Ally)
            {
                m_DungeonManager.UIManager.MiddlePanel.ActiveMiddlePanel(false);
                m_DungeonManager.UIManager.MiddlePanel.ActiveMiddlePanel(true);
                m_DungeonManager.UIManager.MiddlePanel.ActivePanel(EUIPanel.Skill);
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

    void TurnEnd()
    {
        m_CurrentTurn.Data.DoStatus(StatusEnum.EStatusActionTime.EndOfTurn);
        m_CurrentTurn.Data.UpdateStatus(1, StatusEnum.EStatusDurationType.Turn);
        m_DungeonManager.UIManager.MiddlePanel.ActiveMiddlePanel(false);
        m_CurrentTurn = null;
        m_SelectedSkill = null;
        FightCharacters.ForEach(x => x.CancelTarget());
        m_TurnManager.TurnEnd();
        m_TurnManager.UpdateAlive();
    }

    public void SelectSkill(SkillData skill)
    {
        m_SelectedSkill = skill;
        FightCharacters.ForEach(x => x.SkillSelected(skill, m_CurrentTurn));
    }

    public void DoAction(SkillData skill, CharacterObject target)
    {
        //Rand confusion modify skill and target
        if (target.ScriptableObject.Team == Character.ETeam.Ally)
            m_CurrentTurn.Data.UseSkill(skill, target, AlliesObject);
        else
            m_CurrentTurn.Data.UseSkill(skill, target, AlliesObject);
        TurnEnd();
    }

    private void DoContinuousStatus() {
        AlliesAlive.ForEach(x => x.Data.UpdateStatus(Time.deltaTime, StatusEnum.EStatusDurationType.Second));
        EnemiesAlive.ForEach(x => x.Data.UpdateStatus(Time.deltaTime, StatusEnum.EStatusDurationType.Second));
    }
}
