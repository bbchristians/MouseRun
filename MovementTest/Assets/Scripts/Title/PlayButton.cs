using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayButton : MonoBehaviour {

	public void StartGame(){
		Passer.levelDim = LevelDimensions.GetDimensions();
		SceneManager.LoadScene ("Main");
	}
}
