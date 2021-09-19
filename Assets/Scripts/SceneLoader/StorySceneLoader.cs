using UnityEngine;
using UnityEngine.UI;

public class StorySceneLoader : MonoBehaviour
{
    private SceneLoader sceneLoader;
    
    void Start()
    {
        sceneLoader = GameObject.Find("SceneLoader").GetComponent<SceneLoader>();
        GameObject.Find("Story").GetComponent<Text>().text = sceneLoader.answer;
        // Set LevelToGoTo in button
    }
}
