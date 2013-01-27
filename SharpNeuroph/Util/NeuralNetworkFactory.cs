//
// Copyright 2010 Neuroph Project http://neuroph.sourceforge.net
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//    http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Neuroph.NNet;
using Neuroph.Core;
using Neuroph.NNet.Learning;
using Neuroph.NNet.Comp;

namespace Neuroph.Util
{
    /// <summary>
    /// Creates neural networks.
    /// 
    /// Author: Zoran Sevarac <sevarac@gmail.com>
    /// C# Translation: Miley Thomas (http://www.heatonresearch.com)
    /// </summary>
    public class NeuralNetworkFactory
    {
        /// <summary>
        /// Creates and returns a new instance of Adaline network
        /// </summary>
        /// <param name="inputsCount">number of inputs of Adaline network</param>
        /// <returns>instance of Adaline network</returns>
        public static Adaline CreateAdaline(int inputsCount)
        {
            Adaline nnet = new Adaline(inputsCount);
            return nnet;
        }

        /// <summary>
        /// Creates  and returns a new instance of Perceptron network
        /// </summary>
        /// <param name="inputNeuronsCount">number of neurons in input layer</param>
        /// <param name="outputNeuronsCount">number of neurons in output layer</param>
        /// <param name="transferFunctionType">type of transfer function to use</param>
        /// <returns>instance of Perceptron network</returns>
        public static Perceptron CreatePerceptron(int inputNeuronsCount, int outputNeuronsCount, TransferFunctionType transferFunctionType)
        {
            Perceptron nnet = new Perceptron(inputNeuronsCount, outputNeuronsCount, transferFunctionType);
            return nnet;
        }


        /// <summary>
        /// Creates  and returns a new instance of Perceptron network
        /// </summary>
        /// <param name="inputNeuronsCount">number of neurons in input layer</param>
        /// <param name="outputNeuronsCount">number of neurons in output layer</param>
        /// <param name="transferFunctionType">type of transfer function to use</param>
        /// <param name="learningRule">learning rule class</param>
        /// <returns>instance of Perceptron network</returns>
        public static Perceptron CreatePerceptron(int inputNeuronsCount, int outputNeuronsCount, TransferFunctionType transferFunctionType, Type learningRule)
        {
            Perceptron nnet = new Perceptron(inputNeuronsCount, outputNeuronsCount, transferFunctionType);

            if (learningRule.Name.Equals(typeof(PerceptronLearning).Name))
            {
                nnet.LearningRule = new PerceptronLearning();
            }
            else if (learningRule.Name.Equals(typeof(BinaryDeltaRule).Name))
            {
                nnet.LearningRule = new BinaryDeltaRule();
            }

            return nnet;
        }


        /// <summary>
        /// Creates and returns a new instance of Multi Layer Perceptron
        /// </summary>
        /// <param name="layersStr">space separated number of neurons in layers</param>
        /// <param name="transferFunctionType">transfer function type for neurons</param>
        /// <returns>instance of Multi Layer Perceptron</returns>
        public static MultiLayerPerceptron CreateMLPerceptron(String layersStr, TransferFunctionType transferFunctionType)
        {
            IList<int> layerSizes = VectorParser.ParseInteger(layersStr);
            MultiLayerPerceptron nnet = new MultiLayerPerceptron(layerSizes,
                    transferFunctionType);
            return nnet;
        }

        /// <summary>
        /// Creates and returns a new instance of Multi Layer Perceptron
        /// </summary>
        /// <param name="layersStr">space separated number of neurons in layers</param>
        /// <param name="transferFunctionType">transfer function type for neurons</param>
        /// <param name="learningRule">instance of Multi Layer Perceptron</param>
        /// <param name="useBias"></param>
        /// <param name="connectIO"></param>
        /// <returns></returns>
        public static MultiLayerPerceptron CreateMLPerceptron(String layersStr, TransferFunctionType transferFunctionType, Type learningRule, bool useBias, bool connectIO)
        {
            IList<int> layerSizes = VectorParser.ParseInteger(layersStr);
            NeuronProperties neuronProperties = new NeuronProperties(transferFunctionType, useBias);
            MultiLayerPerceptron nnet = new MultiLayerPerceptron(layerSizes, neuronProperties);

            // set learning rule
            if (learningRule.Name.Equals(typeof(BackPropagation).Name))
            {
                nnet.LearningRule = new BackPropagation();
            }
            else if (learningRule.Name.Equals(typeof(MomentumBackpropagation).Name))
            {
                nnet.LearningRule = new MomentumBackpropagation();
            }
            else if (learningRule.Name.Equals(typeof(DynamicBackPropagation).Name))
            {
                nnet.LearningRule = new DynamicBackPropagation();
            }

            // connect io
            if (connectIO)
            {
                nnet.ConnectInputsToOutputs();
            }

            return nnet;
        }


        /// <summary>
        /// Creates and returns a new instance of Hopfield network
        /// </summary>
        /// <param name="neuronsCount">number of neurons in Hopfield network</param>
        /// <returns>instance of Hopfield network</returns>
        public static Hopfield CreateHopfield(int neuronsCount)
        {
            Hopfield nnet = new Hopfield(neuronsCount);
            return nnet;
        }

        /// <summary>
        /// Creates and returns a new instance of BAM network
        /// </summary>
        /// <param name="inputNeuronsCount">number of input neurons</param>
        /// <param name="outputNeuronsCount">number of output neurons</param>
        /// <returns>instance of BAM network</returns>
        public static BAM CreateBam(int inputNeuronsCount, int outputNeuronsCount)
        {
            BAM nnet = new BAM(inputNeuronsCount, outputNeuronsCount);
            return nnet;
        }


