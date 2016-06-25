using UnityEngine;
using System.Collections;

public class Passer : MonoBehaviour {

	public static int levelDim;

	void Awake() {
		DontDestroyOnLoad(this);
	}
}
