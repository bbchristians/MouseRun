using UnityEngine;

public class DontDestroyCanvas : MonoBehaviour {

	// Use this for initialization
	void Awake () {
        DontDestroyOnLoad(this);
	}
}
