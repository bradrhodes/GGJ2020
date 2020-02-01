public class Wall
{
    public MapCoordinate Coordinate { get; }
    public WallState State { get; }

    public Wall(MapCoordinate coordinate, WallState state)
    {
        Coordinate = coordinate;
        State = state;
    }
}