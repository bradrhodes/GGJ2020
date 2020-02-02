using System;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

public class MapPresenter : MonoBehaviour
{
    private readonly int BOARD_SIZE = 20;

    [SerializeField] private GameObject ground;

    [SerializeField] private GameObject pathLeftToBottom;
    [SerializeField] private GameObject pathLeftToRight;
    [SerializeField] private GameObject pathLeftToTop;
    [SerializeField] private GameObject pathTopToRight;
    [SerializeField] private GameObject pathTopToBottom;
    [SerializeField] private GameObject pathRightToBottom;

    [SerializeField] private GameObject brokenWall;
    [SerializeField] private GameObject wall;
    [SerializeField] private GameObject water;

    [SerializeField] private GameObject spawner;
    [SerializeField] private GameObject castle;

    private GameObject[,] _tiles;

    [Inject]
    public MapAggregate MapAggregate { get; set; }

    [Inject]
    public WallsAggregate WallsAggregate { get; set; }

    [Inject]
    public TowersAggregate TowersAggregate { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        Random.InitState((int)System.DateTime.Now.Ticks);
        MapAggregate.Events.OfType<MapEvent, MapEvent.Initialized>().Subscribe(HandleMapInitializedEvent);
        WallsAggregate.Events.OfType<WallsEvent, WallsEvent.WallRepaired>().Subscribe(HandleWallRepairedEvent);

        Observable.NextFrame().Subscribe(_ => CreateLevel());
    }

    void Update()
    {
        //Handle clicks
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 clickedPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            var xCoordinate = (int)Math.Round(clickedPosition.x);
            var yCoordinate = (int)Math.Round(clickedPosition.y);

            MapAggregate.ClickMapCell(xCoordinate, yCoordinate);
        }
    }

    private void CreateLevel()
    {
        MapAggregate.Initialize(BOARD_SIZE, BOARD_SIZE);
    }

    private void HandleMapInitializedEvent(MapEvent.Initialized initializedEvent)
    {
        _tiles = new GameObject[initializedEvent.MapCells.GetLength(0), initializedEvent.MapCells.GetLength(1)];

        for (var x = 0; x < initializedEvent.MapCells.GetLength(0); x++)
        {
            for (var y = 0; y < initializedEvent.MapCells.GetLength(1); y++)
            {
                switch (initializedEvent.MapCells[x, y])
                {
                    case GroundCell cell:
                        _tiles[x, y] = Instantiate(ground, new Vector3(x, y, 0), Quaternion.identity, transform);
                        break;
                    case TowerCell cell:
                        break;
                    case PathCell cell:
                        var pathTile = ChoosePathSprite(initializedEvent, x, y);
                        _tiles[x, y] = Instantiate(pathTile, new Vector3(x, y, 0), Quaternion.identity, transform);
                        break;
                    case WallCell cell:
                        _tiles[x, y] = Instantiate(brokenWall, new Vector3(x, y, 0), Quaternion.identity, transform);
                        break;
                    case StartCell cell:
                        _tiles[x, y] = Instantiate(spawner, new Vector3(x, y, 0), Quaternion.identity, transform);
                        break;
                    case GoalCell cell:
                        _tiles[x, y] = Instantiate(castle, new Vector3(x, y, 0), Quaternion.identity, transform);
                        break;
                    case WaterCell cell:
                        _tiles[x, y] = Instantiate(water, new Vector3(x, y, 0), Quaternion.identity, transform);
                        break;
                    default:
                        throw new NotImplementedException("Unknown cell type");
                }
            }
        }
    }

    private GameObject ChoosePathSprite(MapEvent.Initialized initializedEvent, int x, int y)
    {
        //Start of path
        if (x == 1 && y == 19)
        {
            return pathLeftToBottom;
        }
        //End of path
        else if (x == 1 && y == 0)
        {
            return pathLeftToRight;
        }
        //Other path tiles
        else
        {
            //Path to the left
            if (x != 0 && initializedEvent.MapCells[x - 1, y] is PathCell)
            {
                //Path above
                if (y != 19 && initializedEvent.MapCells[x, y + 1] is PathCell)
                {
                    return pathLeftToTop;
                }

                //Path to the right
                else if (x != 19 && initializedEvent.MapCells[x + 1, y] is PathCell)
                {
                    return pathLeftToRight;
                }

                //Must be path below
                else
                {
                    return pathLeftToBottom;
                }

            }
            //There exists a path to the top
            else if (y != 19 && initializedEvent.MapCells[x, y + 1] is PathCell)
            {
                //Path to the right
                if (x != 19 && initializedEvent.MapCells[x + 1, y] is PathCell)
                {
                    return pathTopToRight;
                }

                //Must be path below
                else
                {
                    return pathTopToBottom;
                }
            }
            //Must be path to the right and below
            else
            {
                return pathRightToBottom;
            }
        }
    }

    private void HandleWallRepairedEvent(WallsEvent.WallRepaired repairedEvent)
    {
        var x = repairedEvent.Coordinate.X;
        var y = repairedEvent.Coordinate.Y;

        Destroy(_tiles[x, y]);
        _tiles[x, y] = Instantiate(wall, new Vector3(x, y, 0), Quaternion.identity, transform);
    }

    //private void HandleTowerRepairedEvent(TowersEvent.TowerRepaired repairedEvent)
    //{
    //    var x = repairedEvent.MapCoordinate.X;
    //    var y = repairedEvent.MapCoordinate.Y;

    //    Destroy(_tiles[x, y]);
    //    _tiles[x, y] = Instantiate(basicTowerBase, new Vector3(x, y, 0), Quaternion.identity, transform);
    //}
}
