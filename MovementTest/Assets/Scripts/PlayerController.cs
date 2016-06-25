using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerController : MonoBehaviour {

    private Rigidbody2D rb;
    private Collider2D cldr;
    private int direction; // The rotational direction of the player in degrees
    private int curRow = 0, curCol = 0; // Current position of the player
    private bool canMove;
    private bool hasCollided;

    public static Text victoryText; // Text to display the victory message in
    public static Collider2D victoryCollider; // The Collider2D of the victory object
    public static float moveScale = .64f;// .64 64px^2 player model
    public bool debug;// Determines if information should be printed to the debug log
	public static float scale; // The scale the game is played in for movemement reference
	public float maxMovementSpeed; // The max speed the player moves at
	public float minMovementSpeed; // The min speed the player moves at


	// Use this for initialization
	void Start () {
		Debug.Log ("Start was called!");
        rb = GetComponent<Rigidbody2D>();
        cldr = GetComponent<Collider2D>();
        canMove = true;
		gameObject.SetActive (true);
	}

    // Moves the player forward 1 square
    public void Forward()
    {
        if (!canMove) return; // return if unable to move

		gameObject.SetActive (true);
		Debug.Log ("This is active? " + gameObject.activeInHierarchy + rb + cldr);

        // Attempt to move the player in the direction it is facing
        switch (direction)
        {
            case 0: // Move north
				StartCoroutine(SmoothMove(1, 0, moveScale*scale));
                break;
            case 90: // Move east
				StartCoroutine(SmoothMove(0, 1, moveScale*scale));
                break;
            case 180: // Move south
				StartCoroutine(SmoothMove(-1, 0, moveScale*scale));
                break;
            case 270: // Move west
				StartCoroutine(SmoothMove(0, -1,  moveScale*scale));
                break;
        }

        if (debug)
        {
            Debug.Log("curRow: " + curRow + "\n" +
                "curCol: " + curCol + "\n");
        }
    }

    // Rotates the player left 90 Degrees
    public void Left()
    {
        direction = ((direction - 90)+360) % 360;// +360 to keep degrees positive
        if (debug)
            Debug.Log("direction: " + direction);
        transform.Rotate(Vector3.forward * 90);
    }

    // Rotates teh player right 90 degrees
    public void Right()
    {
        direction = (direction + 90) % 360;
        if (debug)
            Debug.Log("direction: " + direction);
        transform.Rotate(Vector3.forward * -90);
    }

    // Attempts to move the player in the requested direction
    // If that space is occupied by a non-passable block, then the player
    // bounce off of the spot and mvoe nowhere
    // rowMove and colMove represent the scale of the movement
    //      1, 0, or -1 for moving by one block
    //      other float values for moving back on recursive call in case of collision
    IEnumerator SmoothMove(float rowMove, float colMove, float distance)
    {
        canMove = false;// Prevent player from beginning another move while they are moving
        float remDist = distance; // The remaining distance to move the player
        float thisMove; // Holds how much the player will move each frame
        // Player begins moving slow, and picks up speed until they reach their final position
        while( remDist > 0)
        {
            thisMove = Mathf.Round((distance - remDist) * 100f) / 100f; // Round
            thisMove = Mathf.Max(thisMove, .01f); // Max is used to allow the movement to start
			thisMove = Mathf.Min(remDist, thisMove, maxMovementSpeed); // prevent from moving past the block or too fast

            RaycastHit2D hit = Physics2D.Raycast(transform.position, new Vector2(colMove, rowMove), thisMove);
            Debug.Log(hit.collider.gameObject.tag + ", " + hasCollided);
            if( hit.collider != null && hit.collider.gameObject.tag == "Blocking" && !hasCollided)
            {
                hasCollided = true;
                if( debug )
                    Debug.Log("Collision Detected!");
                StartCoroutine(SmoothMove(-rowMove, -colMove, distance-remDist)); // Move in reverse direction until at original position
                
                yield break; // Leave coroutine as to not finish forward movement
            }
            
            Vector2 move = new Vector2(colMove * thisMove, rowMove * thisMove);
            rb.MovePosition((Vector2)transform.position + move);
            remDist -= thisMove;

			if (debug)
				Debug.Log ("Player moved " + thisMove + " units, remaining: " + remDist);

            yield return new WaitForFixedUpdate(); // Wait for Fixed Update to assure MovePosition functions correctly
            
        }
        if( debug && remDist != 0)
        {
            Debug.Log("Imperfect movement detected: Ramaining Distance of " + remDist + " was not moved!");
        }
        curRow += (int)rowMove;
        curCol += (int)colMove;
        canMove = true; // allow the player to move again
        hasCollided = false; // Resolve any collisions
    }

	public void ButtonTest(){
		Debug.Log ("Button pressed successfully!");
	}

	
	// Update is called once per frame
	void Update () {
        if (cldr.IsTouching(victoryCollider))
        {
            victoryText.text = " Victory!";
            canMove = false;
        }
	}
}
