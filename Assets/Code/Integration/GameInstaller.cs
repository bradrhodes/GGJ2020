using System;
using Zenject;

public class GameInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<EnemiesAggregate>().AsSingle().WithArguments(new EnemiesAggregateParameters(3, TimeSpan.FromSeconds(2)));

        Container.Bind<TimeIntegration>().AsSingle().NonLazy();
    }
}
