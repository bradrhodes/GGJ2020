using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class EnemyPresenter : MonoBehaviour, IHaveIdentity<EnemyIdentifier>
{
	public float Velocity;

	[Inject]
	public EnemyParameters Parameters { private get; set; }

	[Inject]
	public EnemyPositions Positions { private get; set; }

	public EnemyIdentifier Id => Parameters.EnemyId;

	void Start()
	{
		transform.position = Parameters.Position;
	}

	void Update()
	{
		transform.position += Velocity * transform.up * Time.deltaTime;
		Positions[Parameters.EnemyId] = transform.position;
	}

	void OnCollisionEnter2D(Collision2D collision)
	{
		Debug.Log($"{Id} got hit!");

		Destroy(gameObject);
	}
}
