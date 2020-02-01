using UnityEngine;
using Zenject;

public class TowerDetectorPresenter : MonoBehaviour
{
    [Inject]
    public InitialTower Parameters { private get; set; }

    private void Start()
    {
        Debug.Log($"I am {Parameters.TowerId}");
    }
}
