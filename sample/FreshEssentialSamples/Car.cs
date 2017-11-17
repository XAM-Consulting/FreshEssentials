using System;

namespace FreshEssentialSamples
{
    public class Car
    {
        public string Make { get; set; }
        public string Model { get; set; }

        public string MakeAndModel 
        {
            get { return Make + " " + Model; }
        }
    }
}

