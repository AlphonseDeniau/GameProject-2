using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{

    // Room prefab \\
    [SerializeField] private List<ARoom> m_RoomAvailable;


    // Value of the Map \\
    private int m_Depth;
    private bool m_Initialized;
    private ARoom m_CurrentRoom;
    private List<ARoom> m_Rooms = new List<ARoom>();
    private List<ARoom> m_RoomsExplored = new List<ARoom>();


    private void Start()
    {
        CreateMap(8, 15);
        DrawMap();
    }

    public bool Initialized => m_Initialized;

    // Create all the map with procedural check \\
    public void CreateMap(int depthMin, int depthMax)
    {
        // Init the first Room
        ARoom firstRoom = new EmptyRoom(0, 0);
        firstRoom.x = 0;
        firstRoom.y = 0;
        m_Rooms.Add(firstRoom);

        m_Depth = Random.Range(depthMin, depthMax);
        for (int i = 0; i < m_Depth; i++)
        {
            List<ARoom> rooms = m_Rooms.FindAll(x => x.Index == i);
            rooms.ForEach(x => CreateAdjacenteRoom(x));
        }

        m_Initialized = true;
    }

    void CreateAdjacenteRoom(ARoom room)
    {
        CheckAdjacentRoom(room, 0, -1);
        CheckAdjacentRoom(room, 0, 1);
        CheckAdjacentRoom(room, -1, 0);
        CheckAdjacentRoom(room, 1, 0);
    }

    void CheckAdjacentRoom(ARoom room, int offsetX, int offsetY)
    {
        if (!m_Rooms.Exists(k => (k.x == room.x + offsetX && k.y == room.y + offsetY)))
        {
            if (Random.Range(0, 100) < 50 || (offsetX == 1 && offsetY == 1))
            {
                ARoom newRoom = new EmptyRoom(room.Depth + 1, m_Rooms.Count);
                newRoom.x = room.x + offsetX;
                newRoom.y = room.y + offsetY;
                room.AddRoom(newRoom);
                m_Rooms.Add(newRoom);
            }
        }
    }

    public void DrawMap()
    {
        Debug.Log("Number of Rooms: " + m_Rooms.Count);
        m_Rooms.ForEach(x => print("Room: " + x.x + "-" + x.y + " depth " + x.Depth));

        List<string> maps = new List<string>();
        for (int y = m_Depth; y > -m_Depth; y -= 1)
        {
            string line = "";
            for (int x = -m_Depth; x < m_Depth; x++)
            {

                if (m_Rooms.Find(r => r.x == x && r.y == y) != null)
                {
                    line += 'o';
                }
                else
                {
                    line += '.';
                }
            }
            maps.Add(line);
        }
        maps.ForEach(x => Debug.Log(x));
    }

    // Make the new current room and set the exploration \\
    public void UpdateMap(ARoom newRoom)
    {
        newRoom.InitRoom();
        newRoom.EnterInRoom();
        if (m_RoomsExplored.Find(x => x == newRoom) != null)
        {
            m_RoomsExplored.Add(newRoom);
        }
    }


    // Get current room \\
    public ARoom GetCurrentRoom(int index)
    {
        return m_CurrentRoom;
    }

    // Get all rooms \\
    public List<ARoom> GetRooms()
    {
        return m_Rooms;
    }

    // Get next rooms of the current room \\
    public List<ARoom> GetNextRooms(int index)
    {
        return m_CurrentRoom.nextRooms;
    }
}
