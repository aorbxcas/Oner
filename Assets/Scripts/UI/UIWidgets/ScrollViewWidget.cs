using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using QFramework;
using Unity.VisualScripting;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ScrollViewWidget : UISceneWidget, IPointerDownHandler, IDragHandler
{
    private ScrollRect scrollRect;
    void Start()
    {
        scrollRect = GetComponent<ScrollRect>();
    }
    public void OnDrag(PointerEventData eventData)
    {
        
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        
    }
}