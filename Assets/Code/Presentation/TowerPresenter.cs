using UniRx;
using UnityEngine;
using Zenject;

public class TowerPresenter : MonoBehaviour
{
	public GameObject BulletPrefab;

	public float BulletVelocity = 40f;

	public GameObject Turret;

	[Inject]
	public TowerParameters Parameters { private get; set; }

	[Inject]
	public TowersAggregate Towers { private get; set; }

	[Inject]
	public EnemyPositions EnemyPositions { private get; set; }

	private void Start()
	{
		transform.position = new Vector3(Parameters.Coordinate.X, Parameters.Coordinate.Y, 0);

		Towers.Events
			.OfType<TowersEvent, TowersEvent.EnemyTargetted>()
			.Where(enemyTargetted => enemyTargetted.TowerId == Parameters.TowerId)
			.Subscribe(enemyTargetted => TargetEnemy(enemyTargetted.EnemyId));

		Towers.Events
			.OfType<TowersEvent, TowersEvent.EnemyUntargetted>()
			.Where(enemyUntargetted => enemyUntargetted.TowerId == Parameters.TowerId)
			.Subscribe(enemyUntargetted => UntargetEnemy(enemyUntargetted.EnemyId));
	}

	private void TargetEnemy(EnemyIdentifier enemyId)
	{
		FireBulletAt(enemyId);
	}

	private void UntargetEnemy(EnemyIdentifier enemyId)
	{

	}

	private void FireBulletAt(EnemyIdentifier enemyId)
	{
		var toCollision = (EnemyPositions[enemyId] - transform.position).normalized;

		var bullet = Instantiate(BulletPrefab);
		bullet.transform.position = transform.position;

		var bulletRigidBody = bullet.GetComponent<Rigidbody2D>();

		bulletRigidBody.velocity = toCollision * BulletVelocity;
	}
}
