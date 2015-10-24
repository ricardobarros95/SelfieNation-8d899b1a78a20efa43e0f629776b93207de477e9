using UnityEngine;
using System.Collections;
using Newtonsoft.Json;
using System.Collections.Generic;
using System;
using System.Text;
using System.Net;
using System.IO;
using UnityEngine.UI;

public class FreshersLeaderboard : MonoBehaviour {

    string leaderBoardURL;

    public GameObject LeaderboardRectViewParent;
    RectTransform LeaderBoardRectView;

    RectTransform LeaderBoardPrefabRect;
    public GameObject leaderPrefab;

    public int columnCount = 1;

    List<GameObject> leader;

	// Use this for initialization
	void Start () {
        Debug.Log(GameManager.me.leaderBoard);
        leader = new List<GameObject>();
        if (GameManager.me.leaderBoard == 0)
        {
            leaderBoardURL = "http://5.9.251.204/api/leaderboard/global";
            LeaderBoardRectView = LeaderboardRectViewParent.GetComponent<RectTransform>();
            LeaderBoardPrefabRect = leaderPrefab.GetComponent<RectTransform>();
            WWW www = new WWW(leaderBoardURL);
            StartCoroutine(GetLeaderBoardCoroutine(www));
        }
        else
        {
            leaderBoardURL = "http://5.9.251.204/api/leaderboard/local/" + GameManager.me.School;
            LeaderBoardRectView = LeaderboardRectViewParent.GetComponent<RectTransform>();
            LeaderBoardPrefabRect = leaderPrefab.GetComponent<RectTransform>();
            WWW www = new WWW(leaderBoardURL);
            StartCoroutine(GetLeaderBoardCoroutine(www));
        }
	}

    public void SchoolButton()
    {
        if (leader != null)
        {
            foreach (GameObject oj in leader)
            {
                Destroy(oj);
            }
        }
        GameManager.me.leaderBoard = 1;
        leaderBoardURL = "http://5.9.251.204/api/leaderboard/local/" + GameManager.me.School;
        LeaderBoardRectView = LeaderboardRectViewParent.GetComponent<RectTransform>();
        LeaderBoardPrefabRect = leaderPrefab.GetComponent<RectTransform>();
        WWW www = new WWW(leaderBoardURL);
        StartCoroutine(GetLeaderBoardCoroutine(www));
    }

    public void GlobalButton()
    {
        if (leader != null)
        {
            foreach (GameObject oj in leader)
            {
                Destroy(oj);
            }
        }
        leaderBoardURL = "http://5.9.251.204/api/leaderboard/global";
        LeaderBoardRectView = LeaderboardRectViewParent.GetComponent<RectTransform>();
        LeaderBoardPrefabRect = leaderPrefab.GetComponent<RectTransform>();
        WWW www = new WWW(leaderBoardURL);
        StartCoroutine(GetLeaderBoardCoroutine(www));
    }

    IEnumerator GetLeaderBoardCoroutine(WWW www)
    {
        yield return www;
        if (www.error == null)
        {

            LeaderboardRecord[] json = JsonConvert.DeserializeObject<LeaderboardRecord[]>(www.text);
            DisplayLeaderBoard(json);
        }
        else
        {
            Debug.Log(www.error);
            Debug.Log(www.text);
        }
    }
	
	void DisplayLeaderBoard (LeaderboardRecord[] lead) 
    {

        //calculate the width and height of each child item.
        float width = LeaderBoardRectView.rect.width / columnCount;
        float ratio = width / LeaderBoardPrefabRect.rect.width;
        float height = LeaderBoardPrefabRect.rect.height * ratio;
        int rowCount = lead.Length / columnCount;
        if (rowCount > 0)
        {
            if (lead.Length % rowCount > 0)
                rowCount++;
        }


        //adjust the height of the container so that it will just barely fit all its children
        float scrollHeight = height * rowCount;
        LeaderBoardRectView.offsetMin = new Vector2(LeaderBoardRectView.offsetMin.x, -scrollHeight / 2);
        LeaderBoardRectView.offsetMax = new Vector2(LeaderBoardRectView.offsetMax.x, scrollHeight / 2);

        int j = 0;
        for (int i = 0; i < lead.Length; i++)
        {
            //this is used instead of a double for loop because itemCount may not fit perfectly into the rows/columns
            if (i % columnCount == 0)
                j++;

            //create a new item, name it, and set the parent
            GameObject newItem = Instantiate(leaderPrefab) as GameObject;
            newItem.transform.SetParent(LeaderboardRectViewParent.transform, false);
            Debug.Log(newItem);
            leader.Add(newItem);
            //move and size the new item
            RectTransform rectTransform = newItem.GetComponent<RectTransform>();

            float x = -LeaderBoardRectView.rect.width / 2 + width * (i % columnCount);
            float y = LeaderBoardRectView.rect.height / 2 - height * j;
            rectTransform.offsetMin = new Vector2(x, y);

            x = rectTransform.offsetMin.x + width;
            y = rectTransform.offsetMin.y + height;
            rectTransform.offsetMax = new Vector2(x, y);

            string url = "http://5.9.251.204/api/user/" + lead[i];
            WWW www = new WWW(url);
            StartCoroutine(GetUserInfo(www, newItem, i));


        }
	}

    IEnumerator GetUserInfo(WWW www, GameObject newItem, int i)
    {
        Debug.Log("Hello");
        yield return www;
        User user = JsonConvert.DeserializeObject<User>(www.text);
        Debug.Log(user.Name);
        GameObject picChild = newItem.transform.FindChild("ProfilePic").gameObject;
        Image profilePic = picChild.GetComponent<Image>();

        GetPhoto(user.PhotoId, profilePic);
        Debug.Log(user.PhotoId);

        GameObject textChild = newItem.transform.FindChild("Points").gameObject;
        Text text = textChild.GetComponent<Text>();
        text.text = Convert.ToString(i);

        GameObject nameChild = newItem.transform.FindChild("Name").gameObject;
        Text nameText = nameChild.GetComponent<Text>();
        nameText.text = user.Name;
    }

    void GetPhoto(int id, Image im)
    {
        string url = "http://5.9.251.204/api/photolibrary/photo/" + id;
        WWW www = new WWW(url);
        StartCoroutine(GivePhotoPls(www, im));
    }

    IEnumerator GivePhotoPls(WWW www, Image im)
    {
        yield return www;
        if (www.error == null)
        {
            Debug.Log(www.texture);
            im.sprite = Sprite.Create(www.texture, new Rect(0, 0, 300, 300), new Vector2(0, 0));
        }
        else
        {
            Debug.Log(www.error);
            Debug.Log(www.text);
        }

    }
}

public class LeaderboardRecord
{
    public int Points { get; set; }
    public int UserId { get; set; }
    public string Name { get; set; }
}
