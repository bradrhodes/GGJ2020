using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerPresenter : MonoBehaviour
{
    public GameObject BulletPrefab;

    public float BulletVelocity = 40f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log($"Collision at {collision.transform.position}");

        var toCollision = (collision.transform.position - transform.position).normalized;

        var bullet = GameObject.Instantiate(BulletPrefab);

        var bulletRigidBody = bullet.GetComponent<Rigidbody2D>();

        bulletRigidBody.velocity = toCollision * BulletVelocity;
    }
}
