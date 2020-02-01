using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class MapAggregate
{
    private Subject<MapEvent> _events = new Subject<MapEvent>();

    public IObservable<MapEvent> Event => _events;

    public void Initialize(MapCell[,] mapCells)
    {
        Emit(new MapEvent.Initialized(mapCells));
    }

    private void Emit(MapEvent @event)
        => _events.OnNext(@event);
}
