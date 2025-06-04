using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TileTooltip : MonoBehaviour
{
    [SerializeField] private TMP_Text Name;
    [SerializeField] private Image Icon;
    [SerializeField] private Camera uiCamera;
    
    private Coroutine hideTooltipCoroutine;
    private void Start()
    {
        gameObject.SetActive(false);
    }
    public void ShowToolTip(TileSO tile)
    {
        Name.text = tile.Name;
        if (tile.icon)
        {
            Icon.sprite = tile.icon;
        }
        DisplayToolTip();

    }
    
    public void ShowToolTip(BusinessModelSO businessModel)
    {
        Name.text = businessModel.Name;
        if (businessModel.Icon)
        {
            Icon.sprite = businessModel.Icon;
        }
        DisplayToolTip();
    }

    private void DisplayToolTip()
    {
        gameObject.SetActive(true);
        MoveToCursor();
        if (hideTooltipCoroutine != null)
        {
            StopCoroutine(hideTooltipCoroutine);
        }
        hideTooltipCoroutine = StartCoroutine(HideTooltipAfterDelay(0.05f));
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
