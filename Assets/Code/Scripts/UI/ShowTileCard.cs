using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShowTileCard : MonoBehaviour
{
    
    [SerializeField] private TMPro.TMP_Text _tileName;
    [SerializeField] private Image _tileIcon;
    // [SerializeField] private TMPro.TMP_Text _tileDescription;
    [SerializeField] private GameObject businessModelParent;
    [Space]
    [SerializeField] private GameObject businessModelPrefab;
    private ManageBusinessModels _manageBusinessModels;

    private void Awake()
    {
        _manageBusinessModels = GameObject.FindGameObjectWithTag("BusinessModelManager").GetComponent<ManageBusinessModels>();
        _tileIcon.preserveAspect = true; 
    }

    public void ShowCard(TileSO tile)
    {
        foreach (Transform child in businessModelParent.transform) {
            Destroy(child.gameObject);
        }
        _tileName.text = tile.Name;
        if (tile.icon)
        {
            _tileIcon.sprite = tile.icon;
        }
        
        // _tileDescription.text = tile.Description;
        
        BusinessModelSO[] businessModels = _manageBusinessModels.GetBusinessModelsIncludingTile(tile);
        foreach (var businessModel in businessModels)
        {
            GameObject businessModelCard = Instantiate(businessModelPrefab, businessModelParent.transform);
            businessModelCard.GetComponentInChildren<TMP_Text>().text = businessModel.Name;
            businessModelCard.GetComponentInChildren<BusinessModelUI>().businessModelSo = businessModel;
        }
    }
}
