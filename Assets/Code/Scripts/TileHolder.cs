using UnityEngine;

public class TileHolder : MonoBehaviour, ITileHolder
{
   [SerializeField] public GameObject _tile;
   [SerializeField] private float rayLength = 5f; 
   
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
        CastRays();
        _tile.GetComponent<ITile>().OnClicked();
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
