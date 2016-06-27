using UnityEngine;
using System.Collections;

public class ConveyerBelt : MonoBehaviour {

	public GameObject conveyerPrefab; // The conveyer prefab to build the conveyer belt

	// Use this for initialization
	void Start () {
		InvokeRepeating ("NextConveyer", 0f, 1.45f);
	}

	private void NextConveyer(){
		GameObject go;
		go = (GameObject)Instantiate (conveyerPrefab, new Vector3(6, 4, 0), Quaternion.identity);
		go.transform.parent = transform;
	}
}
