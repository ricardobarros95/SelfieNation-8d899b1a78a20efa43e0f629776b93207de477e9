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

public class CreateChallenges : MonoBehaviour {
    public Color[] colors;

    public Color challengesFontColor;

    public GameObject CategoriesParentObject;
    public GameObject categoryButtonPrefabL;
    public GameObject categoryButtonPrefabR;

    public GameObject challengeButtonPrefab;

    public GameObject challengeDescriptionPrefab;
    RectTransform buttonPrefabRectTransform;
    RectTransform rectViewRectTransform;
    float buttonWidth;
    float buttonHeight;

    ArrayList categories;


    List<Challenge> easyAnimalsChallenge;
    List<Challenge> mediumAnimalsChallenge;
    List<Challenge> hardAnimalsChallenge;

    List<Challenge> easyTravelandTourismChallenge;
    List<Challenge> mediumTravelandTourismChallenge;
    List<Challenge> hardTravelandTourismChallenge;

    List<Challenge> easySportsChallenge;
    List<Challenge> mediumSportsChallenge;
    List<Challenge> hardSportsChallenge;

    List<Challenge> easyCarsChallenge;
    List<Challenge> mediumCarsChallenge;
    List<Challenge> hardCarsChallenge;

    List<Challenge> easyDareToBeDigitalChallenge;
    List<Challenge> mediumDareToBeDigitalChallenge;
    List<Challenge> hardDareToBeDigitalChallenge;

    List<Challenge> easyPeopleAndPlacesChallenge;
    List<Challenge> mediumPeopleAndPlacesChallenge;
    List<Challenge> hardPeopleAndPlacesChallenge;

    List<Challenge> easyMiscellaneousChallenge;
    List<Challenge> mediumMiscellaneousChallenge;
    List<Challenge> hardMiscellaneousChallenge;

    List<Challenge> easyFoodAndDrinkChallenge;
    List<Challenge> mediumFoodAndDrinkChallenge;
    List<Challenge> hardFoodAndDrinkChallenge;

    List<Challenge> easyGreatOutdoorsChallenge;
    List<Challenge> mediumGreatOutdoorsChallenge;
    List<Challenge> hardGreatOutdoorsChallenge;

    List<Challenge> easyEventsChallenge;
    List<Challenge> mediumEventsChallenge;
    List<Challenge> hardEventsChallenge;

    List<Challenge> easyEverydayLifeChallenge;
    List<Challenge> mediumEverydayLifeChallenge;
    List<Challenge> hardEverydayLifeChallenge;

    //url for Get Request
    string getChallengesURL;
    
    //Text
    public Font textFont;
    public Color textColor;
    public int textSize;

    string buttonName;

    //Lists of all challenges by Categories
    // Parent object in the hierarchy, used for appropriate placing
    Dictionary<string, List<Challenge>> categoryMap;

    //challenge Categories parentObjects


    public GameObject easyEventsParentObject;
    public GameObject mediumEventsParentObject;
    public GameObject hardEventsParentObject;

    public GameObject easyMiscellaneousParentObject;
    public GameObject mediumMiscellaneousParentObject;
    public GameObject hardMiscellaneousParentObject;

    public GameObject easyAnimalsParentObject;
    public GameObject mediumAnimalsParentObject;
    public GameObject hardAnimalsParentObject;

    public GameObject easyTravelParentObject;
    public GameObject mediumTravelParentObject;
    public GameObject hardTravelParentObject;

    public GameObject easySportsParentObject;
    public GameObject mediumSportsParentObject;
    public GameObject hardSportsParentObject;

    public GameObject easyCarsParentObject;
    public GameObject mediumCarsParentObject;
    public GameObject hardCarsParentObject;

    public GameObject easyDareToBeDigitalParentObject;
    public GameObject mediumDareToBeDigitalParentObject;
    public GameObject hardDareToBeDigitalParentObject;

    public GameObject easyPeopleAndPlacesParentObject;
    public GameObject mediumPeopleAndPlacesParentObject;
    public GameObject hardPeopleAndPlacesParentObject;

    public GameObject easyFoodParentObject;
    public GameObject mediumFoodParentObject;
    public GameObject hardFoodParentObject;

    public GameObject easyGreatOutdoorsParentObject;
    public GameObject mediumGreatOutdoorsParentObject;
    public GameObject hardGreatOutdoorsParentObject;

