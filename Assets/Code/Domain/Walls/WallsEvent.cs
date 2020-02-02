using System.Collections.Generic;

public abstract class WallsEvent
{
    public class Initialized : WallsEvent
    {
        public Initialized(params WallParameters[] Walls)
        {
            Walls = Walls;
        }

        public IEnumerable<WallParameters> Walls { get; }
    }

    public class WallRepaired : WallsEvent
    {
        public MapCoordinate Coordinate { get; }

        public WallRepaired(MapCoordinate coordinate)
        {
            Coordinate = coordinate;
        }
    }
}