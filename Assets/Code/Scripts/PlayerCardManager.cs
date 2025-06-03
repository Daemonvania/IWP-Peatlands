using System.Collections.Generic;
using UnityEngine;

public class PlayerCardManager : MonoBehaviour
{
    public List<TileSO> tileList = new List<TileSO>(); // List of tiles for the player
    private List<TileSO> handTiles = new List<TileSO>();
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        ClearHandAndDrawTiles();
    }

    public void ClearHandAndDrawTiles()
    {
        handTiles.Clear();
        List<TileSO> tempList = new List<TileSO>(tileList);

        for (int i = 0; i < 3 && tempList.Count > 0; i++)
        {
            int randomIndex = Random.Range(0, tempList.Count);
            TileSO drawnTile = tempList[randomIndex];

            handTiles.Add(drawnTile);
            tempList.RemoveAt(randomIndex); // Remove from tempList to prevent duplicates
        }
    }
    // // Update is called once per frame
    // public void DrawTile()
    // {
    //     if (tileList.Count > 0 && handTiles.Count < 4)
    //     {
    //         int randomIndex = Random.Range(0, tileList.Count);
    //         TileSO drawnTile = tileList[randomIndex];
    //         handTiles.Add(drawnTile);
    //     }
    // }
    //
    // public void ReplaceTile(TileSO placedTile)
    // {
    //     // if (handTiles.Contains(placedTile))
    //     // {
    //     //     DrawTile(); // Draw a new tile into the hand
    //     //     handTiles.Remove(placedTile); // Remove the placed tile from hand
    //     // }
    //     handTiles.Clear();
    //     for (int i = 0; i < 3; i++)
    //     {
    //         DrawTile();
    //     }
    // }

    public TileSO[] GetHandTiles()
    {
        return handTiles.ToArray();
    }
}
