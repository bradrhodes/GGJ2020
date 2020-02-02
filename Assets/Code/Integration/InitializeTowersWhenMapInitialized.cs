using System.Linq;
using UniRx;
using UnityEngine;

public class InitializeTowersWhenMapInitialized
{
    public InitializeTowersWhenMapInitialized(MapAggregate map, TowersAggregate towers)
    {
        map.Events
            .OfType<MapEvent, MapEvent.Initialized>()
            .Subscribe(initialized => InitializeTowers(initialized, towers));
    }

    private void InitializeTowers(MapEvent.Initialized initialized, TowersAggregate towers)
    {
        var mapCoords = Enumerable.Range(0, initialized.MapCells.GetLength(0)).SelectMany(x => Enumerable.Range(0, initialized.MapCells.GetLength(1)).Select(y => new MapCoordinate(x, y)));

        var initialTowers = mapCoords
            .Where(coord => initialized.MapCells[coord.X, coord.Y] is TowerCell)
            .Select(coord => new TowerParameters(TowerIdentifier.Create(), coord, PickRandomTowerType()))
            .ToArray();

        towers.Initialize(initialTowers);
    }

    private TowerTypes PickRandomTowerType()
    {
        float randomNumber = Random.value;

        if (randomNumber <= 0.25f)
            return TowerTypes.Basic;

        if (randomNumber <= 0.50f)
            return TowerTypes.Ice;

        if (randomNumber <= 0.75f)
            return TowerTypes.Fire;

        return TowerTypes.Plasma;
    }
}
