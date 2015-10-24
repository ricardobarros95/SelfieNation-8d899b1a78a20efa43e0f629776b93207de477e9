using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EnableDiscriptionButton : MonoBehaviour {


    public int buttonId;
    Button thisButton;
    public Sprite disabledChallenge;

    void setCorrectBackground()
    {
        GameManager.me.ProfileBackground.SetActive(false);
        GameManager.me.CategoryBackground.SetActive(false);
        GameManager.me.FactBackground.SetActive(true);
        GameManager.me.FactBackground.GetComponent<BackgroundChanger>().updateBG();
        thisButton = GetComponent<Button>();

    }

    public void descriptionButton()
    {
        setCorrectBackground();

        GameManager.me.ToggleDescriptions(true);
        if (!string.IsNullOrEmpty(GameManager.me.currentSelectedChallenge))
        {
            GameObject gmoj = GameObject.Find(GameManager.me.currentSelectedChallenge + "Description");

            

            foreach (Transform child in gmoj.transform)
            {
                child.gameObject.SetActiveRecursively(false);
            }

           // gmoj.transform.GetChild(0).gameObject.SetActive(false);
          //  Debug.LogError("THIS is mad"+gmoj.transform.GetChild(3).gameObject.name);
            //gmoj.SetActive(false);
        }
        //GameManager.me.ToggleDescriptions(true);

        

        GameObject go = GameObject.Find(gameObject.name + "Description");
        Debug.Log(go);
        GameManager.me.currentlySelectedDescription = go;
        go.SetActiveRecursively(true);

         //Debug.LogError(transform.GetChild(0).name);
        GameManager.me.categories.SetActive(false);
        GameManager.me.freshersChallenges.SetActive(false);
        GameManager.me.currentSelectedChallenge = gameObject.name;
        GameManager.me.currentlySelectedChallenge = gameObject;

    }

    public void DeactivateButton()
    {

        thisButton.interactable = false;
        
        
    }

    public void ChangeArt()
    {
        Debug.Log("RUNNING NOW");
        this.transform.gameObject.GetComponent<Image>().sprite = disabledChallenge;
    }

}
