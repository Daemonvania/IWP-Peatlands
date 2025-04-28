using UnityEngine;

[CreateAssetMenu(fileName = "Tile", menuName = "Scriptable Objects/BusinessModel")]
public class BusinessModelSO : ScriptableObject
{
    [SerializeField] TileSO[] tilesNeeded;
    
    [SerializeField] private string Name;
    [SerializeField] private string Description;
    
    [SerializeField] private int MoneyScore;
    [SerializeField] private int EcoScore;
}
