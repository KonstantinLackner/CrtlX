using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace DefaultNamespace
{
    public class WordOperationsManager : MonoBehaviour, IDragHandler
    // Just use the IdropDrag stuff for the WordOperations manager so words can be dropped everywhere in it. It can then decide what to do with them (where to snap them)
    {
        public LinkedList<GameObject> words { get; set; }
        public LinkedList<GameObject> wordSlots { get; set; }

        private float snapRange = 0.5f;

        public void Start()
        {
            foreach (var word in words)
            {
                word.GetComponent<Word>().dragEndedCallback = OnDragEnded;
            }
        }

        private void OnDragEnded(Word wordObject)
        {
            float closestDistance = -1f;
            Transform closestWordSlot = null;
            
            foreach (var wordSlot in wordSlots)
            {
                float currentDistance = Vector2.Distance(wordObject.transform.localPosition, wordSlot.transform.localPosition);
                if (closestWordSlot == null || currentDistance < closestDistance)
                {
                    closestWordSlot = wordSlot.transform;
                    closestDistance = currentDistance;
                }
            }

            if (closestWordSlot != null && closestDistance <= snapRange)
            {
                wordObject.transform.localPosition = closestWordSlot.localPosition;
            }
        }
    }
}