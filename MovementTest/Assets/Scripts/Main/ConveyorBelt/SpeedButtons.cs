using UnityEngine;
using System.Collections;

public class SpeedButtons : MonoBehaviour {

    public float incrementScale;

    private GameObject conveyorBelt;

	// Use this for initialization
	void Start () {
        conveyorBelt = this.gameObject.transform.parent.gameObject;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonUp(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit2D[] hits = Physics2D.RaycastAll(ray.origin, ray.direction, Mathf.Infinity);
            foreach (RaycastHit2D hit in hits)
            {
                if (hit.collider != null && hit.collider.gameObject.tag == "Faster")
                {
                    Debug.Log("Increasing the conveyor speed");
                    conveyorBelt.GetComponent<ConveyorBelt>().ChangeScale(incrementScale);
                    return;
                } else if(hit.collider != null && hit.collider.gameObject.tag == "Slower")
                {
                    Debug.Log("Decreasing the conveyor speed");
                    conveyorBelt.GetComponent<ConveyorBelt>().ChangeScale(-incrementScale);
                    return;
                }
            }
        }
    }

}
