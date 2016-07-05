using UnityEngine;

public class ConveyorBelt : MonoBehaviour {

	public GameObject conveyorPrefab; // The conveyor prefab to build the conveyor belt
    public bool on; // Determines if the conveyorbelt is on

	// Starts the conveyor if it is turned on
	void Start () {
        if( on )
		    InvokeRepeating ("NextConveyor", 0f, 1.45f);
	}

    // Generates a new conveyor GameObject which automatically mvoes down the screen and generates movement blocks
	private void NextConveyor(){
        GameObject go = (GameObject)Instantiate (conveyorPrefab, new Vector3(6, 4, 0), Quaternion.identity);
		go.transform.parent = transform;
	}
}
