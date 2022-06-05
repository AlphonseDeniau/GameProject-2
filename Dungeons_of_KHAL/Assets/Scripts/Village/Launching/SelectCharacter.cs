using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ClickExample : MonoBehaviour
{
    public Button Priest_BTN;
    public Button Ranger_BTN;
    public Button Tamplate_BTN;
    public Button Thief_BTN;
    public Button Warrior_BTN;
    public Button Wizzard_BTN;
    int id = 0;
    bool selected = false;
    bool pressed = false;

    void Start()
    {
        Button btn1 = Priest_BTN.GetComponent<Button>();
        btn1.onClick.AddListener(priestManagement);

        Button btn2 = Ranger_BTN.GetComponent<Button>();
        btn2.onClick.AddListener(rangerManagement);

        Button btn3 = Tamplate_BTN.GetComponent<Button>();
        btn3.onClick.AddListener(templateManagement);

        Button btn4 = Thief_BTN.GetComponent<Button>();
        btn4.onClick.AddListener(thiefManagement);

        Button btn5 = Warrior_BTN.GetComponent<Button>();
        btn5.onClick.AddListener(warriorManagement);

        Button btn6 = Wizzard_BTN.GetComponent<Button>();
        btn6.onClick.AddListener(wizzardManagement);
    }

    void priestManagement()
    {
        id = 1;
        Priest_BTN.Select();
        if (selected == false)
            selected = true;
        else if (selected == true) {
            selected = false;
        }

        Debug.Log(id);
    }

    void rangerManagement()
    {
        id = 2;
        selected = true;
        Debug.Log(id);
    }

    void templateManagement()
    {
        id = 3;
        selected = true;
        Debug.Log(id);
    }

    void thiefManagement()
    {
        id = 4;
        Debug.Log(id);
    }

    void warriorManagement()
    {
        id = 5;
        Debug.Log(id);
    }

    void wizzardManagement()
    {
        id = 6;
        Debug.Log(id);
    }
}