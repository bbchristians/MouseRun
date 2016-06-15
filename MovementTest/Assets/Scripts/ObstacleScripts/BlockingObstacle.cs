using UnityEngine;
using System.Collections;

public class BlockingObstacle : Obstacle {

    public override bool Moveable()
    {
        return false;
    }

}
