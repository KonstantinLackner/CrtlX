using System;

namespace DefaultNamespace
{
    public class Variation
    {
        public String sentence { get; }
        public String answer { get; }
        public Sentence leadsTo { get; }

        public Variation(String sentence, Sentence leadsTo, String answer)
        {
            this.sentence = sentence;
            this.leadsTo = leadsTo;
            this.answer = answer;
        }
    }
}