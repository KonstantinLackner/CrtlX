using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader instance;
    // public Animator transitionAnimator;
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
        Sentence testSentence1 = new Sentence("you walk over the burning bridge", 2, 1);
        Sentence testSentence2 = new Sentence("you kill the troll with your sword",2, 1);
        Sentence testSentence3 = new Sentence("you ignore the troll", 0, 1);
            
        Variation testVariationS1 = new Variation("you burn the bridge", testSentence2, "A troll rises from the ashes.'You burn me house!' He spits at you as he shouts");
        Variation testVariationS2 = new Variation("you walk over the bridge", testSentence3, "As you walk over the bridge, a troll happily waves at you");
            
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

        // transitionAnimator.SetTrigger("StartFade");

        yield return new WaitForSeconds(0.1f);

        SceneManager.LoadScene("Scenes/Story");
    }

    public void LoadInputLevel()
    {
        StartCoroutine(LoadInputLevelCoroutine());
    }
    
    IEnumerator LoadInputLevelCoroutine()
    {

        // transitionAnimator.SetTrigger("StartFade");

        yield return new WaitForSeconds(0.1f);

        SceneManager.LoadScene("Scenes/Input");
    }
}
