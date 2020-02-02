using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlasmaBulletPresenter : MonoBehaviour
{
    private void Update()
    {
        Destroy(gameObject, 3f);
    }
}
