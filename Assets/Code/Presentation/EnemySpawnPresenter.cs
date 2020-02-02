using System;
using UniRx;
using UnityEngine;
using Zenject;

public class EnemySpawnPresenter : MonoBehaviour
{
	public GameObject EnemyPrefab;

	[Inject]
	public EnemiesAggregate Enemies { private get; set; }

	[Inject]
	public IFactory<EnemyParameters, EnemyPresenter> EnemyFactory { private get; set; }

	private void Start()
	{
		Enemies.Events
			.OfType<EnemiesEvent, EnemiesEvent.EnemySpawned>()
			.Subscribe(enemySpawned => SpawnEnemy(enemySpawned.EnemyId, enemySpawned.Velocity))
			.AddTo(this);
	}

	private void SpawnEnemy(EnemyIdentifier enemyId, float velocity)
	{
		EnemyFactory.Create(new EnemyParameters(enemyId, transform.position, velocity));
	}
}
