using UnityEngine.EventSystems;
using UnityEngine.UI;

// https://discussions.unity.com/t/scroll-rect-disable-click-and-drag-and-keep-scroll-wheel/253781
public class ScrollRectNoDrag : ScrollRect {
	public override void OnBeginDrag(PointerEventData eventData) { }
	public override void OnDrag(PointerEventData eventData) { }
	public override void OnEndDrag(PointerEventData eventData) { }
}