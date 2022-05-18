using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatBar : MonoBehaviour
{
    public void UpdateStat(int maxStat, int actualStat)
    {
        this.transform.localScale = new Vector3(this.transform.localScale.x, maxStat == 0 ? 0 : actualStat / maxStat, this.transform.localScale.z);
    }
}