    public GameObject easyEveryDayLifeParentObject;
    public GameObject mediumEveryDayLifeParentObject;
    public GameObject hardEveryDayLifeParentObject;

    //Challenge Descriptions parentObjects
    public GameObject eventsDescriptionObject;
    public GameObject miscellaneousDescriptionObject;
    public GameObject AnimalsDescriptionParentObject;
    public GameObject TravelDescriptionParentObject;
    public GameObject SportsDescriptionParentObject;
    public GameObject CarsDescriptionParentObject;
    public GameObject DareToBeDigitalParentObject;
    public GameObject PeopleAndPlacesDescriptionParentObject;
    public GameObject FoodParentDescriptionObject;
    public GameObject GreatOutdoorsDescriptionParentObject;
    public GameObject EveryDayLifeDescriptionParentObject;

    public int columnCount = 1;

    List<GameObject> buttons;

    static Dictionary<string, GameObject> typeToObject;

    string[] ChallangeButtonPrefabVariation = {"L","L","R","L","R"};

    string getAllCompletedChallengesURL;

	// Use this for initialization
	void Start () {
        buttons = new List<GameObject>();
        buttonPrefabRectTransform = categoryButtonPrefabL.GetComponent<RectTransform>();
        rectViewRectTransform = CategoriesParentObject.GetComponent<RectTransform>();
        buttonWidth = buttonPrefabRectTransform.rect.width;
        buttonHeight = buttonPrefabRectTransform.rect.height;

        getAllCompletedChallengesURL = "http://5.9.251.204/api/manager/" + GameManager.me.userId; 

        textColor.a = 255;
        getChallengesURL = "http://5.9.251.204/api/challenge";
        categories = new ArrayList();

        categoryMap = new Dictionary<string, List<Challenge>>();


        easyAnimalsChallenge = new List<Challenge>();
        mediumAnimalsChallenge = new List<Challenge>();
        hardAnimalsChallenge = new List<Challenge>();

        easyTravelandTourismChallenge = new List<Challenge>();
        mediumTravelandTourismChallenge = new List<Challenge>();
        hardTravelandTourismChallenge = new List<Challenge>();

        easySportsChallenge = new List<Challenge>();
        mediumSportsChallenge = new List<Challenge>();
        hardSportsChallenge = new List<Challenge>();

        easyCarsChallenge = new List<Challenge>();
        mediumCarsChallenge = new List<Challenge>();
        hardCarsChallenge = new List<Challenge>();

        easyDareToBeDigitalChallenge = new List<Challenge>();
        mediumDareToBeDigitalChallenge = new List<Challenge>();
        hardDareToBeDigitalChallenge = new List<Challenge>();

        easyPeopleAndPlacesChallenge = new List<Challenge>();
        mediumPeopleAndPlacesChallenge = new List<Challenge>();
        hardPeopleAndPlacesChallenge = new List<Challenge>();

        easyMiscellaneousChallenge = new List<Challenge>();
        mediumMiscellaneousChallenge = new List<Challenge>();
        hardMiscellaneousChallenge = new List<Challenge>();

        easyFoodAndDrinkChallenge = new List<Challenge>();
        mediumFoodAndDrinkChallenge = new List<Challenge>();
        hardFoodAndDrinkChallenge = new List<Challenge>();

        easyGreatOutdoorsChallenge = new List<Challenge>();
        mediumGreatOutdoorsChallenge = new List<Challenge>();
        hardGreatOutdoorsChallenge = new List<Challenge>();

        easyEventsChallenge = new List<Challenge>();
        mediumEventsChallenge = new List<Challenge>();
        hardEventsChallenge = new List<Challenge>();

        easyEverydayLifeChallenge = new List<Challenge>();
        mediumEverydayLifeChallenge = new List<Challenge>();
        hardEverydayLifeChallenge = new List<Challenge>();

        WWW www = new WWW(getChallengesURL);
        StartCoroutine(GetRequest(www));

        
	}

    IEnumerator GetRequest(WWW www)
    {
        yield return www;
        if (www.error == null)
        {
            Challenge[] jsonObject = JsonConvert.DeserializeObject<Challenge[]>(www.text);
            GameManager.me.challengesList = jsonObject;
            CreateChallengesM(jsonObject);
            CreateChallengeDescriptions(jsonObject);
        }
        else
        {
            Debug.Log("Error: " + www.error);
        }
    }



