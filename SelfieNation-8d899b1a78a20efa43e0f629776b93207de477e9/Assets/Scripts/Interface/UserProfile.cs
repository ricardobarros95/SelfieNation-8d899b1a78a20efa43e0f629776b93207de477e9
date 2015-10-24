using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using System.Net.Sockets;
using System.Text;
using Newtonsoft.Json;
using UnityEngine.UI;
using UnityEngine.EventSystems;



namespace Selfie
{
    enum state
    {
        photosNotLoaded,
        photosLOADED,
        noPhotosToLoad,
        everythingLoaded
    };

    public class UserProfile : MonoBehaviour
    {
        //turn debud mode on/off
        bool isDebugOn = true;//false;
        bool isGalleryDebugON = false;
        public Sprite debugsprite;
        public GameObject Username;
        short numberOfDebugSprites = 22;

        //this need to be set somehow!!!
         int userID = 520;

        //Connection Variables
         string photoListURL = "http://5.9.251.204/api/photolibrary/user/";
         string photoDownloadURL = "http://5.9.251.204/api/photolibrary/photo/"; // "/api/photolibrary/photo/{Id}"
         string challangeDownloadURL = "http://5.9.251.204/api/manager/"; //  "/api/manager/{Id}"


        //Internal Variables
        List<string> photoList = new List<string>();
        List<GameObject> ContainerList = new List<GameObject>();
        Texture2D photoTexture;
        Sprite photoSprite;

        //to see where are we: profile ("ProfilePanel") or gallery ("photoFrames")
        string location;
        short isPhotolistLoaded = 0;

        //gallery variables
        Vector3 zeroCubePos = new Vector3();
        Vector2 cubeDimensions = new Vector2();
        public Transform cubePrefab;

   
        //public stuff
        public GameObject ChallangeCount;
        public GameObject profilePicture;
        Image profilePic;

        //public string preChallangeCountText;



        // Use this for initialization
        void Start()
        {
            //grab the current userID
            if (GameManager.me.userId != 0)
            {
                userID = GameManager.me.userId;
                Debug.Log(userID);
            }

            

            //see which page we're on. (the script is attched to more than one object)
            location = this.gameObject.name;
            if (isDebugOn) 
            { 
                Debug.Log("We are on page: " + location); 
            }

            //load the photos assosiated with the given user (above, "public int userID" )
            LoadPhotolist();
            GetUserInfo();

        }

        void GetUserInfo()
        {
            string userInfoURL = "http://5.9.251.204/api/user/" + GameManager.me.userId;
            WWW www = new WWW(userInfoURL);
            StartCoroutine(GetUserInfoCoroutine(www));
        }

        IEnumerator GetUserInfoCoroutine(WWW www)
        {
            yield return www;
            if (www.error == null)
            {
                Debug.Log(www.text);
                User user = JsonConvert.DeserializeObject<User>(www.text);
                LoadProfilePic(user.PhotoId);
                setUserName(user.Name);
            }
            else
            {
                Debug.Log(www.error);
            }
        }

        void LoadProfilePic(int photoId)
        {
            string profilePicURL = "http://5.9.251.204/api/photolibrary/photo/" + photoId;
            WWW www = new WWW(profilePicURL);
            StartCoroutine(GetProfilePic(www));
        }

        void setUserName(string name)
        {
            Username.GetComponent<Text>().text = name;
        }

        IEnumerator GetProfilePic(WWW www)
        {
            yield return www;
            if (www.error == null)
            {
                Debug.Log(www.text);
                profilePic = profilePicture.GetComponent<Image>();
                profilePic.sprite = Sprite.Create(www.texture, new Rect(0, 0, 300, 300), new Vector2(0, 0));
            }
            else
            {
                Debug.Log(www.error);
            }
        }

        // Update is called once per frame
        void Update()
        {

            if (isPhotolistLoaded == 1)
            {
                //load/disable the containers for the 4 most recent photos
                LoadContainers();


                LoadCompletedChallenges();

                //load the first (max.) 4 photos for the profile page if it's on the profile page
                if (location == "ProfilePanel") { LoadPhotos(4); }
                else if (location == "photoFrames") { LoadPhotos(0); }

                isPhotolistLoaded = 3;
            }

            //if the player has no photos yet
            if (isPhotolistLoaded == 2)
            {
                LoadCompletedChallenges();
                if (isGalleryDebugON) { LoadPhotos(numberOfDebugSprites); };
                isPhotolistLoaded = 3;
            }



        }


        void LoadCompletedChallenges()
        {
           challangeDownloadURL += userID;
           if (isDebugOn) { Debug.LogWarning("challangeDownloadURL += userID :" + challangeDownloadURL); }

           Debug.Log("hello");
           WWW www = new WWW(challangeDownloadURL);
           StartCoroutine(GetCompChallengeList(www));
           
        }

