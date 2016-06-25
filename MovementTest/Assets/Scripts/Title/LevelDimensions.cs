using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LevelDimensions : MonoBehaviour {

	private Text thisText;
	private static int dimensions;

	public int maxDimensions;
	public int minDimensions;

	public Button incButton;
	public Button decButton;

	// Use this for initialization
	void Start () {
		thisText = GetComponent<Text> ();
		dimensions = 5;
	}

	public void IncrementDimensions(){
		if (dimensions >= maxDimensions) {
			return;
		}
		dimensions++;
		thisText.text = "" + dimensions;
		decButton.interactable = true;
		if (dimensions >= maxDimensions) {
			incButton.interactable = false;
		}
	}

	public void DecrementDimensions(){
		if (dimensions <= minDimensions) {
			return;
		}
		dimensions--;
		thisText.text = "" + dimensions;
		incButton.interactable = true;
		if (dimensions <= minDimensions) {
			decButton.interactable = false;
		}
	}

	public static int GetDimensions(){
		return dimensions;
	}
}
