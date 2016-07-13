using UnityEngine;
using System.Collections;

public class FunctionBar : MonoBehaviour {

    public float movementSpeed; // The speed the bar moves downwards at

    private Rigidbody2D rb;
    private float oneBlockOfMovement = .82f;

    private int moves = 0;

    // Tells the bar to move downwards
    void Start () {
        rb = GetComponent<Rigidbody2D>();
    }

    public void moveDownOne()
    {
        StartCoroutine(SmoothMoveDown(oneBlockOfMovement));
    }

    // Moves the FunctionBar downwards
    IEnumerator SmoothMoveDown(float distance)
    {
        float remDist = distance; // The remaining distance to move the player
        float thisMove = movementSpeed; // Holds how much the player will move each frame
                                        // Player begins moving slow, and picks up speed until they reach their final position
        while (remDist > 0)
        {
            if (gameObject == null) yield return null;

            Vector2 move = new Vector2(0, -thisMove);
            rb.MovePosition((Vector2)transform.position + move);
            remDist -= thisMove;

            yield return new WaitForFixedUpdate(); // Wait for Fixed Update to assure MovePosition functions correctly

        }
        if( ++moves >= 4)
        {
            Destroy(gameObject);
            Destroy(this);
        }
            
    }
}
