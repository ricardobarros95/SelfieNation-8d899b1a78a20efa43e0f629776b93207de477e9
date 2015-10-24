using UnityEngine;
using System.Collections;

public class TMP_disable_me : MonoBehaviour {

	// Use this for initialization
	void Start () {

        this.gameObject.SetActive(false);

	}

    void OnBecameVisible()
    {
        this.gameObject.SetActive(false);
    }
	

}
