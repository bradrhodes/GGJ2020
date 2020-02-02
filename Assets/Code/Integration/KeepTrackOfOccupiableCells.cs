using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JetBrains.Annotations;
using UniRx;

namespace Assets.Code.Integration
{
    class KeepTrackOfOccupiableCells
    {
        private readonly TilesAggregate _tile;
        public KeepTrackOfOccupiableCells(MapAggregate map, TowersAggregate tower, WallsAggregate wall,
            [NotNull] TilesAggregate tile)
        {
            _tile = tile ?? throw new ArgumentNullException(nameof(tile));
            map.Events.OfType<MapEvent, MapEvent.Initialized>().Subscribe(HandleMapInitialized);
            tower.Events.OfType<TowersEvent, TowersEvent.TowerRepaired>().Subscribe(HandleTowerRepaired);
            wall.Events.OfType<WallsEvent, WallsEvent.WallRepaired>().Subscribe(HandleWallRepaired);
        }

        private void HandleMapInitialized(MapEvent.Initialized e)
        {
            var xDimension = e.MapCells.GetLength(0);
            var yDimension = e.MapCells.GetLength(1);

            _tile.Initialize(xDimension, yDimension);
        }

        private void HandleTowerRepaired(TowersEvent.TowerRepaired e)
        {
            _tile.SetTileAsOccupied(e.MapCoordinate);
        }

        private void HandleWallRepaired(WallsEvent.WallRepaired e)
        {
            _tile.SetTileAsOccupied(e.Coordinate);
        }
    }
}
