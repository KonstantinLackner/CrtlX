using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class GameStateManager : MonoBehaviour
    {
        private Level currentLevel;

        public Canvas canvas;

        private void InitLevel(String sentence)
        {
            String[] words = sentence.Split(' ');
            
            GameObject baseWordGameObject = InitBaseWordGameObject();

            float wordCount = words.Length;
            // Start -100 * wordCount/2 + 50f to the left of the centre so every word gets 100 and the middle word is centred
            Vector3 currentPoint = new Vector3(wordCount / 2f * -100f + 50f , 0f, 0f);

            LinkedList<GameObject> wordGameObjects = new LinkedList<GameObject>();

            foreach (String word in words)
            {
                // Problem rn is that the type of the words is GameObject not Word. No idea how to make sth. Type Word.
                GameObject wordGameObject = Instantiate(baseWordGameObject, currentPoint, Quaternion.identity);
                
                // Can't use fourth parameter for parent as parent needs to be set with worldPositionStays == false
                wordGameObject.transform.SetParent(canvas.transform, false);
                
                wordGameObject.GetComponent<Text>().text = word;

                // Name the gameObject after the word it carries
                wordGameObject.name = word;

                // Add gameObject to the list later given to WorOperationsManager
                wordGameObjects.AddLast(wordGameObject);

                currentPoint += new Vector3(100f, 0, 0);
            }

            // Creates a wordSlot for every word in the sentence
            LinkedList<GameObject> wordSlots = new LinkedList<GameObject>();
            foreach (var wordGameObject in wordGameObjects)
            {
                GameObject wordSlot = new GameObject(wordGameObject.name + " wordSlot");
                wordSlot.transform.position = wordGameObject.transform.position;
                wordSlots.AddLast(wordSlot);
            }
            
            baseWordGameObject.AddComponent<WordOperationsManager>();
            WordOperationsManager wOM = baseWordGameObject.GetComponent<WordOperationsManager>();
            wOM.words = wordGameObjects;
            wOM.wordSlots = wordSlots;
            wOM.Start();
        }
        
        private GameObject InitBaseWordGameObject()
        {
            GameObject baseWordGameObject = new GameObject("BaseWord");
            baseWordGameObject.transform.position = new Vector3(0f, 0f, 0f);
            baseWordGameObject.AddComponent<BoxCollider2D>();
            baseWordGameObject.AddComponent<Text>();
            baseWordGameObject.AddComponent<Word>();
            Text baseWordText = baseWordGameObject.GetComponent<Text>();
            // Not setting any tex for this --/-> baseWordText.text = "text";
            baseWordText.font = Resources.GetBuiltinResource(typeof(Font), "Arial.ttf") as Font;
            baseWordGameObject.transform.SetParent(canvas.transform, false);

            return baseWordGameObject;
        }
        
        private void Start()
        {
            InitLevel("Don't mind me testing oida heast funsn");
        }
    }
}