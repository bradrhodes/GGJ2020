using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPresenter : MonoBehaviour
{
    public float Velocity;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Velocity * transform.up * Time.deltaTime;
    }
}
