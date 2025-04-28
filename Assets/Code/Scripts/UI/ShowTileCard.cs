using UnityEngine;

public class ShowTileCard : MonoBehaviour
{
    
    [SerializeField] private TMPro.TMP_Text _tileName;
    [SerializeField] private TMPro.TMP_Text _tileDescription;

    public void ShowCard(TileSO tile)
    {
        _tileName.text = tile.Name;
        _tileDescription.text = tile.Description;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
