using System.Linq;
using UniRx;

public class InitializeWallsWhenMapInitialized
{
    public InitializeWallsWhenMapInitialized(MapAggregate map, WallsAggregate walls)
    {
        map.Events
            .OfType<MapEvent, MapEvent.Initialized>()
            .Subscribe(initialized => InitilizeWalls(initialized, walls));
    }

    private void InitilizeWalls(MapEvent.Initialized initialized, WallsAggregate walls)
    {
        var mapCoords = Enumerable.Range(0, initialized.MapCells.GetLength(0)).SelectMany(x => Enumerable.Range(0, initialized.MapCells.GetLength(1)).Select(y => new MapCoordinate(x, y)));

        var initialWalls = mapCoords
            .Where(coord => initialized.MapCells[coord.X, coord.Y] is WallCell)
            .Select(coord => new WallParameters(coord))
            .ToArray();

        walls.Initialize(initialWalls);
    }
}