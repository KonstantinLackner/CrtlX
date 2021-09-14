using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class WordEndingChangeButton: MonoBehaviour, IPointerDownHandler
    {
        /*
         * The porblem now is that you can change every word with this. I want this to be a one-time use
         * thing like the cut though. So either you have a one time change and destroy it afterwards (as soon)
         * as another object is touched (like a boolean in the GSM called 'changingNow') - that would however make
         * people restart the level every time they accidentally click a wrong word. (not too bad, is it?)
         */

        private GameStateManager gameStateManager;

        private void Awake()
        {
            gameStateManager = GameObject.Find("Game State Manager").GetComponent<GameStateManager>();
            GetComponent<Image>().color = Color.black;
        }


        public void OnPointerDown(PointerEventData eventData)
        {
            // When this is one there has to be a word set that is being changed.
            gameStateManager.changeOperationMode(OperationMode.ChangeEnding);
            Destroy(gameObject);
        }
    }
}