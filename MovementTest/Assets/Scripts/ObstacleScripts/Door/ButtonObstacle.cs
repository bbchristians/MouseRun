using UnityEngine;
using System.Collections;

public class ButtonObstacle : MonoBehaviour {

    public DoorObstacle door; // The door associated with the button
    public GameObject player; // The player who will walk over the button

    private Collider2D cldr;
    private Collider2D playerCldr;

	// Use this for initialization
	void Start () {
        cldr = GetComponent<Collider2D>();
        playerCldr = playerCldr.GetComponent<Collider2D>();
	}
	
	// Update is called once per frame
	void Update () {
        if (cldr.IsTouching(playerCldr))
        {
            door.Open();
        }
    }
}
