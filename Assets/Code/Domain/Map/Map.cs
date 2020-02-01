using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class Map : MonoBehaviour
{
    private readonly int BOARD_SIZE = 20;

    [SerializeField] private GameObject ground;
    
    [SerializeField] private GameObject brokenBasicTower;
    [SerializeField] private GameObject brokenFireTower;
    [SerializeField] private GameObject brokenIceTower;
    [SerializeField] private GameObject brokenPlasmaTower;

    [SerializeField] private GameObject brokenWall;

    private struct TileThreshold
    {
        public float Threshold;
        public GameObject Tile;
    }

    // Start is called before the first frame update
    void Start()
    {
        Random.InitState((int)System.DateTime.Now.Ticks);
        CreateLevel();
    }

    private void CreateLevel()
    {
        TileThreshold[] tileThresholds = new TileThreshold[]{
            new TileThreshold() { Threshold = 0.05f, Tile = brokenBasicTower},
            new TileThreshold() { Threshold = 0.10f, Tile = brokenIceTower},
            new TileThreshold() { Threshold = 0.15f, Tile = brokenFireTower},
            new TileThreshold() { Threshold = 0.20f, Tile = brokenPlasmaTower},
            new TileThreshold() { Threshold = 0.85f, Tile = brokenWall},
            new TileThreshold() { Threshold = 1.00f, Tile = ground},
        };

        var map = GetComponent<Map>().transform;

        for (var x = 0; x < BOARD_SIZE; x++)
        {
            for (var y = 0; y < BOARD_SIZE; y++)
            {
                var tile = GetRandomTile(tileThresholds);

                Instantiate(tile, new Vector3(x, y, 0), Quaternion.identity, map);
            }
        }
    }

    private GameObject GetRandomTile(IEnumerable<TileThreshold> tileThresholds)
    {
        var randomNumber = Random.value;

        return tileThresholds.First(tileThreshold => randomNumber <= tileThreshold.Threshold).Tile;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
