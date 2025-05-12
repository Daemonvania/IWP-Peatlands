using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class TileHolder : MonoBehaviour
{
   [SerializeField] public GameObject tileGameObject;
   [SerializeField] private float rayLength = 5f;
   [SerializeField] private GameObject emptyTile;
   [SerializeField] private Material completedMat;
   [SerializeField] private TileTooltip _tileTooltip;
   
   TileSO currentTile = null;
   
   private ManageBusinessModels _manageBusinessModels;
   private ManageCurrencies _manageCurrencies;
   
   private BusinessModelSO currentBusinessModel = null;
   
   public TileSO getTile()
   {
       return currentTile;
   }

   private void Awake()
   {
       _manageBusinessModels = GameObject.FindGameObjectWithTag("BusinessModelManager").GetComponent<ManageBusinessModels>();
         _manageCurrencies = GameObject.FindGameObjectWithTag("CurrencyManager").GetComponent<ManageCurrencies>();
         _tileTooltip = FindFirstObjectByType<TileTooltip>(FindObjectsInactive.Include);

   }

   private void Start()
   {
         // emptyTile.SetActive(true);
   }

   public void OnClicked()
    {
        if (currentBusinessModel == null)
        {
            _tileTooltip.ShowToolTip(currentTile);
        }
        else 
        {
            _tileTooltip.ShowToolTip(currentBusinessModel);
        }
    }

    public void HideTooltip()
    {
        _tileTooltip.HideToolTip();
    }
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
        
        // CheckForModel();
    }

    public void CheckForModel()
    {
        if (currentTile == null || currentBusinessModel != null)
        {
            return;
        }
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
                if (hitTileHolder.currentTile == null || hitTileHolder.currentBusinessModel != null)
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
        
        if (previousTileSOs.Contains(currentTile)) { return;}
        
        previousTileHolders.Add(this);

        previousTileSOs.Add(currentTile);
        
        BusinessModelSO[] relevantBusinessModels = _manageBusinessModels.GetBusinessModelsIncludingTiles(previousTileSOs.ToArray());
        bool matchFound = false;

        if (relevantBusinessModels.Length == 0)
        {
            Debug.Log("No relevant business models found.");
            previousTileHolders.Remove(this);
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
                _manageCurrencies.AddMoney(businessModel.MoneyScore);
                _manageCurrencies.AddEnvironment(businessModel.EcoScore);
                matchFound = true;

                foreach (var tileHolder in previousTileHolders)
                {
                    //todo rough
                    if (tileHolder.currentTile.Name == "Peatland")
                    {
                        continue;
                    }
                    tileHolder.currentBusinessModel = businessModel;
                    Renderer renderer = tileHolder.GetComponentInChildren<Renderer>();
                    if (renderer != null)
                    {
                        // Create a black material dynamically
                        // Material blackMaterial = new Material(Shader.Find("Standard"));
                        // blackMaterial.color = Color.black;

                        // Replace each material in the array with the black material
                        Material[] materials = renderer.materials;
                        for (int i = 0; i < materials.Length; i++)
                        {
                            materials[i] = completedMat;
                        }

                        // Assign the updated materials array back to the renderer
                        renderer.materials = materials;
                    }
                }
                
                break; // Exit the loop as a match is found
            }
        }
        //no business model complete, but business model possible
        if (!matchFound)
        {
            CastRays(previousTileHolders);
            foreach (var tileHolder in previousTileHolders)
            {
                if (tileHolder.transform.position.y <= 0)
                {
                    tileHolder.transform.position = new Vector3(tileHolder.transform.position.x,
                        tileHolder.transform.position.y + 0.22f, tileHolder.transform.position.z);
                }
            }
        }
    }
    
}
