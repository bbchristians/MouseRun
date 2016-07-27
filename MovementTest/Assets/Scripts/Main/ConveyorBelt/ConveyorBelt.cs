using UnityEngine;

public class ConveyorBelt : MonoBehaviour {

	public GameObject conveyorPrefab; // The conveyor prefab to build the conveyor belt
    public bool on; // Determines if the conveyorbelt is on

    private float speedScale;

	// Starts the conveyor if it is turned on
	void Start () {
        speedScale = 1;
        if( on )
		    InvokeRepeating ("NextConveyor", 0f, 1.45f / speedScale);
	}

    // Changes the speed of the conveyer by the given scale
    public void ChangeScale(float increment)
    {
        if (Conveyor.speedScale != 0)
        {
            speedScale += increment;
            CancelInvoke();
            InvokeRepeating("NextConveyor", 0f, 1.45f / speedScale);
        }

        Conveyor.speedScale = Mathf.Max( Conveyor.speedScale+increment, 0 );
    }

    // Generates a new conveyor GameObject which automatically mvoes down the screen and generates movement blocks
	private void NextConveyor(){
        GameObject go = (GameObject)Instantiate (conveyorPrefab, new Vector3(6, 3.5f, 0), Quaternion.identity);
		go.transform.parent = transform;
	}
}
