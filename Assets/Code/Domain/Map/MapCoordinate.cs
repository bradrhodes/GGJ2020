using System;
using UnityEngine;

public struct MapCoordinate : IEquatable<MapCoordinate>
{
    public MapCoordinate(int x, int y)
    {
        X = x;
        Y = y;
    }

    public int X { get; }
    public int Y { get; }

    public override bool Equals(object obj)
    {
        return obj is MapCoordinate coordinate && Equals(coordinate);
    }

    public bool Equals(MapCoordinate other)
    {
        return X == other.X &&
               Y == other.Y;
    }

    public override int GetHashCode()
    {
        var hashCode = 1861411795;
        hashCode = hashCode * -1521134295 + X.GetHashCode();
        hashCode = hashCode * -1521134295 + Y.GetHashCode();
        return hashCode;
    }

    public static bool operator ==(MapCoordinate left, MapCoordinate right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(MapCoordinate left, MapCoordinate right)
    {
        return !(left == right);
    }
}

public static class MapCoordinateExtensions
{
    public static Vector3 ToVector3(this MapCoordinate coord)
        => new Vector3(coord.X, coord.Y, 0);

    public static MapCoordinate ToMapCoordinate(this Vector3 vector)
        => new MapCoordinate(Mathf.RoundToInt(vector.x), Mathf.RoundToInt(vector.y));
}
