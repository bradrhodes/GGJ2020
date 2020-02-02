using UnityEngine;

public class BulletPresenter : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject.Destroy(gameObject);
    }
}
