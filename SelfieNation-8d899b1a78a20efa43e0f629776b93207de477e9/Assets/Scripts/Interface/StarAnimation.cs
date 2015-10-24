using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class StarAnimation : MonoBehaviour {

    public GameObject[] stars;
    public StarAnimation st;
    public GameObject tigerLogo;
    AudioSource audios;
    Animation ani;
	// Use this for initialization
	void Start () {
        ani = tigerLogo.GetComponent<Animation>();
        audios = GetComponent<AudioSource>();
 

        st = GetComponent<StarAnimation>();
	}

    void OnEnable()
    {
        Debug.Log("hi");
        StartCoroutine("WaitForChallenges");
        //StartCoroutine("AnimationStars");
    }
	
	// Update is called once per frame
	void Update () {

	}

    IEnumerator WaitForChallenges()
    {
        yield return new WaitForSeconds(0);
        StartCoroutine("AnimationStars");
    }

    IEnumerator AnimationStars()
    {
        tigerLogo.SetActive(true);
        yield return new WaitForSeconds(0.3f);
        for (int i = 0; i < stars.Length; i++)
        {
            yield return new WaitForSeconds(0.25f);
            stars[i].SetActive(true);
            audios.Play();
        }
        st.enabled = false;
    }
    
}
