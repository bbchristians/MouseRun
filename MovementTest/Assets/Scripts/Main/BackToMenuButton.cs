using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class BackToMenuButton : MonoBehaviour {

	public void BackToMainMenu(){
		SceneManager.LoadScene ("Title");
	}

}
