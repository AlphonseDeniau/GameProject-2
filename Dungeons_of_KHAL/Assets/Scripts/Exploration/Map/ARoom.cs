using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ARoom
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

    // Next rooms available
    protected List<ARoom> m_NextRooms = new List<ARoom>();
    public List<ARoom> nextRooms => m_NextRooms;

    public ARoom (int depth, int index) {
        m_Depth = depth;
        m_Index = index;
    }

    // Getter - Setter \\
    public int Index => m_Index;
    public int Depth => m_Depth;
    public bool isExplored => m_Explored;

    public bool AddRoom(ARoom room)
    {
        Debug.Log(room.ToString());
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

    // Abstract Methods \\
    public abstract void InitRoom();
    public abstract void EnterInRoom();
}
