using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;
using Zenject;

public class TowersPresenter : MonoBehaviour
{
    [Inject]
	public TowersAggregate Towers { private get; set; }

	[Inject]
	public IFactory<TowerParameters, TowerPresenter> TowerFactory { private get; set; }

	private Dictionary<TowerIdentifier, TowerParameters> _towerParameters;

	private void Start()
	{
		Towers.Events
			.OfType<TowersEvent, TowersEvent.TowerRepaired>()
			.Subscribe(repaired => PlaceTower(repaired.Identifier));

		Towers.Events
			.OfType<TowersEvent, TowersEvent.Initialized>()
			.Subscribe(initialized => InitializeTowerTypes(initialized.Towers));
	}

	private void InitializeTowerTypes(IEnumerable<TowerParameters> parameters)
	{
		_towerParameters = parameters.ToDictionary(parameter => parameter.TowerId);
	}

	private void PlaceTower(TowerIdentifier towerId)
	{
		TowerFactory.Create(_towerParameters[towerId]);
	}
}