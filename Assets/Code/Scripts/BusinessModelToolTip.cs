using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class BusinessModelToolTip : MonoBehaviour
{
    [SerializeField] private TMP_Text modelName;
    [SerializeField] private TMP_Text modelStats;
    [SerializeField] private Image modelIcon;
    
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
         modelIcon.preserveAspect = true;
    }
    public void ShowToolTip(BusinessModelSO businessModel)
    {
        foreach (Transform child in tileParent.transform) {
            Destroy(child.gameObject);
        }
        modelName.text = businessModel.Name;
        string stats = businessModel.MoneyScore + "<sprite=0>" +" "+ businessModel.EcoScore + "<sprite=1>";
        modelStats.text = stats;
        if (businessModel.Icon != null)
            modelIcon.sprite = businessModel.Icon;
     
        // _tileDescription.text = businessModel.Description;

        ShowTiles(businessModel);
    }

    private void ShowTiles(BusinessModelSO businessModel)
    {
        foreach (var tile in businessModel.tilesNeeded)
        {
            GameObject tileCard = Instantiate(tilePrefabUI, tileParent.transform);
            Debug.Log(tileCard);
            tileCard.GetComponentInChildren<TMP_Text>().text = tile.Name;
            if (tile.icon != null)
            {
                tileCard.GetComponentInChildren<Image>().sprite = tile.icon;
            }
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
        if (!gameObject.activeSelf) return;

        Vector2 localPoint;
        RectTransform canvasRect = transform.parent.GetComponent<RectTransform>();
        RectTransform tooltipRect = GetComponent<RectTransform>();

        // Convert screen point to local point in the parent canvas
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvasRect,
            Input.mousePosition,
            uiCamera,
            out localPoint
        );

        // Offset to the side of the cursor
        if (Input.mousePosition.x > Screen.width - 500)
        {
            localPoint.x -= 150f;
        }
        else
        {
            localPoint.x += 150f;
        }

        // Clamp localPoint to ensure the tooltip stays inside the screen bounds
        Vector2 halfSize = tooltipRect.rect.size * 0.5f;

        // Calculate bounds in local canvas space
        float minX = -canvasRect.rect.width / 2 + halfSize.x;
        float maxX = canvasRect.rect.width / 2 - halfSize.x;
        float minY = -canvasRect.rect.height / 2 + halfSize.y;
        float maxY = canvasRect.rect.height / 2 - halfSize.y;

        localPoint.x = Mathf.Clamp(localPoint.x, minX, maxX);
        localPoint.y = Mathf.Clamp(localPoint.y, minY, maxY);

        transform.localPosition = localPoint;
    }

}
