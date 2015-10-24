using UnityEngine;
using System.Collections;

public class TakeSelfieButton : MonoBehaviour {


	// Use this for initialization
	void Start () {
        
        
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void TakeSelfie()
    {
        GameManager.me.mainCanvas.SetActive(false);
        GameManager.me.takePic.SetActive(true);
        GameManager.me.ToggleDescriptions(false);
        //TestScript challengeID = GameManager.me.cameraHolder.GetComponent<TestScript>();
        //challengeID.challengeID = buttonId;
        GameManager.me.currentChallengeId = buttonId;

    }

    public int buttonId { get; set; }
}
