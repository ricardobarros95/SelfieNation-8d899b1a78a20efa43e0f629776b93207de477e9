using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine.UI;
using System.Linq;

using Facebook.MiniJSON;
using System;
using System.IO;
using System.Net.Sockets;
using System.Text;
public class NewsFeed : MonoBehaviour 
{
    public Color[] colors;

    public GameObject feedItemPrefab;
    public Sprite easySeparator;
    public Sprite mediumSeparator;
    public Sprite hardSeparator;

    public Sprite easyTagIMG;
    public Sprite mediumTagIMG;
    public Sprite hardTagIMG;

    //--- rating ---//
    public Sprite emptyStar;
    public Sprite fullStar;
    public String preAvgText = "AVG: ";

    //turn debud mode on/off
    bool isDebugOn = true;//false;

    //Connection Variables
    string serverIP = "5.9.251.204";

    //internal variables
    int AVGRating = -5;
    int PERSRating = -5;


    //--- rating end ---//

    int loadProcess=0;

    class FeedEvent 
    {
        public int EventId { get; set; }
        public int UserId { get; set; }
        public string Type { get; set; }
        public int TableItemId { get; set; }
    }

    List<FeedEvent> events = new List<FeedEvent>();
    string feedEventURL = "http://5.9.251.204/api/event/";
    string photosUrl = "http://5.9.251.204/api/photolibrary/photo/";
    string userUrl = "http://5.9.251.204/api/user/";
    string challengeUrl = "http://5.9.251.204/api/challenge/";
    string completedChallengeUrl = "http://5.9.251.204/api/manager/";
    string friendsUrl = "http://5.9.251.204/api/manager/friends/friend/";



    void reset()
    {
        if (events.Count != 0)
        {
            GameObject tmp = this.transform.GetChild(0).gameObject;

            for (int i = 0; i != events.Count; i++)
                Destroy(tmp.transform.GetChild(i).gameObject);

            events.Clear();
             
        }
    }



    void OnEnable()
    {
        
        LoadNews();
    }

    public void LoadNews()
    {
        reset();
        WWW request = new WWW(feedEventURL + GameManager.me.userId);
        StartCoroutine(GetNewsFeed(request));
        Debug.Log("THE TIME OF RELOADING IS: "+Time.time);
    }

    IEnumerator GetNewsFeed(WWW www)
    {
        yield return www;
        if(string.IsNullOrEmpty(www.error))
        {
            Debug.Log(www.text);
            List<FeedEvent> newEvents = JsonConvert.DeserializeObject<List<FeedEvent>>(www.text);
            if (events.Count > 0)
            {
                GameManager.me.feed = true;
                for (int i = newEvents.Count - 1; i >= 0; i++)
                {
                    FeedEvent e = newEvents[i];
                    if (e.EventId > events[0].EventId)
                        events.Insert(0, e);
                }
            }
            else
                events.AddRange(newEvents);

            RectTransform newParent = transform.GetChild(0) as RectTransform;
            int count = newParent.childCount;
            for (int i = 0; i < count; i++)
                Destroy(newParent.GetChild(0)); //wiping the children to construct a new feed

            int n=0;
            float height = (feedItemPrefab.transform as RectTransform).sizeDelta.y;
            newParent.sizeDelta = new Vector2(Screen.width, (events.Count - 1) * height);
            float offset = (events.Count - 1) * height / 2;
            newParent.localPosition = new Vector3(0, -offset, 0);
            foreach(FeedEvent e in events)
            {
                GameObject item = (GameObject)GameObject.Instantiate(feedItemPrefab);

                //loop through Stars - adding them to a collection array
                //initially set the number of stars the photo has (if the user already voted on it)
                Transform stargroup = item.transform.Find("Stars");
                Transform[] Stars = new Transform[6];
                for (int s = 1; s < 6; s++)
                {
                    Stars[s] = stargroup.transform.Find("Star" + s);
                }

                string eventType = e.Type.Trim();
                Transform image2 = item.transform.Find("Image-Mask");//.GetComponent<Image>();
                Image image = image2.transform.Find("Image").GetComponent<Image>();
                GameObject avg = stargroup.Find("avg").gameObject;
                if(eventType == "Photo")
                {
                    //store imageID to a gameObject's name
                    GameObject imageID = image2.transform.Find("imageID").gameObject;
                    imageID.name = "photoID_" + e.TableItemId.ToString();
                    GetRating(e.TableItemId, avg, Stars);

                    image.enabled = false;

                    Debug.LogWarning("loading this photo: " + photosUrl + e.TableItemId);
                    WWW request = new WWW(photosUrl + e.TableItemId);
                    StartCoroutine(GetPhoto(request, image));
                }
                else
                {
                    Texture2D texture = new Texture2D(1, 1);
                    texture.SetPixels(new Color[]{ Color.black });
                    texture.Apply();
                    image.sprite = Sprite.Create(texture, new Rect(0, 0, 1, 1), new Vector2(1, 1));
                }

                Text[] texts = item.GetComponentsInChildren<Text>();
                foreach(Text t in texts)
                {
                    if(t.gameObject.name == "Name")
                    {
                        WWW request = new WWW(userUrl + e.UserId);
                        StartCoroutine(GetUserName(request, t));
                    }

                    else if(t.gameObject.name == "Description")
                    {
                        if(eventType == "Photo")
                            t.text = "Uploaded a new photo!";
                        else if(eventType == "Friend")
                        {
                            WWW request = new WWW(friendsUrl + e.TableItemId);
                            StartCoroutine(GetFriendshipInfo(request, e.UserId, t, image));
                        }
                        else if(eventType == "Challenge")
                        {
                            WWW request = new WWW(challengeUrl + e.TableItemId);


                            //chage the separator
                            Transform sep2 = item.transform.Find("Separator-Group");
                            Transform separator = sep2.transform.Find("image");

                            //chage the difficulity tab 
                            Transform diffTag2 = item.transform.Find("DifficultyGroup");
                            Transform diffTag = diffTag2.transform.Find("diffTagImg");

                            Transform diffText = diffTag2.transform.Find("diffTagText");


                            StartCoroutine(GetChallengeInfo(request, t, separator, diffTag, diffText));
                            
                            
                            
                            
                            
                            request = new WWW(completedChallengeUrl + e.UserId);
                            StartCoroutine(GetCompletedChallengePhoto(request, e.TableItemId, image, Stars, avg));
                        }
                    }
                }






                RectTransform otherT = item.transform as RectTransform;
                Vector2 origSize = otherT.sizeDelta;
                Vector3 origScale = otherT.localScale;
                otherT.SetParent(newParent, true);
                otherT.localPosition = new Vector3(0, offset - height/2, 0);
                otherT.sizeDelta = origSize;
                otherT.localScale = origScale;
                offset += -(origSize.y * 6 / 7);
            }
        }
        else
        {
            Debug.LogError(www.error);
            Debug.Log(www.text);
        }

        //if (GameManager.me.feed == false)
        //{
        //    GameManager.me.emptyFeed.SetActive(true);
        //    GameManager.me.homePanel.SetActive(false);
        //}
    }

