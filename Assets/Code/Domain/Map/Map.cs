using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using UniRx;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

public class Map : MonoBehaviour
{
    private struct TileThreshold
    {
        public float Threshold;
        public GameObject Tile;
    }

    private struct PathTile : IEquatable<PathTile>
    {
        public int x;
        public int y;

        public bool Equals(PathTile other)
        {
            return x == other.x && y == other.y;
        }

        public override bool Equals(object obj)
        {
            return obj is PathTile other && Equals(other);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (x * 397) ^ y;
            }
        }

        public static bool operator ==(PathTile left, PathTile right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(PathTile left, PathTile right)
        {
            return !left.Equals(right);
        }
    }

    private readonly HashSet<PathTile> _pathTiles = new HashSet<PathTile>()
    {
        new PathTile() {x = 0, y = 19},
        new PathTile() {x = 1, y = 19},
        new PathTile() {x = 1, y = 18},
        new PathTile() {x = 1, y = 17},
        new PathTile() {x = 2, y = 17},
        new PathTile() {x = 3, y = 17},
        new PathTile() {x = 3, y = 18},
        new PathTile() {x = 4, y = 18},
        new PathTile() {x = 5, y = 18},
        new PathTile() {x = 6, y = 18},
        new PathTile() {x = 7, y = 18},
        new PathTile() {x = 7, y = 17},
        new PathTile() {x = 8, y = 17},
        new PathTile() {x = 9, y = 17},
        new PathTile() {x = 9, y = 16},
        new PathTile() {x = 9, y = 15},
        new PathTile() {x = 10, y = 15},
        new PathTile() {x = 11, y = 15},
        new PathTile() {x = 11, y = 16},
        new PathTile() {x = 11, y = 17},
        new PathTile() {x = 11, y = 18},
        new PathTile() {x = 12, y = 18},
        new PathTile() {x = 13, y = 18},
        new PathTile() {x = 14, y = 18},
        new PathTile() {x = 15, y = 18},
        new PathTile() {x = 16, y = 18},
        new PathTile() {x = 17, y = 18},
        new PathTile() {x = 18, y = 18},
        new PathTile() {x = 18, y = 17},
        new PathTile() {x = 18, y = 16},
        new PathTile() {x = 18, y = 15},
        new PathTile() {x = 18, y = 14},
        new PathTile() {x = 17, y = 14},
        new PathTile() {x = 16, y = 14},
        new PathTile() {x = 16, y = 15},
        new PathTile() {x = 16, y = 16},
        new PathTile() {x = 15, y = 16},
        new PathTile() {x = 14, y = 16},
        new PathTile() {x = 13, y = 16},
        new PathTile() {x = 13, y = 15},
        new PathTile() {x = 13, y = 14},
        new PathTile() {x = 13, y = 13},
        new PathTile() {x = 13, y = 12},
        new PathTile() {x = 12, y = 12},
        new PathTile() {x = 11, y = 12},
        new PathTile() {x = 10, y = 12},
        new PathTile() {x = 9, y = 12},
        new PathTile() {x = 8, y = 12},
        new PathTile() {x = 7, y = 12},
        new PathTile() {x = 7, y = 13},
        new PathTile() {x = 7, y = 14},
        new PathTile() {x = 7, y = 15},
        new PathTile() {x = 6, y = 15},
        new PathTile() {x = 5, y = 15},
        new PathTile() {x = 4, y = 15},
        new PathTile() {x = 3, y = 15},
        new PathTile() {x = 2, y = 15},
        new PathTile() {x = 1, y = 15},
        new PathTile() {x = 1, y = 14},
        new PathTile() {x = 1, y = 13},
        new PathTile() {x = 1, y = 12},
        new PathTile() {x = 1, y = 11},
        new PathTile() {x = 1, y = 10},
        new PathTile() {x = 2, y = 10},
        new PathTile() {x = 3, y = 10},
        new PathTile() {x = 3, y = 11},
        new PathTile() {x = 3, y = 12},
        new PathTile() {x = 3, y = 13},
        new PathTile() {x = 4, y = 13},
        new PathTile() {x = 5, y = 13},
        new PathTile() {x = 5, y = 12},
        new PathTile() {x = 5, y = 11},
        new PathTile() {x = 5, y = 10},
        new PathTile() {x = 6, y = 10},
        new PathTile() {x = 7, y = 10},
        new PathTile() {x = 8, y = 10},
        new PathTile() {x = 8, y = 9},
        new PathTile() {x = 8, y = 8},
        new PathTile() {x = 8, y = 7},
        new PathTile() {x = 8, y = 6},
        new PathTile() {x = 8, y = 5},
        new PathTile() {x = 9, y = 5},
        new PathTile() {x = 10, y = 5},
        new PathTile() {x = 10, y = 6},
        new PathTile() {x = 10, y = 7},
        new PathTile() {x = 10, y = 8},
        new PathTile() {x = 10, y = 9},
        new PathTile() {x = 10, y = 10},
        new PathTile() {x = 11, y = 10},
        new PathTile() {x = 12, y = 10},
        new PathTile() {x = 13, y = 10},
        new PathTile() {x = 14, y = 10},
        new PathTile() {x = 15, y = 10},
        new PathTile() {x = 15, y = 11},
        new PathTile() {x = 15, y = 12},
        new PathTile() {x = 16, y = 12},
        new PathTile() {x = 17, y = 12},
        new PathTile() {x = 18, y = 12},
        new PathTile() {x = 18, y = 11},
        new PathTile() {x = 18, y = 10},
        new PathTile() {x = 18, y = 9},
        new PathTile() {x = 18, y = 8},
        new PathTile() {x = 18, y = 7},
        new PathTile() {x = 18, y = 6},
        new PathTile() {x = 18, y = 5},
        new PathTile() {x = 18, y = 4},
        new PathTile() {x = 18, y = 3},
        new PathTile() {x = 18, y = 2},
        new PathTile() {x = 18, y = 1},
        new PathTile() {x = 17, y = 1},
        new PathTile() {x = 16, y = 1},
        new PathTile() {x = 15, y = 1},
        new PathTile() {x = 14, y = 1},
        new PathTile() {x = 14, y = 2},
        new PathTile() {x = 14, y = 3},
        new PathTile() {x = 15, y = 3},
        new PathTile() {x = 16, y = 3},
        new PathTile() {x = 16, y = 4},
        new PathTile() {x = 16, y = 5},
        new PathTile() {x = 16, y = 6},
        new PathTile() {x = 16, y = 7},
        new PathTile() {x = 16, y = 8},
        new PathTile() {x = 15, y = 8},
        new PathTile() {x = 14, y = 8},
        new PathTile() {x = 13, y = 8},
        new PathTile() {x = 12, y = 8},
        new PathTile() {x = 12, y = 7},
        new PathTile() {x = 12, y = 6},
        new PathTile() {x = 12, y = 5},
        new PathTile() {x = 12, y = 4},
        new PathTile() {x = 12, y = 3},
        new PathTile() {x = 12, y = 2},
        new PathTile() {x = 12, y = 1},
        new PathTile() {x = 11, y = 1},
        new PathTile() {x = 10, y = 1},
        new PathTile() {x = 10, y = 2},
        new PathTile() {x = 10, y = 3},
        new PathTile() {x = 9, y = 3},
        new PathTile() {x = 8, y = 3},
        new PathTile() {x = 8, y = 2},
        new PathTile() {x = 8, y = 1},
        new PathTile() {x = 7, y = 1},
        new PathTile() {x = 6, y = 1},
        new PathTile() {x = 6, y = 2},
        new PathTile() {x = 6, y = 3},
        new PathTile() {x = 6, y = 2},
        new PathTile() {x = 6, y = 1},
        new PathTile() {x = 6, y = 2},
        new PathTile() {x = 6, y = 3},
        new PathTile() {x = 6, y = 4},
        new PathTile() {x = 6, y = 5},
        new PathTile() {x = 6, y = 6},
        new PathTile() {x = 6, y = 7},
        new PathTile() {x = 6, y = 8},
        new PathTile() {x = 5, y = 8},
        new PathTile() {x = 4, y = 8},
        new PathTile() {x = 3, y = 8},
        new PathTile() {x = 2, y = 8},
        new PathTile() {x = 1, y = 8},
        new PathTile() {x = 1, y = 7},
        new PathTile() {x = 1, y = 6},
        new PathTile() {x = 2, y = 6},
        new PathTile() {x = 3, y = 6},
        new PathTile() {x = 4, y = 6},
        new PathTile() {x = 4, y = 5},
        new PathTile() {x = 4, y = 4},
        new PathTile() {x = 3, y = 4},
        new PathTile() {x = 2, y = 4},
        new PathTile() {x = 1, y = 4},
        new PathTile() {x = 1, y = 3},
        new PathTile() {x = 1, y = 2},
        new PathTile() {x = 2, y = 2},
        new PathTile() {x = 3, y = 2},
        new PathTile() {x = 4, y = 2},
        new PathTile() {x = 4, y = 1},
        new PathTile() {x = 4, y = 0},
        new PathTile() {x = 3, y = 0},
        new PathTile() {x = 2, y = 0},
        new PathTile() {x = 1, y = 0},
        new PathTile() {x = 0, y = 0},
    };

    private readonly int BOARD_SIZE = 20;

    [SerializeField] private GameObject ground;
    [SerializeField] private GameObject path;
    
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
        var map = GetComponent<Map>().transform;

        for(var x = 0; x < initializedEvent.MapCells.GetLength(0); x++)
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
                    case WallCell cell:
                        Instantiate(brokenWall, new Vector3(x, y, 0), Quaternion.identity, map);
                        break;
                    default:
                        throw new NotImplementedException("Unknown cell type");

                //GameObject tile = _pathTiles.Contains(new PathTile() {x = x, y = y}) 
                //    ? path 
                //    : GetRandomTile(tileThresholds);

                //Instantiate(tile, new Vector3(x, y, 0), Quaternion.identity, map);

            }
        }
    }

    private GameObject GetRandomTile(IEnumerable<TileThreshold> tileThresholds)
    {
        var randomNumber = Random.value;

        return tileThresholds.First(tileThreshold => randomNumber <= tileThreshold.Threshold).Tile;
    }
}
