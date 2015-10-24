using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using System.Net.Sockets;
using System.Text;
using Newtonsoft.Json;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class status
{
    public string stat { get; set; }
    public string state { get; set; }
}


public class TMP_disable_MEDHARDcats : MonoBehaviour {

    public Sprite normalMedSprite;
    public Sprite disabledMedSprite;
    public Sprite normalHardSprite;
    public Sprite disablesHardSprite;

    public GameObject MediumButton;
    public GameObject HardButton;

    bool isAllEasyCompleted;

	// Use this for initialization
	void Start () {

        CheckState();

	}
	
	// Update is called once per frame
	void Update () {
	
	}


   public  void CheckState()
    {
        string userInfoURL = "http://5.9.251.204/api/user/completedEasy/" + GameManager.me.userId;
        WWW www = new WWW(userInfoURL);
        StartCoroutine(GetUserInfoCoroutine(www));
         
    
    }

    void applyCorrectButtons(bool state)
    {
        if (state)
        {
            Debug.LogWarning(state);
            Debug.LogWarning("All Easy Challenge IS Completed!!!");

            MediumButton.GetComponent<Image>().sprite = normalMedSprite;
            HardButton.GetComponent<Image>().sprite = normalHardSprite;

            MediumButton.GetComponent<Button>().enabled = true;
            HardButton.GetComponent<Button>().enabled = true;


        }
        else {

            MediumButton.GetComponent<Image>().sprite = disabledMedSprite;
            HardButton.GetComponent<Image>().sprite = disablesHardSprite;

            MediumButton.GetComponent<Button>().enabled = false;
            HardButton.GetComponent<Button>().enabled = false;

            Debug.LogWarning(state);
            Debug.LogWarning("NOT All Easy Challenge Completed!!!");
        }
    }


 

    IEnumerator GetUserInfoCoroutine(WWW www)
    {
        yield return www;
        if (www.error == null)
        {

            //Debug.Log(www.text);
            string[] tmp = www.text.Split(new Char[] { ':' });

            isAllEasyCompleted = Convert.ToBoolean(tmp[1].Remove(tmp[1].Length - 1));
            //Debug.Log(isAllEasyCompleted);

            applyCorrectButtons(isAllEasyCompleted);


        }
        else
        {
            Debug.Log(www.error);
        }
    }

}

