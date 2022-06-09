using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackParticleManager : MonoBehaviour
{
    [SerializeField] private GameObject m_PrefabFireParticle;
    [SerializeField] private GameObject m_PrefabWaterParticle;
    [SerializeField] private GameObject m_PrefabWindParticle;
    [SerializeField] private GameObject m_PrefabEarthParticle;
    private StackEnum.EStackType m_StackUsed = StackEnum.EStackType.Neutral;
    private GameObject m_Particle;

    private void InstantiateParticle(GameObject particle)
    {
        m_Particle = Instantiate(particle);
        m_Particle.transform.SetParent(this.gameObject.transform);
        m_Particle.transform.localPosition = new Vector3(0, 0, 0);
    }

    public void UpdateParticle(StackEnum.EStackType type)
    {
        if (m_StackUsed != type)
        {
            if (m_Particle)
                DestroyImmediate(m_Particle);
            if (type == StackEnum.EStackType.Fire)
                InstantiateParticle(m_PrefabFireParticle);
            if (type == StackEnum.EStackType.Water)
                InstantiateParticle(m_PrefabWaterParticle);
            if (type == StackEnum.EStackType.Wind)
                InstantiateParticle(m_PrefabWindParticle);
            if (type == StackEnum.EStackType.Earth)
                InstantiateParticle(m_PrefabEarthParticle);
        }
    }
}
