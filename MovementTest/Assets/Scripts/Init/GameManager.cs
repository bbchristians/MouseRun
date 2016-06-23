using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using Random = UnityEngine.Random;
using System;
using System.Collections;
using System.IO;

public class GameManager : MonoBehaviour {

    public bool debug; // Determines if the game will run in debug mode
    public string debugFilePath; // The file Path leading to the debug file
    public int boardDim; // The dimensions of the board (must be square)
    public int numObstacles; // The number of obstacles to randomly generate if not debugging
    public int failGenerationsTimeoutCount; // Number of times the generator can fail to generate a solvable configuration before the system will time out

    //Prefab lists
    public GameObject[] basicObstaclePrefabs; // The BasicObstacle prefabs
    public GameObject[] buttonDoorPrefabs; // The button and door obstacle prefabs
    public GameObject[] wallPrefabs; // The wall prefabs
	public GameObject[] staticPrefabs; // Objects to place like the Player and the Finish
	public GameObject verticalGridLine; // Vertical grid line to add a grid to the background
	public GameObject horizontalGridLine; // Horizontal grid line to add a grid to the background

	// Buttons to link to player prefab
	public Button forwardButton;
	public Button leftButton;
	public Button rightButton;

    private Queue preInitQueue; // The queue where information generated while loading from a file or through randomization will be generated
    private Queue initQueue; // The queue where the InitObstacles will be store for initialization
    private int generationFailures; // To keep track of the number of generation failures for safety timeout
	private float scale; // Used to scale the GameObjects to fit a dynamic board

    // Generates the initList from a file given by fileName
    private void ReadFile(string fileName)
    {
        preInitQueue = new Queue();
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

    // Generates the preInitQueue and populates it with random obstacles
    private void RandomList()
    {
		//Check to make sure there cn be a possible solution
		if (numObstacles > (boardDim - 1) * (boardDim - 1)) {
			Debug.Log ("Too many obstacles to generate on given board size!");
			return;
		}

        preInitQueue = new Queue();
        for ( int i = 0; i < numObstacles; i++)
        {
            int randRow = Random.Range(0, boardDim);
            int randCol = Random.Range(0, boardDim);
            preInitQueue.Enqueue(new InitObstacle('b', randRow, randCol));
        }

	}

    // Uses the InitObstacles from the initList to generate a queue of InitObjects to be 
    // Initialized later. After this step, all Queued InitObjects should be valid and ready to place.
    private void QueueInitObstacles()
    {
        initQueue = new Queue();
        ArrayList occupiedCoordinates = new ArrayList(); // Contains Vector2s representing the occupied spaces in the grid
        LayoutValidator validator = new LayoutValidator(boardDim); // The validator that will validate the layout of the objects in the queue after they are placed

        occupiedCoordinates.Add(new Vector2(0,0)); // Occupy player spawn
        occupiedCoordinates.Add(new Vector2(boardDim - 1, boardDim - 1)); // Occupy goal

		//Occupy one block adjacent to player start and finish to increase chance of successful generation
		Vector2 playerStartOcc;
		playerStartOcc = Random.Range (0, 1) > 0 ?  new Vector2 (1f, 0f) : new Vector2 (0f, 1f);
		Vector2 playerFinishOcc;
		playerFinishOcc = Random.Range (0, 1) > 0 ?  new Vector2 (boardDim-1, boardDim-2) : new Vector2 (boardDim-2, boardDim-1);
		occupiedCoordinates.Add (playerFinishOcc);
		occupiedCoordinates.Add (playerStartOcc);

        InitObstacle iob;

        while( preInitQueue.Count > 0 )
        {
            iob = (InitObstacle)preInitQueue.Dequeue();
            Vector2 iobCoords = new Vector2(iob.getRow(), iob.getCol());

            if( !iob.OutOfBounds(boardDim) && !ContainsCoords(occupiedCoordinates, iobCoords))
            {
                initQueue.Enqueue(iob);
                occupiedCoordinates.Add(iobCoords); // Occupy this object's coordinates
                validator.AddObstacle(iob);
            } else if ( !debug )
            {
                // Add a new initObstacle of the same type to the initList
                int randRow = Random.Range(0, boardDim);
                int randCol = Random.Range(0, boardDim);
                preInitQueue.Enqueue(new InitObstacle(iob.GetCode(), randRow, randCol));
            } else
            {
                Debug.Log(iob + " Not added to initQueue.");
            }
        }

        // Determine if a valid configuration was generated
        if( !debug)
        {
            if ( !validator.IsSolvable())
            {
				Debug.Log("Invalid configuration generated!\n" + validator.ToString());
                generationFailures++;
				if ( generationFailures >= failGenerationsTimeoutCount)
				{
					Debug.Log("Generation timed out after " + generationFailures + " failures.");
					return; // Exit if maximum generation failures is met
				}
                RandomList();
                QueueInitObstacles();
                return;
            }
        }
        Debug.Log("Queueing finished successfully");
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
            
			instantiateGO.transform.localScale = new Vector3(scale, scale, 1f);
            InstantiateAtPos(instantiateGO, iob.getRow(), iob.getCol());
        }

		GameObject placedLine;

		// Place grid lines
		for (float i = .5f; i < boardDim; i += 1) {
			placedLine = InstantiateAtPos (verticalGridLine, i, boardDim/2f);
			placedLine.transform.localScale = new Vector3 (.75f, 2, 1);
			placedLine = InstantiateAtPos (verticalGridLine, boardDim/2f, i);
			placedLine.transform.localScale = new Vector3 (.75f, 2, 1);
			placedLine.transform.Rotate(Vector3.forward * 90);

		}
    }

