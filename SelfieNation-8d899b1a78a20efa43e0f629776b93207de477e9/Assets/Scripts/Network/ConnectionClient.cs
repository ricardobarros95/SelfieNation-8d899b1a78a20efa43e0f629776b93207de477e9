using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using System.Net.Sockets;
using System.Text;
using Newtonsoft.Json;
using UnityEngine.UI;

public class ConnectionClient : MonoBehaviour {
    WWW www;
    string url;
    string ouuuutput;
    string getChallengesURL;
    public GameObject parent;

    void Start()
    {
        url = "http://5.9.251.204/api/user/register";
         www = new WWW(url);
        StartCoroutine(WaitForRequest(www));
        getChallengesURL = "http://5.9.251.204/api/challenge";
    }

    IEnumerator WaitForRequest(WWW www)
    {
        yield return www;

        if(www.error == null)
        {
            Debug.Log("WWW Ok!: " + www.data);
        }
        else 
        {
         Debug.Log("WWW Error: "+ www.error);
        } 
    }

    IEnumerator PostRequest(WWW www)
    {
        yield return www;

        if (www.error == null)
        {
            Debug.Log("WWW Ok!: " + www.text);
        }
        else
        {
            Debug.Log("WWW Error: " + www.error);
        }

        string[] split = www.text.Split(new Char[] { '}' });
      //  Debug.Log(split.Length); //Length - 2  NUMBER OF CHALLENGES = SPLIT.LENGTH - 2!!!!
        GameObject button = new GameObject("s", typeof(RectTransform));
        button.AddComponent<Button>();
        button.AddComponent<CanvasRenderer>();
        button.AddComponent<Image>();
        button.transform.parent = parent.transform;
        string[] nameSplit = split[0].Split(new Char[] {','});
        Debug.Log(nameSplit[4]); //      index[3] is the Name / / index[1] is the Type/Category     /  / index[0] is the ID     /  / index[2] is the difficulty 
        string name = nameSplit[4].Remove(0, 15);
        name = name.Replace('"', ' ');
        Debug.Log(name);

    }
    void OnGUI()
    {
        if(GUILayout.Button("new client"))
        {
            User user = new User();
            user.Name = "John";
            user.Email = "John@gmail.com";
            string output = JsonConvert.SerializeObject(user);
            ouuuutput = output;
            Dictionary<string, string> headers = new Dictionary<string, string>();
            headers.Add("Content-type", "application/json");
           // byte[] array = Encoding.ASCII.GetBytes(output);
            byte[] pData = Encoding.ASCII.GetBytes(output.ToCharArray());
            WWW www2 = new WWW(url, pData, headers);
            StartCoroutine(PostRequest(www2));

        }

        if(GUILayout.Button("get challenge"))
        {
            //Challenge challenge = new Challenge();
            WWW www3 = new WWW(getChallengesURL);
            StartCoroutine(PostRequest(www3));
            //Debug.Log(www3.text);
            
        }

       
    }
}

public class User 
{
    public string Name { get; set; }
    public string Email { get;set; }
    public int Id { get; set; }
    public int PhotoId { get; set; }
    public string FBId { get; set; }
}

public class Challenge
{
    public string Name { get; set; }
    public string Type { get; set; }
    public string Difficulty { get; set; }
    public string Description { get; set; }
    public int Id { get; set; }
}

public class Photo
{
    public int UserId { get; set; }
    public byte[] PhotoData { get; set; }
    public int photoId { get; set; }
    public bool ProduceEvent { get; set; }
    public int RotateBy { get; set; }
}

public class CompletedChallenge
{
    public int UserId { get; set; }
    public int ChallengeId { get; set; }
    public int PhotoId { get; set; }
    public DateTime CompletedAt { get; set; }
}
   

