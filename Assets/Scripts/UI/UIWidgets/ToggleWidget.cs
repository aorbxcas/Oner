using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using QFramework;
using Unity.VisualScripting;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ToggleWidget : UISceneWidget, IPointerClickHandler
{
    private Toggle toggle;
    private void Start()
    {
        toggle = this.GetComponent<Toggle>();
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        toggle.isOn = true;
    }
}