    // Places a game object at the given coordinates, and returns a reference to it
	// as a GameObject
    private GameObject InstantiateAtPos(GameObject go, float row, float col)
    {
        // Vector dimensions : (girdPos * scale) + (.5 * modelSideDim)
        Vector2 placementVector = new Vector2(row * .64f + .32f, col * .64f + .32f);
		placementVector *= scale;

        if( row >= 0 && row < boardDim && col >= 0 && col < boardDim )
            Debug.Log("Placing obstacle at " + row + ", " + col);

		go.transform.localScale = new Vector3 (scale, scale, 1);

		return (GameObject) Instantiate(go, placementVector, Quaternion.identity);
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

    //Determines if coords is in coordList
    public static bool ContainsCoords(ArrayList coordList, Vector2 coords)
    {
        foreach(Vector2 v2 in coordList)
        {
            //Debug.Log("Comparing: " + v2.x + " and " + coords.x + ", " + v2.y + " and " + coords.y);
            if (v2.x == coords.x && v2.y == coords.y) return true;
        }
        return false;
    }

	// Instantiates the static prefabs from staticPrefabs
	void InstantiateStaticPrefabs() {
		// Instantiate Player
		GameObject player = InstantiateAtPos (staticPrefabs [0], 0, 0);
		player.SetActive (true);
		PlayerController.moveScale = scale * .64f;
			// Link buttons
		UnityAction action;
		action = () => { player.GetComponent<PlayerController>().Forward(); };
		forwardButton.onClick.AddListener(action);
		action = () => { player.GetComponent<PlayerController>().Left(); };
		leftButton.onClick.AddListener(action);
		action = () => { player.GetComponent<PlayerController>().Right(); };
		rightButton.onClick.AddListener(action);


		InstantiateAtPos (staticPrefabs [1], boardDim - 1, boardDim - 1);
	}

	void Start () {
        
		scale = 5f / boardDim;
		PlayerController.scale = scale;
        generationFailures = 0;

        // Generate preInitQueue from file
        if (debug)
        {
            ReadFile(debugFilePath);
        }
        // Generate preInitQueue randomly
        else
        {
            RandomList();
        }

        // Generate initQueue
        QueueInitObstacles();

        // Place objects on grid
        GenerateGrid();

		// Instantiates statics prefabs like the player, and the victory block
		InstantiateStaticPrefabs();

        // Form a wall of basic blocks around the outside of the board
        GenerateWall();

	}
}
