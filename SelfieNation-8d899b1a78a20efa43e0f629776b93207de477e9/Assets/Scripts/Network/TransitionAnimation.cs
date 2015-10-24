using UnityEngine;
using System.Collections;

public class TransitionAnimation : MonoBehaviour {

    private Vector3 origin;
    public GameObject originObject;
    private bool buttonClicked = false;
	// Use this for initialization
	void Start () 
    {
        origin = originObject.transform.localPosition;
	}
	
	// Update is called once per frame
	void Update () 
    {
       Transition();

	}

    public void Transition()
    {
        
        if (buttonClicked && gameObject.transform.localPosition != origin)
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, origin, 3.5f* Time.deltaTime);
            float d = Vector3.Distance(transform.localPosition, origin);
            if (d < 1) transform.localPosition = origin;
        }

    }
    public void SetButtonClicked(bool value)
    {
        buttonClicked = value;
    }
    public void ReversePosition()
    {
            transform.localPosition = new Vector3(300, 0, 0);
    }
}
