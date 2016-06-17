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
    public GameObject[] basicObstaclePrefabs; // The BasicObstacle prefabs
    public GameObject[] buttonDoorPrefabs; // The button and door obstacle prefabs
    public GameObject[] wallPrefabs; // The wall prefabs

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
        GameObject instantiateGO; 
        foreach ( InitObstacle iob in initQueue)
        {
            instantiateGO = null;
            // Store game object to be generated based off the GameObjectCode of iob
            switch( iob.GetCode())
            {
                case 'b': instantiateGO = RandomFromArray(basicObstaclePrefabs);
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
            
            InstantiateAtPos(instantiateGO, iob.getRow(), iob.getCol());
        }
    }

    // Places a game object at the given coordinates
    private void InstantiateAtPos(GameObject go, int row, int col)
    {
        // Vector dimensions : (girdPos * scale) + (.5 * modelSideDim)
        Vector2 placementVector = new Vector2(row * .64f + .32f, col * .64f + .32f);

        Instantiate(go, placementVector, Quaternion.identity);
    }

    // Gets a random GameObject from a GameObject Array
    private GameObject RandomFromArray(GameObject[] array)
    {
        return array[Random.Range(0, array.Length - 1)];
    }

    // Generates the outer wall of the board
    void GenerateWall()
    {
        for( int i = -1; i <= boardDim; i++)
        {
            for ( int k = -1; k <= boardDim; k = boardDim)
            {
                InstantiateAtPos(RandomFromArray(wallPrefabs), i, k);
                InstantiateAtPos(RandomFromArray(wallPrefabs), k, i);
                if (k == boardDim) break;
            }
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

        // Place objects on grid
        GenerateGrid();

        // Form a wall of basic blocks around the outside of the board
        GenerateWall();

	}
}
