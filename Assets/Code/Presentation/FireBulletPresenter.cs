using UnityEngine;

public class FireBulletPresenter : MonoBehaviour
{
    private void Start()
    {
        Destroy(gameObject, 1.5f);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}
