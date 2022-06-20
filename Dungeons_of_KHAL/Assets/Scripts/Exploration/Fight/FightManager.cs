using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class FightManager : Singleton<FightManager>
{
    [SerializeField] private List<FightCharacter> m_Allies;
    [SerializeField] private List<FightCharacter> m_Enemies;
    [SerializeField] private CharacterObject m_CurrentTurn = null;
    private SkillData m_SelectedSkill = null;
    [SerializeField] private bool m_InFight = false;
    public TurnManager TurnManager => m_TurnManager;
    public List<FightCharacter> FightCharacters => new List<FightCharacter>(Allies).Union<FightCharacter>(new List<FightCharacter>(Enemies)).ToList<FightCharacter>();
    public List<FightCharacter> Allies => m_Allies;
    public List<FightCharacter> Enemies => m_Enemies;
    public List<CharacterObject> CharactersObject => new List<CharacterObject>(AlliesObject).Union<CharacterObject>(new List<CharacterObject>(EnemiesObject)).ToList<CharacterObject>();
    public List<CharacterObject> AlliesObject => m_Allies.Select(x => x.CharacterObject).ToList<CharacterObject>();
    public List<CharacterObject> EnemiesObject => m_Enemies.Select(x => x.CharacterObject).ToList<CharacterObject>();
    public List<CharacterObject> CharactersAlive => new List<CharacterObject>(AlliesAlive).Union<CharacterObject>(new List<CharacterObject>(EnemiesAlive)).ToList<CharacterObject>();
    public List<CharacterObject> AlliesAlive => AlliesObject.FindAll(x => x != null && x.Data != null && !x.Data.IsDead);
    public List<CharacterObject> EnemiesAlive => EnemiesObject.FindAll(x => x != null && x.Data != null && !x.Data.IsDead);
    public CharacterObject CurrentTurn => m_CurrentTurn;
    public SkillData SelectedSkill => m_SelectedSkill;

    private DungeonManager m_DungeonManager;
    private TurnManager m_TurnManager;

    public void SetAllies(List<CharacterObject> allies)
    {
        m_Allies.ForEach(x => {
            bool find = false;
            allies.ForEach(y => {
                if (x.Position == y.Data.Position && x.Team == y.ScriptableObject.Team) {
                    x.SetCharacter(y);
                    find = true;
                }
            });
            if (!find) {
                x.NoCharacter();
            }
        });
        FightCharacters.ForEach(x => x.UpdateStat());
    }

    public void CreateFight(List<CharacterObject> enemies)
    {
        m_Enemies.ForEach(x => {
            bool find = false;
            enemies.ForEach(y => {
                if (x.Position == y.Data.Position && x.Team == y.ScriptableObject.Team) {
                    find = true;
                    x.SetCharacter(y);
                }
            });
            if (!find)
            {
                x.NoCharacter();
            }
        });
        m_InFight = true;
        m_DungeonManager.UIManager.MiddlePanel.ActiveMiddlePanel(false);
        m_TurnManager.StartFight();
        UpdateAlive();
        FightCharacters.ForEach(x => x.UpdateStat());
    }

    public void Initialize()
    {
        m_DungeonManager = DungeonManager.Instance;
        m_DungeonManager.UIManager.MiddlePanel.ActiveMiddlePanel(false);
        m_TurnManager = m_DungeonManager.UIManager.TurnManager;

        CreateFight(m_DungeonManager.CharacterManager.Enemies);
    }

    public void Uninitialize()
    {
        m_InFight = false;
        FightCharacters.ForEach(x => x.CharacterObject.Data.Uninitialize());
        FightCharacters.ForEach(x => x.UpdateStat());
        m_TurnManager.EndFight();
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
        FightCharacters.Find(x => x.CharacterObject == m_CurrentTurn).StartTurn();
        m_CurrentTurn.Data.DoStatus(StatusEnum.EStatusActionTime.StartOfTurn);
        FightCharacters.ForEach(x => x.UpdateStat());
        if (m_CurrentTurn.Data.IsDead)
        {
            StartCoroutine(WaitTurn());
            return;
        }
        if (m_CurrentTurn.Data.CheckStatusEffect(StatusEnum.EStatusType.Paralysis))
        {
            StartCoroutine(WaitTurn());
        }
        else
        {
            if (m_CurrentTurn.ScriptableObject.Skills.Count == 0)
                TurnEnd();
            else
            {
                if (m_CurrentTurn.ScriptableObject.Team == Character.ETeam.Ally)
                {
                    m_DungeonManager.UIManager.MiddlePanel.ActiveMiddlePanel(false);
                    m_DungeonManager.UIManager.MiddlePanel.ActiveMiddlePanel(true);
                    m_DungeonManager.UIManager.MiddlePanel.ActivePanel(EUIPanel.Skill);
                    m_DungeonManager.UIManager.InstructionText.text = "Select a skill";
                    if (m_CurrentTurn != null)
                        m_DungeonManager.UIManager.StatUI.UpdateText("Player",
                        m_CurrentTurn.Data.ActualHP, m_CurrentTurn.ScriptableObject.MaxHP,
                        m_CurrentTurn.Data.ActualMP, m_CurrentTurn.ScriptableObject.MaxMP,
                        m_CurrentTurn.ScriptableObject.Strength, m_CurrentTurn.ScriptableObject.Defense,
                        m_CurrentTurn.ScriptableObject.Magic, m_CurrentTurn.ScriptableObject.Speed
                        );
                }
                else
                {
                    StartCoroutine(IAWait());
                }
            }
        }
    }

    IEnumerator WaitTurn()
    {
        yield return new WaitForSecondsRealtime(2.0f);
        TurnEnd();
        m_CurrentTurn = null;
    }

    IEnumerator IAWait()
    {
        yield return new WaitForSecondsRealtime(2.0f);
        IA();
    }

    void IA()
    {
        SkillData skill = m_CurrentTurn.Data.Skills[Random.Range(0, m_CurrentTurn.Data.Skills.Count)];
        while (!skill.IsUsable(m_CurrentTurn))
            skill = m_CurrentTurn.Data.Skills[Random.Range(0, m_CurrentTurn.Data.Skills.Count)];
        m_SelectedSkill = skill;
        if (skill.Skill.Type == SkillEnum.ETargetType.Self)
            DoSkill(m_CurrentTurn, true);
        if (skill.Skill.Type == SkillEnum.ETargetType.Ally)
            DoSkill(AlliesAlive[Random.Range(0, AlliesAlive.Count)], true);
        if (skill.Skill.Type == SkillEnum.ETargetType.Enemy)
            DoSkill(EnemiesAlive[Random.Range(0, EnemiesAlive.Count)], true);
    }

    void TurnEnd()
    {
        FightCharacters.Find(x => x.CharacterObject == m_CurrentTurn).EndTurn();
        m_CurrentTurn.Data.DoStatus(StatusEnum.EStatusActionTime.EndOfTurn);
        m_CurrentTurn.Data.UpdateStatus(1, StatusEnum.EStatusDurationType.Turn);
        m_DungeonManager.UIManager.MiddlePanel.ActiveMiddlePanel(false);
        m_DungeonManager.UIManager.InstructionText.text = "";
        m_CurrentTurn = null;
        m_SelectedSkill = null;
        FightCharacters.ForEach(x => x.CancelTarget());
        m_TurnManager.TurnEnd();
        UpdateAlive();
        FightCharacters.ForEach(x => x.UpdateStat());
        if (AlliesAlive.Count == 0)
        {
            m_DungeonManager.EndFight();
        }
        if (EnemiesAlive.Count == 0)
        {
            m_DungeonManager.EndFight();
        }
    }

    public void SelectSkill(SkillData skill)
    {
        m_SelectedSkill = skill;
        FightCharacters.ForEach(x => x.CancelTarget());
        if (skill != null)
            FightCharacters.ForEach(x => x.SkillSelected(skill, m_CurrentTurn));
        m_DungeonManager.UIManager.InstructionText.text = "Select a target";
    }

    public void SelectItem(Item item)
    {
        m_SelectedSkill = null;
        FightCharacters.ForEach(x => x.CancelTarget());
        if (item != null)
            FightCharacters.ForEach(x => x.ItemSelected(item, m_CurrentTurn));
        m_DungeonManager.UIManager.InstructionText.text = "Select a target";
    }

    private void UpdateAlive()
    {
        FightCharacters.ForEach(x => {
            TurnManager.UpdateAlive(x);
            if (x.CharacterObject == null || x.CharacterObject.Data.IsDead)
                x.HideSprite();
            if (x.CharacterObject != null && !x.CharacterObject.Data.IsDead)
                x.ShowSprite();
        });
    }

    public void DoSkill(CharacterObject target, bool loop)
    {
        if (!loop && (m_CurrentTurn.Data.CheckStatusEffect(StatusEnum.EStatusType.Confusion) && Random.Range(0, 100) < 50))
            IA();
        else
        {
            if (target.ScriptableObject.Team == Character.ETeam.Ally)
                m_CurrentTurn.Data.UseSkill(m_SelectedSkill, target, AlliesObject);
            else
                m_CurrentTurn.Data.UseSkill(m_SelectedSkill, target, EnemiesObject);
            TurnEnd();
        }
    }

    public void DoItem(CharacterObject target)
    {
        if (target.ScriptableObject.Team == Character.ETeam.Ally)
            m_DungeonManager.Inventory.UseItem(target);
        else
            m_DungeonManager.Inventory.UseItem(target);
        TurnEnd();
    }

    private void DoContinuousStatus() {
        AlliesAlive.ForEach(x => x.Data.UpdateStatus(Time.deltaTime, StatusEnum.EStatusDurationType.Second));
        EnemiesAlive.ForEach(x => x.Data.UpdateStatus(Time.deltaTime, StatusEnum.EStatusDurationType.Second));
        UpdateAlive();
        FightCharacters.ForEach(x => x.UpdateStat());
    }
}
