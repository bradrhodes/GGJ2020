using System;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

public class MapPresenter : MonoBehaviour
{
    private struct TileThreshold
    {
        public float Threshold;
        public GameObject Tile;
    }

    private readonly int BOARD_SIZE = 20;

    [SerializeField] private GameObject ground;

    [SerializeField] private GameObject path;
    [SerializeField] private GameObject pathLeftToRight;

    [SerializeField] private GameObject brokenBasicTower;
    [SerializeField] private GameObject brokenFireTower;
    [SerializeField] private GameObject brokenIceTower;
    [SerializeField] private GameObject brokenPlasmaTower;

    [SerializeField] private GameObject brokenWall;

    [Inject]
    public MapAggregate MapAggregate { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        Random.InitState((int)System.DateTime.Now.Ticks);
        MapAggregate.Events.OfType<MapEvent, MapEvent.Initialized>().Subscribe(HandleMapInitializedEvent);
        CreateLevel();
    }

    private void CreateLevel()
    {
        // for now we're just going to ignore different tower types
/*
        TileThreshold[] tileThresholds = new TileThreshold[]{
            new TileThreshold() { Threshold = 0.05f, Tile = brokenBasicTower},
            new TileThreshold() { Threshold = 0.10f, Tile = brokenIceTower},
            new TileThreshold() { Threshold = 0.15f, Tile = brokenFireTower},
            new TileThreshold() { Threshold = 0.20f, Tile = brokenPlasmaTower},
            new TileThreshold() { Threshold = 0.85f, Tile = brokenWall},
            new TileThreshold() { Threshold = 1.00f, Tile = ground},
        };
*/

        MapAggregate.Initialize(BOARD_SIZE, BOARD_SIZE);
    }

    private void HandleMapInitializedEvent(MapEvent.Initialized initializedEvent)
    {
        var map = GetComponent<MapPresenter>().transform;

        for (var x = 0; x < initializedEvent.MapCells.GetLength(0); x++)
        {
            for (var y = 0; y < initializedEvent.MapCells.GetLength(1); y++)
            {
                switch (initializedEvent.MapCells[x,y])
                {
                    case GroundCell cell:
                        Instantiate(ground, new Vector3(x, y, 0), Quaternion.identity, map);
                        break;
                    case TowerCell cell:
                        Instantiate(brokenBasicTower, new Vector3(x, y, 0), Quaternion.identity, map);
                        break;
                    case PathCell cell:
                        Instantiate(path, new Vector3(x, y, 0), Quaternion.identity, map);
                        break;
                    case WallCell cell:
                        Instantiate(brokenWall, new Vector3(x, y, 0), Quaternion.identity, map);
                        break;
                    default:
                        throw new NotImplementedException("Unknown cell type");
                }
            }
        }
    }

    private GameObject GetRandomTile(IEnumerable<TileThreshold> tileThresholds)
    {
        var randomNumber = Random.value;

        return tileThresholds.First(tileThreshold => randomNumber <= tileThreshold.Threshold).Tile;
    }
}
