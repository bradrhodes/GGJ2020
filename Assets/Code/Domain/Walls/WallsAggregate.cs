using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;

public class WallsAggregate
{
	private Subject<WallsEvent> _events = new Subject<WallsEvent>();
	private Dictionary<MapCoordinate, Wall> _walls = new Dictionary<MapCoordinate, Wall>();

	public IObservable<WallsEvent> Events => _events;

	public void Initialize(params InitialWall[] walls)
    {
        _walls = walls.ToDictionary(wall => wall.Coordinate, wall => new Wall(wall.Coordinate, WallState.Broken));
        Emit(new WallsEvent.Initialized(walls));
	}

	private void Emit(WallsEvent @event)
		=> _events.OnNext(@event);

    public void Repair(MapCoordinate clickedCoordinate)
    {
        if (!_walls.ContainsKey(clickedCoordinate))
            return;

        var clickedWall = _walls.First(pair => pair.Key == clickedCoordinate).Value;

		Emit(new WallsEvent.WallRepaired(clickedWall.Coordinate));
    }
}

public class InitialWall
{
	public InitialWall(MapCoordinate coordinate)
	{
		Coordinate = coordinate;
	}

	public MapCoordinate Coordinate { get; }
}

public class Wall
{
    public MapCoordinate Coordinate { get; }
    public WallState State { get; }

    public Wall(MapCoordinate coordinate, WallState state)
    {
        Coordinate = coordinate;
        State = state;
    }
}

public enum WallState
{
	Broken,
	Repaired
}

//public class WallIdentifier : IEquatable<WallIdentifier>
//{
//	private static int _nextId;
//	private readonly int _id;
//
//	private WallIdentifier(int id)
//	{
//		_id = id;
//	}
//
//	public static WallIdentifier Create()
//		=> new WallIdentifier(_nextId++);
//
//	public override bool Equals(object obj)
//		=> Equals(obj as WallIdentifier);
//
//	public bool Equals(WallIdentifier other)
//		=> other != null && _id == other._id;
//
//	public override int GetHashCode()
//		=> 1969571243 + _id.GetHashCode();
//
//	public static bool operator ==(WallIdentifier left, WallIdentifier right)
//		=> EqualityComparer<WallIdentifier>.Default.Equals(left, right);
//
//	public static bool operator !=(WallIdentifier left, WallIdentifier right)
//		=> !(left == right);
//}

public abstract class WallsEvent
{
	public class Initialized : WallsEvent
	{
		public Initialized(params InitialWall[] Walls)
		{
			Walls = Walls;
		}

		public IEnumerable<InitialWall> Walls { get; }
	}

	public class WallRepaired : WallsEvent
	{
        public MapCoordinate Coordinate { get; }

        public WallRepaired(MapCoordinate coordinate)
        {
            Coordinate = coordinate;
        }
	}
}
