using UnityEngine;

public class GridGenerator : MonoBehaviour
{
    
    int _mapwidth = 10;
    int _mapheight = 10;
    [SerializeField] GameObject _tilePrefab;
    [SerializeField] private int _tileSize = 2;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GenerateMapGrid();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private Vector2 GetHexCoords(int x, int z)
    {
        float xPos = x * _tileSize * Mathf.Cos(Mathf.Deg2Rad * 30);
        float zPos = z * _tileSize + ((x % 2 == 1) ? _tileSize * 0.5f : 0);
        return new Vector2(xPos, zPos);
    }
    
    void GenerateMapGrid()
    {
        for (int x = 0; x < _mapwidth; x++)
        {
            for (int z = 0; z < _mapheight; z++)
            {
                Vector2 hexCoords = GetHexCoords(x, z);
                Vector3 position = new Vector3(hexCoords.x, 0, hexCoords.y);
                GameObject tile = Instantiate(_tilePrefab, position, Quaternion.identity);
            }
        }
    }
}
