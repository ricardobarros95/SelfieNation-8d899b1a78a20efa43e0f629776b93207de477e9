using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Newtonsoft.Json;
using System.Collections.Generic;
using System;
using System.Text;

public class LogInUser : MonoBehaviour {

    string userLogInURL;
    public InputField email;
    public GameObject loggedInMenus;
    public GameObject loggedOutMenus;
    public GameObject challengesHolder;
    Challenges completedChallenges;

	// Use this for initialization
	void Start () {
        userLogInURL = "http://5.9.251.204/api/user/login";
        completedChallenges = challengesHolder.GetComponent<Challenges>();
	}

    public void LogInButton()
    {
        string output = JsonConvert.SerializeObject(email.text);
        Dictionary<string, string> headers = new Dictionary<string, string>();
        headers.Add("Content-type", "application/json");
        byte[] pData = Encoding.ASCII.GetBytes(output.ToCharArray());
        WWW www = new WWW(userLogInURL, pData, headers);
        StartCoroutine(LogInUserCoroutine(www));
    }

    IEnumerator LogInUserCoroutine(WWW www)
    {
        yield return www;
        if (www.error == null)
        {
            Debug.Log(www.text);
            fresherUser jsonUser = JsonConvert.DeserializeObject<fresherUser>(www.text);
            GameManager.me.userId = jsonUser.Id;
            completedChallenges.GetCompletedChallenges();
           // loggedInMenus.SetActive(true);
            GetUserInfo();
           
        }
        else
        {
            Debug.Log(www.error);
        }
    }

    void GetUserInfo()
    {
        string url = "http://5.9.251.204/api/user/" + GameManager.me.userId;
        WWW www = new WWW(url);
        StartCoroutine(GetUserInfoCoroutine(www));
    }

    IEnumerator GetUserInfoCoroutine(WWW www)
    {
        yield return www;
        if (www.error == null)
        {
            fresherUser user = JsonConvert.DeserializeObject<fresherUser>(www.text);
            GameManager.me.School = user.School;
            loggedOutMenus.SetActive(false);

            if (GameManager.me.onTutorial < 1)
            {
                GameManager.me.tutorial.SetActive(true);
            }
            else
            {
                GameManager.me.freshersChallenges.SetActive(true);
                loggedInMenus.SetActive(true);
            }
        }

    }
}
