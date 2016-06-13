using UnityEngine;
using System.Collections;

public class Grid : MonoBehaviour  {

    public int rows;
    public int cols;
    public bool debug;
    public BlockingObstacle debugObstacle;

    private Obstacle[][] grid;

	// Use this for initialization
	void Start () {
        grid = new Obstacle[rows][];
        for( int i = 0; i < cols; i++ )
        {
            grid[i] = new Obstacle[cols];
            for( int k = 0; k < rows; k++)
            {
                grid[i][k] = new EmptyObstacle();
            }
        }
        if( debug)
        {
            PlaceObstacle(debugObstacle, 2, 2);
        }
	}

    // Retrieves the Obstacle at the given spot
    public Obstacle Get(int row, int col)
    {
        if (row < 0 || col < 0 || row >= rows || col >= cols)
            return new OutOfBoundsObstacle();
        return grid[row][col];
    }

    // Determines if the space can have something placed in it
    // or if the player can move into it
    public bool CanMove(int row, int col)
    {
        Obstacle o = Get(row, col);
        Debug.Log(o);
        return (o == null || o.Moveable());
    }

    // Tries to place an Obstacle in the space and returns true if it
    // was successful, else false
    public bool PlaceObstacle(Obstacle o, int row, int col)
    {
        if( CanMove(row, col))
        {
            grid[row][col] = o;
            return true;
        }
        return false;
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
