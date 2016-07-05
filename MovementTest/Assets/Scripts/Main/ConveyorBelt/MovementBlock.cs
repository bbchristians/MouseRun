using UnityEngine;
using System.Collections;

public class MovementBlock : MonoBehaviour {

	public char movementCode; // l, f, r ONLY
    public float movementSpeed;

    public static PlayerController playerController;

	private bool clicked;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //StartCoroutine(SmoothMoveDown(6f));
    }

    IEnumerator SmoothMoveDown(float distance)
    {
        float remDist = distance; // The remaining distance to move the player
        float thisMove = movementSpeed; // Holds how much the player will move each frame
                                        // Player begins moving slow, and picks up speed until they reach their final position
        while (remDist > 0)
        {
            Vector2 move = new Vector2(0, -thisMove);
            rb.MovePosition((Vector2)transform.position + move);
            remDist -= thisMove;

            yield return new WaitForFixedUpdate(); // Wait for Fixed Update to assure MovePosition functions correctly

        }
        Destroy(gameObject);
    }

    void OnMouseDown()
    {
        //Move Player
        switch (movementCode)
        {
            case 'l': // LEFT
                playerController.Left();
                break;

            case 'r': // RIGHT
                playerController.Right();
                break;

            case 'f': // FORWARD
                playerController.Forward();
                break;

            default:
                Debug.Log("Invalid movementCode in " + this);
                break;
        }
        Destroy(gameObject);
    }
}
