public struct MapCell
{
    public MapCell(CellType cellType)
    {
        CellType = cellType;
    }

    public CellType CellType { get; }
}
