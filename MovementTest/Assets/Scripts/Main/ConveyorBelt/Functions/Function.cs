using UnityEngine;
using System.Collections;

public class Function : MonoBehaviour {

    public float waitInbetweenMovements;

    public GameObject functionBar; // Function bar prefab
    public GameObject instantiatedBar; // The function bar this function will be moving

    // Use this for initialization
    void Start()
    {
    }
    
    // Starts running the function
    public void CallFunction()
    {
        // Destroy any old instantiated function bars in case of recursion
        if (instantiatedBar != null)
        {
            Destroy(instantiatedBar.gameObject);
            Destroy(instantiatedBar);
        }

        // Instantiate function bar
        GameObject bar = (GameObject)Instantiate(functionBar, new Vector3(), Quaternion.identity);
        bar.transform.parent = transform;
        bar.transform.localPosition = new Vector3(0, -.4f, 0);

        instantiatedBar = bar;

        // Run the function
        StartCoroutine(Run(instantiatedBar));
    }

    // Runs the function based off the movement blocks that are the children of the children of the function
    IEnumerator Run(GameObject bar)
    {
        foreach (Transform t in transform)
        {
            //MovementBlock.canMove = false; // prevent other movement blocks from being clicked while the function in running

            // Start moving the function bar
            bar.GetComponent<FunctionBar>().moveDownOne();

            // Debug.Log("Activating button with code: " + t.gameObject.GetComponent<Holder>().getMovementBlockCode());

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

            MovementBlock.canMove = true;
        }

        
    }

}
