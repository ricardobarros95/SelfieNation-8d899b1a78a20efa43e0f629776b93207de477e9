using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using Facebook.MiniJSON;
using System;
using System.IO;
using System.Net.Sockets;
using System.Text;
using Newtonsoft.Json;



public class NewsFeed_Rating : MonoBehaviour
{

    //turn debud mode on/off
    bool isDebugOn = true;//false;

    //Connection Variables
    string serverIP = "5.9.251.204";


    //this need to be set somehow!!!
    int userID; 
    int PhotoID;
    int desiredRating;

    //internal variables
    int AVGRating;
    int PERSRating;


    //UI

    public String preAvgText = "AVG: ";


    //GET
    void GetRating(int PhotID, string queryType)
    {

        string URL = "http://" + "5.9.251.204" + "/api/photolibrary/rating/";
        string logInOutput = "{\"PhotoId\":" + PhotID + ",\"UserId\":" + userID + "}";

        // string logInOutput = JsonConvert.SerializeObject(tmp);
        Debug.Log("ZZZ - Json string: '" + logInOutput + "'");

        Dictionary<string, string> headers = new Dictionary<string, string>();
        headers.Add("Content-type", "application/json");
        byte[] pData = Encoding.ASCII.GetBytes(logInOutput.ToCharArray());
        WWW www = new WWW(URL, pData, headers);
        StartCoroutine(RatingQuery(www, queryType, PhotoID));
    }



    // SET
    void SetRating(int PhotID, int Rating)
    {
        string URL = "http://" + "5.9.251.204" + "/api/photolibrary/rating/set";

        string logInOutput = "{\"PhotoId\":" + PhotID + ",\"UserId\":" + userID + ",\"Rating\":" + Rating + "}";
        Debug.Log("ZZZ - Json string: '" + logInOutput + "'");

        Dictionary<string, string> headers = new Dictionary<string, string>();
        headers.Add("Content-type", "application/json");
        byte[] pData = Encoding.ASCII.GetBytes(logInOutput.ToCharArray());
        WWW www = new WWW(URL, pData, headers);
        StartCoroutine(RatingQuery(www, "set", PhotoID));
    }


    // UPDATE
    void UpdateRating(int PhotID, int Rating)
    {
        string URL = "http://" + "5.9.251.204" + "/api/photolibrary/rating/update";
        string logInOutput = "{\"PhotoId\":" + PhotID + ",\"UserId\":" + userID + ",\"Rating\":" + Rating + "}";

        Debug.Log("ZZZ - Json string: '" + logInOutput + "'");

        Dictionary<string, string> headers = new Dictionary<string, string>();
        headers.Add("Content-type", "application/json");
        byte[] pData = Encoding.ASCII.GetBytes(logInOutput.ToCharArray());
        WWW www = new WWW(URL, pData, headers);
        StartCoroutine(RatingQuery(www, "update", PhotoID));
    }






    IEnumerator RatingQuery(WWW www, String caller,int Photo)
    {

        yield return www;

        if (www.error == null)
        {
            Debug.Log("ZZZ -- WWW Ok!: " + www.text);

            if (caller == "get-full" || caller == "get-justAVG")
            {
                string[] data = www.text.Split(',');

                AVGRating = int.Parse(data[0].Substring(11));
                Debug.Log("AVGRating:'" + AVGRating + "' on photoID" + Photo.ToString());
                PERSRating = int.Parse(data[1].Substring(11, (data[1].Substring(11).Length - 1)));
                Debug.Log("Personal Rating:'" + PERSRating + "' on photoID" + Photo.ToString());

                if (caller == "get-justAVG")
                {
                    UpdateStarsUI();
                    yield break;
                }
                //check if the desired rating is not already the same as the current
                if (desiredRating != PERSRating)
                {
                    //if it's -1, it means it's not been set, so use SET otherwise UPDATE
                    if (PERSRating == -1)
                    {
                        SetRating(getPhotoID(), desiredRating);
                    }
                    else
                    {
                        UpdateRating(getPhotoID(), desiredRating);
                    }


                }
                else
                    Debug.LogWarning("Rating value is already set to this, pay attention!");

            }

            // it's either SET or UPDATE
            else 
            {
                //needs to update the value
                GetRating(getPhotoID(), "get-justAVG");
            }


        }
        else
        {
            Debug.LogError("ZZZ -- WWW Error: " + www.error);
            Debug.LogError("ZZZ -- WWW Error: " + www.text);
        }

    }




    // Use this for initialization
    void Start()
    {
        userID = GameManager.me.userId;

        //GetRating(PhotoID, userID); //WORKS 100%
        //SetRating(PhotoID,userID,2); //WORKS 100%
        //UpdateRating(PhotoID, userID, 5);



    }


    //set the rating of the actal ID and actual User to the given Rating
    //it checks first the actual rating!
    // if actual rating is !=-1 then it uses Update-method, otherwise uses Set!
    // if actual rating is same as the parameter exit function -> do nothing 
    public void setRatingTo(int RT)
    {
        Debug.Log("---------------------------------------------------------------------------------------------");
        desiredRating = RT;
        Debug.Log("the desired rating is:" + RT);
        Debug.Log("the caller id is:" + getPhotoID());

        //SetRating(PhotoID, userID, RT);

        //first let's see what's the actual rating on the image
        GetRating(getPhotoID(),"get-full");
        //after this the control is with the RatingQuery-Coroutine ...

    }

    int getPhotoID()
    {
        Transform tmp2 = this.transform.Find("Image-Mask");
        string tmp = tmp2.GetChild(1).name;
        tmp = tmp.Substring(8);

        return int.Parse(tmp);

    }

    void UpdateStarsUI()
    {

        /*
        //loop through Stars - turning number of desiredRating ones to full, the rest empty
        
        Transform stargroup = this.transform.Find("Stars");
        Transform Star;
        for (int s = 1; s < 6; s++)
        {
            Star = stargroup.transform.Find("Star" + s);
            if (s <= desiredRating)
            {
                Star.GetComponent<Image>().sprite = fullStar;
            }
            else
            {
                Star.GetComponent<Image>().sprite = emptyStar;
            }
        }
        */

        //also need to update the AVG field:
        Transform stargroup = this.transform.Find("Stars");
        stargroup.transform.Find("avg").gameObject.GetComponent<Text>().text = preAvgText + AVGRating.ToString();
        Debug.Log("--------------- INFINITE LOOP ESCAPED ----------------");
    
    }

  



}
