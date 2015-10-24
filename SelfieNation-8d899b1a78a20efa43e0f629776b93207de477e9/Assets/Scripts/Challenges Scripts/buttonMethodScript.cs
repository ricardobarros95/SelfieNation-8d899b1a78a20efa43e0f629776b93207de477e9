using UnityEngine;
using System.Collections;

public class buttonMethodScript : MonoBehaviour {
   


    public void buttonMethod()
    {
        if(gameObject.name == "Travel/Tourism") 
        {
            GameManager.me.TravelParentObject.SetActive(true);
            GameManager.me.categories.SetActive(true);
            GameManager.me.challengesCategoryPanel.SetActive(false);
        }
        if (gameObject.name == "Sports")
        {
            GameManager.me.SportsParentObject.SetActive(true);
            GameManager.me.categories.SetActive(true);
            GameManager.me.challengesCategoryPanel.SetActive(false);
        }
        if (gameObject.name == "Cars")
        {
            GameManager.me.CarsParentObject.SetActive(true);
            GameManager.me.categories.SetActive(true);
            GameManager.me.challengesCategoryPanel.SetActive(false);
        }
        if (gameObject.name == "Dare to be Digital")
        {
            GameManager.me.DareToBeDigitalParentObject.SetActive(true);
            GameManager.me.categories.SetActive(true);
            GameManager.me.challengesCategoryPanel.SetActive(false);
        }
        if (gameObject.name == "People and Places")
        {
            GameManager.me.PeopleAndPlacesParentObject.SetActive(true);
            GameManager.me.categories.SetActive(true);
            GameManager.me.challengesCategoryPanel.SetActive(false);
        }
        if (gameObject.name == "Miscellaneous")
        {
            GameManager.me.miscellaneousObject.SetActive(true);
            GameManager.me.categories.SetActive(true);
            GameManager.me.challengesCategoryPanel.SetActive(false);
        }
        if (gameObject.name == "Animals")
        {
            GameManager.me.AnimalsParentObject.SetActive(true);
            GameManager.me.categories.SetActive(true);
            GameManager.me.challengesCategoryPanel.SetActive(false);
        }
        if (gameObject.name == "Everyday Life")
        {
            GameManager.me.EveryDayLifeParentObject.SetActive(true);
            GameManager.me.categories.SetActive(true);
            GameManager.me.challengesCategoryPanel.SetActive(false);
        }
        if (gameObject.name == "Food/Drink")
        {
            GameManager.me.FoodParentObject.SetActive(true);
            GameManager.me.categories.SetActive(true);
            GameManager.me.challengesCategoryPanel.SetActive(false);
        }
        if (gameObject.name == "Great Outdoors")
        {
            GameManager.me.GreatOutdoorsParentObject.SetActive(true);
            GameManager.me.categories.SetActive(true);
            GameManager.me.challengesCategoryPanel.SetActive(false);
        }
        if (gameObject.name == "Events")
        {
            GameManager.me.eventsObject.SetActive(true);
            GameManager.me.categories.SetActive(true);
            GameManager.me.challengesCategoryPanel.SetActive(false);
        }
        if (gameObject.name == "Food/Drink")
        {
            GameManager.me.FoodParentObject.SetActive(true);
            GameManager.me.categories.SetActive(true);
            GameManager.me.challengesCategoryPanel.SetActive(false);
        }
        GameManager.me.currentlySelectedCategory = GameObject.Find(gameObject.name + "Category");
        
    }
}
