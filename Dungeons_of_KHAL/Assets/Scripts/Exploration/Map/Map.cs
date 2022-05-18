using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Map : Singleton<Map>
{
    [Serializable]
    public class RoomSelector
    {
        public int m_BeginRange;
        public int m_EndRange;
        public GameObject m_RoomPrefab;
    }

    // Graphical
    [Header("Room Graphical Parameters")]
    [SerializeField] private GameObject m_ParentRoom;
    [SerializeField] private float m_SeperatorLength;
    [SerializeField] private float m_RoomLength;

    [Header("Corridor Graphical Parameters")]
    [SerializeField] private GameObject m_ParentCorridor;
    [SerializeField] private GameObject m_PrefabCorridor;

    [Header("Initialization Map Parameters")]
    [SerializeField] private int m_DepthMin;
    [SerializeField] private int m_DepthMax;
    [SerializeField] private List<RoomSelector> m_RoomSelector = new List<RoomSelector>();


    [Header("Actual Map Parameters")]
    [SerializeField] private bool m_Initialized;
    [SerializeField] private int m_DungeonDepth;

    [SerializeField] private ARoom m_CurrentRoom;
    [SerializeField] private List<ARoom> m_Rooms = new List<ARoom>(); 


    // ------------ Accessors ------------ \\
    public int depthMin => m_DepthMin;
    public int depthMax => m_DepthMax;
    public List<RoomSelector> roomSelector => m_RoomSelector;

    public bool initialized => m_Initialized;
    public int dungeonDepth => m_DungeonDepth;
    public ARoom currentRoom => m_CurrentRoom;
    public List<ARoom> rooms => m_Rooms;

    /// <summary>
    /// Set rooms which can be selected by the Creator Map
    /// </summary>
    /// <param name="newRoomSelector">Selectors</param>
    public void SetRoomSelector(List<RoomSelector> newRoomSelector)
    {
        if (newRoomSelector == null || newRoomSelector.Count == 0)
            return;
        m_RoomSelector = newRoomSelector;
    }

    /// <summary>
    /// Get Room by index
    /// </summary>
    /// <param name="index">index of the room</param>
    /// <returns>room with index</returns>
    public ARoom GetRoomIndex(int index)
    {
        return m_Rooms.Find(x => x.index == index);
    }

    /// <summary>
    /// Get next rooms of the current room
    /// </summary>
    /// <returns>next rooms of the current room</returns>
    public List<ARoom> GetNextRoom()
    {
        return m_CurrentRoom.GetNextRoom();
    }

    // ----------------------------------- \\

    public void MapStart()
    {
        InitializeMap();
        DrawCorridor();
    }

    public void InitializeMap()
    {
        m_Initialized = false;
        CreateMap(m_DepthMin, m_DepthMax);
    }

    // Create all the map with procedural check \\
    public void CreateMap(int depthMin, int depthMax)
    {
        // Init the first Room
        ARoom firstRoom = CreateRoom(0, 0, 0, 0, null);
        firstRoom.InitRoom();
        firstRoom.EnterRoom();
        m_CurrentRoom = firstRoom;

        m_DungeonDepth = UnityEngine.Random.Range(m_DepthMin, m_DepthMax);
        for (int i = 0; i < m_DungeonDepth; i++)
        {
            List<ARoom> rooms = m_Rooms.FindAll(x => x.index == i);
            rooms.ForEach(x => CreateAdjacenteRoom(x));
        }

        m_Initialized = true;
    }

    private void CreateAdjacenteRoom(ARoom room)
    {
        CheckAdjacentRoom(room, 0);
        CheckAdjacentRoom(room, 1);
        CheckAdjacentRoom(room, 2);
        CheckAdjacentRoom(room, 3);
    }

    private void CheckAdjacentRoom(ARoom room, int direction)
    {
        int xDir = direction % 2 == 0 ? direction - 1 : 0;
        int yDir = direction % 2 == 1 ? direction - 2 : 0;
        if (room.HasRoomDirection(direction) || m_Rooms.Exists(x => x.xPos == room.xPos + xDir && x.yPos == room.yPos - yDir)) return;
        if (UnityEngine.Random.Range(0, 100) < 75)
        {
            ARoom newRoom = CreateRoom(room.depth + 1, m_Rooms.Count, room.xPos + xDir, room.yPos - yDir, room);
            room.AddNextRoom(newRoom);
        }
    }

    private ARoom CreateRoom(int depth, int index, float x, float y, ARoom previousRoom)
    {
        int roomRange = UnityEngine.Random.Range(0, 101);

        GameObject roomPrefab = m_RoomSelector.Find(x => roomRange >= x.m_BeginRange && roomRange <= x.m_EndRange).m_RoomPrefab;

        if (roomPrefab == null)
            return null;

        GameObject newRoomObject = Instantiate(roomPrefab);
        newRoomObject.transform.SetParent(m_ParentRoom.transform);
        newRoomObject.transform.localScale = Vector3.one;
        newRoomObject.transform.localPosition = new Vector3(x * m_SeperatorLength, y * m_SeperatorLength, 0);
        newRoomObject.GetComponent<RectTransform>().sizeDelta = new Vector2(m_RoomLength, m_RoomLength);


        ARoom newRoom = newRoomObject.GetComponent<ARoom>();
        newRoom.Initialization(depth, index, x, y, previousRoom);

        m_Rooms.Add(newRoom);
        return newRoom;
    }


    private void DrawCorridor()
    {
        for (int i = 0; i < m_DungeonDepth; i++)
        {
            List<ARoom> rooms = m_Rooms.FindAll(x => x.depth == i);
            rooms.ForEach((room) => 
            {
                List<ARoom> nextRooms = room.GetNextRoom(false);
                nextRooms.ForEach((nextRoom) => {
                    GameObject corridor = Instantiate(m_PrefabCorridor);
                    corridor.transform.SetParent(m_ParentCorridor.transform);
                    corridor.transform.localScale = Vector3.one;
                    corridor.GetComponent<RectTransform>().sizeDelta = new Vector2(m_RoomLength / 5, m_RoomLength / 5);

                    float roomX = room.xPos;
                    float roomY = room.yPos;
                    float nextRoomX = nextRoom.xPos;
                    float nextRoomY = nextRoom.yPos;
                    corridor.transform.localPosition = new Vector3((roomX + ((nextRoomX - roomX) / 2)) * m_SeperatorLength, (roomY + ((nextRoomY - roomY) / 2)) * m_SeperatorLength, 0);
                });
            });
        }
    }


    // Make the new current room and set the exploration \\
    public void UpdateMap(ARoom newRoom)
    {
        m_CurrentRoom.LeaveRoom();
        m_CurrentRoom = newRoom;

        m_CurrentRoom.InitRoom();
        m_CurrentRoom.EnterRoom();
    }

    public void UpdateMapDirection(int direction)
    {
        if (m_CurrentRoom.HasRoomDirection(direction))
        {
            UpdateMap(m_CurrentRoom.GetRoomDirection(direction));
        }
    }
}
