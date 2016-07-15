using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ProgressionManager : MonoBehaviour {
    
    private int level;  // Keeps track of the level the player is on
                        // There are 20 levels and the difficulty you start on will determine the level you start on
    private int levelsCompleted; // The levels the player has completed

    // Use this for initialization
    void Start () {
        level = 1;
	}

    // Plays the game on easy mode
    public void PlayEasy()
    {
        level = 1;
        DontDestroyOnLoad(this);
        SceneManager.LoadScene("Main");
    }

    // Plays the game on normal mode
    public void PlayNormal()
    {
        level = 6;
        DontDestroyOnLoad(this);
        SceneManager.LoadScene("Main");
    }

    // Playes the game on hard mode
    public void PlayHard()
    {
        level = 11;
        DontDestroyOnLoad(this);
        SceneManager.LoadScene("Main");
    }

    public int GetLevel()
    {
        return level;
    }

    public void LevelUp()
    {
        levelsCompleted++;
        if( levelsCompleted >= 10)
        {
            // Player wins
            SceneManager.LoadScene("Title");
        }
        level++;
        SceneManager.LoadScene("Main");
    }
}
