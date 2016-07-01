using UnityEngine;
using System.Collections;

public class Conveyer : MonoBehaviour {

	public static float speedScale = 1;
	public static float movementSpeed = .006f;
	public GameObject[] movementBlockPrefabs;
    public int maxPityTimer; // The number of blocks that can be generated without seeing a block of each type

	private Rigidbody2D rb;
    private static int forwardPity;
    private static int leftPity;
    private static int rightPity;


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
		Destroy (gameObject);
	}

    // Places the movement blocks on the conveyer randomly
	private void GenerateMovementBlocks(){
		int numBlocks = (int)(Mathf.Pow(Random.Range (1, 64), .25f))-1;
		int randIndex;
		GameObject go;
		for (int i = 0; i < numBlocks; i++) {
			randIndex = Random.Range (0, movementBlockPrefabs.Length);
            // Make change if pity has been reached
            randIndex = (forwardPity >= maxPityTimer) ? 0 : randIndex;
            randIndex = (leftPity >= maxPityTimer) ? 2 : randIndex;
            randIndex = (rightPity >= maxPityTimer) ? 3 : randIndex;
            // Calculate new pity timers
            switch ( randIndex)
            {
                case 0: case 1: forwardPity = 0; rightPity++; leftPity++; break;
                case 2:         forwardPity++; rightPity++; leftPity = 0; break;
                case 3:         forwardPity++; rightPity = 0; leftPity++; break;

            }
            
            go = (GameObject)Instantiate (movementBlockPrefabs [randIndex], new Vector3 (6, 4, 0), Quaternion.identity);
			go.transform.parent = transform; // Make the new object a child of current conveyer

            Vector3 randMove = new Vector3(Random.value * 2 - 1, 0, -1);
            go.transform.position += randMove;// Move it so it looks better
        }
	}
		
}
