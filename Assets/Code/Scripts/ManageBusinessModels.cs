using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ManageBusinessModels : MonoBehaviour
{
    [SerializeField] private BusinessModelSO[] businessModels;

    public BusinessModelSO[] GetBusinessModelsIncludingTiles(TileSO[] tileSOs)
    {
        List<BusinessModelSO> relevantBusinessModels = new List<BusinessModelSO>();
        foreach (var businessModel in businessModels)
        {
            if (businessModel.tilesNeeded != null && tileSOs.All(tile => businessModel.tilesNeeded.Contains(tile)))
            {
                relevantBusinessModels.Add(businessModel);
            }
        }
        return relevantBusinessModels.ToArray();
    }
    
    public BusinessModelSO[] GetBusinessModelsIncludingTile(TileSO tileSo)
    {
        List<BusinessModelSO> relevantBusinessModels = new List<BusinessModelSO>();
        foreach (var businessModel in businessModels)
        {
            if (businessModel.tilesNeeded.Contains(tileSo))
            {
                relevantBusinessModels.Add(businessModel);
            }
        }
    
        return relevantBusinessModels.ToArray();
    }
}
