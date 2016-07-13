using UnityEngine;

public class ButtonObstacle : MonoBehaviour {

    public DoorObstacle door; // The door associated with the button
    public static GameObject player; // The player who will walk over the button

	// Use this for initialization
	void Start () {
	}

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
