using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class TowersAggregate
{
	private Subject<TowersEvent> _events = new Subject<TowersEvent>();

	public IObservable<TowersEvent> Events => _events;

	public void Initialize(params InitialTower[] towers)
	{
		Emit(new TowersEvent.Initialized(towers));
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

public class TowerIdentifier : IEquatable<TowerIdentifier>
{
	private static int _nextId;
	private readonly int _id;

	private TowerIdentifier(int id)
	{
		_id = id;
	}

	public static TowerIdentifier Create()
		=> new TowerIdentifier(_nextId++);

	public override bool Equals(object obj)
		=> Equals(obj as TowerIdentifier);

	public bool Equals(TowerIdentifier other)
		=> other != null && _id == other._id;

	public override int GetHashCode()
		=> 1969571243 + _id.GetHashCode();

	public static bool operator ==(TowerIdentifier left, TowerIdentifier right)
		=> EqualityComparer<TowerIdentifier>.Default.Equals(left, right);

	public static bool operator !=(TowerIdentifier left, TowerIdentifier right)
		=> !(left == right);
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
}