using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISlider : MonoBehaviour
{
    public Vector2 endPosition;
    private Vector2 startPosition;
    public RectTransform rect;
    public bool isSlidIn = false;
    public RectTransform content;

    public void Start()
    {
        rect = GetComponent<RectTransform>();
        startPosition = rect.anchoredPosition;
    }

    public void SlideIn()
    {
        isSlidIn = true;
        rect.DOAnchorPos(endPosition, 0.25f);
    }

    public void SlideOut()
    {
        isSlidIn = false;
        rect.DOAnchorPos(startPosition, 0.25f);
    }
}
