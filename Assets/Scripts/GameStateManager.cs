using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public enum OperationMode
    {
        Drag,
        Cut,
        ChangeEnding
    }

    public class GameStateManager : MonoBehaviour
    {
        private Level currentLevel;

        public OperationMode operationMode;
        public LinkedList<Vector3> placementPositions { get; set; }
        public LinkedList<GameObject> words { get; set; }

        public GameObject wordEndingChangeWord { get; set; }

        // References
        public Canvas canvas;
        private Image modeIndicator;
        public Sprite DRAGOPERATOR;
        public Sprite CUTOPERATOR;
        public Sprite WORDCHANGEOPERATOR;

        private void Awake()
        {
            Cursor.lockState = CursorLockMode.Confined; // keep confined in the game window

            operationMode = OperationMode.Drag;

            modeIndicator = GameObject.Find("ModeUI").GetComponent<Image>();

            InitLevel("You walk over the burning bridge", 3, 1);
        }

        private void InitLevel(String sentence, int cutCount, int wordEndingCount)
        {
            /*
             * Buttons
             */
            GameObject baseCutButton = GameObject.Find("CutButton");
            GameObject baseWordEndingButton = GameObject.Find("ChangeWordEndingButton");
            Vector3 currentPointButtons = new Vector3((cutCount + wordEndingCount) / 2f * -200f + 100f, -400f, 0f);
            for (int i = 0; i < cutCount; i++)
            {
                GameObject cutButton = Instantiate(baseCutButton, currentPointButtons, Quaternion.identity);
                cutButton.transform.SetParent(canvas.transform, false);
                currentPointButtons += new Vector3(200f, 0f, 0f);
            }

            for (int i = 0; i < wordEndingCount; i++)
            {
                GameObject wordEndingButton = Instantiate(baseWordEndingButton, currentPointButtons, Quaternion.identity);
                wordEndingButton.transform.SetParent(canvas.transform, false);
                currentPointButtons += new Vector3(200f, 0f, 0f);
            }

            /*
             * Words
             */
            String[] wordArray = sentence.Split(' ');

            GameObject baseWordGameObject = InitBaseWordGameObject();

            float wordCount = wordArray.Length;
            // Start -100 * wordCount/2 + 50f to the left of the centre so every word gets 100 and the middle word is centred
            Vector3 currentPoint = new Vector3(wordCount / 2f * -200f + 100f, 0f, 0f);

            words = new LinkedList<GameObject>();
            placementPositions = new LinkedList<Vector3>();

            foreach (String word in wordArray)
            {
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
                words.AddLast(wordGameObject);

                placementPositions.AddLast(currentPoint);

                currentPoint += new Vector3(200f, 0, 0);
            }

            /*
             * Cleanup of base GameObjects
             */
            Destroy(baseWordGameObject);
            Destroy(baseCutButton);
            Destroy(baseWordEndingButton);
            Debug.Log("GameStateManager ");
            printList(words);
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
            baseWordGameObject.AddComponent<WordEndingChange>();
            Text baseWordText = baseWordGameObject.GetComponent<Text>();
            baseWordText.fontSize = 50;
            // Not setting any tex for this --/-> baseWordText.text = "text";
            baseWordText.font = Resources.GetBuiltinResource(typeof(Font), "Arial.ttf") as Font;
            baseWordText.color = Color.black;
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

        public void AlignWords()
        {
            /*
             * Start off by recounting words and adjusting placement positions
             */
            placementPositions = new LinkedList<Vector3>();
            float wordCount = words.Count;
            // Start -100 * wordCount/2 + 50f to the left of the centre so every word gets 100 and the middle word is centred
            Vector3 currentPoint = new Vector3(wordCount / 2f * -200f + 100f, 0f, 0f);
            for (int i = 0; i < wordCount; i++)
            {
                placementPositions.AddLast(currentPoint);

                currentPoint += new Vector3(200f, 0, 0);
            }

            Vector3[] placementPositionsCopy = new Vector3[placementPositions.Count];
            placementPositions.CopyTo(placementPositionsCopy, 0);
            int index = 0;
            foreach (var word in words)
            {
                word.transform.localPosition = placementPositionsCopy[index];
                index++;
            }
        }

        public void changeOperationMode(OperationMode mode)
        {
            switch (mode)
            {
                case OperationMode.Drag:
                    modeIndicator.sprite = DRAGOPERATOR;
                    modeIndicator.color = Color.black;
                    operationMode = OperationMode.Drag;
                    break;

                case OperationMode.Cut:
                    modeIndicator.sprite = CUTOPERATOR;
                    modeIndicator.color = Color.red;
                    operationMode = OperationMode.Cut;
                    break;

                case OperationMode.ChangeEnding:
                    modeIndicator.sprite = WORDCHANGEOPERATOR;
                    modeIndicator.color = Color.red;
                    operationMode = OperationMode.ChangeEnding;
                    break;
            }
        }
    }
}