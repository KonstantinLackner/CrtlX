using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace DefaultNamespace
{
    public class Word : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
    {
        private Transform transform;
        private CanvasGroup canvasGroup;
        
        private void Awake()
        {
            transform = GetComponent<Transform>();
            canvasGroup = GetComponent<CanvasGroup>();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            Debug.Log("pointerDown");
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            canvasGroup.alpha = .6f;
            canvasGroup.blocksRaycasts = false;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            canvasGroup.alpha = 1f;
            canvasGroup.blocksRaycasts = true;
        }

        public void OnDrag(PointerEventData eventData)
        {
            transform.position += new Vector3(eventData.delta.x, eventData.delta.y, 0);
        }
    }
}