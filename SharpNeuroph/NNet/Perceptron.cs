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
using Neuroph.Core;
using Neuroph.Util;
using Neuroph.NNet.Comp;
using Neuroph.NNet.Learning;

namespace Neuroph.NNet
{
    /// <summary>
    /// Perceptron neural network with some LMS based learning algorithm.
    /// 
    /// Author: Zoran Sevarac <sevarac@gmail.com>
    /// C# Translation: Miley Thomas (http://www.heatonresearch.com)
    /// </summary>
    [Serializable]
    public class Perceptron : NeuralNetwork
    {

        /// <summary>
        /// Creates new Perceptron with specified number of neurons in input and
        /// output layer, with Step trqansfer function
        /// </summary>
        /// <param name="inputNeuronsCount">number of neurons in input layer</param>
        /// <param name="outputNeuronsCount">number of neurons in output layer</param>
        public Perceptron(int inputNeuronsCount, int outputNeuronsCount)
        {
            this.CreateNetwork(inputNeuronsCount, outputNeuronsCount, TransferFunctionType.STEP);
        }


        /// <summary>
        /// Creates new Perceptron with specified number of neurons in input and
        /// output layer, and specified transfer function
        /// </summary>
        /// <param name="inputNeuronsCount">number of neurons in input layer</param>
        /// <param name="outputNeuronsCount">number of neurons in output layer</param>
        /// <param name="transferFunctionType">transfer function type</param>
        public Perceptron(int inputNeuronsCount, int outputNeuronsCount, TransferFunctionType transferFunctionType)
        {
            this.CreateNetwork(inputNeuronsCount, outputNeuronsCount, transferFunctionType);
        }


        /// <summary>
        /// Creates perceptron architecture with specified number of neurons in input
        /// and output layer, specified transfer function
        /// </summary>
        /// <param name="inputNeuronsCount">number of neurons in input layer</param>
        /// <param name="outputNeuronsCount">number of neurons in output layer</param>
        /// <param name="transferFunctionType">neuron transfer function type</param>
        private void CreateNetwork(int inputNeuronsCount, int outputNeuronsCount, TransferFunctionType transferFunctionType)
        {
            // set network type
            this.NetworkType = NeuralNetworkType.PERCEPTRON;

            // init neuron settings for input layer
            NeuronProperties inputNeuronProperties = new NeuronProperties();
            inputNeuronProperties.SetProperty("transferFunction", TransferFunctionType.LINEAR);

            // create input layer
            Layer inputLayer = LayerFactory.createLayer(inputNeuronsCount, inputNeuronProperties);
            this.AddLayer(inputLayer);

            NeuronProperties outputNeuronProperties = new NeuronProperties();
            outputNeuronProperties.SetProperty("neuronType", typeof(ThresholdNeuron));
            outputNeuronProperties.SetProperty("thresh", Math.Abs(ThreadSafeRandom.NextDouble()));
            outputNeuronProperties.SetProperty("transferFunction", transferFunctionType);
            // for sigmoid and tanh transfer functions set slope propery
            outputNeuronProperties.SetProperty("transferFunction.slope", 1);

            // createLayer output layer
            Layer outputLayer = LayerFactory.createLayer(outputNeuronsCount, outputNeuronProperties);
            this.AddLayer(outputLayer);

            // create full conectivity between input and output layer
            ConnectionFactory.FullConnect(inputLayer, outputLayer);

            // set input and output cells for this network
            NeuralNetworkFactory.SetDefaultIO(this);

            this.LearningRule = new BinaryDeltaRule();

        }

    }
}
