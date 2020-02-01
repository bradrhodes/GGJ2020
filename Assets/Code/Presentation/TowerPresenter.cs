using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class TowerPresenter : MonoBehaviour
{
    public GameObject BulletPrefab;

    public float BulletVelocity = 40f;

    [Inject]
    public InitialTower Parameters { private get; set; }

    private void Start()
    {
        transform.position = new Vector3(Parameters.Coordinate.X, Parameters.Coordinate.Y, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log($"Collision at {collision.transform.position} with {collision.gameObject.name}");

        var toCollision = (collision.transform.position - transform.position).normalized;

        var bullet = GameObject.Instantiate(BulletPrefab);
        bullet.transform.position = transform.position;

        var bulletRigidBody = bullet.GetComponent<Rigidbody2D>();

        bulletRigidBody.velocity = toCollision * BulletVelocity;
    }
}