        /// <summary>
        /// CALLED BY: LoadCompletedChallenges() - method level: 2
        /// method to phisically create a list of the challenges competed by the selected user
        /// generic for location
        /// </summary>
        IEnumerator GetCompChallengeList(WWW www)
        {
            yield return www;
            if (www.error == null)
            {
               if (isDebugOn) { Debug.Log("www.text-GetCompChallengeList: " + www.text); }

               if (www.text != "[]")
               {
                   string[] tmp = www.text.Split(new Char[] { ',' });

                   if (isDebugOn) { Debug.Log("www.text - GetCompChallengeList -LENGHT: " + tmp.Length); }

                   ChallangeCount.GetComponent<Text>().text = "Completed challenges:" + " " + tmp.Length.ToString();
               
               }
               else
               {
                   ChallangeCount.GetComponent<Text>().text = "Completed challenges: " + 0;
               }


            }
            else
            {
                Debug.LogError(www.error);
            }
        }


        /// <summary>
        /// CALLED BY: MAIN - method level: 1
        /// method to create a list of the photos assosiated with the selected user
        /// generic for location
        /// </summary>
        public void LoadPhotolist()
        {
            //setting up the string needed for getting the photos assosiated with the user
            photoListURL += userID;
            if (isDebugOn) { Debug.LogWarning("photoListURL += userID :" + photoListURL); }


            //get photoList
            WWW www = new WWW(photoListURL);
            StartCoroutine(GetPhotoList(www));
        }

        /// <summary>
        /// CALLED BY: LoadPhotolist() - method level: 2
        /// method to phisically create a list of the photos assosiated with the selected user
        /// generic for location
        /// </summary>
        IEnumerator GetPhotoList(WWW www)
        {
            yield return www;
            if (www.error == null)
            {
                if (isDebugOn) { Debug.Log("www.text: '" + www.text+"'"); }

                if (www.text != "[]")
                {
                    string[] tmp = www.text.Split(new Char[] { ',' });
                    //filling in the photoList arraylist
                    // if ()
                    for (int z = 0; z < tmp.Length; z++)
                    {
                        if (z == 0) { tmp[0] = tmp[0].Remove(0, 1); } //removing the first char, which is always "["
                        if (z == tmp.Length - 1) { tmp[z] = tmp[z].Replace("]", ""); } //removing the last char, which is always "]"
                        photoList.Add(tmp[z]);
                        if (isDebugOn) { Debug.LogWarning("photoList[z]: '" + photoList[z] + "'"); }
                    }

                    isPhotolistLoaded = 1;
                }

                else 
                {
                    isPhotolistLoaded = 2;
                }

            }
            else
            {
                Debug.LogError(www.error);
            }
        }



        /// <summary>
        /// CALLED BY: MAIN - method level: 1
        /// method to load and disable the recent photo containers based on the number of photos the user has
        /// uses NO ENUMERATORS
        /// </summary>
        void LoadContainers()
        {
            int photoCount = photoList.Count;

            if (location == "ProfilePanel")
            {
                //setting up the container list
                ContainerList.Add(GameObject.Find("recent_photo1"));
                ContainerList.Add(GameObject.Find("recent_photo2"));
                ContainerList.Add(GameObject.Find("recent_photo3"));
                ContainerList.Add(GameObject.Find("recent_photo4"));

                if (photoCount == 0) { if (isDebugOn) { Debug.Log("Number of photos are 0"); } string[] tmp = { "recent_photo1", "recent_photo2", "recent_photo3", "recent_photo4" }; DisableContainer(tmp); }
                else if (photoCount == 1) { if (isDebugOn) { Debug.Log("Number of photos are 1"); } string[] tmp = { "recent_photo2", "recent_photo3", "recent_photo4" }; DisableContainer(tmp); }
                else if (photoCount == 2) { if (isDebugOn) { Debug.Log("Number of photos are 2"); } string[] tmp = { "recent_photo3", "recent_photo4" }; DisableContainer(tmp); }
                else if (photoCount == 3) { if (isDebugOn) { Debug.Log("Number of photos are 3"); } string[] tmp = { "recent_photo4" }; DisableContainer(tmp); }
                else { if (isDebugOn) { Debug.Log("Number of photos are more than 3"); } }
            }

            if (location == "photoFrames")
            {

                //set variables here
                zeroCubePos.Set(220f, -310f, 0.0f);
                cubeDimensions.Set(245, 245);

                float xSpacer = 265;
                float ySpacer = -265;

                int photosCreated = 0;
                GameObject tmp;
                int rowCount;


                //---------------------------------------------//

                for (rowCount = 0; rowCount < ((photoCount / 4) + 1); rowCount++)
                //for (rowCount = 0; rowCount < ((photoCount / 2) + 1); rowCount++)
                {

                    for (int x = 0; x < 4; x++)
                    {
                        //create an object from the prefab, and make it a gameobject
                        Transform t = Instantiate(cubePrefab, new Vector3(0, 0, 0), Quaternion.identity) as Transform;
                        tmp = t.gameObject;

                        //set the proper parent for the new object
                        tmp.transform.SetParent(this.gameObject.transform, false);

                        //add the created frame to a global list
                        tmp.name = "GFrame_" + rowCount + x;
                        ContainerList.Add(tmp);

                        //position the object
                        if (x == 0 && rowCount == 0)
                        {
                            tmp.gameObject.transform.localPosition = zeroCubePos;
                        }
                        else
                        {
                            tmp.gameObject.transform.localPosition = new Vector3((zeroCubePos.x + x * xSpacer), (zeroCubePos.y + rowCount * ySpacer), zeroCubePos.z);
                        }

                        photosCreated++;

                        //terminate the loop if the number of photos reached!!!
                        if (photosCreated == photoCount) { break; }
                    }

                }

                //set the RecView's height to fit all the rows!
                if (isDebugOn) { Debug.Log("Number rows: " + rowCount); }
                GameObject.Find("PhotoGallery Rect View").GetComponent<RectTransform>().offsetMin = new Vector2(0.0f, ( -245.0f * (rowCount-3)));
            
            }


        }

