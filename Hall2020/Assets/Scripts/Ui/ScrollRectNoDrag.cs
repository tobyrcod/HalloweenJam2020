using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ScrollRectNoDrag : ScrollRect {

    public override void OnBeginDrag(PointerEventData eventData) { /*Do nothing*/ }
    public override void OnDrag(PointerEventData eventData) { /*Do nothing*/ }
    public override void OnEndDrag(PointerEventData eventData) { /*Do nothing*/ }
}
