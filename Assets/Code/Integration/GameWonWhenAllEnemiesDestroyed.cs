using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class GameWonWhenAllEnemiesDestroyed
{
	public GameWonWhenAllEnemiesDestroyed(EnemiesAggregate enemies, GameStatusAggregate gameStatus)
	{
		enemies.Events
			.OfType<EnemiesEvent, EnemiesEvent.AllEnemiesDestroyed>()
			.Subscribe(_ => gameStatus.Win());
	}
}
