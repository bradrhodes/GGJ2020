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

    private void Start()
	{
        Towers.Events
			.OfType<TowersEvent, TowersEvent.Initialized>()
			.Subscribe(initialized => InitializeTowerTypes(initialized.Towers));
	}

	private void InitializeTowerTypes(IEnumerable<TowerParameters> parameters)
	{
        foreach (var parameter in parameters)
        {
            TowerFactory.Create(parameter);
        }
    }
}