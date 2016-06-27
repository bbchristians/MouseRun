using UnityEngine;
using System.Collections;

public class MovementBlock : MonoBehaviour {

	public char movementCode; // l, f, r ONLY

	private bool clicked;

	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (0) && !clicked) {
			clicked = true;
			//Move Player here
			switch (movementCode) {
				case 'l': // LEFT
					break;

				case 'r': // RIGHT
					break;

				case 'f': // FORWARD
					break;

				default:
					Debug.Log ("Invalid movementCode in " + this);
					break;
			}
			Destroy(this);
		}
	}
}
