using UnityEngine;
using UnityEngine.SceneManagement;

public class BackToMenuButton : MonoBehaviour {

	public void BackToMainMenu(){
        SceneManager.LoadScene ("Title");
	}

    public void BackToMenuAbsolute()
    {
        GameObject pManager = GameObject.Find("ProgressionManager");
        if (pManager != null) pManager.GetComponent<ProgressionManager>().ResetAndDestroy();

        SceneManager.LoadScene("Title");
    }

}
