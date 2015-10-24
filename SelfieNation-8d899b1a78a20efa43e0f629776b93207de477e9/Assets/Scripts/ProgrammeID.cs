using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ProgrammeID : MonoBehaviour {
    public int Id;
    int school;
    public GameObject pickProgramme;
    Text programme;
    Text buttonText;
    Button myButton;
    public GameObject schoolObject;
    Text schoolText;
	// Use this for initialization
	void Start () {
        programme = pickProgramme.GetComponentInChildren<Text>();
        buttonText = GetComponentInChildren<Text>();
        myButton = GetComponent<Button>();
        myButton.onClick.AddListener(() => { programmeButton(); });
        Debug.Log(programme);
        gameObject.name = buttonText.text;
        schoolText = schoolObject.GetComponent<Text>();
	}
	
	// Update is called once per frame
    public void programmeButton()
    {
        GameManager.me.currentlySelectedProgramme = Id;
        programme.text = buttonText.text;
        if (Id >= 0 && Id <= 66 || Id == 192) 
        { 
            school = 0;
            GameManager.me.School = 0;
        }
        else if (Id >= 67 && Id <= 132) 
        { 
           school = 1;
           GameManager.me.School = 1;
        }
        else if (Id >= 133 && Id <= 191) 
        {
            school = 2;
            GameManager.me.School = 2;
        }
        DetermineSchool();
    }
    void DetermineSchool()
    {
        switch (school)
        {
            case 0:
                schoolText.text = "You are part of the School of Engineering and Built Environment!";
                break;
            case 1:
                schoolText.text = "You are part of the Glasgow School for Business and Society!";
                break;
            case 2:
                schoolText.text = "You are part of the School of Health and Life Sciences!";
                break;
        }
    }
}
