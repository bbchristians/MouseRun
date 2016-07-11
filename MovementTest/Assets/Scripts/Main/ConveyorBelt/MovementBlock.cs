using UnityEngine;
using System.Collections;

public class MovementBlock : MonoBehaviour {

	public char movementCode; // l, f, r, 1, or 2 ONLY
    public float movementSpeed;
    public bool inFunction;

    public static PlayerController playerController;

	private bool clicked;
    private bool dragging;
    private Rigidbody2D rb;

    private Vector3 screenPoint;
    private Vector3 scanPos;
    private Vector3 offset;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
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
                StartCoroutine(Function1.Run());
                return;

            case '2': // Function2
                break;

            default:
                Debug.Log("Invalid movementCode in " + this);
                break;
        }
    }

    // Activate button and destroy it when clicked on
    void OnMouseDown()
    {
        // Save some information in case of dragging
        screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
        offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint( new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));

        dragging = true;

        if (inFunction) return; // Make movement blocks in function unclickable
    }

    // Used when dragging the gameObeject
    void OnMouseDrag()
    {
        Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);

        Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
        transform.position = curPosition;
    }

    // Updates to see if the object has been released when dragging and responds accordingly
    void Update()
    {
        if (!dragging) return;
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
        }
    }
}
