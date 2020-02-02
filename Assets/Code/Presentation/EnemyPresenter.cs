using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class EnemyPresenter : MonoBehaviour, IHaveIdentity<EnemyIdentifier>
{
	public float Velocity;

	private List<Vector3> _path;
	private Vector3 _target;
	private bool _endOfPathReached;

	[Inject]
	public EnemyParameters Parameters { private get; set; }

	[Inject]
	public EnemyPositions Positions { private get; set; }

	[Inject]
	public EnemiesAggregate Enemies { private get; set; }

	public EnemyIdentifier Id => Parameters.EnemyId;

	void Start()
	{
		transform.position = Parameters.Position;

		var s = transform.position;

		_path = new List<Vector3>()
		{
			s + Vector3.right * 2,
			s + Vector3.right * 2 + Vector3.down,
			s + Vector3.right + Vector3.down
		};

		_target = _path[0];
		_path.RemoveAt(0);
	}

	void Update()
	{
		if (_endOfPathReached)
			return;

		var toTarget = _target - transform.position;

		if (toTarget.magnitude < 0.1f)
		{
			transform.position = _target;

			if (_path.Count > 0)
			{
				_target = _path[0];
				_path.RemoveAt(0);
			} else
			{
				_endOfPathReached = true;
			}
		}

		var dir = toTarget.normalized;

		transform.position += Velocity * dir * Time.deltaTime;

		transform.rotation = Quaternion.LookRotation(Vector3.forward, dir);
		
		Positions[Parameters.EnemyId] = transform.position;
	}

	void OnCollisionEnter2D(Collision2D collision)
	{
		Debug.Log($"{Id} got hit!");

		Enemies.Destroy(Parameters.EnemyId);

		Destroy(gameObject);
	}
}
