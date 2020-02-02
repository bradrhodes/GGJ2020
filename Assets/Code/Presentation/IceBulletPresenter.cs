using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceBulletPresenter : MonoBehaviour
{
    Vector3 originPos;
    public GameObject iceCirclePrefab;

    void Start()
    {
        originPos = transform.position;
        Destroy(gameObject, 1f);
    }

    public void Update()
    {
        var currentPos = transform.position;

        if (Vector2.Distance(currentPos, originPos) > 30)
        {
            var icecircle = GameObject.Instantiate(iceCirclePrefab);
            icecircle.transform.position = transform.position;
            GameObject.Destroy(gameObject);
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        var icecircle = GameObject.Instantiate(iceCirclePrefab);
        icecircle.transform.position = transform.position;
        GameObject.Destroy(gameObject);
    }
}
