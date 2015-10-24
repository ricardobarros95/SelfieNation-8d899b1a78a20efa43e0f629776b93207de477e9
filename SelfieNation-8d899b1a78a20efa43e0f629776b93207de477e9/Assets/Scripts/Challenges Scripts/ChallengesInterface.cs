using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Newtonsoft.Json;
using System.Collections.Generic;
using System;
using System.Text;

public class ChallengesInterface : MonoBehaviour {

    //Interface Variables
    public InputField name;
    public InputField category;
    public InputField description;
    public InputField id;
    string difficulty;

    //Connection Variables
    string url;
    Challenge challenge;
    string modifyURL;
    string getChallengeURL;

	// Use this for initialization
	void Start () 
    {
        challenge = new Challenge();
        url = "http://5.9.251.204/api/challenge/create";
       // input = name.GetComponent<InputField>();
        modifyURL = "http://5.9.251.204/api/challenge/modify";
        
        Debug.Log(getChallengeURL);
	}

    public void SendButton()
    {
        challenge.Name = name.text;
        challenge.Type = category.text;
        challenge.Description = description.text;	
        string output = JsonConvert.SerializeObject(challenge);
        Dictionary<string, string> headers = new Dictionary<string, string>();
        headers.Add("Content-type", "application/json");
        byte[] pData = Encoding.ASCII.GetBytes(output.ToCharArray());
        WWW www = new WWW(url, pData, headers);
        StartCoroutine(SendChallenge(www));
        name.text = string.Empty;
        category.text = string.Empty;
        description.text = string.Empty;
    }

    public void ModifyButton()
    {
        challenge.Name = name.text;
        challenge.Type = category.text;
        challenge.Description = description.text;
        challenge.Id = Convert.ToInt32(id.text);
        string output = JsonConvert.SerializeObject(challenge);
        Dictionary<string, string> headers = new Dictionary<string, string>();
        headers.Add("Content-type", "application/json");
        byte[] pData = Encoding.ASCII.GetBytes(output.ToCharArray());
        WWW www = new WWW(modifyURL, pData, headers);
        StartCoroutine(SendChallenge(www));
        name.text = string.Empty;
        category.text = string.Empty;
        description.text = string.Empty;
        id.text = string.Empty;
        challenge.Name = string.Empty;
        challenge.Type = string.Empty;
        challenge.Description = string.Empty;
        challenge.Difficulty = string.Empty;
        Debug.Log(output);
    }
    public void GetButton()
    {
        getChallengeURL = "http://5.9.251.204/api/challenge/" + id.text;
        WWW www = new WWW(getChallengeURL);
        StartCoroutine(GetChallenge(www));
    }

    IEnumerator GetChallenge(WWW www)
    {
        yield return www;
        if (www.error == null)
        {
            Debug.Log(www.text);
            string[] challengesSplit = www.text.Split(new Char[] { ',' });
            // o = id; 1 = type; 2 = difficulty;  3 = name; 4 = Description
            string type = challengesSplit[1].Remove(0, 8);
            type = type.Replace('"', ' ');
            category.text = type;
            string nameTemp = challengesSplit[3].Remove(0, 8);
            nameTemp = nameTemp.Replace('"', ' ');
            name.text = nameTemp;
            string descriptionTemp = challengesSplit[4].Remove(0, 15);
            descriptionTemp = descriptionTemp.Replace('"', ' ');
            description.text = descriptionTemp;
            string difficultyTemp = challengesSplit[2].Remove(0,14);
          //  Debug.Log(difficultyTemp);
            difficultyTemp = difficultyTemp.Replace('"', ' ');
            if (difficultyTemp == "Easy ")
            {
                difficulty = "Easy";
                EasyButton();
            }
            if (difficultyTemp == "Medium ")
            {
                difficulty = "Medium";
                MediumBotton();
            }
            if (difficultyTemp == "Hard ")
            {
                difficulty = "Hard";
                HardButton();
            }
         //   Debug.Log(difficultyTemp);
        }
        else
        {
            Debug.Log(www.error);
        }
    }

    IEnumerator SendChallenge(WWW www)
    {
        yield return www;

        if (www.error == null)
        {
            Debug.Log("WWW Ok!: " + www.data);
        }
        else
        {
            Debug.Log("WWW Error: " + www.error);
        } 
    }


    public void EasyButton()
    {
        challenge.Difficulty = "Easy";
    }

    public void MediumBotton()
    {
        challenge.Difficulty = "Medium";
    }

    public void HardButton()
    {
        challenge.Difficulty = "Hard";
    }
    

}
