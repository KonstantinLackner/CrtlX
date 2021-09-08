using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace DefaultNamespace
{
    public class WordOperationsManager : MonoBehaviour, IDropHandler
        // Just use the IdropDrag stuff for the WordOperations manager so words can be dropped everywhere in it. It can then decide what to do with them (where to snap them)
    {
        public LinkedList<GameObject> words { get; set; }
        public LinkedList<GameObject> wordSlots { get; set; }

        private float snapRange = 0.5f;

        public void OnDrop(PointerEventData eventData)
        {
            if (eventData.pointerDrag != null)
            {
                eventData.pointerDrag.GetComponent<Transform>().position = GetComponent<Transform>().position;
            }
        }
    }
}