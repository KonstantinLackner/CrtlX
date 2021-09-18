using System;

namespace DefaultNamespace
{
    public class Variation
    {
        public String sentence { get; }
        public String answer { get; }
        public Level leadsTo { get; }

        public Variation(Level leadsTo, String sentence, String answer)
        {
            this.leadsTo = leadsTo;
            this.sentence = sentence;
            this.answer = answer;
        }
    }
}