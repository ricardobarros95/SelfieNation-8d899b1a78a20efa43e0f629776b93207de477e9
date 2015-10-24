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


public class Challenges : MonoBehaviour {
    public Color[] colors;

    string importChallengesURL;
    string completedChallengesURL;
    public int columnCount = 1;

    List<GameObject> buttons;
    List<Challenge> challenges;
    List<Challenge> easyChallenges;
    List<Challenge> mediumChallenges;
    List<Challenge> hardChallenges;

    public GameObject buttonPrefab;
    RectTransform buttonRect;

    public GameObject easyButtonContainer;
    public GameObject mediumButtonContainer;
    public GameObject hardButtonContainer;
    RectTransform buttonContainerRect;

    public GameObject challengeDescriptionPrefab;
    public GameObject challengeDescriptionParent;

    public Font textFont;
    public Color textColor;
    public int textSize;

   
	// Use this for initialization
	void Start () {
        importChallengesURL = "http://5.9.251.204/api/challenge";
        challenges = new List<Challenge>();
        easyChallenges = new List<Challenge>();
        mediumChallenges = new List<Challenge>();
        hardChallenges = new List<Challenge>();
        buttons = new List<GameObject>();
        WWW www = new WWW(importChallengesURL);
        StartCoroutine(GetChallenges(www));
        buttonRect = buttonPrefab.GetComponent<RectTransform>();
        buttonContainerRect = easyButtonContainer.GetComponent<RectTransform>();

	}

    IEnumerator GetChallenges(WWW www)
    {
        yield return www;
        if (www.error == null)
        {
            Challenge[] jsonObject = JsonConvert.DeserializeObject<Challenge[]>(www.text);
            CreateDemChallenges(jsonObject);
            CreateChallengeDescriptions(jsonObject);
        }
    }


    void CreateDemChallenges(Challenge[] jsonObject)
    {
        foreach (Challenge challenge in jsonObject)
        {
            challenges.Add(challenge);
            if (challenge.Difficulty.Trim() == "Easy") easyChallenges.Add(challenge);
            if (challenge.Difficulty.Trim() == "Medium") mediumChallenges.Add(challenge);
            if (challenge.Difficulty.Trim() == "Hard") hardChallenges.Add(challenge);
        }
        SetButtons(buttonRect, easyButtonContainer.GetComponent<RectTransform>(), easyChallenges, buttonPrefab, easyButtonContainer, textColor);
        SetButtons(buttonRect, mediumButtonContainer.GetComponent<RectTransform>(), mediumChallenges, buttonPrefab, mediumButtonContainer, textColor);
        SetButtons(buttonRect, hardButtonContainer.GetComponent<RectTransform>(), hardChallenges, buttonPrefab, hardButtonContainer, textColor);
    }
    void SetButtons(RectTransform buttonRect, RectTransform containerRect, List<Challenge> challengeList, GameObject buttonPrefabObject, GameObject parentObject, Color fontColor)
    {
        //calculate the width and height of each child item.
        float width = containerRect.rect.width / columnCount;
        float ratio = width / buttonRect.rect.width;
        float height = buttonRect.rect.height * ratio;
        int rowCount = challengeList.Count / columnCount;
        if (rowCount > 0)
        {
            if (challengeList.Count % rowCount > 0)
                rowCount++;
        }


        //adjust the height of the container so that it will just barely fit all its children
        float scrollHeight = height * rowCount;
        containerRect.offsetMin = new Vector2(containerRect.offsetMin.x, -scrollHeight / 2);
        containerRect.offsetMax = new Vector2(containerRect.offsetMax.x, scrollHeight / 2);

        int j = 0;
        for (int i = 0; i < challengeList.Count; i++)
        {
            //this is used instead of a double for loop because itemCount may not fit perfectly into the rows/columns
            if (i % columnCount == 0)
                j++;

            //create a new item, name it, and set the parent
            GameObject newItem = Instantiate(buttonPrefabObject) as GameObject;
            Debug.Log(buttons);
            

            Challenge c = challengeList[i];
            newItem.name = c.Name;
            newItem.transform.SetParent(parentObject.transform, false);

            //Assigning an Id to each button, the same Id has the challenge Id so that we can track a challenge to a specific button
            EnableDiscriptionButton buttonId = newItem.GetComponent<EnableDiscriptionButton>();
            buttonId.buttonId = c.Id;
            buttons.Add(newItem);
            //move and size the new item
            RectTransform rectTransform = newItem.GetComponent<RectTransform>();

            float x = -containerRect.rect.width / 2 + width * (i % columnCount);
            float y = containerRect.rect.height / 2 - height * j;
            rectTransform.offsetMin = new Vector2(x, y);

            x = rectTransform.offsetMin.x + width;
            y = rectTransform.offsetMin.y + height;
            rectTransform.offsetMax = new Vector2(x, y);
            CreateTextButton(c.Name, newItem.transform, fontColor);
        }
    }

