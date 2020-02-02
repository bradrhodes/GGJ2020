using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class MapAggregate
{
    private Subject<MapEvent> _events = new Subject<MapEvent>();
    private readonly IMapGenerator _mapGenerator;

    public IObservable<MapEvent> Events => _events;

    public void Initialize(int width, int height)
    {
        var map = _mapGenerator.GenerateMap(width, height);
        var startAndGoalCells = GetStartAndGoalCells(map);

        Emit(new MapEvent.Initialized(map, startAndGoalCells.startCell, startAndGoalCells.goalCell));
    }

    private (MapCoordinate startCell, MapCoordinate goalCell) GetStartAndGoalCells(MapCell[,] map)
    {
        MapCoordinate startCell = default;
        MapCoordinate goalCell = default;
        for(var x= 0; x < map.GetLength(0); x++)
        for (var y = 0; y < map.GetLength(0); y++)
        {
            switch (map[x,y])
            {
                    case StartCell cell:
                        startCell = new MapCoordinate(x, y);
                        break;
                    case GoalCell cell:
                        goalCell = new MapCoordinate(x, y);
                        break;
            }
        }

        return (startCell, goalCell);
    }

    private void Emit(MapEvent @event)
        => _events.OnNext(@event);

    public MapAggregate(IMapGenerator mapGenerator)
    {
        _mapGenerator = mapGenerator;
    }

    public void ClickMapCell(int xCoordinate, int yCoordinate)
    {
        Emit(new MapEvent.MapCellClicked(new MapCoordinate(xCoordinate, yCoordinate)));
    }
}
