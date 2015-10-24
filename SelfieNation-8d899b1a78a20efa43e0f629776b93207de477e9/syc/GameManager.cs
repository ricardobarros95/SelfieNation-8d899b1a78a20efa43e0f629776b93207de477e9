using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;



public enum State { Tutorial, RepeatingEasy, Medium, Hard };

public class GameManager : MonoBehaviour {


    public int onTutorial = 0;
    public GameObject tutorial;
    public GameObject freshersChallenges;
    public GameObject freshersChallengesDescription;
    public GameObject secondTutorialMessage;
    public GameObject secondTutorialButton;
    public GameObject InvisibleOverlay;
    public int currentlySelectedProgramme;
    public byte[] Bytes { get;set;}
    public GameObject loggedInMenus;
    public GameObject loggedOutMenus;
    public GameObject logInScreen;
    public GameObject signUpScreen;
    public State state;

    public GameObject LeaderBoardPanel;
    //BackgroundHolders

    public GameObject ProfileBackground;
    public GameObject FactBackground;
    public GameObject CategoryBackground;

    public GameObject homePanel;

    public GameObject logOutPanel;

    public GameObject currentlySelectedCategory;

    public GameObject profilePanel;

    //public GameObject homePanel;

    public GameObject currentlySelectedDescription;

    public GameObject challengeDescriptions;

    public GameObject challengesCategoryPanel;

    public GameObject photoGallery;

    public GameObject congratzPanel;

    public int userId;

    public Image currentChallengeImage;

    public string currentSelectedChallenge;

    public GameObject currentlySelectedChallenge;

    public GameObject facebookHolder;

    public GameObject cameraHolder;

    public GameObject takePic;

    public GameObject descriptions;

    public GameObject mainCanvas;

    public GameObject categories;

    public GameObject eventsObject;

    public GameObject miscellaneousObject;

    public GameObject AnimalsParentObject;

    public GameObject TravelParentObject;

    public GameObject SportsParentObject;

    public GameObject CarsParentObject;

    public GameObject DareToBeDigitalParentObject;

    public GameObject PeopleAndPlacesParentObject;

    public GameObject FoodParentObject;

    public GameObject GreatOutdoorsParentObject;

    public GameObject EveryDayLifeParentObject;

    public int currentChallengeId;

    public bool feed = false;
    public GameObject emptyFeed;

    public Challenge[] challengesList;

    public GameObject animalsDecription;
    public GameObject Travel_TourismDescriptions;
    public GameObject SportsDescriptions;
    public GameObject CarsDescriptions;
    public GameObject DareToBeDigitalDescriptions;
    public GameObject PeopleandPlacesDescriptions;
    public GameObject MiscellaneousDescriptions;
    public GameObject Food_DrinkDescriptions;
    public GameObject GreatOutdoorsDescriptions;
    public GameObject EventsDescriptions;
    public GameObject EverydayLifeDescriptions;

    public int i = 0;
    public GameObject tutorialPanel;
    public GameObject disclaimerPanel;
    public GameObject friendsPanel;

    public static GameManager me = null;

    public GameObject fullSizePhotoFrame;
    public GameObject FullSizePhoto_greyFilter;

    

    void Awake()
    {
        PlayerPrefs.DeleteKey("tutorial");
        if (me != null)
            Debug.LogError("GameManager must be attached to one gameobject only!");
        me = this;
        onTutorial = PlayerPrefs.GetInt("tutorial");
    }


    public void ToggleDescriptions(bool active)
    {
        animalsDecription.SetActive(active);
        Travel_TourismDescriptions.SetActive(active);
        SportsDescriptions.SetActive(active);
        CarsDescriptions.SetActive(active);
        DareToBeDigitalDescriptions.SetActive(active);
        PeopleandPlacesDescriptions.SetActive(active);
        MiscellaneousDescriptions.SetActive(active);
        Food_DrinkDescriptions.SetActive(active);
        GreatOutdoorsDescriptions.SetActive(active);
        EventsDescriptions.SetActive(active);
        EverydayLifeDescriptions.SetActive(active);
        freshersChallengesDescription.SetActive(active);
    }

    public void ToggleCategoriesParentObjects(bool active)
    {
        eventsObject.SetActive(active);
        miscellaneousObject.SetActive(active);
        AnimalsParentObject.SetActive(active);
        TravelParentObject.SetActive(active);
        SportsParentObject.SetActive(active);
        CarsParentObject.SetActive(active);
        DareToBeDigitalParentObject.SetActive(active);
        PeopleAndPlacesParentObject.SetActive(active);
        FoodParentObject.SetActive(active);
        GreatOutdoorsParentObject.SetActive(active);
        EveryDayLifeParentObject.SetActive(active);
    }

}
