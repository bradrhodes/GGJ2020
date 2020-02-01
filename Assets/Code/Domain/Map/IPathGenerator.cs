namespace Assets.Code.Domain.Map
{
    public interface IPathGenerator
    {
        Path GeneratePath(MapCoordinate startingPoint, MapCoordinate endingPoint, int mapWidth, int mapHeight);
    }
}