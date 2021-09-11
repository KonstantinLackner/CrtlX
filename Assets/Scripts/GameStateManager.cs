using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class GameStateManager : MonoBehaviour
    {
        private Level currentLevel;

        public Canvas canvas;
        private void Start()
        {
            InitLevel("Don't mind me testing oida heast funsn");
            Cursor.lockState = CursorLockMode.Confined; // keep confined in the game window
        }

        private void InitLevel(String sentence)
        {
            String[] words = sentence.Split(' ');
            
            GameObject baseWordGameObject = InitBaseWordGameObject();

            float wordCount = words.Length;
            // Start -100 * wordCount/2 + 50f to the left of the centre so every word gets 100 and the middle word is centred
            Vector3 currentPoint = new Vector3(wordCount / 2f * -100f + 50f , 0f, 0f);

            LinkedList<GameObject> wordGameObjects = new LinkedList<GameObject>();
            LinkedList<Vector3> placementPositions = new LinkedList<Vector3>();

            WordOperationsManager wOM = GetComponent<WordOperationsManager>();
            
            foreach (String word in words)
            {
                // Problem rn is that the type of the words is GameObject not Word. No idea how to make sth. Type Word.
                GameObject wordGameObject = Instantiate(baseWordGameObject, currentPoint, Quaternion.identity);
                
                // Can't use fourth parameter for parent as parent needs to be set with worldPositionStays == false
                wordGameObject.transform.SetParent(canvas.transform, false);
                
                wordGameObject.GetComponent<Text>().text = word;
                
                wordGameObject.GetComponent<Word>().canvas = canvas;

                wordGameObject.GetComponent<ContentSizeFitter>().horizontalFit =
                    ContentSizeFitter.FitMode.PreferredSize;

                // Name the gameObject after the word it carries
                wordGameObject.name = word;

                // Add gameObject to the list later given to WorOperationsManager
                wordGameObjects.AddLast(wordGameObject);

                placementPositions.AddLast(currentPoint);
                
                currentPoint += new Vector3(200f, 0, 0);
            }

            wOM.words = wordGameObjects;
            wOM.placementPositions = placementPositions;
            Debug.Log("GameStateManager ");
            printList(wOM.words);
        }
        
        private GameObject InitBaseWordGameObject()
        {
            GameObject baseWordGameObject = new GameObject("BaseWord");
            baseWordGameObject.transform.position = new Vector3(0f, 0f, 0f);
            baseWordGameObject.AddComponent<BoxCollider2D>();
            baseWordGameObject.AddComponent<Text>();
            baseWordGameObject.AddComponent<CanvasGroup>();
            baseWordGameObject.AddComponent<Word>();
            baseWordGameObject.AddComponent<ContentSizeFitter>();
            Text baseWordText = baseWordGameObject.GetComponent<Text>();
            baseWordText.fontSize = 50;
            // Not setting any tex for this --/-> baseWordText.text = "text";
            baseWordText.font = Resources.GetBuiltinResource(typeof(Font), "Arial.ttf") as Font;
            baseWordGameObject.transform.SetParent(canvas.transform, false);

            return baseWordGameObject;
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