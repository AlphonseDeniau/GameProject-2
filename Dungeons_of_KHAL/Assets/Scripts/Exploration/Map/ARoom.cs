using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ARoom : MonoBehaviour
{

    // Graphical and Logical Place
    protected int m_OffsetX;
    protected int m_OffsetY;
    public int x { get { return m_OffsetX; } set { m_OffsetX = value; } }
    public int y { get { return m_OffsetY; } set { m_OffsetY = value; } }

    // Index of the creation (make it unique)
    protected int m_Index;
    // Depth to know the distance from the beginning
    protected int m_Depth;

    // State of Room \\
    protected bool m_Explored;
    public bool isExplored => m_Explored;

    // Next rooms available
    protected List<ARoom> m_NextRooms;
    public List<ARoom> nextRooms => m_NextRooms;

    ARoom (int depth, int index) {
        m_Depth = depth;
        m_Index = index;
    }

    // Abstract Methods \\
    public abstract void InitRoom();
    public abstract void EnterInRoom();


    // Getter - Setter \\
    public bool AddRoom(ARoom room)
    {
        if (room == null)
        {
            return false;
        }
        m_NextRooms.Add(room);
        return true;
    }

    public bool RemoveRoom(ARoom room)
    {
        if (room == null)
        {
            return false;
        }
        m_NextRooms.Remove(room);
        return true;
    }
}
