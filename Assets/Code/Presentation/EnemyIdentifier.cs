public class EnemyIdentifier
{
	private static int _nextId;
	private readonly int _id;

	private EnemyIdentifier(int id)
	{
		_id = id;
	}

	public override string ToString()
		=> $"Enemy {_id}";

	public static EnemyIdentifier Create()
		=> new EnemyIdentifier(_nextId++);
}