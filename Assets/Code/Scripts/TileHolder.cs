using UnityEngine;

public class TileHolder : MonoBehaviour, ITileHolder
{
   [SerializeField] public GameObject _tile;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public ITile getTile()
    {
        return _tile.GetComponent<ITile>();
    }
    
    public void OnClicked()
    {
        _tile.GetComponent<ITile>().OnClicked();
    }
}
