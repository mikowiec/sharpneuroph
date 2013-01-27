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
    public class XorMultiLayerPerceptronSample
    {
        public void Run()
        {

            // create training set (logical XOR function)
            TrainingSet trainingSet = new TrainingSet(2, 1);
            trainingSet.Add(new SupervisedTrainingElement(new double[] { 0, 0 }, new double[] { 0 }));
            trainingSet.Add(new SupervisedTrainingElement(new double[] { 0, 1 }, new double[] { 1 }));
            trainingSet.Add(new SupervisedTrainingElement(new double[] { 1, 0 }, new double[] { 1 }));
            trainingSet.Add(new SupervisedTrainingElement(new double[] { 1, 1 }, new double[] { 0 }));

            // create multi layer perceptron
            MultiLayerPerceptron myMlPerceptron = new MultiLayerPerceptron(TransferFunctionType.TANH, 2, 3, 1);
            // learn the training set
            Console.WriteLine("Training neural network...");
            myMlPerceptron.LearnInSameThread(trainingSet);

            // test perceptron
            Console.WriteLine("Testing trained neural network");
            TestNeuralNetwork(myMlPerceptron, trainingSet);

            // save trained neural network
            myMlPerceptron.Save("myMlPerceptron.nnet");

            // load saved neural network
            NeuralNetwork loadedMlPerceptron = NeuralNetwork.Load("myMlPerceptron.nnet");

            // test loaded neural network
            //Console.WriteLine("Testing loaded neural network");
            //testNeuralNetwork(loadedMlPerceptron, trainingSet);
        }

        /**
         * Prints network output for the each element from the specified training set.
         * @param neuralNet neural network
         * @param trainingSet training set
         */
        public static void TestNeuralNetwork(NeuralNetwork neuralNet, TrainingSet trainingSet)
        {

            foreach (TrainingElement trainingElement in trainingSet.TrainingElements)
            {
                neuralNet.SetInput(trainingElement.Input);
                neuralNet.Calculate();
                double[] networkOutput = neuralNet.Output;

                Console.Write("Input: " + trainingElement.Input.ArrayString());
                Console.WriteLine(" Output: " + networkOutput.ArrayString());

            }
        }


    }
}
