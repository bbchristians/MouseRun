﻿using UnityEngine;
using System.Collections;

public class LayoutValidator{

    private static InitObstacle[][] grid; // Holds the InitObjects that will be used to determine if there is a viable path
    private static int size;
    private static ArrayList visited =  new ArrayList(); // Holds the values of the already visited positions 
    private Vector2 curPosition;

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

    // Copy constructor
    private LayoutValidator(LayoutValidator other)
    {
        // grid = other.grid; // Made this static
        // curPosition = other.curPosition; // changed immediately
    }

    // Adds the InitObstacle to the grid of the LayoutValidator
    public void AddObstacle(InitObstacle iob)
    {
        grid[iob.getRow()][iob.getCol()] = iob;
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
                
                LayoutValidator newValidator = new LayoutValidator(this);
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