using UnityEngine;
using System.Collections;

public abstract class Obstacle : MonoBehaviour {

    private int rowPos, 
                colPos;

    public void SetPos(int row, int col)
    {
        rowPos = row;
        colPos = col;
    }

    // Determines if the player can move into the given Obstacle's position
    public abstract bool Moveable();
}
