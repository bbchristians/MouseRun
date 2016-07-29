using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ProgressionManager : MonoBehaviour {
    
    private int level;  // Keeps track of the level the player is on
                        // There are 20 levels and the difficulty you start on will determine the level you start on
    private int levelsCompleted; // The levels the player has completed

    // Use this for initialization
    void Start () {
        DontDestroyOnLoad(this.gameObject);
        level = 1;
	}

    // Plays the game on easy mode
    public void PlayEasy()
    {
        level = 1;
        levelsCompleted = 0;
        Debug.Log("level: " + level + ", levelsCompleted: " + levelsCompleted);
        Passer.conveyor = false;
        GameObject.Find("Passer").GetComponent<Passer>().Easy();
        DontDestroyOnLoad(this);
        SceneManager.LoadScene("Main");
    }

    // Plays the game on normal mode
    public void PlayNormal()
    {
        level = 6;
        levelsCompleted = 0;
        Debug.Log("level: " + level + ", levelsCompleted: " + levelsCompleted);
        Passer.conveyor = false;
        GameObject.Find("Passer").GetComponent<Passer>().Normal();
        DontDestroyOnLoad(this);
        SceneManager.LoadScene("Main");
    }

    // Playes the game on hard mode
    public void PlayHard()
    {
        level = 11;
        levelsCompleted = 0;
        Passer.conveyor = true;
        GameObject.Find("Passer").GetComponent<Passer>().Hard();
        DontDestroyOnLoad(this);
        SceneManager.LoadScene("Main");
    }

    public int GetLevel()
    {
        return level;
    }

    // Increases the level of the player's progression
    public void LevelUp()
    {
        levelsCompleted++;
        Debug.Log("Levels Completed: " + levelsCompleted + ", Current Level: " + level);
        if( levelsCompleted >= 10)
        {
            // Player wins
            SceneManager.LoadScene("Title");
            ResetAndDestroy();
            return;
        }

        level++;
        if( level > 10)
        {
            Passer.conveyor = true;
        }

        SceneManager.LoadScene("Main");

        // Destroy duplicate persistant objects
    }

    // Resets the Progression Manager
    // To be used when returing back to the main menu
    public void ResetAndDestroy()
    {
        Destroy(this.gameObject);
        levelsCompleted = 0;
        level = 0;
    }

    // Returns the appropriate number of coins for the level
    public int GetNumberOfCoins()
    {
        return ((levelsCompleted/2)*Passer.levelDim/5);
    }

    void Update()
    {
        if( Input.GetKey("n") && SceneManager.GetActiveScene().name == "Main")
        {
            LevelUp();
        }
    }
}
