using DefaultNamespace;
using UnityEngine;

public class InputSceneLoader : MonoBehaviour
{
    private GameStateManager gameStateManager;
    private SceneLoader sceneLoader;
    void Start()
    {
        gameStateManager = GameObject.Find("Game State Manager").GetComponent<GameStateManager>();
        sceneLoader = GameObject.Find("SceneLoader").GetComponent<SceneLoader>();
        gameStateManager.InitLevel(sceneLoader.sentenceToGoTo);
    }
}