    void CreateChallengesM(Challenge[] challenges)
    {   
  
        GameObject gameobje = new GameObject();

        foreach (Challenge challenge in challenges)  
        {
            //Creates the button
            string challengeCategory = challenge.Type.Trim();
            if (!categories.Contains(challengeCategory)) 
            {
                categories.Add(challengeCategory);
            }

            if (challenge.Type.Trim() == "Travel/Tourism" && challenge.Difficulty.Trim() == "Easy") easyTravelandTourismChallenge.Add(challenge);
            if (challenge.Type.Trim() == "Travel/Tourism" && challenge.Difficulty.Trim() == "Medium") mediumTravelandTourismChallenge.Add(challenge);
            if (challenge.Type.Trim() == "Travel/Tourism" && challenge.Difficulty.Trim() == "Hard") hardTravelandTourismChallenge.Add(challenge);

            if (challenge.Type.Trim() == "Sports" && challenge.Difficulty.Trim() == "Easy") easySportsChallenge.Add(challenge);
            if (challenge.Type.Trim() == "Sports" && challenge.Difficulty.Trim() == "Medium") mediumSportsChallenge.Add(challenge);
            if (challenge.Type.Trim() == "Sports" && challenge.Difficulty.Trim() == "Hard") hardSportsChallenge.Add(challenge);

            if (challenge.Type.Trim() == "Cars" && challenge.Difficulty.Trim() == "Easy") easyCarsChallenge.Add(challenge);
            if (challenge.Type.Trim() == "Cars" && challenge.Difficulty.Trim() == "Medium") mediumCarsChallenge.Add(challenge);
            if (challenge.Type.Trim() == "Cars" && challenge.Difficulty.Trim() == "Hard") hardCarsChallenge.Add(challenge);

            if (challenge.Type.Trim() == "Dare to be Digital" && challenge.Difficulty.Trim() == "Easy") easyDareToBeDigitalChallenge.Add(challenge);
            if (challenge.Type.Trim() == "Dare to be Digital" && challenge.Difficulty.Trim() == "Medium") mediumDareToBeDigitalChallenge.Add(challenge);
            if (challenge.Type.Trim() == "Dare to be Digital" && challenge.Difficulty.Trim() == "Hard") hardDareToBeDigitalChallenge.Add(challenge);

            if (challenge.Type.Trim() == "People and Places" && challenge.Difficulty.Trim() == "Easy") easyPeopleAndPlacesChallenge.Add(challenge);
            if (challenge.Type.Trim() == "People and Places" && challenge.Difficulty.Trim() == "Medium") mediumPeopleAndPlacesChallenge.Add(challenge);
            if (challenge.Type.Trim() == "People and Places" && challenge.Difficulty.Trim() == "Hard") hardPeopleAndPlacesChallenge.Add(challenge);

            if (challenge.Type.Trim() == "Miscellaneous" && challenge.Difficulty.Trim() == "Easy") easyMiscellaneousChallenge.Add(challenge);
            if (challenge.Type.Trim() == "Miscellaneous" && challenge.Difficulty.Trim() == "Medium") mediumMiscellaneousChallenge.Add(challenge);
            if (challenge.Type.Trim() == "Miscellaneous" && challenge.Difficulty.Trim() == "Hard") hardMiscellaneousChallenge.Add(challenge);

            if (challenge.Type.Trim() == "Animals" && challenge.Difficulty.Trim() == "Easy") easyAnimalsChallenge.Add(challenge);
            if (challenge.Type.Trim() == "Animals" && challenge.Difficulty.Trim() == "Medium") mediumAnimalsChallenge.Add(challenge);
            if (challenge.Type.Trim() == "Animals" && challenge.Difficulty.Trim() == "Hard") hardAnimalsChallenge.Add(challenge);

            if (challenge.Type.Trim() == "Food/Drink" && challenge.Difficulty.Trim() == "Easy") easyFoodAndDrinkChallenge.Add(challenge);
            if (challenge.Type.Trim() == "Food/Drink" && challenge.Difficulty.Trim() == "Medium") mediumFoodAndDrinkChallenge.Add(challenge);
            if (challenge.Type.Trim() == "Food/Drink" && challenge.Difficulty.Trim() == "Hard") hardFoodAndDrinkChallenge.Add(challenge);

            if (challenge.Type.Trim() == "Great Outdoors" && challenge.Difficulty.Trim() == "Easy") easyGreatOutdoorsChallenge.Add(challenge);
            if (challenge.Type.Trim() == "Great Outdoors" && challenge.Difficulty.Trim() == "Medium") mediumGreatOutdoorsChallenge.Add(challenge);
            if (challenge.Type.Trim() == "Great Outdoors" && challenge.Difficulty.Trim() == "Hard") hardGreatOutdoorsChallenge.Add(challenge);

            if (challenge.Type.Trim() == "Everyday Life" && challenge.Difficulty.Trim() == "Easy") easyEverydayLifeChallenge.Add(challenge);
            if (challenge.Type.Trim() == "Everyday Life" && challenge.Difficulty.Trim() == "Medium") mediumEverydayLifeChallenge.Add(challenge);
            if (challenge.Type.Trim() == "Everyday Life" && challenge.Difficulty.Trim() == "Hard") hardEverydayLifeChallenge.Add(challenge);

            if (challenge.Type.Trim() == "Events" && challenge.Difficulty.Trim() == "Easy") easyEventsChallenge.Add(challenge);
            if (challenge.Type.Trim() == "Events" && challenge.Difficulty.Trim() == "Medium") mediumEventsChallenge.Add(challenge);
            if (challenge.Type.Trim() == "Events" && challenge.Difficulty.Trim() == "Hard") hardEventsChallenge.Add(challenge);

        }
        CreateCategories();
        CreateChallenge();
    }

