using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace DefaultNamespace
{
    public class Word : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
    {
        private Transform transform;
        private CanvasGroup canvasGroup;
        private GameObject GameStateManager;
        private WordOperationsManager wordOperationsManager;
        public Canvas canvas { get; set; }

        private void Awake()
        {
            transform = GetComponent<Transform>();
            canvasGroup = GetComponent<CanvasGroup>();
            GameStateManager = GameObject.Find("Game State Manager");
            wordOperationsManager = GameStateManager.GetComponent<WordOperationsManager>();
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            // Has to be done for all words to prevent accidentally dropping onto the wron word
            foreach (var word in GameStateManager.GetComponent<WordOperationsManager>().words)
            {
                word.GetComponent<CanvasGroup>().blocksRaycasts = false;
            }
            canvasGroup.alpha = 0.6f;
            LinkedListNode<GameObject> targetNode = wordOperationsManager.words.Find(gameObject);
            wordOperationsManager.words.Remove(targetNode);
            printList(GameStateManager.GetComponent<WordOperationsManager>().words);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            foreach (var word in GameStateManager.GetComponent<WordOperationsManager>().words)
            {
                word.GetComponent<CanvasGroup>().blocksRaycasts = true;
            }
            canvasGroup.alpha = 1f;
            canvasGroup.blocksRaycasts = true;
            printList(GameStateManager.GetComponent<WordOperationsManager>().words);
        }

        public void OnDrag(PointerEventData eventData)
        {
            transform.position += new Vector3(eventData.delta.x, 0, 0) / canvas.scaleFactor;
        }
        
        private void printList(LinkedList<GameObject> words)
        {
            String print = "";
            foreach (var word in words)
            {
                print += " " + word.name;
            }
            Debug.Log(print);
        }
    }
}