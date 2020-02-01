public class MapGenerator
{
    public MapCell[,] GenerateMap(int width, int height)
    {
        var map = new MapCell[width - 1, height - 1];
        for(var x = 0; x < width; x++)
            for(var y = 0; y < height; y++)
            {
                map[x, y] = GenerateTile();
            }

        return map;
    }

    private MapCell GenerateTile()
    {
        var randomNumber = (int)(UnityEngine.Random.value * 3);

        switch (randomNumber)
        {
            case 1:
                return new MapCell(CellType.Ground);
            case 2:
                return new MapCell(CellType.Tower);
            default:
                return new MapCell(CellType.Wall);
        }
    }
}
