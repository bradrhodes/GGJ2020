using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlamePresenter : MonoBehaviour
{
    // Update is called once per frame

    float lifeTime = 1;
    void Start()
    {
        StartCoroutine(WaitThenDie());
    }
    IEnumerator WaitThenDie()
    {
        yield return new WaitForSeconds(lifeTime);
        Destroy(gameObject);
    }
    void Update()
    {

    }
}
