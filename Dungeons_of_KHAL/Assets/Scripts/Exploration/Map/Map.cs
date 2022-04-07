using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{

    // Room prefab \\
    [SerializeField] private List<ARoom> m_RoomAvailable;


    // Value of the Map \\
    private int m_NbRoom;
    private int m_NbRoomExplored;
    private ARoom m_CurrentRoom;
    private List<ARoom> m_Rooms;

    // Create all the map with procedural check \\
    void CreateMap(int depthMin, int depthMax)
    {
        /// Make procedural :D
    }

    // Make the new current room and set the exploration \\
    void UpdateMap(ARoom newRoom)
    {
        newRoom.InitRoom();
        newRoom.EnterInRoom();
    }


    // Get current room \\
    ARoom GetCurrentRoom(int index)
    {
        return m_CurrentRoom;
    }

    // Get all rooms \\
    List<ARoom> GetRooms()
    {
        return m_Rooms;
    }

    // Get next rooms of the current room \\
    List<ARoom> GetNextRooms(int index)
    {
        return m_CurrentRoom.nextRooms;
    }
}
