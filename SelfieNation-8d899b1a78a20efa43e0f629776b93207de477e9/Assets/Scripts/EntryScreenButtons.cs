using UnityEngine;
using System.Collections;

public class EntryScreenButtons : MonoBehaviour {

    public GameObject logInPanel;
    public GameObject signUPanel;
    public GameObject entryPanel;
    bool buttonPressed = false;
    public GameObject dropDownMenu;
    public GameObject scrollBar;
    

    public void LogInButton()
    {
        logInPanel.SetActive(true);
        entryPanel.SetActive(false);
    }

    public void SignUpButton()
    {
        signUPanel.SetActive(true);
        entryPanel.SetActive(false);
    }

    public void droplDownButton()
    {
        buttonPressed = !buttonPressed;
        if (buttonPressed)
        {
            dropDownMenu.SetActive(true);
            scrollBar.SetActive(true);
        }
        else
        {
            dropDownMenu.SetActive(false);
            scrollBar.SetActive(false);
        }
    }
}
