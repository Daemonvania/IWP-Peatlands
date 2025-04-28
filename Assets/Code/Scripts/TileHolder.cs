using System;
using UnityEngine;
using UnityEngine.WSA;
using Random = UnityEngine.Random;

public class TileHolder : MonoBehaviour
{
   [SerializeField] public GameObject tileGameObject;
   [SerializeField] private float rayLength = 5f;
   [SerializeField] private GameObject emptyTile;
   
   [SerializeField] private TileSO[] tileSOList;
   
   TileSO currentTile = null;

   public TileSO getTile()
   {
       return currentTile;
   }

   private void Start()
   {
       if (Random.Range(0, 6) == 0)
       {
           PlaceTile(tileSOList[Random.Range(0, tileSOList.Length)]);
       }
       else
       { 
           emptyTile.SetActive(true);
       }
   }

   // public void OnClicked(TileSO tileSO)
    // {
    //     if (currentTile == null)
    //     {
    //         PlaceTile(tileSO);
    //     }
    //     else
    //     {
    //         Debug.Log("Tile is already occupied.");
    //     }
    // }
    public bool isEmpty()
    {
        if (currentTile == null)
        {
            return true;
        }

        return false;
    }

    public void PlaceTile(TileSO tileSo)
    {
        currentTile = tileSo;
        emptyTile.SetActive(false);
        tileGameObject = Instantiate(tileSo.asset, transform.position, Quaternion.identity);
        tileGameObject.transform.SetParent(transform);
    }
    
    public void OnHover(TileSO tileSo)
    {
        if (currentTile == null)
        {
           
        }
        else
        {
            Debug.Log("Tile is already occupied.");
        }
    }
    
    

    void CastRays()
    {
        // Define the six directions for a hexagonal grid
        Vector3[] directions = new Vector3[]
        {
            Vector3.forward, // Up
            Quaternion.Euler(0, 60, 0) * Vector3.forward, // Up/Right
            Quaternion.Euler(0, 120, 0) * Vector3.forward, // Down/Right
            Vector3.back, // Down
            Quaternion.Euler(0, -120, 0) * Vector3.forward, // Down/Left
            Quaternion.Euler(0, -60, 0) * Vector3.forward // Up/Left
        };

        foreach (var direction in directions)
        {
            // Perform the raycast
            if (Physics.Raycast(transform.position, direction, out RaycastHit hit, rayLength))
            {
                Debug.Log($"Hit: {hit.collider.name} in direction {direction}");
            }

            // Optional: Visualize the ray
            Debug.DrawRay(transform.position, direction * rayLength, Color.red, 1f);
        }
    }
    
}
