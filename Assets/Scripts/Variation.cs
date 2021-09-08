using System;

namespace DefaultNamespace
{
    public class Variation
    {
        public String sentence { get; }
        public Level leadsTo { get; }

        public Variation(Level leadsTo, string sentence)
        {
            this.leadsTo = leadsTo;
            this.sentence = sentence;
        }
    }
}