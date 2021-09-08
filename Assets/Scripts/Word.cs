using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class Word : MonoBehaviour
    {
        public delegate void DragEndDelegate(Word wordObject);

        public DragEndDelegate dragEndedCallback;
        
        private bool isBeingDragged = false;
        private Vector3 mouseDragStartPosition;
        private Vector3 spriteDragStartPosition;

        private void OnMouseDown()
        {
            isBeingDragged = true;
            mouseDragStartPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            spriteDragStartPosition = transform.localPosition;
        }

        private void OnMouseDrag()
        {
            if (isBeingDragged)
            {
                transform.localPosition = spriteDragStartPosition +
                                          (Camera.main.ScreenToWorldPoint(Input.mousePosition) -
                                           mouseDragStartPosition);
            }
        }

        private void OnMouseUp()
        {
            isBeingDragged = false;
            dragEndedCallback(this);
        }
    }
}