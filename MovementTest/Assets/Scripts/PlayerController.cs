using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerController : MonoBehaviour {

    private Rigidbody2D rb;
    private Collider2D cldr;
    private int direction; // The rotational direction of the player in degrees
    private int curRow = 0, curCol = 0; // Current position of the player
    private bool canMove;

    public Text victoryText; // Text to display the victory message in
    public Collider2D victoryCollider; // The Collider2D of the victory object
    public float moveScale;// .64 32px^2 player model
    public bool debug;// Determines if information should be printed to the debug log
    public Grid grid;// The grid the player will move across

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
        cldr = GetComponent<Collider2D>();
        canMove = true;
	}

    // Moves the player forward 1 square
    public void Forward()
    {
        if (!canMove) return; // return if unable to move

        // Check if movement would put player out of bounds
        // Move if it wouldn't
        // Update position
        switch( direction)
        {
            /*
            case 0: // Move north
                if (curRow >= grid.rows - 1) break;
                rb.MovePosition((Vector2)transform.position + new Vector2(0, moveScale));
                curRow++;
                break;
            case 90: // Move east
                if (curCol >= grid.cols - 1) break;
                rb.MovePosition((Vector2)transform.position + new Vector2(moveScale, 0));
                curCol++;
                break;
            case 180: // Move south
                if (curRow < 1) break;
                rb.MovePosition((Vector2)transform.position + new Vector2(0, -moveScale));
                curRow--;
                break;
            case 270: // Move west
                if (curCol < 1) break;
                rb.MovePosition((Vector2)transform.position + new Vector2(-moveScale, 0));
                curCol--;
                break;
            */
            case 0: // Move north
                if (!grid.CanMove(curRow+1, curCol)) break;
                rb.MovePosition((Vector2)transform.position + new Vector2(0, moveScale));
                curRow++;
                break;
            case 90: // Move east
                if (!grid.CanMove(curRow, curCol+1)) break;
                rb.MovePosition((Vector2)transform.position + new Vector2(moveScale, 0));
                curCol++;
                break;
            case 180: // Move south
                if (!grid.CanMove(curRow-1, curCol)) break;
                rb.MovePosition((Vector2)transform.position + new Vector2(0, -moveScale));
                curRow--;
                break;
            case 270: // Move west
                if (!grid.CanMove(curRow, curCol-1)) break;
                rb.MovePosition((Vector2)transform.position + new Vector2(-moveScale, 0));
                curCol--;
                break;
        }
        if (debug)
        {
            //Debug.Log("Currrent Position: (" + curCol + ", " + curRow + ")");
            Debug.Log("curRow: " + curRow + "\n" +
                "curCol: " + curCol + "\n" +
                "gridRows: " + grid.rows + "\n" +
                "gridCols: " + grid.cols);
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

	
	// Update is called once per frame
	void Update () {
        if (cldr.IsTouching(victoryCollider))
        {
            victoryText.text = "Victory!";
            canMove = false;
        }
	}
}
