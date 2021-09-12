using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace DefaultNamespace
{
    public class Word : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler, IPointerDownHandler
    {
        private Transform transform;
        private CanvasGroup canvasGroup;
        private GameObject GameStateManagerGameObject;
        private GameStateManager GameStateManagerComponent;
        private WordOperationsManager wordOperationsManager;
        public Canvas canvas { get; set; }

        private void Awake()
        {
            GameStateManagerGameObject = GameObject.Find("Game State Manager");
            GameStateManagerComponent = GameStateManagerGameObject.GetComponent<GameStateManager>();
            
            transform = GetComponent<Transform>();
            canvasGroup = GetComponent<CanvasGroup>();
            
            wordOperationsManager = GameStateManagerGameObject.GetComponent<WordOperationsManager>();
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            if (GameStateManagerComponent.OperationMode == OperationMode.Drag)
            {
                // Has to be done for all words to prevent accidentally dropping onto the wrong word
                foreach (var word in GameStateManagerComponent.words)
                {
                    word.GetComponent<CanvasGroup>().blocksRaycasts = false;
                }

                canvasGroup.alpha = 0.6f;
                LinkedListNode<GameObject> targetNode = GameStateManagerComponent.words.Find(gameObject);
                GameStateManagerComponent.words.Remove(targetNode);
                printList(GameStateManagerComponent.words);
            }
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            foreach (var word in GameStateManagerComponent.words)
            {
                word.GetComponent<CanvasGroup>().blocksRaycasts = true;
            }

            canvasGroup.alpha = 1f;
            canvasGroup.blocksRaycasts = true;
            printList(GameStateManagerComponent.words);
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

        public void OnPointerDown(PointerEventData eventData)
        {
            if (GameStateManagerComponent.OperationMode == OperationMode.Cut)
            {
                LinkedListNode<GameObject> targetNode = GameStateManagerComponent.words.Find(gameObject);
                GameStateManagerComponent.words.Remove(targetNode);
                printList(GameStateManagerComponent.words);
                Destroy(gameObject);
                GameStateManagerComponent.AlignWords();
            }
        }
    }
}