using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GridGenerator : MonoBehaviour
{
    
    [SerializeField] int _mapwidth = 5;
    [SerializeField] int _mapheight = 5;
    [SerializeField] GameObject _tilePrefab;
    [SerializeField] private float _tileSize = 2;
    public List<TileHolder> _tiles = new List<TileHolder>();
    
    
    [SerializeField] private TileSO[] RandomPlaceTileSOList;
    
    void Start()
    {
        GenerateMapGrid();
     
    }

    private Vector2 GetHexCoords(int x, int z)
    {
        float xPos = x * _tileSize * Mathf.Cos(Mathf.Deg2Rad * 30);
        float zPos = z * _tileSize + ((x % 2 == 1) ? _tileSize * 0.5f : 0);
        return new Vector2(xPos, zPos);
    }
    
    //todo for random generation, check around if peatlands are adjascent and delete if so? Or also check if there is a certain amount
    //of peatlands in the map if not run it again
    void GenerateMapGrid()
    {
        for (int x = 0; x < _mapwidth; x++)
        {
            for (int z = 0; z < _mapheight; z++)
            {
                Vector2 hexCoords = GetHexCoords(x, z);
                Vector3 position = new Vector3(hexCoords.x, 0, hexCoords.y);
                GameObject tile = Instantiate(_tilePrefab, position, Quaternion.identity);
                tile.transform.SetParent(transform);
                _tiles.Add(tile.GetComponent<TileHolder>());
            }
        }

        SpawnRandomTiles();

        int PeatlandCount = 0;
        foreach (var tile in _tiles )
        {
            if (tile.getTile() != null)
            {
                if (tile.getTile().Name == "Peatland")
                {
                    PeatlandCount++;
                }
            }
        }
        if (PeatlandCount <= 2)
        {
            SpawnRandomTiles();
        }
    }

    private void SpawnRandomTiles()
    {
        //ranndomly place items
        foreach (var tile in _tiles)
        {
            if (Random.Range(0, 8) == 0)
            {
                tile.PlaceTile(RandomPlaceTileSOList[Random.Range(0, RandomPlaceTileSOList.Length)]);
            }
        }
    }
 
    public void CheckForBusinessModels()
    {
        foreach (var tile in _tiles)
        {
            tile.CheckForModel();
        }
    }
    
    public bool CheckIfMapComplete()
    {
        foreach (var tile in _tiles)
        {
            if (tile.getTile() == null)
            {
                return false;
            }
        }
        return true;
    }
}
