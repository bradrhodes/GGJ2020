using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;

public class TowersAggregate
{
	private Subject<TowersEvent> _events = new Subject<TowersEvent>();

	public IObservable<TowersEvent> Events => _events;

	private Dictionary<TowerIdentifier, List<EnemyIdentifier>> _enemiesInRange = new Dictionary<TowerIdentifier, List<EnemyIdentifier>>();

	private Dictionary<TowerIdentifier, EnemyIdentifier> _targettedEnemies = new Dictionary<TowerIdentifier, EnemyIdentifier>();

	public void Initialize(params InitialTower[] towers)
	{
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

	private void Emit(TowersEvent @event)
		=> _events.OnNext(@event);
}

public class InitialTower
{
	public InitialTower(TowerIdentifier towerId, MapCoordinate coordinate)
	{
		TowerId = towerId;
		Coordinate = coordinate;
	}

	public TowerIdentifier TowerId { get; }
	public MapCoordinate Coordinate { get; }
}

public abstract class TowersEvent
{
	public class Initialized : TowersEvent
	{
		public Initialized(params InitialTower[] towers)
		{
			Towers = towers;
		}

		public IEnumerable<InitialTower> Towers { get; }
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