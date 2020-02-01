using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameTowerPresenter : MonoBehaviour
{
    public GameObject FlamePrefab;

    public float FlameVelocity = 40f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log($"Collision at {collision.transform.position}");

        var toCollision = (collision.transform.position - transform.position).normalized;

            var Flame = GameObject.Instantiate(FlamePrefab);

            var FlameRigidBody = Flame.GetComponent<Rigidbody2D>();

            FlameRigidBody.velocity = toCollision * FlameVelocity;

    }

}

