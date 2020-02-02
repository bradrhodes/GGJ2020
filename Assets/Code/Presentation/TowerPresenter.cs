using UniRx;
using UnityEngine;
using Zenject;

public class TowerPresenter : MonoBehaviour
{
	[SerializeField] private GameObject brokenBaseSprite;
	[SerializeField] private GameObject baseSprite;
	[SerializeField] private GameObject turretSprite;
	[SerializeField] private GameObject detectorSprite;

	public GameObject BulletPrefab;

	public float BulletVelocity = 40f;

	private EnemyIdentifier _target;

    [SerializeField] private float _reloadTime = 1f;
	private float _remainingReloadTime;

	[Inject]
	public TowerParameters Parameters { private get; set; }

	[Inject]
	public TowersAggregate Towers { private get; set; }

	[Inject]
	public EnemyPositions EnemyPositions { private get; set; }

	private void Start()
	{
		transform.position = new Vector3(Parameters.Coordinate.X, Parameters.Coordinate.Y, 0);

		ShowBrokenTower();

		Towers.Events
			.OfType<TowersEvent, TowersEvent.TowerRepaired>()
			.Where(repaired => repaired.Identifier == Parameters.TowerId)
			.Subscribe(repaired => ShowRepairedTower());

		Towers.Events
			.OfType<TowersEvent, TowersEvent.EnemyTargetted>()
			.Where(enemyTargetted => enemyTargetted.TowerId == Parameters.TowerId)
			.Subscribe(enemyTargetted => TargetEnemy(enemyTargetted.EnemyId));

		Towers.Events
			.OfType<TowersEvent, TowersEvent.EnemyUntargetted>()
			.Where(enemyUntargetted => enemyUntargetted.TowerId == Parameters.TowerId)
			.Subscribe(enemyUntargetted => UntargetEnemy(enemyUntargetted.EnemyId));
	}

	private void ShowBrokenTower()
	{
		brokenBaseSprite.SetActive(true);
		baseSprite.SetActive(false);
		turretSprite.SetActive(false);
		detectorSprite.SetActive(false);
	}

	private void ShowRepairedTower()
	{
		brokenBaseSprite.SetActive(false);
		baseSprite.SetActive(true);
		turretSprite.SetActive(true);
		detectorSprite.SetActive(true);
	}

	private void TargetEnemy(EnemyIdentifier enemyId)
	{
		_target = enemyId;
		//FireBulletAt(enemyId);
	}

	private void UntargetEnemy(EnemyIdentifier enemyId)
	{
		if (_target == enemyId)
			_target = null;
	}

	private void FireBulletAt(EnemyIdentifier enemyId)
	{
		var toEnemy = (EnemyPositions[enemyId] - transform.position).normalized;

		var bullet = Instantiate(BulletPrefab);
		bullet.transform.position = transform.position;
		bullet.transform.rotation = Quaternion.LookRotation(Vector3.forward, toEnemy);

		var bulletRigidBody = bullet.GetComponent<Rigidbody2D>();

		bulletRigidBody.velocity = toEnemy * BulletVelocity;
	}

	private void Update()
	{
		if (_remainingReloadTime > 0)
			_remainingReloadTime -= Time.deltaTime;

		if (_target is null)
			return;

		var toEnemy = (EnemyPositions[_target] - transform.position).normalized;
		turretSprite.transform.rotation = Quaternion.LookRotation(Vector3.forward, toEnemy);

		if (_remainingReloadTime <= 0)
		{
			_remainingReloadTime = _reloadTime;

			FireBulletAt(_target);
		}
	}
}
