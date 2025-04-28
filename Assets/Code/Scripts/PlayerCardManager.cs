using System.Collections.Generic;
using UnityEngine;

public class PlayerCardManager : MonoBehaviour
{
    public List<TileSO> tileList = new List<TileSO>(); // List of tiles for the player
    private List<TileSO> handTiles = new List<TileSO>();
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (int i = 0; i < 3; i++)
        {
            DrawTile();
        }
    }

    // Update is called once per frame
    public void DrawTile()
    {
        if (tileList.Count > 0 && handTiles.Count < 4)
        {
            int randomIndex = Random.Range(0, tileList.Count);
            TileSO drawnTile = tileList[randomIndex];
            handTiles.Add(drawnTile);
        }
    }
    
    public void ReplaceTile(TileSO placedTile)
    {
        if (handTiles.Contains(placedTile))
        {
            DrawTile(); // Draw a new tile into the hand
            handTiles.Remove(placedTile); // Remove the placed tile from hand
        }
    }

    public TileSO[] GetHandTiles()
    {
        return handTiles.ToArray();
    }
}
