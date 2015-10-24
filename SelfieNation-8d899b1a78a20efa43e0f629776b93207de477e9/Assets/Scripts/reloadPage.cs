/// description: code that reloads the conent of the page it's attached to
/// needs to be attached to scrolling recView

using UnityEngine;
using System.Collections;
using System;

public class reloadPage : MonoBehaviour {

    public string message = "refresh needed!";
    public float startYPos; //the top of the recview upon loading
    public float currentYpos;
    public float oldPos = 1500.0f;
    float triggerDifference = 200.00f;  // the minimum difference between start and changed position, that triggers loading
    bool loadingInProgress = false;
    float lastUpdate = 0.0f;

    public bool isScriptActive = false;
    //public bool isFirstRun = true;


    /*
	// Use this for initialization
	void Start () {

        startYPos = this.gameObject.GetComponent<RectTransform>().anchoredPosition.y;
        Debug.Log(startYPos.ToString());
     * 
     *     void Start() {
        StartCoroutine(Example());
    }
    
    IEnumerator Example() {
        print(Time.time);
        yield return new WaitForSeconds(5);
        print(Time.time);
    }
     * 
     * 
	}
	*/



    void Start()
    {

        //oldPos = currentYpos;

        InvokeRepeating("Initialisation", 2, 2.0F);




    }


    void Initialisation()
    {
        if (oldPos == currentYpos)
        {
            isScriptActive = true;
            startYPos = currentYpos;
            CancelInvoke("Initialisation"); 
        }
        else
            oldPos = currentYpos;
    }

    

    // Update is called once per frame
	void Update () {

        currentYpos = this.gameObject.GetComponent<RectTransform>().anchoredPosition.y;




        if (isScriptActive)
        {
            



            //reset the lock, so refresh can only requested once the recview is back in start position
            if (Math.Abs(currentYpos - startYPos) < 5)
            {
                loadingInProgress = false;
            }

            //if the recview is pulled down more than the trigger it calles the reload function, if it's not in progress already
            if (startYPos - currentYpos >= triggerDifference)
            {

                if (!loadingInProgress)
                {
                    loadingInProgress = true;
                    ReloadContent();
                }
            }
        }
        //isScriptActive = false;
	}

    //function to actually reload the page.
    //needs filling in, depends on actual element it's used for
    void ReloadContent()
    {

        //limit the reload call to be enabled after 5 seconds past
        if (Time.time - lastUpdate > 5)
        {
            Debug.LogError(message);
            GameObject.Find("newsFeed").GetComponent<NewsFeed>().LoadNews();
            lastUpdate = Time.time;
        }
        else
            Debug.LogError("It's too soon to reload the page!");
    }
}
