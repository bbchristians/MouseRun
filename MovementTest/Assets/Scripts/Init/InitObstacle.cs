
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

    // Determines if the given coordinates make the InitObstacle out of bounds
    public bool OutOfBounds(int maxBound)
    {
        return row < 0 || col < 0 || row >= maxBound || col >= maxBound;
    }

    // Returns the obstacle code of the InitObstacle
    public char GetCode()
    {
        return obstacleCode;
    }

    // Returns the Row of the Obstacle
    public int getRow()
    {
        return row;
    }

    // Returns the Collumn of the Obstacle
    public int getCol()
    {
        return col;
    }

    // Determines if the obstacle can be moved on
    public bool CanBeMovedOn()
    {
        return false;
        // return obstacleCode == ''; return true if the obstacle code matches a movable-on obstacle
        // ^ for use in future obstacles
    }

    public override string ToString()
    {
        return obstacleCode + "@ (" + row + ", " + col + ")";
    }
}
