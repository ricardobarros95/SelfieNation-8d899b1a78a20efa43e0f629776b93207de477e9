using UnityEngine;
using System.Collections;

public class DisableStars : MonoBehaviour {

    public GameObject[] stars;
    public GameObject tigerLogo;
	// Use this for initialization
	void OnEnable () {
        for (int i = 0; i < stars.Length; i++)
        {
            stars[i].SetActive(false);
        }
        tigerLogo.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
