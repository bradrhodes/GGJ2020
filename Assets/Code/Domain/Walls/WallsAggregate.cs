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