using UnityEngine;
using UnityEngine.WSA;

public class ShowTileCards : MonoBehaviour
{
    [HideInInspector] private TileSO[] _tiles;
    
    private ShowTileCard[] _tileCards;
    
    private TilePlacing _tilePlacing;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        _tileCards = GetComponentsInChildren<ShowTileCard>();
        _tilePlacing = GameObject.FindGameObjectWithTag("Player").GetComponent<TilePlacing>();
    }


    public void PickTile(int index)
    {
        if (index < 0 || index >= _tileCards.Length)
        {
            Debug.LogError("Index out of range");
            return;
        }

        _tilePlacing.SelectTile(_tiles[index]);
    }

    public void SetHandTiles(TileSO[] tiles)
    {
        _tiles = tiles;
    }


    public void ShowPlayerHand()
    {
        for (int i = 0; i < _tileCards.Length; i++)
        {
            if (_tiles.Length > i)
            {
                _tileCards[i].ShowCard(_tiles[i]);
            }
            else
            {
                // _tileCards[i].gameObject.SetActive(false);
            }
        }
    }
}
