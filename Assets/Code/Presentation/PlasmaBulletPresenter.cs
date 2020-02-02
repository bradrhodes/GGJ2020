using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlasmaBulletPresenter : MonoBehaviour
{
    private void Start()
    {
        Destroy(gameObject, 3f);
    }
}
