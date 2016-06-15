using UnityEngine;
using Random = UnityEngine.Random;
using System;
using System.Collections;
using System.IO;

public class GameManager : MonoBehaviour {

    public bool debug; // Determines if the game will run in debug mode
    public string debugFilePath; // The file Path leading to the debug file
    public int boardDim; // The dimensions of the board (must be square)

    //Prefab lists
    public GameObject[] basicObstaclePrefabs; // The BasicObstacle prefab list
    public GameObject[] buttonDoorPrefabs; // The button and door obstacle prefabs

    private ArrayList initList; // The List where information generated while loading from a file will be generated
    private Queue initQueue; // The queue where the InitObstacles will be store for initialization

    // Generates the initList from a file given by fileName
    private void ReadFile(string fileName)
    {
        StreamReader reader = new StreamReader(fileName);
        string line;
        string[] parts;

        while( (line = reader.ReadLine()) != null)
        {
            parts = line.Split();
            initList.Add(new InitObstacle(parts[0].ToCharArray()[0], 
                Int32.Parse(parts[1]), Int32.Parse(parts[2])));
        }
    }

    // Uses the InitObstacles from the initList to generate a queue of InitObjects to be 
    // Initialized later
    private void QueueInitObstacles()
    {
        foreach( InitObstacle iob in initList)
        {
            if( !iob.OutOfBounds(boardDim))
            {
                initQueue.Enqueue(iob);
            } else if ( debug )
            {
                Debug.Log(iob + " Not added to initQueue.");
            }
        }
    } 

    // Generates the grid by populating it with GameObjects
    private void GenerateGrid()
    {
        foreach( InitObstacle iob in initQueue)
        {
            GameObject instantiateGO = null;
            switch( iob.GetCode())
            {
                case 'b': instantiateGO = basicObstaclePrefabs[Random.Range(0, basicObstaclePrefabs.Length - 1)];
                    break;
            }
            if( instantiateGO == null) // Dont instantiate a null GameObject
            {
                if( debug)
                {
                    Debug.Log("Failed to generate: " + iob);
                }
                continue;
            }
            // Vector dimensions : (girdPos * scale) + (.5 * modelSideDim)
            Instantiate(instantiateGO, new Vector2(iob.getRow()*.64f + .32f, iob.getCol()*.64f + .32f), Quaternion.identity);
        }
    }

	void Start () {
        initList = new ArrayList();
        initQueue = new Queue();

        // Generate initList
	    if( debug )
        {
            ReadFile(debugFilePath);
        } // Else randomly generate

        // Generate initQueue
        QueueInitObstacles();

        //Place objects on grid
        GenerateGrid();

	}
}
