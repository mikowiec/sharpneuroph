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
    /// Instar neural network with Instar learning rule.
    /// 
    /// Author: Zoran Sevarac <sevarac@gmail.com>
    /// C# Translation: Miley Thomas (http://www.heatonresearch.com)
    /// </summary>
    [Serializable]
    public class Instar : NeuralNetwork
    {
        /// <summary>
        /// Creates new Instar with specified number of input neurons.
        /// </summary>
        /// <param name="inputNeuronsCount">number of neurons in input layer</param>
        public Instar(int inputNeuronsCount)
        {
            this.CreateNetwork(inputNeuronsCount);
        }


        /// <summary>
        /// Creates Instar architecture with specified number of input neurons
        /// </summary>
        /// <param name="inputNeuronsCount">number of neurons in input layer</param>
        private void CreateNetwork(int inputNeuronsCount)
        {

            // set network type
            this.NetworkType = NeuralNetworkType.INSTAR;

            // init neuron settings for this type of network
            NeuronProperties neuronProperties = new NeuronProperties();
            neuronProperties.SetProperty("transferFunction", TransferFunctionType.STEP);

            // create input layer
            Layer inputLayer = LayerFactory.createLayer(inputNeuronsCount, neuronProperties);
            this.AddLayer(inputLayer);

            // createLayer output layer
            neuronProperties.SetProperty("transferFunction", TransferFunctionType.STEP);
            Layer outputLayer = LayerFactory.createLayer(1, neuronProperties);
            this.AddLayer(outputLayer);

            // create full conectivity between input and output layer
            ConnectionFactory.FullConnect(inputLayer, outputLayer);

            // set input and output cells for this network
            NeuralNetworkFactory.SetDefaultIO(this);

            // set appropriate learning rule for this network
            this.LearningRule = new InstarLearning(this);
        }

    }
}
