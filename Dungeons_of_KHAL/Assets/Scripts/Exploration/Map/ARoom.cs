using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public abstract class ARoom : MonoBehaviour
{
    [Header("Graphical Parameters")]
    [SerializeField] protected Sprite m_HiddenRoom;
    [SerializeField] protected Sprite m_ShowedRoom;
    protected Image m_Image;

    [Header("Logical Parameters")]
    [SerializeField] protected float m_XPos;
    [SerializeField] protected float m_YPos;

    [SerializeField] protected int m_Index;
    [SerializeField] protected int m_Depth;
    [SerializeField] protected bool m_Explored = false;

    // Next rooms available
    [Header("Connection")]
    [SerializeField] protected ARoom m_PreviousRoom = null;
    [SerializeField] protected List<ARoom> m_NextRooms = new List<ARoom>();

    // -------- Accessors -------- \\
    public float xPos => m_XPos;
    public float yPos => m_YPos;

    public int index => m_Index;
    public int depth => m_Depth;
    public bool isExplored => m_Explored;

    public List<ARoom> GetNextRoom(bool withPrevious = true)
    {
        return m_NextRooms.FindAll(x => x != m_PreviousRoom || ((x == m_PreviousRoom) && withPrevious));
    }

    public bool HasRoomDirection(int direction)
    {
        int xDir = direction % 2 == 0 ? direction - 1 : 0;
        int yDir = direction % 2 == 1 ? direction - 2 : 0;
        return m_NextRooms.Exists(x => x.xPos == xPos + xDir && x.yPos == yPos - yDir);
    }

    public ARoom GetRoomDirection(int direction)
    {
        int xDir = direction % 2 == 0 ? direction - 1 : 0;
        int yDir = direction % 2 == 1 ? direction - 2 : 0;
        return m_NextRooms.Find(x => x.xPos == xPos + xDir && x.yPos == yPos - yDir);
    }

    public bool AddNextRoom(ARoom room)
    {
        if (room == null || room == m_PreviousRoom || m_NextRooms.Contains(room)) return false;
        m_NextRooms.Add(room);
        return true;
    }

    public bool RemoveNextRoom(ARoom room)
    {
        if (room == null || room == m_PreviousRoom || !m_NextRooms.Contains(room)) return false;
        m_NextRooms.Remove(room);
        return true;
    }

    // -------- --------- -------- \\

    private void Awake()
    {
        m_Image = GetComponent<Image>();
        m_Image.sprite = m_Explored ? m_ShowedRoom : m_HiddenRoom;
    }

    public void Initialization(int depth, int index, float x = 0, float y = 0, ARoom previousRoom = null) {
        m_Depth = depth;
        m_Index = index;

        m_XPos = x;
        m_YPos = y;

        m_PreviousRoom = previousRoom;
        if (m_PreviousRoom != null)
            m_NextRooms.Add(previousRoom);
    }

    protected void UpdateImage(bool selected)
    {
        m_Image.sprite = m_Explored ? m_ShowedRoom : m_HiddenRoom;
        m_Image.color = selected ? Color.gray : Color.white;
    }

    // Abstract Methods \\
    public abstract void InitRoom();
    public abstract void EnterRoom();
    public abstract void LeaveRoom();
}
