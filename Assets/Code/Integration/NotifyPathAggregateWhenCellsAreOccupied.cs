using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JetBrains.Annotations;
using UniRx;

class NotifyPathAggregateWhenCellsAreOccupied
{
    private readonly PathFinderAggregate _pathFinder;
    public NotifyPathAggregateWhenCellsAreOccupied(MapAggregate map, TowersAggregate tower, WallsAggregate wall,
        [NotNull] PathFinderAggregate pathFinder)
    {
        _pathFinder = pathFinder ?? throw new ArgumentNullException(nameof(pathFinder));
        map.Events.OfType<MapEvent, MapEvent.Initialized>().Subscribe(HandleMapInitialized);
        tower.Events.OfType<TowersEvent, TowersEvent.TowerRepaired>().Subscribe(HandleTowerRepaired);
        wall.Events.OfType<WallsEvent, WallsEvent.WallRepaired>().Subscribe(HandleWallRepaired);
    }

    private void HandleMapInitialized(MapEvent.Initialized e)
    {
        _pathFinder.Initialize(e.MapCells, e.GoalCell);
    }

    private void HandleTowerRepaired(TowersEvent.TowerRepaired e)
    {
        _pathFinder.SetTileAsOccupied(e.MapCoordinate);
    }

    private void HandleWallRepaired(WallsEvent.WallRepaired e)
    {
        _pathFinder.SetTileAsOccupied(e.Coordinate);
    }
}
