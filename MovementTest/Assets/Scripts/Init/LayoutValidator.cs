using UnityEngine;
using System.Collections;

public class LayoutValidator{

    private static InitObstacle[][] grid; // Holds the InitObjects that will be used to determine if there is a viable path
    private static int size; // The size of the grid
    private static ArrayList visited =  new ArrayList(); // Holds the values of the already visited positions 
    private Vector2 curPosition; // The current position in the grid

    // Default constructor
    public LayoutValidator() { }

    //Constructor to be used in the GameManager script
    public LayoutValidator(int dim)
    {
        size = dim;
        // Generate the grid
        grid = new InitObstacle[dim][];
        for( int i = 0; i < dim; i++)
        {
            grid[i] = new InitObstacle[dim];
        }
		// Reset visited List
		visited = new ArrayList();
        
    }

    // Adds the InitObstacle to the grid of the LayoutValidator
    public void AddObstacle(InitObstacle iob)
    {
        grid[iob.getRow()][iob.getCol()] = iob;
    }

    // Resets the list of the visited coordinates
    public void ResetVisited()
    {
        visited = new ArrayList();
    }

    // Returns the list of visited points in the form of Vector2Ds
    public ArrayList GetVisited()
    {
        return visited;
    }

    // Generates a stack containing the successors of the current configuration
    // curPos will be moved to the location of the space you just placed
    private Stack GetSuccessors()
    {
        Stack successors = new Stack();

        for( int row = -1; row <= 1; row++ )
        {
            for( int col = -1; col <= 1; col++ )
            {
				if ((Mathf.Abs(row) + Mathf.Abs(col)) != 1) continue; // if not up down left or right of the current position
                
				Vector2 newPos = new Vector2(curPosition.x + row, curPosition.y + col);
			
                if (GameManager.ContainsCoords(visited, newPos)) continue; // skip current space if already visited
                
                LayoutValidator newValidator = new LayoutValidator();
                newValidator.curPosition = newPos;

                visited.Add(newValidator.curPosition);

                successors.Push(newValidator);
            }
        }

        return successors;
    }

    // Returns true if the recently "moved-to" space can be moved to validly
    private bool IsValid()
    {
        // Take care of out of bounds issues
        if (curPosition.x < 0 || curPosition.y < 0 || curPosition.x >= size || curPosition.y >= size) return false;

        InitObstacle recentlyPlaced = grid[(int)curPosition.x][(int)curPosition.y];

        return recentlyPlaced == null || recentlyPlaced.CanBeMovedOn();
    }

    // Determines if the goal has been reached
    private bool IsGoal()
    {
        return curPosition == new Vector2(size - 1, size - 1);
    }

	// Determines if the configuration is solvable
    public bool IsSolvable()
    {
        if (this.IsGoal()) return true;

		Stack successors = this.GetSuccessors ();
        foreach( LayoutValidator successor in successors)
        {
            if( successor.IsValid())
            {
                if (successor.IsSolvable()) return true;
            }
        }
        return false;
    }

    // Returns an ArrayList of the possible placements for a door
    // The returned positions will result in a blockage by a door placed in it's location
    public ArrayList FindDoorPlacement()
    {
        ArrayList places = new ArrayList();

        for ( int i = 0; i < size; i++)
        {
            for( int j = 0; j < size; j++)
            {
                if (grid[i][j] == null) continue; // skip over occupied spaces
                if ((i == 0 || grid[i - 1][j] != null) && (i == size - 1 || grid[i + 1][j] != null)) places.Add(new Vector2(i, j));
                else if ((j == 0 || grid[i][j - 1] != null) && (j == size - 1 || grid[i][j + 1] != null)) places.Add(new Vector2(i, j));
            }
        }

        return places;
    }

    // ToString for debugging purposes
	public override string ToString(){
		string ret = "";

		for (int j = size-1; j >= 0; j--) {
			for (int i = 0; i < size; i++) {
				InitObstacle iob = grid [i] [j];
				if (iob == null)
					ret += "\t.";
				else 
					ret += "\tO"; 
				if (curPosition.x == i && curPosition.y == j)
					ret += "P";
					
			}
			ret += "\n";
		}

		return ret;
	}
}
