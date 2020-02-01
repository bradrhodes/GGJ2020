using UnityEngine;
using Zenject;

public class TowerDetectorPresenter : MonoBehaviour
{
	[Inject]
	public InitialTower Parameters { private get; set; }

	[Inject]
	public TowersAggregate Towers { private get; set; }

	private void OnTriggerEnter2D(Collider2D collision)
	{
		var enemy = collision.GetComponent<IHaveIdentity<EnemyIdentifier>>();

		if (enemy is null)
			return;

		Debug.Log($"{enemy.Id} entered {Parameters.TowerId} range");

		Towers.AddEnemy(Parameters.TowerId, enemy.Id);
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		var enemy = collision.GetComponent<IHaveIdentity<EnemyIdentifier>>();

		if (enemy is null)
			return;

		Debug.Log($"{enemy.Id} exited {Parameters.TowerId} range");

		Towers.RemoveEnemy(Parameters.TowerId, enemy.Id);
	}
}
