using System;
using UnityEngine;
using Zenject;

public class TowerFactory : IFactory<TowerParameters, TowerPresenter>
{
	private readonly DiContainer _container;
	private readonly TowerTypeMap _typeMap;

	public TowerFactory(DiContainer container, TowerTypeMap typeMap)
	{
		_container = container;
		_typeMap = typeMap;
	}

	public TowerPresenter Create(TowerParameters param)
	{
		var subContainer = _container.CreateSubContainer();

		subContainer.Bind<TowerPresenter>().FromSubContainerResolve().ByInstaller<TowerInstaller>().AsCached();
		subContainer.BindInstance(param);

		return subContainer.InstantiatePrefabForComponent<TowerPresenter>(ChoosePrefab(param.TowerType));
	}

	private GameObject ChoosePrefab(TowerTypes towerType)
	{
		switch (towerType)
		{
			case TowerTypes.Basic:
				return _typeMap.BasicTowerPrefab;
			case TowerTypes.Fire:
				return _typeMap.FireTowerPrefab;
			case TowerTypes.Ice:
				return _typeMap.IceTowerPrefab;
			case TowerTypes.Plasma:
				return _typeMap.PlasmaTowerPrefab;
			default:
				throw new InvalidOperationException($"Unknown Tower Type {towerType}");
		}
	}
}
