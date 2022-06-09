using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class FeedbackManager : MonoBehaviour
{
    [SerializeField] private GameObject m_DamageTextPrefab;
    [SerializeField] private GameObject m_DodgeTextPrefab;
    [SerializeField] private GameObject m_ParryTextPrefab;
    [SerializeField] private GameObject m_HealTextPrefab;

    private Vector3 ScalePos(Vector3 pos)
    {
        return (new Vector3(pos.x * 1920 / (16 * 11 / 9), pos.y * 1080 / 11, pos.z));
    }

    private GameObject CreateText(GameObject obj, int position, Character.ETeam team)
    {
        Vector3 pos = FightManager.Instance.FightCharacters.Find(x => x.CharacterObject.ScriptableObject.Team == team && x.CharacterObject.Data.Position == position).transform.position;
        GameObject tmp = Instantiate(obj, new Vector3(), new Quaternion());
        tmp.transform.SetParent(this.transform);
        tmp.GetComponent<RectTransform>().localPosition = ScalePos(pos);
        tmp.transform.DOMoveY(tmp.transform.position.y + 50, 3).OnComplete(tmp.GetComponent<FeedbackText>().DestroyObject);
        return tmp;
    }

    public void DamageText(int position, Character.ETeam team, int value)
    {
        GameObject tmp = CreateText(m_DamageTextPrefab, position, team);
        tmp.GetComponent<Text>().text = value.ToString();
    }

    public void DodgeText(int position, Character.ETeam team)
    {
        GameObject tmp = CreateText(m_DodgeTextPrefab, position, team);
    }

    public void ParryText(int position, Character.ETeam team)
    {
        GameObject tmp = CreateText(m_ParryTextPrefab, position, team);
    }

    public void HealText(int position, Character.ETeam team, int value)
    {
        GameObject tmp = CreateText(m_HealTextPrefab, position, team);
        tmp.GetComponent<Text>().text = value.ToString();
    }
}
