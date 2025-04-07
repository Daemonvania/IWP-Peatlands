using UnityEngine;

public class FarmTile : MonoBehaviour, ITile
{
    public string Name { get; set; }
    
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

    }

}
