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
			.WithArguments(new EnemiesAggregateParameters(TimeSpan.FromSeconds(2), 2.0f, 0.5f, 3.0f));

		Container.BindInterfacesAndSelfTo<EnemyPositions>()
			.AsSingle();
	}
}
