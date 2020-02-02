using UniRx;

public class RepairTowersWhenMapCellClicked
{
    public RepairTowersWhenMapCellClicked(MapAggregate map, TowersAggregate Towers)
    {
        map.Events
            .OfType<MapEvent, MapEvent.MapCellClicked>()
            .Subscribe(clicked => RepairTowers(clicked, Towers));
    }

    private void RepairTowers(MapEvent.MapCellClicked clicked, TowersAggregate Towers)
    {
        Towers.Repair(clicked.Coordinate);
    }
}
