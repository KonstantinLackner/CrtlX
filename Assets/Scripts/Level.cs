using System;
using UnityEngine.SceneManagement;

namespace DefaultNamespace
{
    public class Level
    {
        private Sentence sentence;

        private void tryVariation(String potentialVariation)
        {
            if (sentence.CheckIfStringIsVariation(potentialVariation) != null)
            {
                Level nextLevel = sentence.FetchRespectiveLevel(potentialVariation);
            }
        }
    }
}