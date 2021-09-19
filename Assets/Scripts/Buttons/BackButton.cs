using UnityEngine;
using UnityEngine.EventSystems;

public class BackButton : MonoBehaviour, IPointerDownHandler
{
    private SceneLoader sceneLoader;
    void Awake()
    {
        sceneLoader = GameObject.Find("SceneLoader").GetComponent<SceneLoader>();
    }
    
    public void OnPointerDown(PointerEventData eventData)
    {
        sceneLoader.LoadInputLevel();
    }
}
