using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class MapAggregate
{
    private Subject<MapEvent> _events = new Subject<MapEvent>();
    private readonly MapGenerator _mapGenerator;

    public IObservable<MapEvent> Event => _events;

    public void Initialize(int width, int height)
    {
        var map = _mapGenerator.GenerateMap(width, height);
        Emit(new MapEvent.Initialized(map));
    }

    private void Emit(MapEvent @event)
        => _events.OnNext(@event);

    public MapAggregate(MapGenerator mapGenerator)
    {
        this._mapGenerator = mapGenerator;
    }
}