    void CreateCategories()
    {
        //calculate the width and height of each child item.
        float width = rectViewRectTransform.rect.width / columnCount;
        float ratio = width / buttonPrefabRectTransform.rect.width;
        float height = buttonPrefabRectTransform.rect.height * ratio;
        int rowCount = categories.Count / columnCount;
        if (categories.Count % rowCount > 0)
            rowCount++;

        //adjust the height of the container so that it will just barely fit all its children
        float scrollHeight = height * rowCount;
        rectViewRectTransform.offsetMin = new Vector2(rectViewRectTransform.offsetMin.x, -scrollHeight / 2);
        rectViewRectTransform.offsetMax = new Vector2(rectViewRectTransform.offsetMax.x, scrollHeight / 2);


        int z = 0;
        int j = 0;
        for (int i = 0; i < categories.Count; i++)
        {
            //this is used instead of a double for loop because itemCount may not fit perfectly into the rows/columns
            if (i % columnCount == 0)
                j++;

            GameObject theChoosenOne;
            if (ChallangeButtonPrefabVariation[z] == "L")
            {
                theChoosenOne = categoryButtonPrefabL;
            }
            else if (ChallangeButtonPrefabVariation[z] == "R")
            {
                theChoosenOne = categoryButtonPrefabR;
            }
            else
            {
                theChoosenOne = categoryButtonPrefabL;
            }



            //create a new item, name it, and set the parent
            GameObject newItem = Instantiate(theChoosenOne) as GameObject;
            newItem.name = (string)categories[i];
            newItem.transform.SetParent(CategoriesParentObject.transform, false);
            buttonName = newItem.name;
            
            
            //move and size the new item
            RectTransform rectTransform = newItem.GetComponent<RectTransform>();

            float x = -rectViewRectTransform.rect.width / 2 + width * (i % columnCount);
            float y = rectViewRectTransform.rect.height / 2 - height * j;
            rectTransform.offsetMin = new Vector2(x, y);

            x = rectTransform.offsetMin.x + width;
//offsetting the right buttons - for Cheri =)
            if (ChallangeButtonPrefabVariation[z] == "R")
            {
                x += 10;
            }

            if (ChallangeButtonPrefabVariation[z] == "L")
            {
                //put new x here if needed
            }


            y = rectTransform.offsetMin.y + height;
            rectTransform.offsetMax = new Vector2(x, y);

            GameObject gjText = new GameObject((string)categories[i], typeof(RectTransform));
            gjText.AddComponent<CanvasRenderer>();
            gjText.AddComponent<Text>();
            Text text = gjText.GetComponent<Text>();
            text.text = (string)categories[i];
            gjText.transform.SetParent(newItem.transform, false);
            text.font = textFont;
            text.fontSize = textSize;
            text.color = textColor;
            text.alignment = TextAnchor.MiddleCenter;
            RectTransform gjTextRect = gjText.GetComponent<RectTransform>();
            RectTransform newItemRect = newItem.GetComponent<RectTransform>();
            gjTextRect.sizeDelta = new Vector2(newItemRect.rect.width, newItemRect.rect.height);

            z++;
            if (z > ChallangeButtonPrefabVariation.Length - 1)
            {
                z = 0;
            }
        
        }
    }

