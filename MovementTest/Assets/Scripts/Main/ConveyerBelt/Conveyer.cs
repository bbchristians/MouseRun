using UnityEngine;
using System.Collections;

public class Conveyer : MonoBehaviour {

	public static float speedScale = 1;
	public static float movementSpeed = .006f;
	public GameObject[] movementBlockPrefabs;

	private Rigidbody2D rb;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D> ();
		GenerateMovementBlocks ();
		StartCoroutine (SmoothMoveDown (6f));
	}

	// Moves the conveyerbelt downwards
	IEnumerator SmoothMoveDown( float distance)
	{
		float remDist = distance; // The remaining distance to move the player
		float thisMove = movementSpeed; // Holds how much the player will move each frame
		// Player begins moving slow, and picks up speed until they reach their final position
		while( remDist > 0)
		{
			Vector2 move = new Vector2(0, -thisMove);
			rb.MovePosition((Vector2)transform.position + move);
			remDist -= thisMove;

			yield return new WaitForFixedUpdate(); // Wait for Fixed Update to assure MovePosition functions correctly

		}
		Destroy (this);
	}

	private void GenerateMovementBlocks(){
		int numBlocks = (int)(Mathf.Sqrt(Random.Range (1, 16)))-1;
		int randIndex;
		GameObject go;
		for (int i = 0; i < numBlocks; i++) {
			randIndex = Random.Range (0, movementBlockPrefabs.Length);
			go = (GameObject)Instantiate (movementBlockPrefabs [randIndex], new Vector3 (6, 4, 0), Quaternion.identity);
			go.transform.parent = transform; // Make the new object a child of current conveyer
			// Other movement here
		}
	}
		
}
