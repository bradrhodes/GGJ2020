using System;
using UniRx;
using UnityEngine;
using Zenject;

public class GoalCellPresenter : MonoBehaviour
{
	[Inject]
	public MapAggregate Map { private get; set; }

	[Inject]
	public GameStatusAggregate GameStatus { private get; set; }

	private void Start()
	{
		Map.Events
			.OfType<MapEvent, MapEvent.Initialized>()
			.Subscribe(initialized => InitializeGoalCell(initialized.MapCells));
	}

	private void InitializeGoalCell(MapCell[,] mapCells)
	{
		var goalCoord = FindGoalCoord(mapCells);

		transform.position = new Vector3(goalCoord.X, goalCoord.Y, 0);
	}

	private MapCoordinate FindGoalCoord(MapCell[,] mapCells)
	{
		for (int x = 0; x < mapCells.GetLength(0); x++)
		{
			for (int y = 0; y < mapCells.GetLength(1); y++)
			{
				if (mapCells[x, y] is GoalCell)
				{
					return new MapCoordinate(x, y);
				}
			}
		}

		throw new InvalidOperationException("No Goal Cell Found!");
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		GameStatus.LoseLife();
	}
}
