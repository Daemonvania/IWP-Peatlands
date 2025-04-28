using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Tile", menuName = "Scriptable Objects/Tile")]
public class TileSO : ScriptableObject
{
    [SerializeField] public string Name;
    [SerializeField] public string Description;
    
    [SerializeField] public GameObject asset;
    
    [SerializeField] public Image icon;
}
