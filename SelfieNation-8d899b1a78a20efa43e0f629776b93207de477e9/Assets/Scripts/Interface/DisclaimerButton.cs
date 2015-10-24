using UnityEngine;
using System.Collections;

public class DisclaimerButton : MonoBehaviour {

    int i = 0;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void DisclaimerButtonClick()
    {

        i = PlayerPrefs.GetInt("i");
        if(i  <1)
        {
            GameManager.me.tutorialPanel.SetActive(true);
            GameManager.me.disclaimerPanel.SetActive(false);
        }
        else
        {
            GameManager.me.homePanel.SetActive(true);
            GameManager.me.disclaimerPanel.SetActive(false);
        }
        i++;
        PlayerPrefs.SetInt("i", i);
    }
}
