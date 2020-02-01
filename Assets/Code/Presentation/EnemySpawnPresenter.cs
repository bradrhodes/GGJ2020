using System;
using UniRx;
using UnityEngine;
using Zenject;

public class EnemySpawnPresenter : MonoBehaviour
{
	public GameObject EnemyPrefab;

	[Inject]
	public EnemiesAggregate Enemies { private get; set; }

	private void Start()
	{
		Enemies.Events
			.OfType<EnemiesEvent, EnemiesEvent.EnemySpawned>()
			.Subscribe(_ => SpawnEnemy());
	}

	private void SpawnEnemy()
	{
		Instantiate(EnemyPrefab);
	}
}
