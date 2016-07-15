using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayButton : MonoBehaviour {

    // Starts the game
	public void StartGame(){
		Passer.levelDim = LevelDimensions.GetDimensions();
		SceneManager.LoadScene ("Main");
	}
}
