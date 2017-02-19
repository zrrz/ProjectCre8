using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.UI.Extensions;

using Designer;

public class Connectable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {

	public Transform anchor;

	Connectable tempConnectable;

	public void OnBeginDrag(PointerEventData eventData)
	{
		GameObject obj = new GameObject("anchor", new System.Type[] {typeof(RectTransform)});
		tempConnectable = obj.AddComponent<Connectable>();
		GetComponentInParent<Node>().next = tempConnectable;
		tempConnectable.anchor = tempConnectable.transform;
		tempConnectable.transform.SetParent(transform, true);
		tempConnectable.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
//		tempConnectable.GetComponent<RectTransform>().anchoredPosition = RectTransformExtension.switchToRectTransform(GetComponent<RectTransform>(), tempConnectable.GetComponent<RectTransform>());
	}

	public void OnDrag(PointerEventData eventData)
	{
		tempConnectable.GetComponent<RectTransform>().anchoredPosition += eventData.delta;
	}

	public void OnEndDrag(PointerEventData eventData)
	{
		Destroy(tempConnectable.gameObject);

		for(int i = 0; i < eventData.hovered.Count; i++) {
			if(eventData.hovered[i] == gameObject)
				continue;
			if(eventData.hovered[i].GetComponent<Connectable>() != null)
				GetComponentInParent<Node>().next = eventData.hovered[i].GetComponent<Connectable>();
		}
	}
}
