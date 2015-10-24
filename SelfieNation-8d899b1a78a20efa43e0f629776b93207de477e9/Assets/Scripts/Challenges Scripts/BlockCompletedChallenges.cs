using UnityEngine;
using System.Collections;

public class BlockCompletedChallenges : MonoBehaviour {

    Challenge[] challenges;
    string getCompletedChallengesURL;
	// Use this for initialization
	void Start () {
        challenges = GameManager.me.challengesList;
        getCompletedChallengesURL = "http://5.9.251.204/api/manager/" + GameManager.me.userId;
        WWW www = new WWW(getCompletedChallengesURL);
        StartCoroutine(GetChallenges(www));
        Debug.Log(challenges.Length);
	}

    IEnumerator GetChallenges(WWW www)
    {
        yield return www;
        Debug.Log(www.text);
        foreach (Challenge ch in challenges)
        {
            for (int i = 0; i < www.text.Length; i++)
            {
                if (ch.Id == www.text[i])
                {
                    Debug.Log(www.text[i]);
                }
            }
        }
    }
}
