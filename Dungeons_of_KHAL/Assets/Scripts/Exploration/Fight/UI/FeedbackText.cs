using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeedbackText : MonoBehaviour
{
    public void DestroyObject()
    {
        DestroyImmediate(this.gameObject);
    }
}
