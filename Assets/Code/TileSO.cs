using UnityEngine;

[CreateAssetMenu(fileName = "Tile", menuName = "Scriptable Objects/Tile")]
public class TileSO : ScriptableObject
{
    [SerializeField] string Name;
    [SerializeField] private string Description;
    
    [SerializeField] GameObject prefab;
}
