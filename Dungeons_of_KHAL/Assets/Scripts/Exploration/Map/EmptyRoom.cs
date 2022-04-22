using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EmptyRoom : ARoom
{
    public override void InitRoom()
    {
        Debug.Log("Initialization of a room: " + m_Index);
    }

    public override void EnterRoom()
    {
        Debug.Log("Enter in room: " + m_Index);
        m_Explored = true;
        UpdateImage(true);

    }

    public override void LeaveRoom()
    {
        Debug.Log("Leave this room: " + m_Index);
        UpdateImage(false);
    }
}
