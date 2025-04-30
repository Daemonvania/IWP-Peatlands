using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.WSA;

public class TilePlacing : MonoBehaviour
{
    [SerializeField] private Camera _camera;

    bool isPlacing = false;
    
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
        Vector3 mousePos = Input.mousePosition;
        Ray ray = _camera.ScreenPointToRay(mousePos);

        if (isPlacing)
        {
            if (!Physics.Raycast(ray, out RaycastHit hit)) return;

            TileHolder tileHolder = hit.transform.gameObject.GetComponent<TileHolder>();

            tileHolder.OnHover(_selectedTile);

            if (Input.GetMouseButtonDown(0))
            {
                if (tileHolder.isEmpty())
                {
                    isPlacing = false;
                    tileHolder.PlaceTile(_selectedTile);
                    _gridGenerator.CheckForBusinessModels();
                    StartCoroutine(EndTurn());

                

                };
            }
        }
    }

    private IEnumerator EndTurn()
    {
        yield return new WaitForSeconds(0.3f);
        if (_gridGenerator.CheckIfMapComplete())
        {
            _resultsScreen.gameObject.SetActive(true);
            _resultsScreen.ShowResults();
        }
        else
        {
            OnTurnEnded?.Invoke();
        }
    }
    
    public void SelectTile(TileSO tile)
    {
        _selectedTile = tile;
        isPlacing = true;
        OnTurnStarted?.Invoke(tile);
    }

}
