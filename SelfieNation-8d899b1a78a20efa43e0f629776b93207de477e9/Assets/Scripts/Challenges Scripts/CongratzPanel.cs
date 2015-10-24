using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Facebook.MiniJSON;

public class CongratzPanel : MonoBehaviour {

    public Image challengePhoto;
    public Text challengeName;
    public GameObject img;
    //public GameObject homePanel;
    public GameObject takePicPanel;
    public string appStoreURL;

	// Use this for initialization
	void Start () {
        challengePhoto = GetComponent<Image>();
        challengePhoto = GameManager.me.currentChallengeImage;

        
	}
    void Update()
    {
        challengeName.text = "You have completed " + GameManager.me.currentSelectedChallenge + " challenge!";
    }

    public void NextButton()
    {
        gameObject.SetActive(false);
        //GameManager.me.ToggleDescriptions(true);
        GameManager.me.homePanel.SetActive(true);
    }

    public void ShareButton()
    {
        FB.Feed(
            link: appStoreURL,
            linkName: "I have just completed the " + GameManager.me.currentSelectedChallenge + " challenge",
            linkCaption: "Come join the Selfie Nation!",
            picture: "http://i.imgur.com/9fMgD17.png?1"
            );
    }
    public void backButton()
    {
        gameObject.SetActive(false);
        takePicPanel.SetActive(true);
    }
}
