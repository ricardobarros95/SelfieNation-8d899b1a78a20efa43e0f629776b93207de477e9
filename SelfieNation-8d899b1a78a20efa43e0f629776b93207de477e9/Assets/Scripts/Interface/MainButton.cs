using UnityEngine;
using System.Collections;

public class MainButton : MonoBehaviour {
    public GameObject[] panelAndButtons;
    public GameObject[] ChallengesPanels;
    public GameObject profilePanel;
    public bool mainButtonClicked = false;
    public  bool animationBool = false;
    private Vector3 initPos;
    private Vector3 initScale;

	// Use this for initialization
	void Start () {
        initPos = panelAndButtons[0].transform.position;
        initScale = panelAndButtons[0].transform.localScale;
	}
	
	// Update is called once per frame
	void Update () {
        if(animationBool && mainButtonClicked)
        {
            mainButtonAnimation();
        }
        
	}

    public void MainButtonClick()
    {
        mainButtonClicked = !mainButtonClicked;

        
        if(mainButtonClicked)
        {
            panelAndButtons[0].SetActive(true);
            animationBool = true;
        }
        else
        {
            resetPos();
        }
    }

    void resetPos()
    {
        panelAndButtons[0].SetActive(false);
        for (int i = 0; i < panelAndButtons.Length; i++)
        {
            panelAndButtons[i].transform.localScale = initScale;
        }
        panelAndButtons[0].transform.position = initPos;
        animationBool = false;
    }

    void mainButtonAnimation()
    {
        for (int i = 0; i < panelAndButtons.Length; i++)
        {
            if (panelAndButtons[i].transform.localScale.x < 0.5f && panelAndButtons[i].transform.localScale.y < 1)
            {
                panelAndButtons[i].transform.localScale += new Vector3(0.05f, 0.05f, 0);
            }
        }
        if (panelAndButtons[0].transform.position.y < Screen.height/12)
        {
            panelAndButtons[0].transform.position += new Vector3(0, 35, 0);
        }
        else
        {
            animationBool = false;
        }
    }

    //Challenges Button functionality
    public void ChallengesButton()
    {
        mainButtonClicked = false;
        //Disable the main button panel
        panelAndButtons[0].SetActive(false);
        //Enables the challenges panel
        //for(int i = 0; i<ChallengesPanels.Length; i++)
        //{
        //    ChallengesPanels[i].SetActive(true);
        //}
        profilePanel.SetActive(false);
        Debug.Log("HE");
        GameManager.me.freshersChallenges.SetActive(true);
        GameManager.me.profilePanel.SetActive(false);
        GameManager.me.photoGallery.SetActive(false);
        GameManager.me.ToggleDescriptions(false);
        GameManager.me.ToggleCategoriesParentObjects(false);
        if(GameManager.me.currentlySelectedCategory != null) GameManager.me.currentlySelectedCategory.SetActive(false);
        GameManager.me.logOutPanel.SetActive(false);
        GameManager.me.homePanel.SetActive(false);
        GameManager.me.friendsPanel.SetActive(false);
        GameManager.me.LeaderBoardPanel.SetActive(false);
        resetPos();
    }

    //Profile Button Functionality
    public void ProfileButton()
    {
        mainButtonClicked = false;
        //Disable ChallengesPanel
        for (int i = 0; i < ChallengesPanels.Length; i++)
        {
            ChallengesPanels[i].SetActive(false);
        }
        //Disable all the panel of the main Button
            panelAndButtons[0].SetActive(false);
        //Enable the profile panel
        profilePanel.SetActive(true);

        GameManager.me.photoGallery.SetActive(false);
        GameManager.me.challengesCategoryPanel.SetActive(false);
        GameManager.me.categories.SetActive(false);
        GameManager.me.ToggleDescriptions(false);
        GameManager.me.ToggleCategoriesParentObjects(false);
        if (GameManager.me.currentlySelectedCategory != null) GameManager.me.currentlySelectedCategory.SetActive(false);
        GameManager.me.logOutPanel.SetActive(false);
        GameManager.me.homePanel.SetActive(false);
        GameManager.me.friendsPanel.SetActive(false);
        GameManager.me.LeaderBoardPanel.SetActive(false);
        resetPos();
    }

    public void homePageButton()
    {
        GameManager.me.homePanel.SetActive(true);
        GameManager.me.photoGallery.SetActive(false);
        GameManager.me.challengesCategoryPanel.SetActive(false);
        GameManager.me.categories.SetActive(false);
        GameManager.me.ToggleDescriptions(false);
        GameManager.me.ToggleCategoriesParentObjects(false);
        if (GameManager.me.currentlySelectedCategory != null) GameManager.me.currentlySelectedCategory.SetActive(false);
        GameManager.me.logOutPanel.SetActive(false);
        GameManager.me.friendsPanel.SetActive(false);
        GameManager.me.LeaderBoardPanel.SetActive(false);
        resetPos();
    }

    public void friendsPanel()
    {
        GameManager.me.friendsPanel.SetActive(true);
        GameManager.me.homePanel.SetActive(false);
        GameManager.me.photoGallery.SetActive(false);
        GameManager.me.challengesCategoryPanel.SetActive(false);
        GameManager.me.categories.SetActive(false);
        GameManager.me.ToggleDescriptions(false);
        GameManager.me.ToggleCategoriesParentObjects(false);
        if (GameManager.me.currentlySelectedCategory != null) GameManager.me.currentlySelectedCategory.SetActive(false);
        GameManager.me.logOutPanel.SetActive(false);
        GameManager.me.profilePanel.SetActive(false);
        GameManager.me.LeaderBoardPanel.SetActive(false);
        resetPos();
    }

    public void LeaderBoard()
    {
        GameManager.me.LeaderBoardPanel.SetActive(true);
        GameManager.me.friendsPanel.SetActive(false);
        GameManager.me.homePanel.SetActive(false);
        GameManager.me.photoGallery.SetActive(false);
        GameManager.me.challengesCategoryPanel.SetActive(false);
        GameManager.me.categories.SetActive(false);
        GameManager.me.ToggleDescriptions(false);
        GameManager.me.ToggleCategoriesParentObjects(false);
        if (GameManager.me.currentlySelectedCategory != null) GameManager.me.currentlySelectedCategory.SetActive(false);
        GameManager.me.logOutPanel.SetActive(false);
        GameManager.me.profilePanel.SetActive(false);
        resetPos();
    }
}
