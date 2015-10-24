using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Text;

public class fullSizePhoto : MonoBehaviour {

   // public GameObject fullSizeFrame;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void openFullSize()
    {
        GameManager.me.fullSizePhotoFrame.transform.parent.gameObject.SetActive(true);
        GameManager.me.FullSizePhoto_greyFilter.SetActive(true);
        GameManager.me.fullSizePhotoFrame.GetComponent<Image>().sprite = this.gameObject.GetComponent<Image>().sprite;
        
    }
}
