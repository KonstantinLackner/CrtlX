using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CutButton : MonoBehaviour, IPointerDownHandler
{
    private GameStateManager gameStateManager;
    
    void Awake()
    {
        gameStateManager = GameObject.Find("Game State Manager").GetComponent<GameStateManager>();
        GetComponent<Image>().color = Color.black;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        // delete this but change something about the hud
        gameStateManager.changeUIModes(OperationMode.Cut);
        Destroy(gameObject);
    }
}
