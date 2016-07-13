using UnityEngine;
using System.Collections;

public class Holder : MonoBehaviour {

    private GameObject movementBlock; // The movement block the holder is holding

	// Use this for initialization
	void Start () {
	
	}

    public char getMovementBlockCode()
    {
        if (movementBlock == null) return 'N';
        return movementBlock.GetComponent<MovementBlock>().movementCode;
    }

    // Attaches a movement block to the Holder
    public void AddMovementBlock(GameObject go)
    {
        movementBlock = null; // Delete old movement block
        movementBlock = go;

        movementBlock.GetComponent<MovementBlock>().inFunction = true;

        go.transform.parent = this.transform; // Make this block a parent of the movement block

        go.transform.localPosition = new Vector3(0, 0, 0);
    }

    // Activates the button held by the current Holder
    public void ActivateButton()
    {
        if (movementBlock == null) return;
        movementBlock.GetComponent<MovementBlock>().ActivateButton();
    }
}
