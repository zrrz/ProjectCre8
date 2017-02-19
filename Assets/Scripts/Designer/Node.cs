using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.UI.Extensions;

namespace Designer {
	public class Node : MonoBehaviour, IDragHandler {

		public Node next;
		public Node prev;

		Connectable anchorPrev;
		Connectable AnchorPrev { get { return anchorPrev;} }
		Connectable anchorNext;
		Connectable AnchorNext { get { return anchorNext;} }

		UILineRenderer line;

		void Start () {
			line = gameObject.GetComponentInChildren<UILineRenderer>();

			anchorPrev = transform.FindChild("Body").FindChild("FlowAnchorPrev").GetComponent<Connectable>();
			anchorNext = transform.FindChild("Body").FindChild("FlowAnchorNext").GetComponent<Connectable>();
		}

		void Update () {
			if(next) {
				line.enabled = true;
				line.Points = new Vector2[] {
					new Vector2(0f, 0f), 
					new Vector2(30f, 0), 
					RectTransformExtension.switchToRectTransform(next.AnchorPrev.GetComponent<RectTransform>(), line.GetComponent<RectTransform>()) - new Vector2(30f, 0),
					RectTransformExtension.switchToRectTransform(next.AnchorPrev.GetComponent<RectTransform>(), line.GetComponent<RectTransform>())
				};
			} else {
				line.enabled = false;
			}
		}

		public void OnDrag(PointerEventData data) {
			GetComponent<RectTransform>().anchoredPosition += data.delta;
		}
	}
}
