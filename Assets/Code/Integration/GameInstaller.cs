using Zenject;

public class GameInstaller : MonoInstaller
{
	public override void InstallBindings()
	{
		Container.Bind<TimeIntegration>().AsSingle().NonLazy();
		Container.Bind<GameStatusAggregate>().AsSingle();

		Container.Bind<GameWonWhenAllEnemiesDestroyed>().AsSingle().NonLazy();
	}
}
