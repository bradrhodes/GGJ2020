using Zenject;

public class ScrapWalletInstaller : MonoInstaller
{
	public override void InstallBindings()
	{
		Container.Bind<ScrapWalletAggregate>().AsSingle().WithArguments(new ScrapWalletParameters(1000));
	}
}
