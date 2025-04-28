using UnityEngine;

public class EmptyTile : MonoBehaviour, ITile
{
    public string Name { get; set; }

    [SerializeField] private GameObject TileToSpawn;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Initialize the tile name or any other properties if needed
        Name = gameObject.name;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClicked()
    {
        // TileHolder tileHolder = gameObject.GetComponentInParent<TileHolder>();
        //
        // tileHolder._tile = Instantiate(TileToSpawn, transform.position, transform.rotation);
        // tileHolder._tile.transform.SetParent(tileHolder.transform);
        //
        // Destroy(gameObject);
    }

}
