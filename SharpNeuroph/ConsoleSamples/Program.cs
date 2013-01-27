using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Neuroph.Core;
using Neuroph.Core.Learning;
using Neuroph.NNet;
using Neuroph.Util;

namespace ConsoleSamples
{
    public class Program
    {
        /**
         * Runs this sample
         */
        public static void Main(String[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Please specify the sample you want to run");
                Console.WriteLine("xormlp - XOR Multi-Layer Perceptron");
                Console.WriteLine("sunspots - Predict Sunspots");
            }
            else
            {
                String command = args[0].ToLower();
                if (command.Equals("xormlp"))
                {
                    XorMultiLayerPerceptronSample sample = new XorMultiLayerPerceptronSample();
                    sample.Run();
                }
                else if (command.Equals("sunspot"))
                {
                    SunSpot sample = new SunSpot();
                    sample.Run();
                }

                Console.WriteLine("Sample done");
            }
        }

    }
}
