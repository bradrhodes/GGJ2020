using UnityEngine;
using Zenject;

public class TowersInstaller : MonoInstaller
{
    public GameObject TowerPrefab;
    public Transform TowersParent;

    public override void InstallBindings()
    {
        Container.Bind<TowersAggregate>().AsSingle();
        Container.Bind<InitializeTowersWhenMapInitialized>().AsSingle().NonLazy();

        Container.BindIFactory<InitialTower, TowerInstaller>()
            .FromSubContainerResolve()
            .ByNewPrefabInstaller<TowerInstaller>(TowerPrefab)
            //.FromComponentInNewPrefab(TowerPrefab)
            .UnderTransform(TowersParent);
    }
}