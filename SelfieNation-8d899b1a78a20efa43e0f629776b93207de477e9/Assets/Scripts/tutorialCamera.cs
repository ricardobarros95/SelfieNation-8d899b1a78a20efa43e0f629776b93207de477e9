using UnityEngine;
using System.Collections;
using Newtonsoft.Json;
using System.Text;
using System.Collections.Generic;
using System.Net;
using System.IO;
using UnityEngine.UI;
using System;

public class tutorialCamera : MonoBehaviour {

    //Camera
    WebCamTexture camera;
    Renderer renderer;
    WebCamDevice[] devices;
    Texture2D photo;
    int photoNumber;
    int photoId;
    Sprite currentSprite;

    //Game Objects
    public GameObject cameraSwitchButton;
    public GameObject snapShotButton;
    public GameObject retakePhotoButton;
    public GameObject keepPhotoButton;
    public GameObject backButton;
    

    //URL's
    string uploadPhotoURL;
    string completeChallengesURL;
    string updateProfilePicURL;

	void Start () {
        devices = WebCamTexture.devices;
        camera = new WebCamTexture();
        renderer = GetComponent<Renderer>();
        renderer.material.mainTexture = camera;
        camera.Play();
        if (devices.Length > 1) cameraSwitchButton.SetActive(true);
        uploadPhotoURL = "http://5.9.251.204/api/photolibrary/";
        completeChallengesURL = "http://5.9.251.204/api/manager/complete";
        
	}
	
	public void SnapShotButton () 
    {
        photo = new Texture2D(camera.width, camera.height);
        photo.SetPixels(camera.GetPixels());
        photo.Apply();
        camera.Pause();
        cameraSwitchButton.SetActive(false);
        snapShotButton.SetActive(false);
        keepPhotoButton.SetActive(true);
        retakePhotoButton.SetActive(true);
	}

    public void KeepPhotoButton()
    {
        camera.Stop();
        byte[] bytes = photo.EncodeToJPG();
        GameManager.me.Bytes = bytes;
        photoNumber = PlayerPrefs.GetInt("photoNumber");
       // File.WriteAllBytes(Application.persistentDataPath + "/pic" + photoNumber + ".jpg", bytes);
        photoNumber++;
        PlayerPrefs.SetInt("photoNumber", photoNumber);
        GameManager.me.onTutorial = PlayerPrefs.GetInt("tutorial");
        currentSprite = Sprite.Create(photo, new Rect(0, 0, photo.width, photo.height), new Vector2(0, 0));
        GameManager.me.currentChallengeImage.sprite = currentSprite;
        if (GameManager.me.onTutorial<1)
        {
            UploadPhoto(bytes); 
        }
        else
        {
            UploadPhoto(bytes);
            CompleteChallenge();
            
            Button b = GameManager.me.currentlySelectedChallenge.GetComponent<Button>();
            b.interactable = false;
            b.GetComponent<EnableDiscriptionButton>().ChangeArt();
        }
        GameManager.me.onTutorial++;
        PlayerPrefs.SetInt("tutorial", GameManager.me.onTutorial);
        PlayerPrefs.Save();
    }

    public void RetakePhotoButton()
    {
        camera.Play();
        snapShotButton.SetActive(true);
        if (devices.Length > 1) cameraSwitchButton.SetActive(true);
        retakePhotoButton.SetActive(false);
        keepPhotoButton.SetActive(false);
        if (GameManager.me.onTutorial>1) backButton.SetActive(true);
    }

    public void switchCamera()
    {
        camera.Stop();

        
        GameManager.me.cameraHolder = this.gameObject;

        camera.deviceName = (camera.deviceName == devices[0].name) ? devices[1].name : devices[0].name;
        GameManager.me.cameraHolder.transform.Rotate(new Vector3(180, 180, 0));
        camera.Play();
    }



    void UploadPhoto(byte[] bytes)
    {
        Photo payload = new Photo();
        payload.UserId = GameManager.me.userId;
        payload.PhotoData = bytes;
        if (GameManager.me.onTutorial > 1) payload.ProduceEvent = true;
        else { payload.ProduceEvent = false; }
        payload.RotateBy = camera.videoRotationAngle;
        string output = JsonConvert.SerializeObject(payload);
        Dictionary<string, string> headers = new Dictionary<string, string>();
        headers.Add("Content-type", "application/json");
        byte[] pData = Encoding.ASCII.GetBytes(output.ToCharArray());
        WWW www = new WWW(uploadPhotoURL, pData, headers);
        StartCoroutine(UploadPhotoCoroutine(www));
    }

    IEnumerator UploadPhotoCoroutine(WWW www)
    {
        yield return www;
        if (www.error == null)
        {
            photoId = JsonConvert.DeserializeObject<int>(www.text);
            if (GameManager.me.onTutorial < 1)
            {
                updateProfilePicURL = "http://5.9.251.204/api/user/setphoto/" + GameManager.me.userId;

                Dictionary<string, string> headers = new Dictionary<string, string>();
                headers.Add("Content-type", "application/json");
                byte[] pData = Encoding.ASCII.GetBytes(www.text.ToCharArray());
                WWW www1 = new WWW(updateProfilePicURL, pData, headers);
                StartCoroutine(UpdateProfilePicture(www1));
            }
        }
    }

    IEnumerator UpdateProfilePicture(WWW www)
    {
        yield return www;
        if (www.error == null)
        {
            Debug.Log(www.text);
            GameManager.me.state = State.Tutorial;
            GameManager.me.freshersChallenges.SetActive(true);
            GameManager.me.secondTutorialMessage.SetActive(true);
            GameManager.me.secondTutorialButton.SetActive(true);
            GameManager.me.InvisibleOverlay.SetActive(true);
            GameManager.me.loggedInMenus.SetActive(true);
            GameManager.me.tutorial.SetActive(false);
        }
        else
        {
            Debug.Log(www.error);
        }
    }

    void CompleteChallenge()
    {
        CompletedChallenge completedChallenge = new CompletedChallenge();
        completedChallenge.ChallengeId = GameManager.me.currentChallengeId;
        completedChallenge.UserId = GameManager.me.userId;
        completedChallenge.PhotoId = photoId;
        completedChallenge.CompletedAt = DateTime.Now;
        string output = JsonConvert.SerializeObject(completedChallenge);
        Dictionary<string, string> headers = new Dictionary<string, string>();
        headers.Add("content-type", "appication/json");
        byte[] data = Encoding.ASCII.GetBytes(output.ToCharArray());
        WWW www = new WWW(completeChallengesURL);
        StartCoroutine(CompleteChallengeCoroutine(www));
        
    }

    IEnumerator CompleteChallengeCoroutine(WWW www)
    {
        yield return www;
        if (www.error == null)
        {
            Debug.Log(www.text);
            GameManager.me.congratzPanel.SetActive(true);
            GameManager.me.takePic.SetActive(false);
            GameManager.me.mainCanvas.SetActive(true);
        }
    }

}