        /// <summary>
        /// CALLED BY: void LoadContainers() - method level: 2
        /// method to disable the given container
        /// specific to "PROFILE"
        /// </summary>
        /// <param name="containerID"></param>
        void DisableContainer(string[] containerID)
        {
            GameObject tmp;
            //disable passed in photoframes (there's no photo to put into it)
            for (int i = 0; i < containerID.Length; i++)
            {
                tmp = GameObject.Find(containerID[i]);
                tmp.SetActive(false);
            }

            //disable "more photos" link
            //tmp = GameObject.Find("more_button");
            //tmp.SetActive(false);

        }



        /// <summary>
        /// CALLED BY: LoadPhotos() - method level: 2
        /// method to phisically load a photo from the server and assign it to an empty frame
        /// generic for location
        /// </summary>
        IEnumerator GetPhoto(WWW www, int containerID)
        {
            if (isGalleryDebugON)
            {
                photoTexture = debugsprite.texture;
                Rect rec = new Rect(0, 0, photoTexture.width, photoTexture.height);
                photoSprite = Sprite.Create(photoTexture, rec, new Vector2(0, 0), .01f);
                ContainerList[containerID].GetComponent<Image>().sprite = photoSprite;
            }

            else
            {
                yield return www;
                if (www.error == null)
                {

                    photoTexture = www.texture;
                    Rect rec = new Rect(0, 0, photoTexture.width, photoTexture.height);
                    photoSprite = Sprite.Create(photoTexture, rec, new Vector2(0, 0), .01f);

                    //NOT IN USE  string container = containerID.ToString();
                    //* DEPRICATED: GameObject.Find("recent_photo2").GetComponent<Image>().sprite = photoSprite;
                    ContainerList[containerID].GetComponent<Image>().sprite = photoSprite;

                }
                else
                {
                    Debug.LogError(www.error);
                    Debug.Log(www.text);
                }
            }
        }

        /// <summary>
        /// CALLED BY: MAIN() - method level: 1
        /// method to load all the photos to a list and most recent first
        /// generic for location
        /// </summary>
        void LoadPhotos(int count)
        {

            string currenntphotoDownloadURL;

            //if parameter is 0, it loads all images avaliable
            if (count == 0) { count = photoList.Count; }

            //if parameter is more than 0, it loads at photos per parameter if that many is avaliable
            if (photoList.Count < count) { count = photoList.Count; }

            //debug overwrite
            if (isPhotolistLoaded == 2) { count = numberOfDebugSprites; }

            if (isDebugOn) { Debug.Log("photoList.Count: " + photoList.Count); }
            if (isDebugOn) { Debug.Log("count: " + count); }

            int i = 0;
            while(i != count)
            { 
                //constract the request link for the actual photo
                currenntphotoDownloadURL = photoDownloadURL + photoList[(photoList.Count-1) - i];

                if (isDebugOn) { Debug.Log("the link for the photo is: " + currenntphotoDownloadURL); }

                //download the image
                WWW www2 = new WWW(currenntphotoDownloadURL);
                
                StartCoroutine(GetPhoto(www2, i));

                i++;
            }

        }

    }

}