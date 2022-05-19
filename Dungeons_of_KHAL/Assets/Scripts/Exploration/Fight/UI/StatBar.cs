using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatBar : MonoBehaviour
{
    public void UpdateStat(int maxStat, int actualStat)
    {
        this.transform.localScale = new Vector3(this.transform.localScale.x, maxStat == 0 ? 0 : (float) actualStat / (float) maxStat, this.transform.localScale.z);
        this.transform.localPosition = new Vector3(this.transform.localPosition.x, (this.transform.localScale.y - 1) / 2, this.transform.localPosition.z);
    }
}
