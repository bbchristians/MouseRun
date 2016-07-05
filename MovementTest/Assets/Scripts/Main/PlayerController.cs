using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.ImageEffects;
using System.Collections;

public class PlayerController : MonoBehaviour {

    private Rigidbody2D rb; // The Rigidbody2D of the player
    private Collider2D cldr; // The Collider2D of the player
    private int direction; // The rotational direction of the player in degrees
    private bool canMove; // Determines if the player can move
    private bool hasCollided; // Determines if the player has collided with another object and is therefor moving backwards

    public static Text victoryText; // Text to display the victory message in
    public static Collider2D victoryCollider; // The Collider2D of the victory object
    public static float moveScale = .64f;// .64 64px^2 player model
    public static GameObject door; // The door that may be opened
    public bool debug;// Determines if information should be printed to the debug log
	public static float scale; // The scale the game is played in for movemement reference
	public float maxMovementSpeed; // The max speed the player moves at
	public float minMovementSpeed; // The min speed the player moves at
	public Button backToMenuButton; // Button to appear on victory to allow user back to menu


	// Initializes the player, the Camera and the back-to-menu button which interact with the player
	void Start () {
        rb = GetComponent<Rigidbody2D>();
        cldr = GetComponent<Collider2D>();
        canMove = true;
		gameObject.SetActive (true);
		Camera.main.GetComponent<BlurOptimized> ().enabled = false; // Blur used in win condition
		backToMenuButton.gameObject.SetActive (false); // Button used to return to menu
	}

    // Moves the player forward 1 square
    public void Forward()
    {
        if (!canMove) return; // return if unable to move

		gameObject.SetActive (true);

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

            RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, new Vector2(colMove, rowMove), thisMove);
			foreach (RaycastHit2D hit in hits) {
                if( debug ) Debug.Log(hit.collider.gameObject.tag + ", " + hasCollided);
                
                if(hit.collider.gameObject.tag == "Blocking" && !hasCollided)
                {
                    hasCollided = true; // Prevents multiple collisions fro happening
                    if (debug) Debug.Log("Collision Detected!");

                    StartCoroutine(SmoothMove(-rowMove, -colMove, distance - remDist)); // Move in reverse direction until at original position

                    yield break; // Leave coroutine as to not finish forward movement
                }
                else if (hit.collider.gameObject.tag == "Button")
                {
                        door.GetComponent<DoorObstacle>().Open();
                }
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
        canMove = true; // allow the player to move again
        hasCollided = false; // Resolve any collisions
    }

    // Checks for collision in victory collider
	void Update () {
        if (cldr.IsTouching(victoryCollider))
        {
            victoryText.text = " Victory!";
            canMove = false;

			backToMenuButton.gameObject.SetActive(true);
			Camera.main.GetComponent<BlurOptimized> ().enabled = true;
        }
	}
}
