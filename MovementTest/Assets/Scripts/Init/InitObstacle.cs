using UnityEngine;
using System.Collections;

public class InitObstacle {

    private char obstacleCode; // The character that represents which obstacle the InitObstacle should initialize as
    private int row, col; // The position to initialize the obstacle at

    // Initializes the InitObstacle
    public InitObstacle(char code, int row, int col)
    {
        this.obstacleCode = code;
        this.row = row;
        this.col = col;
    }

    public bool OutOfBounds(int maxBound)
    {
        return row < 0 || col < 0 || row >= maxBound || col >= maxBound;
    }

    public char GetCode()
    {
        return obstacleCode;
    }

    public int getRow()
    {
        return row;
    }

    public int getCol()
    {
        return col;
    }
}
