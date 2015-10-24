using UnityEngine;
using System.Collections;
using UnityEngine.UI; 

public class BackgroundChanger : MonoBehaviour {

    public Sprite easyBG;
    public Sprite mediumBG;
    public Sprite hardBG;

    string difficulity = "Easy";


    public void setDificulity(string diff)
    {
        difficulity = diff;
        Debug.Log(difficulity);

    }

    public void updateBG()
    {
        Sprite BG;

        switch (difficulity)
                {
                    case "Easy": BG = easyBG;
                        break;

                    case "Medium": BG = mediumBG;
                        break;

                    case "Hard": BG = hardBG;
                        break;

                    default: BG = null; 
                        break;
                }


        this.gameObject.GetComponent<Image>().sprite = BG;
        gameObject.GetComponentInChildren<Text>().text = difficulity;
    
    }

}
