using UnityEngine;

public class BulletPresenter : MonoBehaviour
{
    private void Start()
    {
        Destroy(gameObject, 3f);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}
