using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPresenter : MonoBehaviour, IHaveIdentity<EnemyIdentifier>
{
	public float Velocity;

	public EnemyIdentifier Id { get; } = EnemyIdentifier.Create();

	// Start is called before the first frame update
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		transform.position += Velocity * transform.up * Time.deltaTime;
	}

	void OnCollisionEnter2D(Collision2D collision)
	{
		Debug.Log("I got hit!");

		GameObject.Destroy(gameObject);
	}
}
