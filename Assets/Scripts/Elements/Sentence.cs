using System;
using System.Collections.Generic;
using DefaultNamespace;

public class Sentence
{
    public String original { get; }
    public LinkedList<Variation> variations { get; set; }
    
    public int cutCount { get; set; }
    
    public int wordEndingCount { get; set; }
    
    public Sentence(String original, int cutCount, int wordEndingCount)
    {
        this.original = original;
        this.cutCount = cutCount;
        this.wordEndingCount = wordEndingCount;
    }
    public Variation CheckIfStringIsVariation(String potentialVariation)
    {
        Variation returnVariation = null;
        
        foreach (Variation variation in variations)
        {
            if (variation.sentence.Equals(potentialVariation))
            {
                returnVariation = variation;
                break;
            }
        }

        return returnVariation;
    }
}
