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

        public LinkedList<Vector3> placementPositions { get; set; }

        private float snapRange = 0.5f;

        public void OnDrop(PointerEventData eventData)
        {
            if (eventData.pointerDrag != null)
            {
                SortList(eventData.pointerDrag, eventData.position.x);
                AlignWords();
            }
        }

        private void SortList(GameObject draggedObject, float dropPosition)
        {
            // Find the closest wordSlot
            GameObject closestWord = words.First.Value;
            float distanceToClosest = Math.Abs(words.First.Value.transform.position.x - dropPosition);
            
            foreach (var word in words)
            {
                float distanceToWord = Math.Abs(word.transform.position.x - dropPosition);
                if (distanceToWord < distanceToClosest)
                {
                    closestWord = word;
                    distanceToClosest = distanceToWord;
                }
            }

            /*
             Find the slot to drop into
             If left of the closest node -> closest node
             If right of closest node -> next of closest node
             
             This doesn't return the first matching value as there can only be one matching value
             since this is looking for GameObjects not Strings 
             */
            LinkedListNode<GameObject> slotToDropInto = words.Find(closestWord); 
            if (closestWord.transform.position.x > dropPosition) // If the dropPosition is left of the closest slot
            {
                words.AddBefore(slotToDropInto, new LinkedListNode<GameObject>(draggedObject));
            }
            else
            {
                words.AddAfter(slotToDropInto, new LinkedListNode<GameObject>(draggedObject));
            }
        }

        private void AlignWords()
        {
            Vector3[] placementPositionsCopy = new Vector3[placementPositions.Count];
            placementPositions.CopyTo(placementPositionsCopy, 0);
            int index = 0;
            foreach (var word in words)
            {
                word.transform.localPosition = placementPositionsCopy[index];
                index++;
            }
        }
    }
}