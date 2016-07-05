using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LevelDimensions : MonoBehaviour {

	private Text thisText; // The text that represents the currently selected size of the game board
	private static int dimensions; // The integer representation of the game board's dimensions

	public int maxDimensions; // The maximum dimensions of the board
	public int minDimensions; // The minimum dimensions of the board

	public Button incButton; // The button that increases the board dimensions
	public Button decButton; // The button that decreases the board dimensions

	// Initializes the Title screen
	void Start () {
		thisText = GetComponent<Text> ();
		dimensions = 5;
	}

    // Increases the dimensions by 1 if it is not already the max
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

    // Decreases the dimensions by 1 if it is not already the minimum
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

    // Returns the dimensions of the game board
	public static int GetDimensions(){
		return dimensions;
	}
}
