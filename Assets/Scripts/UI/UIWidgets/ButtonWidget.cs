using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using QFramework;
using Unity.VisualScripting;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonWidget : UISceneWidget, IPointerEnterHandler, IPointerExitHandler
{
    bool isHover = false;
    [Header("按钮属性")]
    public bool HoverColor = true;
    public Color normalColor = Color.white;
    public Color hoverColor = Color.gray;
    public float colorChangeSpeed = 5.0f;
    private Image buttonImage;
    private void Start()
    {
        buttonImage = GetComponent<Image>();
    }
    private void Update()
    {
        if(HoverColor)  buttonImage.color = Color.Lerp(buttonImage.color, isHover ? hoverColor : normalColor, Time.deltaTime * colorChangeSpeed);
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        isHover = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isHover = false;
    }
}