using UnityEngine;

public class FireBulletPresenter : MonoBehaviour
{
    private void Update()
    {
        Destroy(gameObject, 3f);
    }
}
