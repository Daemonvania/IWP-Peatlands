using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
public class BusinessModelUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private BusinessModelToolTip _businessModelToolTip;
    
    [HideInInspector] public BusinessModelSO businessModelSo;
    private void Awake()
    {
        _businessModelToolTip = FindFirstObjectByType<BusinessModelToolTip>(FindObjectsInactive.Include);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Hovering over: " + businessModelSo.Name);
        _businessModelToolTip.ShowToolTip(businessModelSo);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _businessModelToolTip.HideToolTip();
    }
}
