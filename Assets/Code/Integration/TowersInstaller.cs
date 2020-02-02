using UnityEngine;
using Zenject;

public class TowersInstaller : MonoInstaller
{
	public GameObject TowerPrefab;
	public Transform TowersParent;
	public TowerTypeMap TowerTypes;

	public override void InstallBindings()
	{
		Container.Bind<TowersAggregate>().AsSingle();
		Container.Bind<InitializeTowersWhenMapInitialized>().AsSingle().NonLazy();
        Container.Bind<RepairTowersWhenMapCellClicked>().AsSingle().NonLazy();

		Container.BindIFactory<TowerParameters, TowerPresenter>()
			.FromFactory<TowerFactory>();

		Container.BindInstance(TowerTypes);
	}
}