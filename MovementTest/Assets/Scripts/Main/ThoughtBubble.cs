using UnityEngine;
using System.Collections;

public class ThoughtBubble : MonoBehaviour {

    public GameObject thoughtBubble;
    public float displayTime;

    private bool isDisplayed;
    private GameObject instanceOfBubble;

    // Positions to detect if the player has moved and to destroy the thought bubble if they have
    public static Vector3 startPos;
    public static bool stillMoving;

	// Use this for initialization
	void Start () {
	
	}
	
	// Displays the thought bubble
    public void DisplayBubble()
    {
        if (isDisplayed) return;

        isDisplayed = true;

        StartCoroutine(ShowBubble());
    }

    // Instantiates and displays the bubble for the given time
    IEnumerator ShowBubble()
    {
        instanceOfBubble  = (GameObject)Instantiate(thoughtBubble, new Vector2(), Quaternion.identity);
        instanceOfBubble.transform.position = gameObject.transform.position + new Vector3(.32f, .32f, 0);
        instanceOfBubble.transform.parent = null;

        yield return new WaitForSeconds(displayTime);

        Destroy(instanceOfBubble);
        instanceOfBubble = null;
        isDisplayed = false;
        yield return null;
    }

    // Destroys the bubble if the player moves once
    void LateUpdate()
    {
        if(Vector3.Distance(this.gameObject.transform.position, startPos) > .1 && !stillMoving)
        {
            Destroy(instanceOfBubble);
            instanceOfBubble = null;
            isDisplayed = false;
        }
    }
}
