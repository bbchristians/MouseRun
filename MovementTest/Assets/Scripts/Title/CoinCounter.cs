using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CoinCounter : MonoBehaviour {

    public static int count;

    // Use this for initialization
    void Start()
    {
        DontDestroyOnLoad(transform.gameObject);
        DontDestroyOnLoad(GameObject.Find("GoldCountCanvas"));
        count = 0;
    }

    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
        DontDestroyOnLoad(GameObject.Find("GoldCountCanvas"));
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
    }
	
	
}
