using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPresenter : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }
}
