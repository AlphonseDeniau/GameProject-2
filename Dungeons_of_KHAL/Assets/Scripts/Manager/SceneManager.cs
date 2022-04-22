using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneManager : MonoBehaviour
{
    public class Scene
    {
        public string m_Name;
        public int m_Index;
    }

    [SerializeField] private List<Scene> m_Scenes = new List<Scene>();

    public void ChangeSceneByName(string name)
    {
        if (m_Scenes.Exists(s => s.m_Name == name))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(m_Scenes.Find(s => s.m_Name == name).m_Index);
        }
    }

    public void ChangeSceneByIndex(int index)
    {
        if (m_Scenes.Exists(s => s.m_Index == index))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(m_Scenes.Find(s => s.m_Index == index).m_Index);
        }
    }
}
