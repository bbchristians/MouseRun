using UnityEngine;

public class DoorObstacle : MonoBehaviour {

    private SpriteRenderer sr; // The object's sprite renderer, value assigned in Start()

    public Sprite openedSprite; // Opened door Sprite
    
	// Initializes the DoorObstacle
	void Start () {
        sr = GetComponent<SpriteRenderer>();
	}

    // Opens the door
    public void Open()
    {
        sr.sprite = openedSprite;
        gameObject.tag = "Untagged"; // Remove blocking tag that the player script uses to determine if they can move onto the spot
    }
}
