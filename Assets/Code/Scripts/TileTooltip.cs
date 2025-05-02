using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TileTooltip : MonoBehaviour
{
    [SerializeField] private TMP_Text peatlandName;
    [SerializeField] private Camera uiCamera;
    private void Start()
    {
        gameObject.SetActive(false);
    }
    public void ShowToolTip(TileSO tile)
    {
        peatlandName.text = tile.Name;
        gameObject.SetActive(true);
        MoveToCursor();
        StartCoroutine(HideTooltipAfterDelay(0.6f));
    }
    
    private IEnumerator HideTooltipAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        HideToolTip();
    }
    public void HideToolTip()
    {
        gameObject.SetActive(false);
    }
    
    void MoveToCursor()
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
            localPoint.x += 100;

            transform.localPosition = localPoint;
        }
    }
}
