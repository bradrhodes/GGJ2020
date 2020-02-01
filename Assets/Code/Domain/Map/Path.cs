using System.Collections.Generic;
using System.Linq;

namespace Assets.Code.Domain.Map
{
    public class Path
    {
        private readonly HashSet<MapCoordinate> _pathCoordinates;

        public Path(HashSet<MapCoordinate> pathCoordinates)
        {
            _pathCoordinates = pathCoordinates;
        }

        /// <summary>
        /// Returns true of the given coordinate lies on the path
        /// </summary>
        public bool IsOnPath(MapCoordinate coordinate)
        {
            return _pathCoordinates.Contains(coordinate);
        }

        public bool IsStartingPoing(MapCoordinate coordinate)
        {
            return _pathCoordinates.First() == coordinate;
        }

        public bool IsEndingPoint(MapCoordinate coordinate)
        {
            return _pathCoordinates.Last() == coordinate;
        }
    }
}