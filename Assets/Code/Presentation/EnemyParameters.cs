using UnityEngine;

public class EnemyParameters
{
	public EnemyParameters(EnemyIdentifier enemyId, Vector3 position, float velocity)
	{
		EnemyId = enemyId;
		Position = position;
		Velocity = velocity;
	}

	public EnemyIdentifier EnemyId { get; }
	public Vector3 Position { get; }
	public float Velocity { get; }
}