    void CreateChallenge()
    {
        SetButtons(categoryButtonPrefabL.GetComponent<RectTransform>(), easyAnimalsParentObject.GetComponent<RectTransform>(), easyAnimalsChallenge, challengeButtonPrefab, easyAnimalsParentObject, challengesFontColor);
        SetButtons(categoryButtonPrefabL.GetComponent<RectTransform>(), mediumAnimalsParentObject.GetComponent<RectTransform>(), mediumAnimalsChallenge, challengeButtonPrefab, mediumAnimalsParentObject, challengesFontColor);
        SetButtons(categoryButtonPrefabL.GetComponent<RectTransform>(), hardAnimalsParentObject.GetComponent<RectTransform>(), hardAnimalsChallenge, challengeButtonPrefab, hardAnimalsParentObject, challengesFontColor);

        SetButtons(categoryButtonPrefabL.GetComponent<RectTransform>(), easyTravelParentObject.GetComponent<RectTransform>(), easyTravelandTourismChallenge, challengeButtonPrefab, easyTravelParentObject, challengesFontColor);
        SetButtons(categoryButtonPrefabL.GetComponent<RectTransform>(), mediumTravelParentObject.GetComponent<RectTransform>(), mediumTravelandTourismChallenge, challengeButtonPrefab, mediumTravelParentObject, challengesFontColor);
        SetButtons(categoryButtonPrefabL.GetComponent<RectTransform>(), hardTravelParentObject.GetComponent<RectTransform>(), hardTravelandTourismChallenge, challengeButtonPrefab, hardTravelParentObject, challengesFontColor);

        SetButtons(categoryButtonPrefabL.GetComponent<RectTransform>(), easySportsParentObject.GetComponent<RectTransform>(), easySportsChallenge, challengeButtonPrefab, easySportsParentObject, challengesFontColor);
        SetButtons(categoryButtonPrefabL.GetComponent<RectTransform>(), mediumSportsParentObject.GetComponent<RectTransform>(), mediumSportsChallenge, challengeButtonPrefab, mediumSportsParentObject, challengesFontColor);
        SetButtons(categoryButtonPrefabL.GetComponent<RectTransform>(), hardSportsParentObject.GetComponent<RectTransform>(), hardSportsChallenge, challengeButtonPrefab, hardSportsParentObject, challengesFontColor);

        SetButtons(categoryButtonPrefabL.GetComponent<RectTransform>(), easyCarsParentObject.GetComponent<RectTransform>(), easyCarsChallenge, challengeButtonPrefab, easyCarsParentObject, challengesFontColor);
        SetButtons(categoryButtonPrefabL.GetComponent<RectTransform>(), mediumCarsParentObject.GetComponent<RectTransform>(), mediumCarsChallenge, challengeButtonPrefab, mediumCarsParentObject, challengesFontColor);
        SetButtons(categoryButtonPrefabL.GetComponent<RectTransform>(), hardCarsParentObject.GetComponent<RectTransform>(), hardCarsChallenge, challengeButtonPrefab, hardCarsParentObject, challengesFontColor);

        SetButtons(categoryButtonPrefabL.GetComponent<RectTransform>(), easyDareToBeDigitalParentObject.GetComponent<RectTransform>(), easyDareToBeDigitalChallenge, challengeButtonPrefab, easyDareToBeDigitalParentObject, challengesFontColor);
        SetButtons(categoryButtonPrefabL.GetComponent<RectTransform>(), mediumDareToBeDigitalParentObject.GetComponent<RectTransform>(), mediumDareToBeDigitalChallenge, challengeButtonPrefab, mediumDareToBeDigitalParentObject, challengesFontColor);
        SetButtons(categoryButtonPrefabL.GetComponent<RectTransform>(), hardDareToBeDigitalParentObject.GetComponent<RectTransform>(), hardDareToBeDigitalChallenge, challengeButtonPrefab, hardDareToBeDigitalParentObject, challengesFontColor);

        SetButtons(categoryButtonPrefabL.GetComponent<RectTransform>(), easyPeopleAndPlacesParentObject.GetComponent<RectTransform>(), easyPeopleAndPlacesChallenge, challengeButtonPrefab, easyPeopleAndPlacesParentObject, challengesFontColor);
        SetButtons(categoryButtonPrefabL.GetComponent<RectTransform>(), mediumPeopleAndPlacesParentObject.GetComponent<RectTransform>(), mediumPeopleAndPlacesChallenge, challengeButtonPrefab, mediumPeopleAndPlacesParentObject, challengesFontColor);
        SetButtons(categoryButtonPrefabL.GetComponent<RectTransform>(), hardPeopleAndPlacesParentObject.GetComponent<RectTransform>(), hardPeopleAndPlacesChallenge, challengeButtonPrefab, hardPeopleAndPlacesParentObject, challengesFontColor);

        SetButtons(categoryButtonPrefabL.GetComponent<RectTransform>(), easyFoodParentObject.GetComponent<RectTransform>(), easyFoodAndDrinkChallenge, challengeButtonPrefab, easyFoodParentObject, challengesFontColor);
        SetButtons(categoryButtonPrefabL.GetComponent<RectTransform>(), mediumFoodParentObject.GetComponent<RectTransform>(), mediumFoodAndDrinkChallenge, challengeButtonPrefab, mediumFoodParentObject, challengesFontColor);
        SetButtons(categoryButtonPrefabL.GetComponent<RectTransform>(), hardFoodParentObject.GetComponent<RectTransform>(), hardFoodAndDrinkChallenge, challengeButtonPrefab, hardFoodParentObject, challengesFontColor);

        SetButtons(categoryButtonPrefabL.GetComponent<RectTransform>(), easyGreatOutdoorsParentObject.GetComponent<RectTransform>(), easyGreatOutdoorsChallenge, challengeButtonPrefab, easyGreatOutdoorsParentObject, challengesFontColor);
        SetButtons(categoryButtonPrefabL.GetComponent<RectTransform>(), mediumGreatOutdoorsParentObject.GetComponent<RectTransform>(), mediumGreatOutdoorsChallenge, challengeButtonPrefab, mediumGreatOutdoorsParentObject, challengesFontColor);
        SetButtons(categoryButtonPrefabL.GetComponent<RectTransform>(), hardGreatOutdoorsParentObject.GetComponent<RectTransform>(), hardGreatOutdoorsChallenge, challengeButtonPrefab, hardGreatOutdoorsParentObject, challengesFontColor);

        SetButtons(categoryButtonPrefabL.GetComponent<RectTransform>(), easyEveryDayLifeParentObject.GetComponent<RectTransform>(), easyEverydayLifeChallenge, challengeButtonPrefab, easyEveryDayLifeParentObject, challengesFontColor);
        SetButtons(categoryButtonPrefabL.GetComponent<RectTransform>(), mediumEveryDayLifeParentObject.GetComponent<RectTransform>(), mediumEverydayLifeChallenge, challengeButtonPrefab, mediumEveryDayLifeParentObject, challengesFontColor);
        SetButtons(categoryButtonPrefabL.GetComponent<RectTransform>(), hardEveryDayLifeParentObject.GetComponent<RectTransform>(), hardEverydayLifeChallenge, challengeButtonPrefab, hardEveryDayLifeParentObject, challengesFontColor);

        SetButtons(categoryButtonPrefabL.GetComponent<RectTransform>(), easyMiscellaneousParentObject.GetComponent<RectTransform>(), easyMiscellaneousChallenge, challengeButtonPrefab, easyMiscellaneousParentObject, challengesFontColor);
        SetButtons(categoryButtonPrefabL.GetComponent<RectTransform>(), mediumMiscellaneousParentObject.GetComponent<RectTransform>(), mediumMiscellaneousChallenge, challengeButtonPrefab, mediumMiscellaneousParentObject, challengesFontColor);
        SetButtons(categoryButtonPrefabL.GetComponent<RectTransform>(), hardMiscellaneousParentObject.GetComponent<RectTransform>(), hardMiscellaneousChallenge, challengeButtonPrefab, hardMiscellaneousParentObject, challengesFontColor);

        SetButtons(categoryButtonPrefabL.GetComponent<RectTransform>(), easyEventsParentObject.GetComponent<RectTransform>(), easyEventsChallenge, challengeButtonPrefab, easyEventsParentObject, challengesFontColor);
        SetButtons(categoryButtonPrefabL.GetComponent<RectTransform>(), mediumEventsParentObject.GetComponent<RectTransform>(), mediumEventsChallenge, challengeButtonPrefab, mediumEventsParentObject, challengesFontColor);
        SetButtons(categoryButtonPrefabL.GetComponent<RectTransform>(), hardEventsParentObject.GetComponent<RectTransform>(), hardEventsChallenge, challengeButtonPrefab, hardEventsParentObject, challengesFontColor);
       
    }
    GameObject CreateTextButton(string label, Transform parent, Color fontColor )
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
        text.horizontalOverflow =HorizontalWrapMode.Wrap;


