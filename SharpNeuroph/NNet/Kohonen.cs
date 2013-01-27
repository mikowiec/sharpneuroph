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
    /// Kohonen neural network.
    /// 
    /// Author: Zoran Sevarac <sevarac@gmail.com>
    /// C# Translation: Miley Thomas (http://www.heatonresearch.com)
    /// </summary>
    [Serializable]
    public class Kohonen : NeuralNetwork
    {
        /// <summary>
        /// Creates new Kohonen network with specified number of neurons in input and
        /// map layer 
        /// </summary>
        /// <param name="inputNeuronsCount">number of neurons in input layer</param>
        /// <param name="outputNeuronsCount">number of neurons in output layer</param>
        public Kohonen(int inputNeuronsCount, int outputNeuronsCount)
        {
            this.CreateNetwork(inputNeuronsCount, outputNeuronsCount);
        }
        
        /// <summary>
        /// Creates Kohonen network architecture with specified number of neurons in
        /// input and map layer 
        /// </summary>
        /// <param name="inputNeuronsCount">number of neurons in input layer</param>
        /// <param name="outputNeuronsCount">number of neurons in output layer</param>
        private void CreateNetwork(int inputNeuronsCount, int outputNeuronsCount)
        {

            // specify input neuron properties (use default: weighted sum input with
            // linear transfer)
            NeuronProperties inputNeuronProperties = new NeuronProperties();

            // specify map neuron properties
            NeuronProperties outputNeuronProperties = new NeuronProperties();
            outputNeuronProperties.SetProperty("weightsFunction", typeof(Diference));
            outputNeuronProperties.SetProperty("summingFunction", typeof(Intensity));
            outputNeuronProperties.SetProperty("transferFunction", typeof(Linear));

            // set network type
            this.NetworkType = NeuralNetworkType.KOHONEN;

            // createLayer input layer
            Layer inLayer = LayerFactory.createLayer(inputNeuronsCount,
                    inputNeuronProperties);
            this.AddLayer(inLayer);

            // createLayer map layer
            Layer mapLayer = LayerFactory.createLayer(outputNeuronsCount,
                    outputNeuronProperties);
            this.AddLayer(mapLayer);

            // createLayer full connectivity between input and output layer
            ConnectionFactory.FullConnect(inLayer, mapLayer);

            // set network input and output cells
            NeuralNetworkFactory.SetDefaultIO(this);

            this.LearningRule = new KohonenLearning(this);
        }
    }
}
