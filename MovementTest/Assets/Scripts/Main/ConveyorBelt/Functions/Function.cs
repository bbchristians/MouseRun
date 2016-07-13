using UnityEngine;
using System.Collections;

public class Function : MonoBehaviour {

    public float waitInbetweenMovements;

    // Use this for initialization
    void Start()
    {
    }
    
    // Starts running the function
    public void CallFunction()
    {
        StartCoroutine(Run());
    }

    // Runs the function based off the movement blocks that are the children of the children of the function
    IEnumerator Run()
    {
        foreach (Transform t in transform)
        {
            MovementBlock.canMove = false; // prevent other movement blocks from being clicked while the function in running

            Debug.Log("Activating button with code: " + t.gameObject.GetComponent<Holder>().getMovementBlockCode());

            t.gameObject.GetComponent<Holder>().ActivateButton();

            //Determine appropriate wait time
            switch(t.gameObject.GetComponent<Holder>().getMovementBlockCode())
            {
                case '1': case '2': // Function call
                    yield return new WaitForSeconds(waitInbetweenMovements*4);
                    break;
                default:
                    yield return new WaitForSeconds(waitInbetweenMovements);
                    break;
            }
        }

        MovementBlock.canMove = true;
    }

}
