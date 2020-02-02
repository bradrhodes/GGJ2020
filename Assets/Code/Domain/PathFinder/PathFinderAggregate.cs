using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UniRx;
using RoyT.AStar;

public interface IGetEnemyPositions
{
	UnityEngine.Vector3 this[EnemyIdentifier enemyId] { get; }
}

public class PathFinderAggregate
{
	private readonly IGetEnemyPositions _enemyPositionService;
	private readonly Subject<PathFinderEvent> _events = new Subject<PathFinderEvent>();
	private RoyT.AStar.Grid _grid = new Grid(0, 0);
	private HashSet<EnemyIdentifier> _enemies = new HashSet<EnemyIdentifier>();
	private MapCoordinate _goalCell = new MapCoordinate(0, 0);

	public IObservable<PathFinderEvent> Events => _events;

	public PathFinderAggregate([NotNull] IGetEnemyPositions enemyPositionService)
	{
		_enemyPositionService = enemyPositionService ?? throw new ArgumentNullException(nameof(enemyPositionService));
	}
	public void Initialize(MapCell[,] map, MapCoordinate goalCell)
	{
		_goalCell = goalCell;

        var xDimension = map.GetLength(0);
        var yDimension = map.GetLength(1);

		_grid = new Grid(xDimension, yDimension);
		AdjustInitialCostOfTraversal(map);
	}

	public void SetTileAsOccupied(MapCoordinate coordinate)
	{
		_grid.BlockCell(coordinate.ToPosition());

		var events = _enemies.Select(identifier => new PathFinderEvent.PathCalculated(identifier, FindPath(_enemyPositionService[identifier].ToMapCoordinate())));

		foreach (var pathCalulated in events)
		{
			Emit(pathCalulated);
		}
	}

	private MapCoordinate[] FindPath(MapCoordinate currentLocation)
	{
		return _grid.GetPath(currentLocation.ToPosition(), _goalCell.ToPosition(), MovementPatterns.LateralOnly)
		   .Select(position => position.ToMapCoordinate()).ToArray();
	}

	public void AddEnemy(EnemyIdentifier enemyId)
	{
		_enemies.Add(enemyId);

		var path = FindPath(_enemyPositionService[enemyId].ToMapCoordinate());

		Emit(new PathFinderEvent.PathCalculated(enemyId, path));
	}
    private void AdjustInitialCostOfTraversal(MapCell[,] map)
    {
        for(int x = 0; x < map.GetLength(0); x++)
        for (int y = 0; y < map.GetLength(1); y++)
        {
            switch (map[x,y])
            {
                    case WaterCell cell:
						_grid.BlockCell(new Position(x,y));
                        break;
					case WallCell cell:
						_grid.SetCellCost(new Position(x,y), 15.0f);
                        break;
					case TowerCell cell:
						_grid.SetCellCost(new Position(x,y), 25.0f);
                        break;
            }
        }
    }

	private void Emit(PathFinderEvent @event)
		=> _events.OnNext(@event);
}



public abstract class PathFinderEvent
{
	public class TilesInitialized : PathFinderEvent
	{
		public bool[,] OccupiedTiles { get; }

		public TilesInitialized(bool[,] occupiedTiles)
		{
			OccupiedTiles = occupiedTiles;
		}
	}

	public class TileOccupied : PathFinderEvent
	{
		public MapCoordinate Coordinate { get; }

		public TileOccupied(MapCoordinate coordinate)
		{
			Coordinate = coordinate;
		}
	}

	public class PathCalculated : PathFinderEvent
	{
		public EnemyIdentifier EnemyId { get; }
		public MapCoordinate[] Path { get; }

		public PathCalculated(EnemyIdentifier enemyId, MapCoordinate[] path)
		{
			EnemyId = enemyId;
			Path = path;
		}
	}

}

public static class TileExtensions
{
	public static Position ToPosition(this MapCoordinate source)
	{
		return new Position(source.X, source.Y);
	}

	public static MapCoordinate ToMapCoordinate(this Position source)
	{
		return new MapCoordinate(source.X, source.Y);
	}
}