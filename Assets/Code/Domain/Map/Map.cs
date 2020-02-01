using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    public int BoardSize;

    [SerializeField] private GameObject ground;
    [SerializeField] private GameObject tower;
    [SerializeField] private GameObject wall;

    // Start is called before the first frame update
    void Start()
    {
        Random.InitState((int)System.DateTime.Now.Ticks);
        CreateLevel();
    }

    private void CreateLevel()
    {
        var map = GetComponent<Map>().transform;

        for (var x = 0; x < BoardSize; x++)
        {
            for (var y = 0; y < BoardSize; y++)
            {
                var tile = GetRandomTile();

                Instantiate(tile, new Vector3(x, y, 0), Quaternion.identity, map);
            }
        }
    }

    private GameObject GetRandomTile()
    {
        var randomNumber = (int)(Random.value * 3);

        switch (randomNumber)
        {
            case 1:
                return ground;
            case 2:
                return tower;
            default:
                return wall;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
