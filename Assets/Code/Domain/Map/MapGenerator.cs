using System;
using System.Collections.Generic;
using Assets.Code.Domain.Map;
using JetBrains.Annotations;

public class MapGenerator
{
    private readonly IPathGenerator _pathGenerator;

    public MapGenerator([NotNull] IPathGenerator pathGenerator)
    {
        _pathGenerator = pathGenerator ?? throw new ArgumentNullException(nameof(pathGenerator));
    }

    public MapCell[,] GenerateMap(int width, int height)
    {
        var map = new MapCell[width, height];
        var path = _pathGenerator.GeneratePath(new MapCoordinate(0, 19), new MapCoordinate(0, 0), width, height);

        for (var x = 0; x < width; x++)
        {
            for (var y = 0; y < height; y++)
            {
                map[x, y] = GenerateCell(path, new MapCoordinate(x, y));
            }
        }

        return map;
    }

    private MapCell GenerateCell(Path path, MapCoordinate coordinate)
    {
        if(path.IsStartingPoing(coordinate))
            return new StartCell();
        if(path.IsEndingPoint(coordinate))
            return new GoalCell();
        if(path.IsOnPath(coordinate))
            return new PathCell();

        return GenerateRandomCell();
    }

    private MapCell GenerateRandomCell()
    {
        var randomNumber = (int)(UnityEngine.Random.value * 3);

        switch (randomNumber)
        {
            case 1:
                return new GroundCell();
            case 2:
                return new TowerCell();
            default:
                return new WallCell();
        }
    }
}
