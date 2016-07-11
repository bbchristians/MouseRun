using UnityEngine;
using System.Collections;

public class Function1 : MonoBehaviour {

    private static Transform f1Transform;

    // Use this for initialization
    void Start()
    {
        f1Transform = this.transform;
    }

    // Runs the Function
    public static void RunFuntion()
    {
        //StartCoroutine(Run());
    }

    public static IEnumerator Run()
    {
        foreach (Transform t in f1Transform)
        {
            Debug.Log("Activating button with code: " + t.gameObject.GetComponent<Holder>().getMovementBlockCode());
            t.gameObject.GetComponent<Holder>().ActivateButton();
            Debug.Log("Got here");
            yield return new WaitForSeconds(1);
            Debug.Log("Did this part");
        }
        Debug.Log("Leaving foreach loop");
    }

}
