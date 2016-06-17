using UnityEngine;
using Random = UnityEngine.Random;
using System;
using System.Collections;
using System.IO;

public class GameManager : MonoBehaviour {

    public bool debug; // Determines if the game will run in debug mode
    public string debugFilePath; // The file Path leading to the debug file
    public int boardDim; // The dimensions of the board (must be square)
    public int numObstacles; // The number of obstacles to randomly generate if not debugging

    //Prefab lists
    public GameObject[] basicObstaclePrefabs; // The BasicObstacle prefabs
    public GameObject[] buttonDoorPrefabs; // The button and door obstacle prefabs
    public GameObject[] wallPrefabs; // The wall prefabs

    private Queue preInitQueue; // The queue where information generated while loading from a file or through randomization will be generated
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
            preInitQueue.Enqueue(new InitObstacle(parts[0].ToCharArray()[0], 
                Int32.Parse(parts[1]), Int32.Parse(parts[2])));
        }
    }

    // Generates the initList as random objects
    private void RandomList()
    {
        for( int i = 0; i < numObstacles; i++)
        {
            int randRow = Random.Range(0, boardDim);
            int randCol = Random.Range(0, boardDim);
            preInitQueue.Enqueue(new InitObstacle('b', randRow, randCol));
        }

        // Here we will test to see if the level is solvable, and we will recursively call to redo if it isn't
    }

    // Uses the InitObstacles from the initList to generate a queue of InitObjects to be 
    // Initialized later. After this step, all Queued InitObjects should be valid and ready to place.
    private void QueueInitObstacles()
    {
        ArrayList occupiedCoordinates = new ArrayList(); // Contains Vector2s representing the occupied spaces in the grid

        occupiedCoordinates.Add(new Vector2(0,0)); // Occupy player spawn
        occupiedCoordinates.Add(new Vector2(boardDim - 1, boardDim - 1)); // Occupy goal

        InitObstacle iob;

        while( preInitQueue.Count > 0 )
        {
            iob = (InitObstacle)preInitQueue.Dequeue();
            Vector2 iobCoords = new Vector2(iob.getRow(), iob.getCol());

            if( !iob.OutOfBounds(boardDim) && !occupiedCoordinates.Contains(iobCoords))
            {
                initQueue.Enqueue(iob);
                occupiedCoordinates.Add(iobCoords); // Occupy this object's coordinates
            } else if ( !debug )
            {
                // Add a new initObstacle of the same type to the initList
                int randRow = Random.Range(0, boardDim);
                int randCol = Random.Range(0, boardDim);
                initQueue.Enqueue(new InitObstacle(iob.GetCode(), randRow, randCol));
            } else
            {
                Debug.Log(iob + " Not added to initQueue.");
            }
        }
    } 

    // Not Implemented
    private bool IsntOccupied(InitObstacle iob, ArrayList occupiedCoordinates)
    {
        return false;
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
    private void GenerateWall()
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
        preInitQueue = new Queue();
        initQueue = new Queue();

        // Generate initList from file
        if (debug)
        {
            ReadFile(debugFilePath);
        }
        // Generate initlist randomly
        else
        {
            RandomList();
        }

        // Generate initQueue
        QueueInitObstacles();

        // Place objects on grid
        GenerateGrid();

        // Form a wall of basic blocks around the outside of the board
        GenerateWall();

	}
}
