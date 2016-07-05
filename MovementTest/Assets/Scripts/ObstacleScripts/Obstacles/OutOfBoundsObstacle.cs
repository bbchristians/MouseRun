
public class OutOfBoundsObstacle : Obstacle {

    // Makes the OutOfBoundsObstacle unable to be moved onto
	public override bool Moveable()
    {
        return false;
    }

}
