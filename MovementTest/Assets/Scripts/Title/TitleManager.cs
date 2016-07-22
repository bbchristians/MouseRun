using UnityEngine;
using System.Collections;

public class TitleManager : MonoBehaviour {

    public GameObject CountCounterCanvas;

	// Use this for initialization
	void Start () {
        GameObject coinCountCanvas;
        if (GameObject.Find("/GoldCountCanvas/GoldCount/Number") == null)
        {
            coinCountCanvas = (GameObject)Instantiate(CountCounterCanvas, new Vector3(), Quaternion.identity);
            coinCountCanvas.name = "GoldCountCanvas";
            DontDestroyOnLoad(coinCountCanvas);
        }
            
        
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
