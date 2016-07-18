using UnityEngine;
using System.Collections;

public class MovementBlock : MonoBehaviour {

	public char movementCode; // l, f, r, 1, or 2 ONLY
    public bool inFunction; // Determines if the movementblock is locked in a function so it cannot be clicked

    public static PlayerController playerController; // The playercontroller for linking
    
    public static bool canMove; // Determines if the movementblocks can be clicked


    // For use in determining if the block is clicked or dragged
	private bool clicked;
    private bool dragging;

    // Positioning variables for use in dragging and dropping the Blocks
    private Vector3 screenPoint;
    private Vector3 scanPos;
    private Vector3 offset;

    // Function game objects to be run if functional button clicked
    // NOTE: These will be null if movementCode != '1' or '2'
    // private GameObject function1;
    // private GameObject function2;

    void Start()
    {
        // if (movementCode == '1') function1 = GameObject.Find("Function1");
        // if (movementCode == '2') function2 = GameObject.Find("Function2");

    }

    // Runs the button's command
    public void ActivateButton()
    {
        switch (movementCode)
        {
            case 'l': // LEFT
                playerController.Left();
                break;

            case 'r': // RIGHT
                playerController.Right();
                break;

            case 'f': // FORWARD
                playerController.Forward();
                break;

            case '1': // Function1
                GameObject.Find("/ConveyorBelt(Clone)/Function1").GetComponent<Function>().CallFunction();
                return;

            case '2': // Function2
                GameObject.Find("/ConveyorBelt(Clone)/Function2").GetComponent<Function>().CallFunction();
                break;

            default:
                Debug.Log("Invalid movementCode in " + this);
                break;
        }
    }

    // Activate button and destroy it when clicked on
    void OnMouseDown()
    {
        if (!canMove) return;

        // Save some information in case of dragging
        screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
        offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint( new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));

        dragging = true;

        if (inFunction) return; // Make movement blocks in function unclickable
    }

    // Used when dragging the gameObeject
    void OnMouseDrag()
    {
        if (!canMove) return;

        gameObject.transform.parent = null;

        Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);

        Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
        transform.position = curPosition;
    }

    // Updates to see if the object has been released when dragging and responds accordingly
    void Update()
    {
        if (!dragging || !canMove) return;
        if (Input.GetMouseButtonUp(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit2D[] hits = Physics2D.RaycastAll(ray.origin, ray.direction, Mathf.Infinity);
            foreach (RaycastHit2D hit in hits)
            {
                if (hit.collider != null && hit.collider.gameObject.tag == "Holder")
                {
                    hit.collider.gameObject.GetComponent<Holder>().AddMovementBlock(this.gameObject);
                    dragging = false;
                    return;
                }
            }
            
            // Press button as normal if not dragged over holder
            ActivateButton();
            Destroy(this.gameObject);
            Destroy(this);
        }
    }
}
