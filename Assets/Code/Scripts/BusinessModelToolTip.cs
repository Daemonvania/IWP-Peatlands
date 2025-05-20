using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class BusinessModelToolTip : MonoBehaviour
{
    [SerializeField] private TMP_Text modelName;
    [SerializeField] private TMP_Text modelStats;
    
    // [SerializeField] private TMPro.TMP_Text _modelDescription;
     [SerializeField] private GameObject tileParent;
    [Space]
    [SerializeField] private GameObject tilePrefabUI;
    
    [SerializeField]
    Camera uiCamera;
    
    private ManageBusinessModels _manageBusinessModels;
    
    
    private void Awake()
    {
        _manageBusinessModels = GameObject.FindGameObjectWithTag("BusinessModelManager").GetComponent<ManageBusinessModels>();
    }
    
    private void Start()
    {
         gameObject.SetActive(false);
    }
    public void ShowToolTip(BusinessModelSO businessModel)
    {
        foreach (Transform child in tileParent.transform) {
            Destroy(child.gameObject);
        }
        modelName.text = businessModel.Name;

        string stats = businessModel.MoneyScore + "<sprite=0>" +" "+ businessModel.EcoScore + "<sprite=1>";
        modelStats.text = stats;
        // _tileDescription.text = businessModel.Description;
        
        foreach (var tile in businessModel.tilesNeeded)
        {
            GameObject tileCard = Instantiate(tilePrefabUI, tileParent.transform);
            Debug.Log(tileCard);
            tileCard.GetComponentInChildren<TMP_Text>().text = tile.Name;
        }
        gameObject.SetActive(true);
    }
    
    public void HideToolTip()
    {
        gameObject.SetActive(false);
        foreach (Transform child in tileParent.transform) {
            Destroy(child.gameObject);
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        if (gameObject.activeSelf) // Only update position if the tooltip is active
        {
            Vector2 localPoint;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                transform.parent.GetComponent<RectTransform>(),
                Input.mousePosition,
                uiCamera,
                out localPoint
            );
            if (Input.mousePosition.x > Screen.width - 500)
            {
                localPoint.x -= 150f;
            }
            else
            {
                localPoint.x += 150f;
            }

            transform.localPosition = localPoint;
        }
    }
}
