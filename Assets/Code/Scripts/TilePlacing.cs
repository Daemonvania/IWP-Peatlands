using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class TilePlacing : MonoBehaviour
{
    [SerializeField] private Camera _camera;

    bool isPlacing = false;
    [HideInInspector] public bool isViewing = true;
    
    TileSO _selectedTile;
    
    //todo these two should not be handled here, but in TurnManager and UIManager
    [SerializeField] private ResultsScreen _resultsScreen;
    
    private GridGenerator _gridGenerator;
    
    
    public event Action<TileSO> OnTurnStarted;
    public event Action OnTurnEnded;

    private void Awake()
    {
        _gridGenerator = GameObject.FindGameObjectWithTag("GridGenerator").GetComponent<GridGenerator>();
        _resultsScreen.gameObject.SetActive(false);
    }
    private void Start()
    {
        OnTurnEnded.Invoke();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isViewing) return;
        Vector3 mousePos = Input.mousePosition;
        Ray ray = _camera.ScreenPointToRay(mousePos);
        if (!Physics.Raycast(ray, out RaycastHit hit)) return;

        TileHolder tileHolder = hit.transform.gameObject.GetComponent<TileHolder>();

        if (Input.GetMouseButtonDown(0))
        {
        if (!tileHolder.isEmpty())
        {
            tileHolder.OnClicked();
        }
        
        if (isPlacing) 
        {
            // tileHolder.OnHover(_selectedTile);
                if (tileHolder.isEmpty())
                {
                    tileHolder.HideTooltip();

                    isPlacing = false;
                    tileHolder.PlaceTile(_selectedTile);
                    _gridGenerator.CheckForBusinessModels();
                    StartCoroutine(EndTurn());
                }
             
            }
        }
    }

    private IEnumerator EndTurn()
    {
        yield return new WaitForSeconds(0.5f);
        if (_gridGenerator.CheckIfMapComplete())
        {
            _resultsScreen.gameObject.SetActive(true);
            _resultsScreen.ShowResults();
        }
        else
        {
            isPlacing = false;
            isViewing = false; 
            OnTurnEnded?.Invoke();
        }
    }
    
    public void SelectTile(TileSO tile)
    {
        _selectedTile = tile;
        isPlacing = true;
        isViewing = true;
        OnTurnStarted?.Invoke(tile);
    }

}
