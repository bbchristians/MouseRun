using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Passer : MonoBehaviour {

	public static int levelDim; // The dimensions of the level to be generated
	public static Difficulty levelDiff = Difficulty.Normal; // The defficulty of the level, normal is the default
	public static bool conveyor; // Determines if the level will be conveyor or not

    // The buttons of the title menu to be linked
	public Button easyButton;
	public Button normalButton;
	public Button hardButton;
	public Button conveyorButton;

    // The colors of the buttons to signify selected/not selected
	public Color selected;
	public Color notSelected;

    private bool hasLoaded; // Determines if a level has been loaded or not
                            // This is used to determine if the Passer GameObject should be destroyed or not

	public enum Difficulty{ Easy, Normal, Hard }; // And enum that represents the level difficulty

    // Prevents the Passer from being destroyed so that it can transfer information to the Main scene
	void Awake() {
		DontDestroyOnLoad(this);
	}

    // Sets the difficulty of the level to be generated to easy
	public void Easy(){
		levelDiff = Difficulty.Easy;
		easyButton.GetComponent<Image>().color = selected;
		normalButton.GetComponent<Image>().color = notSelected;
		hardButton.GetComponent<Image>().color = notSelected;
	}

    // Sets the difficulty of the level to be generated to normal
    public void Normal(){
		levelDiff = Difficulty.Normal;
		easyButton.GetComponent<Image>().color = notSelected;
		normalButton.GetComponent<Image>().color = selected;
		hardButton.GetComponent<Image>().color = notSelected;
	}

    // Sets the difficulty of the level to be generated to hard
    public void Hard(){
		levelDiff = Difficulty.Hard;
		easyButton.GetComponent<Image>().color = notSelected;
		normalButton.GetComponent<Image>().color = notSelected;
		hardButton.GetComponent<Image>().color = selected;
	}

    // Starts the Passer with the normal difficulty selected
	void Start(){
		easyButton.GetComponent<Image>().color = notSelected;
		normalButton.GetComponent<Image>().color = selected;
		hardButton.GetComponent<Image>().color = notSelected;
	}

    // Toggles the conveyor on or off
	public void ToggleConveyor(){
		conveyor = !conveyor;
        conveyorButton.GetComponentInChildren<Text>().text = ( conveyor )? "Conveyor-Belt" : "Buttons";

	}

    // Determines if the game has been loaded or not for use in destroying duplicate Passer Instances
    void Update()
    {
        hasLoaded = hasLoaded || SceneManager.GetActiveScene().name == "Main";
        if (hasLoaded && SceneManager.GetActiveScene().name == "Title") Destroy(this.gameObject);
    }

}
