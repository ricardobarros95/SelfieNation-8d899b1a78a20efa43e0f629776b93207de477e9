using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Newtonsoft.Json;
using System.Collections.Generic;
using System;
using System.Text;

public class RegisterUser : MonoBehaviour {

    string registerUserURl;
    public InputField name;
    public InputField email;
    List<int> programmes;
    public GameObject[] programmeObjects;
    ProgrammeID pId;

	// Use this for initialization
	void Start () {
        registerUserURl = "http://5.9.251.204/api/user/register";
        programmes = new List<int>();
        for (int i = 0; i < programmeObjects.Length; i++)
        {
            programmes.Add(i);
            pId = programmeObjects[i].GetComponent<ProgrammeID>();
            pId.Id = i;
        }
	}

    public void SignUpButton()
    {
        fresherUser fresher = new fresherUser();
        fresher.Name = name.text;
        fresher.Email = email.text;
        fresher.Programme = GameManager.me.currentlySelectedProgramme;
        fresher.School = GameManager.me.School;
        string output = JsonConvert.SerializeObject(fresher);
        Dictionary<string, string> headers = new Dictionary<string, string>();
        headers.Add("Content-type", "application/json");
        byte[] pData = Encoding.ASCII.GetBytes(output.ToCharArray());
        WWW www = new WWW(registerUserURl, pData, headers);
        StartCoroutine(RegisterUserCoroutine(www));
    }

    IEnumerator RegisterUserCoroutine(WWW www)
    {
        yield return www;
        if (www.error == null)
        {
            Debug.Log(www.text);
            GameManager.me.logInScreen.SetActive(true);
            GameManager.me.signUpScreen.SetActive(false);
        }
        else
        {
            Debug.Log(www.error);
        }
    }


    
}
// 0 = School of Engineering and Built Environment, 1 = Glasgow School for Business and Society, 2 = School of Health and Life Sciences

public class fresherUser
{
    public string Name { get; set; }
    public string Email { get; set; }
    public int Programme { get; set; }
    public int Id { get; set; }
    public int School { get; set; }
}