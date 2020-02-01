using System;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using Zenject;

public class TowersPresenter : MonoBehaviour
{
	public GameObject TowerPrefab;

	[Inject]
	public TowersAggregate Towers { private get; set; }

	private void Start()
	{
		Towers.Events
			.OfType<TowersEvent, TowersEvent.Initialized>()
			.Subscribe(initialized => PlaceTowers(initialized.Towers));
	}

	private void PlaceTowers(IEnumerable<InitialTower> towers)
	{
		foreach (var tower in towers)
		{
			var towerGameObject = Instantiate(TowerPrefab);

			towerGameObject.transform.position = new Vector3(tower.Coordinate.X, tower.Coordinate.Y, 0);
			towerGameObject.transform.parent = transform;
		}
	}
}
