using UnityEngine;

[CreateAssetMenu(fileName = "Tile", menuName = "Scriptable Objects/BusinessModel")]
public class BusinessModelSO : ScriptableObject
{
    [SerializeField] public TileSO[] tilesNeeded;
    
    [SerializeField] public string Name;
    [SerializeField] public string Description;
    [SerializeField] public Sprite Icon;
    [Space]
    [SerializeField] public  int MoneyScore;
    [SerializeField] public  int EcoScore;
}
