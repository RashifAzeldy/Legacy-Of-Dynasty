using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIAnchor : MonoBehaviour
{
    [SerializeField] Canvas canvas;
    [SerializeField] HUDItem[] hudItems;

    RectTransform objTransform;

    void Start()
    {
        foreach ( HUDItem item in hudItems )
        {
            SetPosition(item.anchor, item.hudItem.GetComponent<RectTransform>(), item.offset);
        }
    }

    void SetPosition( AnchorPoint anchor, RectTransform objectRect, Vector2 offset )
    {
        Vector2 pos;
        switch ( anchor )
        {
            case AnchorPoint.UpperLeft:
            pos = new Vector2(-canvas.GetComponent<RectTransform>().position.x,
            canvas.GetComponent<RectTransform>().position.y);
            objectRect.anchoredPosition = pos + offset;
            break;
            case AnchorPoint.UpperCenter:
            pos = new Vector2(0f,
            canvas.GetComponent<RectTransform>().position.y);
            objectRect.anchoredPosition = pos + offset;
            break;
            case AnchorPoint.UpperRight:
            pos = new Vector2(canvas.GetComponent<RectTransform>().position.x,
            canvas.GetComponent<RectTransform>().position.y);
            objectRect.anchoredPosition = pos + offset;
            break;
            case AnchorPoint.LowerLeft:
            pos = new Vector2(-canvas.GetComponent<RectTransform>().position.x,
            -canvas.GetComponent<RectTransform>().position.y);
            objectRect.anchoredPosition = pos + offset;
            break;
            case AnchorPoint.LowerCenter:
            pos = new Vector2(0f,
            -canvas.GetComponent<RectTransform>().position.y);
            objectRect.anchoredPosition = pos + offset;
            break;
            case AnchorPoint.LowerRight:
            pos = new Vector2(canvas.GetComponent<RectTransform>().position.x,
            -canvas.GetComponent<RectTransform>().position.y);
            objectRect.anchoredPosition = pos + offset;
            break;
            default:
            objectRect.anchoredPosition = Vector2.zero;
            break;
        }
    }
}

[System.Serializable]
public class HUDItem
{
    public Transform hudItem;
    public AnchorPoint anchor;

    public Vector2 offset;
}

public enum AnchorPoint
{
    UpperLeft,
    UpperCenter,
    UpperRight,
    LowerLeft,
    LowerCenter,
    LowerRight
}