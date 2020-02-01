public abstract class MapCell
{
    public MapCellStatus Status { get; }

    public MapCell(MapCellStatus status)
    {
        Status = status;
    }
}

public class GroundCell : MapCell
{
    public GroundCell(MapCellStatus status) : base(status)
    {
    }
}

public class TowerCell : MapCell
{
    public TowerCell(MapCellStatus status) : base(status)
    {
    }
}

public class WallCell : MapCell
{
    public WallCell(MapCellStatus status) : base(status)
    {
    }
}

public enum MapCellStatus
{
    Broken,
    Repaired
}
