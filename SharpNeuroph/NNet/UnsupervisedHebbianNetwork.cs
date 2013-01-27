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
using Neuroph.NNet.Learning;

namespace Neuroph.NNet
{
    /// <summary>
    /// Hebbian neural network with unsupervised Hebbian learning algorithm.
    /// 
    /// Author: Zoran Sevarac <sevarac@gmail.com>
    /// C# Translation: Miley Thomas (http://www.heatonresearch.com)
    /// </summary>
    [Serializable]
    public class UnsupervisedHebbianNetwork : NeuralNetwork
    {
        /// <summary>
        /// Creates an instance of Unsuervised Hebian net with specified number 
        /// of neurons in input and output layer 
        /// </summary>
        /// <param name="inputNeuronsNum">number of neurons in input layer</param>
        /// <param name="outputNeuronsNum">number of neurons in output layer</param>
        public UnsupervisedHebbianNetwork(int inputNeuronsNum, int outputNeuronsNum)
        {
            this.CreateNetwork(inputNeuronsNum, outputNeuronsNum,
                TransferFunctionType.LINEAR);

        }


        /// <summary>
        /// Creates an instance of Unsuervised Hebian net with specified number
        /// of neurons in input layer and output layer, and transfer function 
        /// </summary>
        /// <param name="inputNeuronsNum">number of neurons in input layer</param>
        /// <param name="outputNeuronsNum">number of neurons in output layer</param>
        /// <param name="transferFunctionType">transfer function type id</param>
        public UnsupervisedHebbianNetwork(int inputNeuronsNum, int outputNeuronsNum,
            TransferFunctionType transferFunctionType)
        {
            this.CreateNetwork(inputNeuronsNum, outputNeuronsNum,
                transferFunctionType);
        }


        /// <summary>
        /// Creates an instance of Unsuervised Hebian net with specified number
        /// of neurons in input layer and output layer, and transfer function 
        /// </summary>
        /// <param name="inputNeuronsNum">number of neurons in input layer</param>
        /// <param name="outputNeuronsNum">number of neurons in output layer</param>
        /// <param name="transferFunctionType">transfer function type</param>
        private void CreateNetwork(int inputNeuronsNum, int outputNeuronsNum,
            TransferFunctionType transferFunctionType)
        {

            // init neuron properties
            NeuronProperties neuronProperties = new NeuronProperties();
            //		neuronProperties.setProperty("bias", new Double(-Math
            //				.abs(Math.random() - 0.5))); // Hebbian network cann not work
            // without bias
            neuronProperties.SetProperty("transferFunction", transferFunctionType);
            neuronProperties.SetProperty("transferFunction.slope", 1);

            // set network type code
            this.NetworkType = NeuralNetworkType.UNSUPERVISED_HEBBIAN_NET;

            // createLayer input layer
            Layer inputLayer = LayerFactory.createLayer(inputNeuronsNum,
                neuronProperties);
            this.AddLayer(inputLayer);

            // createLayer output layer
            Layer outputLayer = LayerFactory.createLayer(outputNeuronsNum,
                neuronProperties);
            this.AddLayer(outputLayer);

            // createLayer full conectivity between input and output layer
            ConnectionFactory.FullConnect(inputLayer, outputLayer);

            // set input and output cells for this network
            NeuralNetworkFactory.SetDefaultIO(this);

            // set appropriate learning rule for this network
            this.LearningRule = new UnsupervisedHebbianLearning(this);
            //this.setLearningRule(new OjaLearning(this));
        }
    }
}