    IEnumerator GetPhoto(WWW request, Image image)
    {
        yield return request;

        if(string.IsNullOrEmpty(request.error))
        {
            Texture2D texture = request.texture;
            image.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(texture.width, texture.height) / 2);
            image.enabled = true;
        }
        else
        {
            Debug.LogError(request.error);
            Debug.Log(request.text);
        }
    }

    IEnumerator GetUserName(WWW request, Text text)
    {
        yield return request;

        if(string.IsNullOrEmpty(request.error))
        {
            User user = JsonConvert.DeserializeObject<User>(request.text);
            text.text = user.Name;
        }
        else
        {
            Debug.LogError(request.error);
            Debug.Log(request.text);
        }
    }

    IEnumerator GetFriendshipInfo(WWW request, int myId, Text text, Image image)
    {
        yield return request;

        if (string.IsNullOrEmpty(request.error))
        {
            List<int> friends = JsonConvert.DeserializeObject<List<int>>(request.text);
            int otherFriend = friends[0] == myId ? friends[1] : friends[0];
            request = new WWW(userUrl + otherFriend);
            yield return request;
            if(string.IsNullOrEmpty(request.error))
            {
                User user = JsonConvert.DeserializeObject<User>(request.text);
                text.text = "Is friends with " + user.Name + "!";

                request = new WWW(photosUrl + user.PhotoId);
                yield return request;
                if(string.IsNullOrEmpty(request.error))
                {
                    Texture2D texture = request.texture;
                    image.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(texture.width, texture.height) / 2);
                }
                else
                {
                    Debug.LogError(request.error);
                    Debug.Log(request.text);
                }
            }
            else
            {
                Debug.LogError(request.error);
                Debug.Log(request.text);
            }
        }
        else
        {
            Debug.LogError(request.error);
            Debug.Log(request.text);
        }
    }

    IEnumerator GetChallengeInfo(WWW request, Text text, Transform S, Transform D, Transform diffText)
    {
        yield return request;

        if (string.IsNullOrEmpty(request.error))
        {
            Challenge challenge = JsonConvert.DeserializeObject<Challenge>(request.text);

            string result = string.Format("Completed: {0}", challenge.Name);
//difficulity HERE
            Debug.Log(challenge.Difficulty);

            S.GetComponent<Image>().sprite = easySeparator;

            if (challenge.Difficulty == "Easy") 
            { 
                S.GetComponent<Image>().sprite = easySeparator;
                D.GetComponent<Image>().sprite = easyTagIMG;
                diffText.GetComponent<Text>().text = "Easy";
                Debug.Log("changed to easy"); 
            }
            else if (challenge.Difficulty == "Medium") 
            { 
                S.GetComponent<Image>().sprite = mediumSeparator;
                D.GetComponent<Image>().sprite = mediumTagIMG;
                diffText.GetComponent<Text>().text = "Medium";
                Debug.Log("changed to med"); 
            }
            else if (challenge.Difficulty == "Hard") 
            { 
                S.GetComponent<Image>().sprite = hardSeparator;
                D.GetComponent<Image>().sprite = hardTagIMG;
                diffText.GetComponent<Text>().text = "Hard";
                Debug.Log("changed to hard"); 
            }

            text.text = result;

// ---- HACK FOR FRESHERS ---- //
            colors[0] = Color.white;
            colors[1] = Color.white;
            colors[2] = Color.white;


            if (challenge.Difficulty.Trim() == "Easy") text.color = colors[0];
            if (challenge.Difficulty.Trim() == "Medium") text.color = colors[1];
            if (challenge.Difficulty.Trim() == "Hard") text.color = colors[2];
           


        }
        else
        {
            Debug.LogError(request.error);
            Debug.Log(request.text);
        }
    }

    IEnumerator GetCompletedChallengePhoto(WWW request, int challengeId, Image image, Transform[] Stars, GameObject avg)
    {
        yield return request;

        if (string.IsNullOrEmpty(request.error))
        {
            List<int> info = JsonConvert.DeserializeObject<List<int>>(request.text);
            for(int i=0; i<info.Count; i += 2)
            {
                if(info[i] == challengeId) //the list is the pairs of ChallngeId-PhotoId
                {
                    Transform image2 = image.gameObject.transform.parent;
                    //store imageID to a gameObject's name
                    GameObject imageID = image2.transform.Find("imageID").gameObject;
                    imageID.name = "photoID_" + info[i + 1].ToString();
                    GetRating(info[i + 1], avg, Stars);
                    request = new WWW(photosUrl + info[i + 1]);
                    yield return request;

                    if(string.IsNullOrEmpty(request.error))
                    {
                        Texture2D texture = request.texture;
                        image.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(texture.width, texture.height) / 2);
                    }
                    else
                    {
                        Debug.LogError(request.error);
                        Debug.Log(request.text);
                    }
                    break;
                }
            }
        }
        else
        {
            Debug.LogError(request.error);
            Debug.Log(request.text);
        }
    }

    // --------------  rating system  ----------------------------//

    //GET the current rating, and apply it
    void GetRating(int PhotID, GameObject targetAVG, Transform[] targetStars)
    {
        int userID = GameManager.me.userId; //it always acts as the logged on user

        string URL = "http://" + serverIP + "/api/photolibrary/rating/";
        string logInOutput = "{\"PhotoId\":" + PhotID + ",\"UserId\":" + userID + "}";

        Debug.Log("ZZZ - initial readin: - Json string: '" + logInOutput + "'");

        Dictionary<string, string> headers = new Dictionary<string, string>();
        headers.Add("Content-type", "application/json");
        byte[] pData = Encoding.ASCII.GetBytes(logInOutput.ToCharArray());
        WWW www = new WWW(URL, pData, headers);
        StartCoroutine(RatingQuery(www, "get", targetAVG, targetStars));
    }


    // SHARED QUERY SCRIPT
    IEnumerator RatingQuery(WWW www, String caller, GameObject target, Transform[] targetStars)
    {

        yield return www;

        if (www.error == null)
        {
            Debug.Log("ZZZ -- WWW Ok!: " + www.text);

            if (caller == "get")
            {
                string[] data = www.text.Split(',');

                AVGRating = int.Parse(data[0].Substring(11));
                target.gameObject.GetComponent<Text>().text = preAvgText + AVGRating.ToString();
                Debug.Log("AVGRating:'" + AVGRating + "'");


                PERSRating = int.Parse(data[1].Substring(11, (data[1].Substring(11).Length - 1)));
                Debug.Log("Personal Rating:'" + PERSRating + "'");

                if (PERSRating > 0)
                {
                    for (int s = 1; s <= PERSRating; s++)
                    {
                        targetStars[s].GetComponent<Image>().sprite = fullStar;
                    }
                    Debug.Log("Stars set to:" + PERSRating);
                }

            }
        }
        else
        {
            Debug.LogError("ZZZ -- WWW Error: " + www.error);
            Debug.LogError("ZZZ -- WWW Error: " + www.text);
        }

    }





    // --------------  rating system end  ----------------------------//

}
