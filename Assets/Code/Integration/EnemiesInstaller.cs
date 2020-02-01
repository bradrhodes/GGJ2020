using System;
using UnityEngine;
using Zenject;

public class EnemiesInstaller : MonoInstaller
{
	public GameObject EnemyPrefab;
	public Transform EnemiesParent;
	
	public override void InstallBindings()
	{
		Container.BindIFactory<EnemyParameters, EnemyPresenter>()
			.FromComponentInNewPrefab(EnemyPrefab)
			.UnderTransform(EnemiesParent);

		Container.Bind<EnemiesAggregate>()
			.AsSingle()
			.WithArguments(new EnemiesAggregateParameters(3, TimeSpan.FromSeconds(2)));

		Container.Bind<EnemyPositions>()
			.AsSingle();
	}
}
