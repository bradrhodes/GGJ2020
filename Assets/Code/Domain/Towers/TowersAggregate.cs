using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;

public class TowersAggregate
{
	private Subject<TowersEvent> _events = new Subject<TowersEvent>();
    private IDictionary<TowerIdentifier, Tower> _towers = new Dictionary<TowerIdentifier, Tower>();

	public IObservable<TowersEvent> Events => _events;

	private Dictionary<TowerIdentifier, List<EnemyIdentifier>> _enemiesInRange = new Dictionary<TowerIdentifier, List<EnemyIdentifier>>();

	private Dictionary<TowerIdentifier, EnemyIdentifier> _targettedEnemies = new Dictionary<TowerIdentifier, EnemyIdentifier>();

	private int _availableScrap;
	private readonly RepairCosts _repairCosts;

	public TowersAggregate(RepairCosts repairCosts)
	{
		_repairCosts = repairCosts;
	}

	public void Initialize(params TowerParameters[] towers)
    {
        _towers = towers.ToDictionary(tower => tower.TowerId,
            tower => new Tower(tower.TowerId, tower.Coordinate, TowerState.Broken));
		Emit(new TowersEvent.Initialized(towers));
	}

	public void AddEnemy(TowerIdentifier towerId, EnemyIdentifier enemyId)
	{
		if (!_enemiesInRange.TryGetValue(towerId, out var enemies))
			enemies = _enemiesInRange[towerId] = new List<EnemyIdentifier>();

		enemies.Add(enemyId);

		if (!_targettedEnemies.ContainsKey(towerId))
		{
			_targettedEnemies[towerId] = enemyId;
			Emit(new TowersEvent.EnemyTargetted(towerId, enemyId));
		}
	}

	public void RemoveEnemy(TowerIdentifier towerId, EnemyIdentifier enemyId)
	{
		if (!_enemiesInRange.TryGetValue(towerId, out var enemies))
			enemies = _enemiesInRange[towerId] = new List<EnemyIdentifier>();

		enemies.Remove(enemyId);

		if (_targettedEnemies.TryGetValue(towerId, out var currentTargetId) && enemyId == currentTargetId)
		{
			Emit(new TowersEvent.EnemyUntargetted(towerId, enemyId));
			_targettedEnemies.Remove(towerId);

			if (enemies.Any())
			{
				var newTargetId = enemies.First();

				_targettedEnemies[towerId] = newTargetId;
				Emit(new TowersEvent.EnemyTargetted(towerId, newTargetId));
			}
		}
	}
    public void Repair(TowerIdentifier identifier)
    {
		if (_availableScrap < _repairCosts.Tower)
			return;

        if (!_towers.ContainsKey(identifier))
            return;

        var clickedTower = _towers.First(pair => pair.Key == identifier).Value;

        if (clickedTower.State == TowerState.Repaired)
            return;

		_towers[identifier] = new Tower(identifier, clickedTower.Coordinate, TowerState.Repaired);

		Emit(new TowersEvent.TowerRepaired(clickedTower.Coordinate, clickedTower.Identifier));
    }

	public void Repair(MapCoordinate coordinate)
    {
        var tower = _towers.FirstOrDefault(pair => pair.Value.Coordinate == coordinate).Value;

        if (tower == default(Tower))
            return;

		Repair(tower.Identifier);
    }

	public void UpdateAvailableScrap(int amount)
	{
		_availableScrap = amount;
	}

	private void Emit(TowersEvent @event)
		=> _events.OnNext(@event);
}

public class Tower
{
    public TowerIdentifier Identifier { get; }
    public MapCoordinate Coordinate { get; }
    public TowerState State { get; }

    public Tower(TowerIdentifier identifier, MapCoordinate coordinate, TowerState state)
    {
        Identifier = identifier;
        Coordinate = coordinate;
        State = state;
    }
}

public enum TowerState
{
	Broken,
	Repaired
}

public class TowerParameters
{
	public TowerParameters(TowerIdentifier towerId, MapCoordinate coordinate, TowerTypes towerType)
	{
		TowerId = towerId;
		Coordinate = coordinate;
		TowerType = towerType;
	}

	public TowerIdentifier TowerId { get; }
	public MapCoordinate Coordinate { get; }
	public TowerTypes TowerType { get; }
}

public abstract class TowersEvent
{
	public class Initialized : TowersEvent
	{
		public Initialized(params TowerParameters[] towers)
		{
			Towers = towers;
		}

		public IEnumerable<TowerParameters> Towers { get; }
	}

	public class EnemyTargetted : TowersEvent
	{
		public EnemyTargetted(TowerIdentifier towerId, EnemyIdentifier enemyId)
		{
			TowerId = towerId;
			EnemyId = enemyId;
		}

		public TowerIdentifier TowerId { get; }
		public EnemyIdentifier EnemyId { get; }
	}

	public class EnemyUntargetted : TowersEvent
	{
		public TowerIdentifier TowerId { get; }
		public EnemyIdentifier EnemyId { get; }

		public EnemyUntargetted(TowerIdentifier towerId, EnemyIdentifier enemyId)
		{
			TowerId = towerId;
			EnemyId = enemyId;
		}
	}

	public class TowerRepaired : TowersEvent
	{
        public MapCoordinate MapCoordinate { get; }
        public TowerIdentifier Identifier { get; }

        public TowerRepaired(MapCoordinate mapCoordinate, TowerIdentifier identifier)
        {
            MapCoordinate = mapCoordinate;
            Identifier = identifier;
        }
	}
}