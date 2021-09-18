using System;
using System.Collections;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    public Animator transitionAnimator;
    public String answer { get; set; }
    private Level levelToGoTo;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void LoadStoryLevel(String answer, Level levelToGoTo)
    {
        this.answer = answer;
        this.levelToGoTo = levelToGoTo;
        StartCoroutine(LoadStoryLevelCoroutine());
    }
    
    IEnumerator LoadStoryLevelCoroutine()
    {
        // Maybe make transition time dependent on sentence length

        // transitionAnimator.SetTrigger("Start");

        yield return new WaitForSeconds(3);

        SceneManager.LoadScene("Scenes/Story");
    }

    public void LoadInputLevel()
    {
    }
}
