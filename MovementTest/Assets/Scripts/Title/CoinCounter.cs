using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CoinCounter : MonoBehaviour {

    public static int count;

    // Use this for initialization
    void Start()
    {
    }

    // Adds 5 coins to the coin counter
    public static void AddCoin()
    {
        count += 5;
        GameObject.Find("/GoldCountCanvas/GoldCount/Number").GetComponent<Text>().text = "" + count;
    }

    void Update()
    {
        GameObject.Find("/GoldCountCanvas/GoldCount/Number").GetComponent<Text>().text = "" + count;
        if (Input.GetKey("m"))
            AddCoin();
    }
	
	
}
