using UnityEngine;
using System.Collections;

public class ChallengesButtons : MonoBehaviour {

    public GameObject challengesCategoryPanel;
    public GameObject animalsCategory;
    public GameObject[] disabledObjects;
    public GameObject cameraScreen;
    public GameObject categories;

    public void AnimalsButton()
    {
        //challengesCategoryPanel.SetActive(false);
        animalsCategory.SetActive(true);
    }

    public void GoToCameraScreen()
    {
        for (int i = 0; i < disabledObjects.Length; i++)
        {
            disabledObjects[i].SetActive(false);
        }
        cameraScreen.SetActive(true);
    }

    public void DisableChallengesPanel()
    {
        categories.SetActive(false);
    }
    


}
