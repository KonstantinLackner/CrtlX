using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace DefaultNamespace
{
    public class Word : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
    {
        private Transform transform;
        private CanvasGroup canvasGroup;
        public Canvas canvas { get; set; }

        private void Awake()
        {
            transform = GetComponent<Transform>();
            canvasGroup = GetComponent<CanvasGroup>();
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
            transform.position += new Vector3(eventData.delta.x, eventData.delta.y, 0) / canvas.scaleFactor;
        }
    }
}