        text.alignment = TextAnchor.MiddleCenter;
        RectTransform gjTextRect = textGJ.GetComponent<RectTransform>();
        RectTransform newItemRect = parent.GetComponent<RectTransform>();
        gjTextRect.sizeDelta = new Vector2(newItemRect.rect.width, newItemRect.rect.height);
        return textGJ;
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
            buttons.Add(newItem);
            
            Challenge c = challengeList[i];
            newItem.name = c.Name;
            newItem.transform.SetParent(parentObject.transform, false);

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

    void CreateChallengeDescriptions(Challenge[] challenges)
    {
        foreach (Challenge challenge in challenges)
        {
            GameObject challengeDescription = Instantiate(challengeDescriptionPrefab) as GameObject;
            
            challengeDescription.name = challenge.Name + "Description";
            if (challenge.Type.Trim() == "Travel/Tourism") challengeDescription.transform.SetParent(TravelDescriptionParentObject.transform,false);
            if (challenge.Type.Trim() == "Sports") challengeDescription.transform.SetParent(SportsDescriptionParentObject.transform, false);
            if (challenge.Type.Trim() == "Cars") challengeDescription.transform.SetParent(CarsDescriptionParentObject.transform, false);
            if (challenge.Type.Trim() == "Dare to be Digital") challengeDescription.transform.SetParent(DareToBeDigitalParentObject.transform, false);
            if (challenge.Type.Trim() == "People and Places") challengeDescription.transform.SetParent(PeopleAndPlacesDescriptionParentObject.transform, false);
            if (challenge.Type.Trim() == "Miscellaneous") challengeDescription.transform.SetParent(miscellaneousDescriptionObject.transform, false);
            if (challenge.Type.Trim() == "Animals") challengeDescription.transform.SetParent(AnimalsDescriptionParentObject.transform, false);
            if (challenge.Type.Trim() == "Food/Drink") challengeDescription.transform.SetParent(FoodParentDescriptionObject.transform, false);
            if (challenge.Type.Trim() == "Great Outdoors") challengeDescription.transform.SetParent(GreatOutdoorsDescriptionParentObject.transform, false);
            if (challenge.Type.Trim() == "Everyday Life") challengeDescription.transform.SetParent(EveryDayLifeDescriptionParentObject.transform, false);
            if (challenge.Type.Trim() == "Events") challengeDescription.transform.SetParent(eventsDescriptionObject.transform, false);
            GameObject titleChild = challengeDescription.transform.FindChild("Title").gameObject;
            Text titleText = titleChild.GetComponent<Text>();
            titleText.text = challenge.Name;
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
            //DEPRICATED
            // descriptionText.text = challenge.Description;

           
                        GameObject factChild1 = challengeDescription.transform.FindChild("Fact").gameObject;
                       GameObject factChild2 = factChild1.transform.FindChild("scrollBox").gameObject;
                       GameObject factChild = factChild2.transform.FindChild("Text").gameObject;
                        Text factText = factChild.GetComponent<Text>();
                        String[] strings = challenge.Description.Split(new char[] { '/' });
                        if (strings.Length >1)
                        {
                            factText.text = strings[1];
                        }
                        Debug.LogError("this should disable: " + factChild1.name);
            
            descriptionText.text = strings[0];

            GameObject takeSelfieChild = challengeDescription.transform.FindChild("Take Selfie Button").gameObject;

            TakeSelfieButton buttonId = takeSelfieChild.GetComponent<TakeSelfieButton>();
            buttonId.buttonId = challenge.Id;


            //factChild1.SetActive(false);
            

        }
    }
}
