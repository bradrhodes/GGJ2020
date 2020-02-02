public class TestMapGenerator : IMapGenerator
{
	public MapCell[,] GenerateMap(int width, int height)
	{
		return new MapCell[,]
		{
			{ new GroundCell(), new GroundCell(), new GroundCell() },
			{ new GroundCell(), new WallCell(), new TowerCell() },
			{ new GroundCell(), new GroundCell(), new GroundCell() },
		};

	}
}
