using UnityEngine;
using System.Collections;

public class FunctionBar : MonoBehaviour {

    private Vector2 movement = new Vector2(0, -.0001f);
    private int destroy = 0;
    private Rigidbody2D rb;

    // Tells the bar to move downwards
    void Start () {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        rb.MovePosition(movement);
        if (destroy++ >= 500) Destroy(this.gameObject);
    }
}
