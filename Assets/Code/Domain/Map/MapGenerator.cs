using System;
using System.Collections.Generic;

public class MapGenerator
{
    private struct PathTile : IEquatable<PathTile>
    {
        public int x;
        public int y;

        public bool Equals(PathTile other)
        {
            return x == other.x && y == other.y;
        }

        public override bool Equals(object obj)
        {
            return obj is PathTile other && Equals(other);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (x * 397) ^ y;
            }
        }

        public static bool operator ==(PathTile left, PathTile right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(PathTile left, PathTile right)
        {
            return !left.Equals(right);
        }
    }

    private readonly HashSet<PathTile> _pathTiles = new HashSet<PathTile>()
    {
        new PathTile() {x = 0, y = 19},
        new PathTile() {x = 1, y = 19},
        new PathTile() {x = 1, y = 18},
        new PathTile() {x = 1, y = 17},
        new PathTile() {x = 2, y = 17},
        new PathTile() {x = 3, y = 17},
        new PathTile() {x = 3, y = 18},
        new PathTile() {x = 4, y = 18},
        new PathTile() {x = 5, y = 18},
        new PathTile() {x = 6, y = 18},
        new PathTile() {x = 7, y = 18},
        new PathTile() {x = 7, y = 17},
        new PathTile() {x = 8, y = 17},
        new PathTile() {x = 9, y = 17},
        new PathTile() {x = 9, y = 16},
        new PathTile() {x = 9, y = 15},
        new PathTile() {x = 10, y = 15},
        new PathTile() {x = 11, y = 15},
        new PathTile() {x = 11, y = 16},
        new PathTile() {x = 11, y = 17},
        new PathTile() {x = 11, y = 18},
        new PathTile() {x = 12, y = 18},
        new PathTile() {x = 13, y = 18},
        new PathTile() {x = 14, y = 18},
        new PathTile() {x = 15, y = 18},
        new PathTile() {x = 16, y = 18},
        new PathTile() {x = 17, y = 18},
        new PathTile() {x = 18, y = 18},
        new PathTile() {x = 18, y = 17},
        new PathTile() {x = 18, y = 16},
        new PathTile() {x = 18, y = 15},
        new PathTile() {x = 18, y = 14},
        new PathTile() {x = 17, y = 14},
        new PathTile() {x = 16, y = 14},
        new PathTile() {x = 16, y = 15},
        new PathTile() {x = 16, y = 16},
        new PathTile() {x = 15, y = 16},
        new PathTile() {x = 14, y = 16},
        new PathTile() {x = 13, y = 16},
        new PathTile() {x = 13, y = 15},
        new PathTile() {x = 13, y = 14},
        new PathTile() {x = 13, y = 13},
        new PathTile() {x = 13, y = 12},
        new PathTile() {x = 12, y = 12},
        new PathTile() {x = 11, y = 12},
        new PathTile() {x = 10, y = 12},
        new PathTile() {x = 9, y = 12},
        new PathTile() {x = 8, y = 12},
        new PathTile() {x = 7, y = 12},
        new PathTile() {x = 7, y = 13},
        new PathTile() {x = 7, y = 14},
        new PathTile() {x = 7, y = 15},
        new PathTile() {x = 6, y = 15},
        new PathTile() {x = 5, y = 15},
        new PathTile() {x = 4, y = 15},
        new PathTile() {x = 3, y = 15},
        new PathTile() {x = 2, y = 15},
        new PathTile() {x = 1, y = 15},
        new PathTile() {x = 1, y = 14},
        new PathTile() {x = 1, y = 13},
        new PathTile() {x = 1, y = 12},
        new PathTile() {x = 1, y = 11},
        new PathTile() {x = 1, y = 10},
        new PathTile() {x = 2, y = 10},
        new PathTile() {x = 3, y = 10},
        new PathTile() {x = 3, y = 11},
        new PathTile() {x = 3, y = 12},
        new PathTile() {x = 3, y = 13},
        new PathTile() {x = 4, y = 13},
        new PathTile() {x = 5, y = 13},
        new PathTile() {x = 5, y = 12},
        new PathTile() {x = 5, y = 11},
        new PathTile() {x = 5, y = 10},
        new PathTile() {x = 6, y = 10},
        new PathTile() {x = 7, y = 10},
        new PathTile() {x = 8, y = 10},
        new PathTile() {x = 8, y = 9},
        new PathTile() {x = 8, y = 8},
        new PathTile() {x = 8, y = 7},
        new PathTile() {x = 8, y = 6},
        new PathTile() {x = 8, y = 5},
        new PathTile() {x = 9, y = 5},
        new PathTile() {x = 10, y = 5},
        new PathTile() {x = 10, y = 6},
        new PathTile() {x = 10, y = 7},
        new PathTile() {x = 10, y = 8},
        new PathTile() {x = 10, y = 9},
        new PathTile() {x = 10, y = 10},
        new PathTile() {x = 11, y = 10},
        new PathTile() {x = 12, y = 10},
        new PathTile() {x = 13, y = 10},
        new PathTile() {x = 14, y = 10},
        new PathTile() {x = 15, y = 10},
        new PathTile() {x = 15, y = 11},
        new PathTile() {x = 15, y = 12},
        new PathTile() {x = 16, y = 12},
        new PathTile() {x = 17, y = 12},
        new PathTile() {x = 18, y = 12},
        new PathTile() {x = 18, y = 11},
        new PathTile() {x = 18, y = 10},
        new PathTile() {x = 18, y = 9},
        new PathTile() {x = 18, y = 8},
        new PathTile() {x = 18, y = 7},
        new PathTile() {x = 18, y = 6},
        new PathTile() {x = 18, y = 5},
        new PathTile() {x = 18, y = 4},
        new PathTile() {x = 18, y = 3},
        new PathTile() {x = 18, y = 2},
        new PathTile() {x = 18, y = 1},
        new PathTile() {x = 17, y = 1},
        new PathTile() {x = 16, y = 1},
        new PathTile() {x = 15, y = 1},
        new PathTile() {x = 14, y = 1},
        new PathTile() {x = 14, y = 2},
        new PathTile() {x = 14, y = 3},
        new PathTile() {x = 15, y = 3},
        new PathTile() {x = 16, y = 3},
        new PathTile() {x = 16, y = 4},
        new PathTile() {x = 16, y = 5},
        new PathTile() {x = 16, y = 6},
        new PathTile() {x = 16, y = 7},
        new PathTile() {x = 16, y = 8},
        new PathTile() {x = 15, y = 8},
        new PathTile() {x = 14, y = 8},
        new PathTile() {x = 13, y = 8},
        new PathTile() {x = 12, y = 8},
        new PathTile() {x = 12, y = 7},
        new PathTile() {x = 12, y = 6},
        new PathTile() {x = 12, y = 5},
        new PathTile() {x = 12, y = 4},
        new PathTile() {x = 12, y = 3},
        new PathTile() {x = 12, y = 2},
        new PathTile() {x = 12, y = 1},
        new PathTile() {x = 11, y = 1},
        new PathTile() {x = 10, y = 1},
        new PathTile() {x = 10, y = 2},
        new PathTile() {x = 10, y = 3},
        new PathTile() {x = 9, y = 3},
        new PathTile() {x = 8, y = 3},
        new PathTile() {x = 8, y = 2},
        new PathTile() {x = 8, y = 1},
        new PathTile() {x = 7, y = 1},
        new PathTile() {x = 6, y = 1},
        new PathTile() {x = 6, y = 2},
        new PathTile() {x = 6, y = 3},
        new PathTile() {x = 6, y = 2},
        new PathTile() {x = 6, y = 1},
        new PathTile() {x = 6, y = 2},
        new PathTile() {x = 6, y = 3},
        new PathTile() {x = 6, y = 4},
        new PathTile() {x = 6, y = 5},
        new PathTile() {x = 6, y = 6},
        new PathTile() {x = 6, y = 7},
        new PathTile() {x = 6, y = 8},
        new PathTile() {x = 5, y = 8},
        new PathTile() {x = 4, y = 8},
        new PathTile() {x = 3, y = 8},
        new PathTile() {x = 2, y = 8},
        new PathTile() {x = 1, y = 8},
        new PathTile() {x = 1, y = 7},
        new PathTile() {x = 1, y = 6},
        new PathTile() {x = 2, y = 6},
        new PathTile() {x = 3, y = 6},
        new PathTile() {x = 4, y = 6},
        new PathTile() {x = 4, y = 5},
        new PathTile() {x = 4, y = 4},
        new PathTile() {x = 3, y = 4},
        new PathTile() {x = 2, y = 4},
        new PathTile() {x = 1, y = 4},
        new PathTile() {x = 1, y = 3},
        new PathTile() {x = 1, y = 2},
        new PathTile() {x = 2, y = 2},
        new PathTile() {x = 3, y = 2},
        new PathTile() {x = 4, y = 2},
        new PathTile() {x = 4, y = 1},
        new PathTile() {x = 4, y = 0},
        new PathTile() {x = 3, y = 0},
        new PathTile() {x = 2, y = 0},
        new PathTile() {x = 1, y = 0},
        new PathTile() {x = 0, y = 0},
    };


    public MapCell[,] GenerateMap(int width, int height)
    {
        var map = new MapCell[width, height];
        for (var x = 0; x < width; x++)
        {
            for (var y = 0; y < height; y++)
            {
                map[x, y] = _pathTiles.Contains(new PathTile() {x = x, y = y}) 
                    ? new PathCell(MapCellStatus.Repaired) 
                    : GenerateTile();
            }
        }

        return map;
    }

    private MapCell GenerateTile()
    {
        var randomNumber = (int)(UnityEngine.Random.value * 3);

        switch (randomNumber)
        {
            case 1:
                return new GroundCell(MapCellStatus.Broken);
            case 2:
                return new TowerCell(MapCellStatus.Broken);
            default:
                return new WallCell(MapCellStatus.Broken);
        }
    }
}
