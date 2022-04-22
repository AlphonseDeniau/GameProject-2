using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonMap : MonoBehaviour
{
    public Button m_LeftButton;
    public Button m_UpButton;
    public Button m_RightButton;
    public Button m_DownButton;

    private Map m_Map;

    private void Start()
    {
        m_Map = Map.Instance;
        while (!m_Map.initialized);
        UpdateButton();
    }

    public void UpdateButton()
    {
        m_LeftButton.interactable = false;
        m_RightButton.interactable = false;
        m_UpButton.interactable = false;
        m_DownButton.interactable = false;

        if (m_Map.currentRoom.HasRoomDirection(0))
            m_LeftButton.interactable = true;
        if (m_Map.currentRoom.HasRoomDirection(1))
            m_UpButton.interactable = true;
        if (m_Map.currentRoom.HasRoomDirection(2))
            m_RightButton.interactable = true;
        if (m_Map.currentRoom.HasRoomDirection(3))
            m_DownButton.interactable = true;
    }

    public void MoveToNextRoom(int direction)
    {
        m_Map.UpdateMapDirection(direction);
        UpdateButton();
    }
}
