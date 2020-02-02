using System.Collections.Generic;
using System.Linq;

namespace Assets.Code.Domain.Map
{
    public class Path
    {
        public HashSet<MapCoordinate> PathCoordinates { get; }

        public Path(HashSet<MapCoordinate> pathCoordinates)
        {
            PathCoordinates = pathCoordinates;
        }

        /// <summary>
        /// Returns true of the given coordinate lies on the path
        /// </summary>
        public bool IsOnPath(MapCoordinate coordinate)
        {
            return PathCoordinates.Contains(coordinate);
        }

        public bool IsStartingPoing(MapCoordinate coordinate)
        {
            return PathCoordinates.First() == coordinate;
        }

        public bool IsEndingPoint(MapCoordinate coordinate)
        {
            return PathCoordinates.Last() == coordinate;
        }
    }
}