    GameObject CreateTextButton(string label, Transform parent, Color fontColor)
    {
        GameObject textGJ = new GameObject(label.Trim(), typeof(RectTransform));
        textGJ.AddComponent<CanvasRenderer>();
        Text text = textGJ.AddComponent<Text>();
        text.text = label.Trim();
        textGJ.transform.SetParent(parent, false);
        text.font = textFont;
        text.fontSize = textSize;
        text.color = fontColor;

        text.resizeTextForBestFit = true;
        text.resizeTextMinSize = 50;
        text.resizeTextMaxSize = 70;
        text.horizontalOverflow = HorizontalWrapMode.Wrap;


        text.alignment = TextAnchor.MiddleCenter;
        RectTransform gjTextRect = textGJ.GetComponent<RectTransform>();
        RectTransform newItemRect = parent.GetComponent<RectTransform>();
        gjTextRect.sizeDelta = new Vector2(newItemRect.rect.width, newItemRect.rect.height);
        return textGJ;
    }

    void CreateChallengeDescriptions(Challenge[] challenges)
    {
        foreach (Challenge challenge in challenges)
        {
            GameObject challengeDescription = Instantiate(challengeDescriptionPrefab) as GameObject;

            challengeDescription.name = challenge.Name + "Description";
            challengeDescription.transform.SetParent(challengeDescriptionParent.transform, false);
            GameObject titleChild = challengeDescription.transform.FindChild("Title").gameObject;
            Text titleText = titleChild.GetComponent<Text>();
            titleText.text = challenge.Name;

            colors[0] = Color.white;
            colors[1] = Color.white;
            colors[2] = Color.white;

            if (challenge.Difficulty.Trim() == "Easy")
            {
                titleText.color = colors[0];
            }
            if (challenge.Difficulty.Trim() == "Medium")
            {
                titleText.color = colors[1];
            }

            if (challenge.Difficulty.Trim() == "Hard")
            {
                titleText.color = colors[2];
            }


            GameObject descriptionChild = challengeDescription.transform.FindChild("Description").gameObject;
            Text descriptionText = descriptionChild.GetComponent<Text>();

            GameObject factChild1 = challengeDescription.transform.FindChild("Fact").gameObject;
            GameObject factChild2 = factChild1.transform.FindChild("scrollBox").gameObject;
            GameObject factChild = factChild2.transform.FindChild("Text").gameObject;
            Text factText = factChild.GetComponent<Text>();
            String[] strings = challenge.Description.Split(new char[] { '/' });
            if (strings.Length > 1)
            {
                factText.text = strings[1];



                factText.color = new Color(0.01960f, 0.066666f, 0.05882f, 1.0f);
            }

            descriptionText.text = strings[0];

            GameObject takeSelfieChild = challengeDescription.transform.FindChild("Take Selfie Button").gameObject;

            TakeSelfieButton buttonId = takeSelfieChild.GetComponent<TakeSelfieButton>();
            buttonId.buttonId = challenge.Id;
        }
    }

    public void GetCompletedChallenges()
    {
        completedChallengesURL = "http://5.9.251.204/api/manager/" + GameManager.me.userId;
        Debug.Log(completedChallengesURL);
        WWW www = new WWW(completedChallengesURL);
        StartCoroutine(GetCompletedChallengesCoroutine(www));
    }

    IEnumerator GetCompletedChallengesCoroutine(WWW www)
    {
        yield return www;
        if (www.error == null)
        {
            Debug.Log(www.text);
            List<int> output = JsonConvert.DeserializeObject<List<int>>(www.text);
            Debug.Log(output);
            LockCompletedChallenges(output);
        }
        else
        {
            Debug.Log(www.error);
        }
    }

    void LockCompletedChallenges(List<int> completedChallenges)
    {
       foreach(GameObject button in buttons)
       {
           foreach (int Id in completedChallenges)
           {
               if (Id == button.GetComponent<EnableDiscriptionButton>().buttonId)
               {
                   Button b = button.GetComponent<Button>();
                   b.interactable = false;
                   button.GetComponent<EnableDiscriptionButton>().ChangeArt();
                   Debug.Log(b.interactable);
               }
           }
       }
    }
}
