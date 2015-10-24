using UnityEngine;
using System.Collections;
using Newtonsoft.Json;
using System.Collections.Generic;
using System;
using System.Text;
using System.Net;
using System.IO;
using UnityEngine.UI;

public class DisplayFriends : MonoBehaviour {

    string getFriendsURL;
    List<int> friends;

    public GameObject friendsRectViewParent;
    RectTransform friendsRectView;

    RectTransform friendsPrefabRect;
    public GameObject friendsPrefab;

    public int columnCount = 1;

	// Use this for initialization
	void Start () {
	    getFriendsURL = "http://5.9.251.204/api/manager/friends/" + GameManager.me.userId;
        friendsRectView = friendsRectViewParent.GetComponent<RectTransform>();
        friendsPrefabRect = friendsPrefab.GetComponent<RectTransform>();
        friends = new List<int>();
        WWW www = new WWW(getFriendsURL);
        StartCoroutine(GetAllFriends(www));
	}

    IEnumerator GetAllFriends(WWW www)
    {
        yield return www;
        if (www.error == null)
        {
            Debug.Log(www.text);
            //friends = JsonConvert.DeserializeObject<List<int>>(www.text);
            string output = JsonConvert.DeserializeObject<string>(www.text);
            string[] elements = output.Split(new Char[] { ',' });
            for (int i = 0; i < elements.Length; i++)
            {
                friends.Add(Convert.ToInt32(elements[i]));
            }
                Debug.Log(elements[0]);
            DisplayFriendsM(friends);
        }
        else
        {
            Debug.Log(www.error);
        }
    }

    void DisplayFriendsM(List<int> friends)
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

            string url = "http://5.9.251.204/api/user/" + friends[i];
            WWW www = new WWW(url);
            StartCoroutine(GetUserInfo(www, newItem));


        }

    }

    IEnumerator GetUserInfo(WWW www, GameObject newItem)
    {
        yield return www;
        User user = JsonConvert.DeserializeObject<User>(www.text);
        Debug.Log(user.Name);
        GameObject picChild = newItem.transform.FindChild("ProfilePic").gameObject;
        Image profilePic = picChild.GetComponent<Image>();
        GetPhoto(user.PhotoId, profilePic);
        Debug.Log(user.PhotoId);


        GameObject nameChild = newItem.transform.FindChild("FriendsName").gameObject;
        Text text = nameChild.GetComponent<Text>();
        text.text = user.Name;
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
