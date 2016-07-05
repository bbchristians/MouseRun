using UnityEngine;

public class ButtonObstacle : MonoBehaviour {

    public DoorObstacle door; // The door associated with the button
    public static GameObject player; // The player who will walk over the button

    private Collider2D cldr; // The collider of this object
    // private Collider2D playerCldr; // The collider of the Player, who

	// Use this for initialization
	void Start () {
        cldr = GetComponent<Collider2D>();
	}
	
	// Update is called once per frame
	//void Update () {
    //    if ( playerCldr == null && player != null ) playerCldr = player.GetComponent<Collider2D>();
    //}

    // When the collider is triggered, this opens the door
    void OnTriggerEnter(Collider coll)
    {
        Debug.Log("");
        if( coll.gameObject.tag == "Player")
        {
            door.GetComponent<DoorObstacle>().Open();
            Debug.Log("Door should be opened");
        }
    }
}
