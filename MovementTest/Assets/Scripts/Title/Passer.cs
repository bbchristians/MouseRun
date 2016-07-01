using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Passer : MonoBehaviour {

	public static int levelDim;
	public static Difficulty levelDiff = Difficulty.Normal;
	public static bool conveyer;

	public Button easyButton;
	public Button normalButton;
	public Button hardButton;
	public Button conveyerButton;

	public Color selected;
	public Color notSelected;

    private bool hasLoaded;

	public enum Difficulty{ Easy, Normal, Hard };

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

	public void ToggleConveyer(){
		conveyer = !conveyer;
		if( conveyer )
			conveyerButton.GetComponentInChildren<Text>().text = "Conveyer-Belt";
		else 
			conveyerButton.GetComponentInChildren<Text>().text = "Buttons";

	}

    void Update()
    {
        hasLoaded = hasLoaded || SceneManager.GetActiveScene().name == "Main";
        if (hasLoaded && SceneManager.GetActiveScene().name == "Title") Destroy(this.gameObject);
    }

}
