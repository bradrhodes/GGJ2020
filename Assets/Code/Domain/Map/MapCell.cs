public abstract class MapCell
{
    public CellType CellType { get; }
    (int xpos, int ypos) Position { get; }
    public CellState CellState { get; }

    protected MapCell(CellType cellType, (int xpos, int ypos) position, CellState cellState = CellState.Broken)
    {
        CellType = cellType;
        Position = position;
        CellState = cellState;
    }

    public class Ground : MapCell
    {
        public Ground((int xpos, int ypos) position, CellState cellState = CellState.Broken) : base(CellType.Ground, position, cellState)
        {
        }
    }

    public class Tower : MapCell
    {
        public Tower((int xpos, int ypos) position, CellState cellState = CellState.Broken) : base(CellType.Tower, position, cellState)
        {
        }
    }

    public class Wall : MapCell
    {
        public Wall((int xpos, int ypos) position, CellState cellState = CellState.Broken) : base(CellType.Wall, position, cellState)
        {
        }
    }
}
