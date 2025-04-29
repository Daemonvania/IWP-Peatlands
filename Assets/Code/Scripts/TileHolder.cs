using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.WSA;
using Random = UnityEngine.Random;

public class TileHolder : MonoBehaviour
{
   [SerializeField] public GameObject tileGameObject;
   [SerializeField] private float rayLength = 5f;
   [SerializeField] private GameObject emptyTile;
   
   [SerializeField] private TileSO[] RandomPlaceTileSOList;
   
   TileSO currentTile = null;
   
   private ManageBusinessModels _manageBusinessModels;
   
   private bool isInBusinessModel = false;
   
   public TileSO getTile()
   {
       return currentTile;
   }

   private void Awake()
   {
       _manageBusinessModels = GameObject.FindGameObjectWithTag("BusinessModelManager").GetComponent<ManageBusinessModels>();
   }

   private void Start()
   {
       if (Random.Range(0, 6) == 0)
       {
           // PlaceTile(RandomPlaceTileSOList[Random.Range(0, RandomPlaceTileSOList.Length)]);
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
        
        
        List<TileHolder> tileHolders = new List<TileHolder>();
        tileHolders.Add(this);
        CastRays(tileHolders);
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
    
    
 //todo on turn start empty the tileSOList 
    void CastRays(List<TileHolder> previousTileHolders)
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
                if (hit.transform == transform) { continue; }
                Debug.DrawRay(transform.position, direction * rayLength, Color.red, 1f);
                
                TileHolder hitTileHolder = hit.transform.GetComponent<TileHolder>();
                if (hitTileHolder.currentTile == null || hitTileHolder.isInBusinessModel)
                {
                    continue;
                }
                hitTileHolder.OnRayHit(previousTileHolders);
            }
            
        }
    }
    
    void OnRayHit(List<TileHolder> previousTileHolders)
    {
        List<TileSO> previousTileSOs = new List<TileSO>();
        foreach (var tileHolder in previousTileHolders)
        {
            if (tileHolder.currentTile != null)
            {
                previousTileSOs.Add(tileHolder.currentTile);
            }
        }
        previousTileHolders.Add(this);
        
        if (previousTileSOs.Contains(currentTile)) { return;}
        
        previousTileSOs.Add(currentTile);

        foreach (var tile in previousTileSOs)
        {
            Debug.Log(tile.Name);    
        }
        
        Debug.Log(previousTileSOs.Count);
        BusinessModelSO[] relevantBusinessModels = _manageBusinessModels.GetBusinessModelsIncludingTiles(previousTileSOs.ToArray());
        bool matchFound = false;

        if (relevantBusinessModels.Length == 0)
        {
            Debug.Log("No relevant business models found.");
            return;
        }
        
        foreach (var businessModel in relevantBusinessModels)
        {
            HashSet<TileSO> tileSet = new HashSet<TileSO>(previousTileSOs);
            HashSet<TileSO> neededSet = new HashSet<TileSO>(businessModel.tilesNeeded);

            if (tileSet.SetEquals(neededSet))
            {
                // Business Model Complete
                Debug.Log($"Tiles match for business model: {businessModel.Name}");
                matchFound = true;

                foreach (var timeHolder in previousTileHolders)
                {
                    timeHolder.isInBusinessModel = true;
                    timeHolder.gameObject.SetActive(false);
                }
                
                break; // Exit the loop as a match is found
            }
        }

        if (!matchFound)
        {
            CastRays(previousTileHolders);
        }
    }
    
}
