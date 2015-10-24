using UnityEngine;
using System.Collections;
using Newtonsoft.Json;
using System.Collections.Generic;
using System;
using System.Text;
using System.Net;
using System.IO;
using UnityEngine.UI;

public class LeaderBoard : MonoBehaviour {

    string leaderboardURL;
    WWW www;
   
    string userInfoURL;

    public GameObject friendsRectViewParent;
    RectTransform friendsRectView;

    RectTransform friendsPrefabRect;
    public GameObject friendsPrefab;

    public int columnCount = 1;


	// Use this for initialization
	void Start () {
        friendsRectView = friendsRectViewParent.GetComponent<RectTransform>();
        friendsPrefabRect = friendsPrefab.GetComponent<RectTransform>();
        GetLeaderBoard();
	}



    void GetLeaderBoard()
    {
        leaderboardURL = "http://5.9.251.204/api/manager/top/" + GameManager.me.userId;
        www = new WWW(leaderboardURL);
        StartCoroutine(LeaderBoardRoutine(www));
    }

    IEnumerator LeaderBoardRoutine(WWW www)
    {
        yield return www;
        if (www.error == null)
        {

            Debug.Log(www.text);
            List<int> json = JsonConvert.DeserializeObject<List<int>>(www.text);
            int k = 0;
            Debug.Log(json[1]);
            List<int> userIdList = new List<int>();
            for (int i = 0; i < json.Count/2; i++)
            {
                userIdList.Add(json[i + k]);
                k = 1;
            }
            Debug.Log(userIdList.Count);
            List<int> completedChallengesList = new List<int>();
            int j = 0;
            for (int i = 1; i < json.Count/2 + 1;i++ )
            {
                completedChallengesList.Add(json[i + j]);

                j = 1;

            }
            Debug.Log(completedChallengesList[1]);
            DisplayFriends(userIdList, completedChallengesList);
        }
        else
        {
            Debug.Log(www.error);
        }
    }

    void DisplayFriends(List<int> friends, List<int> completedChallenges)
    {
        //calculate the width and height of each child item.
        float width = friendsRectView.rect.width / columnCount;
        float ratio = width / friendsPrefabRect.rect.width;
        float height = friendsPrefabRect.rect.height * ratio;
        int rowCount = friends.Count / columnCount;
        if (rowCount > 0)
        {
            if (friends.Count % rowCount > 0)
                rowCount++;
        }


        //adjust the height of the container so that it will just barely fit all its children
        float scrollHeight = height * rowCount;
        friendsRectView.offsetMin = new Vector2(friendsRectView.offsetMin.x, -scrollHeight / 2);
        friendsRectView.offsetMax = new Vector2(friendsRectView.offsetMax.x, scrollHeight / 2);

        int j = 0;
        for (int i = 0; i < friends.Count; i++)
        {
            //this is used instead of a double for loop because itemCount may not fit perfectly into the rows/columns
            if (i % columnCount == 0)
                j++;
            Debug.Log(friends.Count);
            //create a new item, name it, and set the parent
            GameObject newItem = Instantiate(friendsPrefab) as GameObject;
            newItem.transform.SetParent(friendsRectViewParent.transform, false);

            //move and size the new item
            RectTransform rectTransform = newItem.GetComponent<RectTransform>();

            float x = -friendsRectView.rect.width / 2 + width * (i % columnCount);
            float y = friendsRectView.rect.height / 2 - height * j;
            rectTransform.offsetMin = new Vector2(x, y);

            x = rectTransform.offsetMin.x + width;
            y = rectTransform.offsetMin.y + height;
            rectTransform.offsetMax = new Vector2(x, y);
            Debug.Log(friends[i]);
            string url = "http://5.9.251.204/api/user/" + friends[i];
            WWW www = new WWW(url);
            Debug.Log(completedChallenges[i]);
            StartCoroutine(GetUserInfo(www, newItem, completedChallenges[i]));
        }

    }

    IEnumerator GetUserInfo(WWW www, GameObject newItem, int i)
    {
        Debug.Log(i);
        yield return www;
        User user = JsonConvert.DeserializeObject<User>(www.text);
        GameObject picChild = newItem.transform.FindChild("ProfilePic").gameObject;
        Image profilePic = picChild.GetComponent<Image>();
        GetPhoto(user.PhotoId, profilePic);
        GameObject textChild = newItem.transform.FindChild("CompletedChallenges").gameObject;
        Text text = textChild.GetComponent<Text>();
        text.text = Convert.ToString(i*2);
        GameObject nameChild = newItem.transform.FindChild("Name").gameObject;
        Text te = nameChild.GetComponent<Text>();
        te.text = user.Name;
        Debug.Log(user.Name);
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
            Debug.Log(www.text);
            Texture2D tex = www.texture;
            im.sprite = Sprite.Create(tex, new Rect(0, 0, 300, 300), new Vector2(0, 0));
        }
        else
        {
            Debug.Log(www.error);
            Debug.Log(www.text);
        }

    }

}
