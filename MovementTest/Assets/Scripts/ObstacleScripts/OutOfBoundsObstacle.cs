using UnityEngine;
using System.Collections;

public class OutOfBoundsObstacle : Obstacle {

	public override bool Moveable()
    {
        return false;
    }

}
