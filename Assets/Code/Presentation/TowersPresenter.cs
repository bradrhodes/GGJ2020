using UniRx;
using UnityEngine;
using Zenject;

public class TowersPresenter : MonoBehaviour
{
    [Inject]
	public TowersAggregate Towers { private get; set; }

	[Inject]
	public IFactory<InitialTower, TowerInstaller> TowerFactory { private get; set; }

	private void Start()
	{
		Towers.Events
			.OfType<TowersEvent, TowersEvent.TowerRepaired>()
			.Subscribe(repaired => PlaceTower(repaired.MapCoordinate, repaired.Identifier));
	}

	private void PlaceTower(MapCoordinate coordinate, TowerIdentifier towerId)
	{
		TowerFactory.Create(new InitialTower(towerId, coordinate));
	}
}
