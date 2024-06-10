using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GetRectPositon : MonoBehaviour
    , IPointerClickHandler
    , IDragHandler
    , IPointerEnterHandler
    , IPointerExitHandler
    , IPointerDownHandler

{
    private bool isEnter;
    private bool isExit;

    private void Start()
    {
        isEnter = false;
        isExit = false;
    }

    public void OnPointerClick(PointerEventData eventData)
    {

        PointerEventData __pointer = eventData as PointerEventData;
        switch (__pointer.button)
        {
            case PointerEventData.InputButton.Left:
                //Debug.Log("Left");
                break;
            case PointerEventData.InputButton.Right:
                //Debug.Log("right");
                break;
        }
        //throw new System.NotImplementedException();

    }

    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        //Debug.Log("Drag");
        //throw new System.NotImplementedException();
    }

    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Enter");
        isEnter = true;
        isExit = false;
        //throw new System.NotImplementedException();
    }

    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("Exit");
        isEnter = false;
        isExit = true;
        //throw new System.NotImplementedException();
    }
    //----------------------------------------------------------------------------------------------------------------

    [SerializeField] private RectTransform _rectTransform;

    private void Awake()
    {
        if (!_rectTransform) _rectTransform = GetComponent<RectTransform>();
    }

    private bool GetNormalizedPosition(PointerEventData pointerEventData, out Vector2 normalizedPosition)
    {
        normalizedPosition = default;

        // get the pointer position in the local space of the UI element
        // NOTE: For click vents use "pointerEventData.pressEventCamera"
        // For hover events you would rather use "pointerEventData.enterEventCamera"
        if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(_rectTransform, pointerEventData.position, pointerEventData.pressEventCamera, out var localPosition)) return false;

        normalizedPosition = Rect.PointToNormalized(_rectTransform.rect, localPosition);
        // I think this kind of equals doing something like
        //var rect = _rectTransform.rect;
        //var normalizedPosition = new Vector2 (
        //    (localPosition.x - rect.x) / rect.width, 
        //    (localPosition.y - rect.y) / rect.height);  

        //Debug.Log(normalizedPosition);

        return true;
    }


    // Shift the normalized Rect position from [0,0] (bottom-left), [1,1] (top-right)
    // into [-1, -1] (bottom-left), [1,1] (top-right)
    private static readonly Vector2 _multiplcator = Vector2.one * 2f;
    private static readonly Vector2 _shifter = Vector2.one * 0.5f;

    private static Vector2 GetShiftedNormalizedPosition(Vector2 normalizedPosition)
    {
        return Vector2.Scale((normalizedPosition - _shifter), _multiplcator);
    }
    public void OnPointerDown(PointerEventData pointerEventData)
    {
        if (!GetNormalizedPosition(pointerEventData, out var normalizedPosition)) return;

        var shiftedNormalizedPosition = GetShiftedNormalizedPosition(normalizedPosition);

        //SetAccelerationValue(shiftedNormalizedPosition.y);

        // And probably for your other question also
        //SetSteeringValue(shiftedNormalizedPosition.x);
    }

    public bool CheckEnter()
    {
        return isEnter;
    }
    public bool CheckExit()
    {
        return isExit;
    }
}
