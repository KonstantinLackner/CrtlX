using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class ValidateButton: MonoBehaviour, IPointerDownHandler
    {

        private GameStateManager gameStateManager;
        private Sentence sentence;
        private String variationString;
        private SceneLoader sceneLoader;

        private void Awake()
        {
            gameStateManager = GameObject.Find("Game State Manager").GetComponent<GameStateManager>();
            sceneLoader = GameObject.Find("SceneLoader").GetComponent<SceneLoader>();
            GetComponent<Image>().color = Color.black;
        }


        public void OnPointerDown(PointerEventData eventData)
        {
            variationString = gameStateManager.currentPotentialVariation;
            sentence = gameStateManager.sentence;
            
            Variation variation = sentence.CheckIfStringIsVariation(variationString);
            if (variation != null)
            {
                sceneLoader.LoadStoryLevel(variation.answer, variation.leadsTo);
            }
            else
            {
                gameObject.GetComponent<Image>().color = Color.red;
                // So the button can't be clicked until a new word order has been established
                gameObject.GetComponent<CanvasGroup>().blocksRaycasts = false;
            }
        }
    }
}