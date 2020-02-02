using System.Collections;
using UnityEngine;

public class IceCirclePresenter : MonoBehaviour
{
    // Update is called once per frame

    float lifeTime = 3;
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
