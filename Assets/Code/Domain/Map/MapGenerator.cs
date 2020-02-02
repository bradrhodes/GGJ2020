using Assets.Code.Domain.Map;
using JetBrains.Annotations;
using System;

public class MapGenerator : IMapGenerator
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

		AdjustForWater(path, ref map);
		return map;
	}

	private MapCell GenerateCell(Path path, MapCoordinate coordinate)
	{
		if (path.IsStartingPoing(coordinate))
			return new StartCell();
		if (path.IsEndingPoint(coordinate))
			return new GoalCell();
		if (path.IsOnPath(coordinate))
			return new PathCell();

		return GenerateRandomCell();
	}

	private MapCell GenerateRandomCell()
	{
		var randomNumber = UnityEngine.Random.value;

		if (randomNumber <= 0.20)
			return new TowerCell();
		if (randomNumber <= 0.85)
			return new WallCell();

		return new GroundCell();
	}

    private void AdjustForWater(Path path, ref MapCell[,] map)
    {
        foreach (var coordinate in path.PathCoordinates)
        {
            var yToCheck = coordinate.Y - 1;
            if (yToCheck < 0)
                break;

            if (map[coordinate.X, yToCheck] is GroundCell)
                map[coordinate.X, yToCheck] = new WaterCell();
        }
    }
}
