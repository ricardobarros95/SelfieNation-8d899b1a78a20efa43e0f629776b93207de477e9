using UnityEngine;
using System.Collections;

public class openLink : MonoBehaviour {


	// Use this for initialization
	void Start () {

	
	}


   public void openURL(string targetURL)
    {
        if (targetURL != "")
        {
            Application.OpenURL(targetURL);
        }
    }
}
