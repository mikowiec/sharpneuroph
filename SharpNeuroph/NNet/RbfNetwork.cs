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
using Neuroph.Core.Input;
using Neuroph.Core.Transfer;
using Neuroph.NNet.Learning;

namespace Neuroph.NNet
{
    /// <summary>
    /// Radial basis function neural network.
    /// 
    /// Author: Zoran Sevarac <sevarac@gmail.com>
    /// C# Translation: Miley Thomas (http://www.heatonresearch.com)
    /// </summary>
    [Serializable]
    public class RbfNetwork : NeuralNetwork
    {
        /// <summary>
        /// Creates new RbfNetwork with specified number of neurons in input, rbf and output layer 
        /// </summary>
        /// <param name="inputNeuronsCount">number of neurons in input layer</param>
        /// <param name="rbfNeuronsCount">number of neurons in rbf layer</param>
        /// <param name="outputNeuronsCount">number of neurons in output layer</param>
        public RbfNetwork(int inputNeuronsCount, int rbfNeuronsCount, int outputNeuronsCount)
        {
            this.CreateNetwork(inputNeuronsCount, rbfNeuronsCount, outputNeuronsCount);
        }
        
        /// <summary>
        /// Creates RbfNetwork architecture with specified number of neurons in input
        /// layer, output layer and transfer function 
        /// </summary>
        /// <param name="inputNeuronsCount">number of neurons in input layer</param>
        /// <param name="rbfNeuronsCount">number of neurons in rbf layer</param>
        /// <param name="outputNeuronsCount">number of neurons in output layer</param>
        private void CreateNetwork(int inputNeuronsCount, int rbfNeuronsCount,
                int outputNeuronsCount)
        {
            // init neuron settings for this network
            NeuronProperties rbfNeuronProperties = new NeuronProperties();
            rbfNeuronProperties.SetProperty("weightsFunction", typeof(Diference));
            rbfNeuronProperties.SetProperty("summingFunction", typeof(Intensity));
            rbfNeuronProperties.SetProperty("transferFunction", typeof(Gaussian));

            // set network type code
            this.NetworkType = NeuralNetworkType.RBF_NETWORK;

            // create input layer
            Layer inputLayer = LayerFactory.createLayer(inputNeuronsCount, TransferFunctionType.LINEAR);
            this.AddLayer(inputLayer);

            // create rbf layer
            Layer rbfLayer = LayerFactory.createLayer(rbfNeuronsCount, rbfNeuronProperties);
            this.AddLayer(rbfLayer);

            // create output layer
            Layer outputLayer = LayerFactory.createLayer(outputNeuronsCount, TransferFunctionType.LINEAR);
            this.AddLayer(outputLayer);

            // create full conectivity between input and rbf layer
            ConnectionFactory.FullConnect(inputLayer, rbfLayer);
            // create full conectivity between rbf and output layer
            ConnectionFactory.FullConnect(rbfLayer, outputLayer);

            // set input and output cells for this network
            NeuralNetworkFactory.SetDefaultIO(this);

            // set appropriate learning rule for this network
            this.LearningRule = new LMS(this);
        }
    }
}
