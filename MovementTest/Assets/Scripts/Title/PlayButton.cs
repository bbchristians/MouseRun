using UnityEngine;
using System.Collections;

public class PlayButton : MonoBehaviour {

	public void StartGame(){
		Passer.levelDim = LevelDimensions.GetDimensions();
		Application.LoadLevel ("Main");
	}
}
