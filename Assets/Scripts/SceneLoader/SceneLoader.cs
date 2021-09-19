using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader instance;
    public Animator transitionAnimator;
    public String answer { get; set; }
    public Sentence sentenceToGoTo { get; set; }

    private void Awake()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        
        /*
         * Bullshit test init
         */
        Sentence testSentence1 = new Sentence("This does not lead to tS2", 1, 1);
        Sentence testSentence2 = new Sentence("This is tS2",2, 1);
        Sentence testSentence3 = new Sentence("This is tS3", 0, 0);
            
        Variation testVariationS1 = new Variation("This does lead to tS2", testSentence2, "This is the story1");
        Variation testVariationS2 = new Variation("This does not lead to tS2", testSentence3, "This is the story2");
            
        LinkedList<Variation> variations = new LinkedList<Variation>();
        variations.AddLast(testVariationS1);
        variations.AddLast(testVariationS2);
            
        testSentence1.variations = variations;
        
        sentenceToGoTo = testSentence1;
    }

    public void LoadStoryLevel(String answer, Sentence sentenceToGoTo)
    {
        this.answer = answer;
        this.sentenceToGoTo = sentenceToGoTo;
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
        StartCoroutine(LoadInputLevelCoroutine());
    }
    
    IEnumerator LoadInputLevelCoroutine()
    {
        // Maybe make transition time dependent on sentence length

        // transitionAnimator.SetTrigger("Start");

        yield return new WaitForSeconds(3);

        SceneManager.LoadScene("Scenes/Input");
    }
}
