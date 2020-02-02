using System.Collections.Generic;
using UnityEngine;

public class EnemyPositions : IGetEnemyPositions
{
	private readonly Dictionary<EnemyIdentifier, Vector3> _positions = new Dictionary<EnemyIdentifier, Vector3>();

	public Vector3 this[EnemyIdentifier enemyId]
	{
		get => _positions[enemyId];
		set => _positions[enemyId] = value;
	}
}
