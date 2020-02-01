using UnityEngine;

public class EnemyParameters
{
	public EnemyParameters(EnemyIdentifier enemyId, Vector3 position)
	{
		EnemyId = enemyId;
		Position = position;
	}

	public EnemyIdentifier EnemyId { get; }
	public Vector3 Position { get; }
}