        /// <summary>
        /// Creates and returns a new instance of Kohonen network
        /// </summary>
        /// <param name="inputNeuronsCount">number of input neurons</param>
        /// <param name="outputNeuronsCount">number of output neurons</param>
        /// <returns>instance of Kohonen network</returns>
        public static Kohonen CreateKohonen(int inputNeuronsCount, int outputNeuronsCount)
        {
            Kohonen nnet = new Kohonen(inputNeuronsCount, outputNeuronsCount);
            return nnet;
        }


        /// <summary>
        /// Creates and returns a new instance of Hebbian network
        /// </summary>
        /// <param name="inputNeuronsCount">number of neurons in input layer</param>
        /// <param name="outputNeuronsCount">number of neurons in output layer</param>
        /// <param name="transferFunctionType">neuron's transfer function type</param>
        /// <returns>instance of Hebbian network</returns>
        public static SupervisedHebbianNetwork CreateSupervisedHebbian(int inputNeuronsCount,
                int outputNeuronsCount, TransferFunctionType transferFunctionType)
        {
            SupervisedHebbianNetwork nnet = new SupervisedHebbianNetwork(inputNeuronsCount,
                    outputNeuronsCount, transferFunctionType);
            return nnet;
        }
        
        /// <summary>
        /// Creates and returns a new instance of Unsupervised Hebbian Network
        /// </summary>
        /// <param name="inputNeuronsCount">number of neurons in input layer</param>
        /// <param name="outputNeuronsCount">number of neurons in output layer</param>
        /// <param name="transferFunctionType">neuron's transfer function type</param>
        /// <returns>instance of Unsupervised Hebbian Network</returns>
        public static UnsupervisedHebbianNetwork CreateUnsupervisedHebbian(int inputNeuronsCount,
                int outputNeuronsCount, TransferFunctionType transferFunctionType)
        {
            UnsupervisedHebbianNetwork nnet = new UnsupervisedHebbianNetwork(inputNeuronsCount,
                    outputNeuronsCount, transferFunctionType);
            return nnet;
        }

        /// <summary>
        /// Creates and returns a new instance of Max Net network
        /// </summary>
        /// <param name="neuronsCount">number of neurons (same num in input and output layer)</param>
        /// <returns>instance of Max Net network</returns>
        public static MaxNet CreateMaxNet(int neuronsCount)
        {
            MaxNet nnet = new MaxNet(neuronsCount);
            return nnet;
        }
        
        /// <summary>
        /// Creates and returns a new instance of Instar network
        /// </summary>
        /// <param name="inputNeuronsCount">number of input neurons</param>
        /// <returns>instance of Instar network</returns>
        public static Instar CreateInstar(int inputNeuronsCount)
        {
            Instar nnet = new Instar(inputNeuronsCount);
            return nnet;
        }

        /// <summary>
        /// Creates and returns a new instance of Outstar network
        /// </summary>
        /// <param name="outputNeuronsCount">number of output neurons</param>
        /// <returns>instance of Outstar network</returns>
        public static Outstar CreateOutstar(int outputNeuronsCount)
        {
            Outstar nnet = new Outstar(outputNeuronsCount);
            return nnet;
        }


        /// <summary>
        /// Creates and returns a new instance of competitive network 
        /// </summary>
        /// <param name="inputNeuronsCount">number of neurons in input layer</param>
        /// <param name="outputNeuronsCount">number of neurons in output layer</param>
        /// <returns>instance of CompetitiveNetwork</returns>
        public static CompetitiveNetwork CreateCompetitiveNetwork(
                int inputNeuronsCount, int outputNeuronsCount)
        {
            CompetitiveNetwork nnet = new CompetitiveNetwork(inputNeuronsCount, outputNeuronsCount);
            return nnet;
        }


        /// <summary>
        /// Creates and returns a new instance of RBF network
        /// </summary>
        /// <param name="inputNeuronsCount">number of neurons in input layer</param>
        /// <param name="rbfNeuronsCount">number of neurons in RBF layer</param>
        /// <param name="outputNeuronsCount">number of neurons in output layer</param>
        /// <returns>instance of RBF network</returns>
        public static RbfNetwork CreateRbfNetwork(int inputNeuronsCount,
                int rbfNeuronsCount, int outputNeuronsCount)
        {
            RbfNetwork nnet = new RbfNetwork(inputNeuronsCount, rbfNeuronsCount,
                    outputNeuronsCount);
            return nnet;
        }

        /// <summary>
        /// Sets default input and output neurons for network (first layer as input,
        /// last as output)
        /// </summary>
        /// <param name="nnet"></param>
        public static void SetDefaultIO(NeuralNetwork nnet)
        {
            IList<Neuron> inputNeurons = new List<Neuron>();
            Layer firstLayer = (Layer)nnet.Layers[0];
            foreach (Neuron neuron in firstLayer.Neurons)
            {
                if (!(neuron is BiasNeuron))
                {  // dont set input to bias neurons
                    inputNeurons.Add(neuron);
                }
            }

            IList<Neuron> outputNeurons = ((Layer)nnet.Layers[nnet.Layers.Count - 1]).Neurons;

            nnet.InputNeurons = inputNeurons;
            nnet.OutputNeurons = outputNeurons;
        }
    }
}
