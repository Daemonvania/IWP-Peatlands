using System;
using System.Collections.Generic;
using DG.Tweening;
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


    //Setting the sound clip variable
    [SerializeField] private AudioClip placeTileSound;
    [SerializeField] private AudioClip businnessModelSound;

    //Making the variables for greyscaling
    [SerializeField]
    [Range(0, 1)]
    float grayscale;
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
        tileGameObject.transform.DOPunchScale(new Vector3(10f, 10f, 10f), 0.3f, 5, 0.25f);
        Debug.Log("Placed tile: " + tileSo.Name);
        // CheckForModel();

        //Play the sound when place tile is called

        SoundFXManager.Instance.PlaySoundFXClip(placeTileSound, transform, 0.3f);
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

                //Play completion sound
                SoundFXManager.Instance.PlaySoundFXClip(businnessModelSound, transform, 0.3f);


                //Grey out the materials of the tile when a business model is completed
                //DarkenMaterials();

                foreach (var tileHolder in previousTileHolders)
                {
                    //todo rough
                    if (tileHolder.currentTile.Name == "Peatland")
                    {
                        continue;
                    }
                    DarkenMaterials(tileHolder.tileGameObject);
                    tileHolder.currentBusinessModel = businessModel;
                    tileHolder.tileGameObject.transform.DOPunchPosition(new Vector3(0, 0.5f, 0), 0.6f, 3, 0.25f);
                    tileHolder.GetComponentInChildren<Tile>().SetCompletedMaterial();
                }
                
                break; // Exit the loop as a match is found
            }
        }
        //no business model complete, but business model possible
        if (!matchFound)
        {
            Debug.Log("No complete business model found, but tiles are connected.");
            CastRays(previousTileHolders);
            foreach (var tileHolder in previousTileHolders)
            {
                if (tileHolder.currentBusinessModel == null)
                {
                    tileHolder.GetComponentInChildren<Tile>().SetInteractingMaterial();

                    if (tileHolder.tileGameObject.transform.position.y < 1)
                    {
                        tileHolder.tileGameObject.transform
                            .DOMove(
                                new Vector3(tileHolder.transform.position.x, tileHolder.transform.position.y + 0.1f,
                                    tileHolder.transform.position.z), 0.15f).SetEase(Ease.Linear);
                    }
                }
            }
        }
    }

    public void DarkenMaterials(GameObject tileGameObject)
    {
       
        Renderer[] renderers = tileGameObject.GetComponentsInChildren<Renderer>();
        foreach (Renderer renderer in renderers)
        {
            
            foreach (Material material in renderer.materials)
            {
                material.color *= new Color(grayscale, grayscale, grayscale);
            }
        }
    }
}
