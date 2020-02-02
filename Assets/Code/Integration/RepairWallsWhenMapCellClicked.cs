using UniRx;

public class RepairWallsWhenMapCellClicked
{
    public RepairWallsWhenMapCellClicked(MapAggregate map, WallsAggregate walls)
    {
        map.Events
            .OfType<MapEvent, MapEvent.MapCellClicked>()
            .Subscribe(clicked => RepairWalls(clicked, walls));
    }

    private void RepairWalls(MapEvent.MapCellClicked clicked, WallsAggregate walls)
    {
        walls.Repair(clicked.Coordinate);
    }
}