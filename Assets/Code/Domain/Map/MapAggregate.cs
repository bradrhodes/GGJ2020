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

public class MapCell
{
    public CellType CellType { get; }
    (int xpos, int ypos) Position { get; }
    public CellState CellState { get; }

    public MapCell(CellType cellType, (int xpos, int ypos) position, CellState cellState = CellState.Broken)
    {
        CellType = cellType;
        Position = position;
        CellState = cellState;
    }
}

public enum CellType
{
    Ground,
    Tower,
    Wall
}

public enum CellState
{
    Broken,
    Repaired
}


public abstract class MapEvent
{
    public class Initialized : MapEvent
    {
        public Initialized(MapCell[,] mapCells)
        {

        }
    }
}