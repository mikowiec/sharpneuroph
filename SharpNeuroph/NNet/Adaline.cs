﻿//
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
    /// Adaline neural network architecture with LMS learning rule.
    /// 
    /// Author: Zoran Sevarac <sevarac@gmail.com>
    /// C# Translation: Miley Thomas (http://www.heatonresearch.com)
    /// </summary>
    [Serializable]
    public class Adaline : NeuralNetwork
    {
        /// <summary>
        /// Creates new Adaline network with specified number of neurons in input
        /// layer 
        /// </summary>
        /// <param name="inputNeuronsCount">number of neurons in input layer</param>
        public Adaline(int inputNeuronsCount)
        {
            this.CreateNetwork(inputNeuronsCount);
        }

        /// <summary>
        /// Creates adaline network architecture with specified number of input neurons 
        /// </summary>
        /// <param name="inputNeuronsCount">number of neurons in input layer</param>
        private void CreateNetwork(int inputNeuronsCount)
        {

            // createLayer neuron settings for this network
            NeuronProperties neuronProperties = new NeuronProperties();

            // set network type code
            this.NetworkType = NeuralNetworkType.ADALINE;

            // createLayer input layer with specified number of neurons
            Layer inputLayer = LayerFactory.createLayer(inputNeuronsCount, neuronProperties);
            this.AddLayer(inputLayer);

            // createLayer output layer (only one neuron)
            Layer outputLayer = LayerFactory.createLayer(1, neuronProperties);
            this.AddLayer(outputLayer);

            // createLayer full conectivity between input and output layer
            ConnectionFactory.FullConnect(inputLayer, outputLayer);

            // set input and output cells for network
            NeuralNetworkFactory.SetDefaultIO(this);

            // set LMS learning rule for this network
            this.LearningRule = new LMS(this);
        }
    }
}
