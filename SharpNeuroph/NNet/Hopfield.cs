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
    /// Hopfield neural network.
    /// Notes: try to use [1, -1] activation levels, sgn as transfer function, or real numbers for activation
    /// 
    /// Author: Zoran Sevarac <sevarac@gmail.com>
    /// C# Translation: Miley Thomas (http://www.heatonresearch.com)
    /// </summary>
    [Serializable]
    public class Hopfield : NeuralNetwork
    {
        /// <summary>
        /// Creates new Hopfield network with specified neuron number 
        /// </summary>
        /// <param name="neuronsCount">neurons number in Hopfied network</param>
        public Hopfield(int neuronsCount)
        {

            // init neuron settings for hopfield network
            NeuronProperties neuronProperties = new NeuronProperties();
            neuronProperties.SetProperty("neuronType", typeof(InputOutputNeuron));
            neuronProperties.SetProperty("bias", 0);
            neuronProperties.SetProperty("transferFunction", TransferFunctionType.STEP);
            neuronProperties.SetProperty("transferFunction.yHigh", 1);
            neuronProperties.SetProperty("transferFunction.yLow", 0);

            this.CreateNetwork(neuronsCount, neuronProperties);
        }


        /// <summary>
        /// Creates new Hopfield network with specified neuron number and neuron
        /// properties
        /// </summary>
        /// <param name="neuronsCount">neurons number in Hopfied network</param>
        /// <param name="neuronProperties">neuron properties</param>
        public Hopfield(int neuronsCount, NeuronProperties neuronProperties)
        {
            this.CreateNetwork(neuronsCount, neuronProperties);
        }

        /// <summary>
        /// Creates Hopfield network architecture
        /// </summary>
        /// <param name="neuronsCount">neurons number in Hopfied network</param>
        /// <param name="neuronProperties">neuron properties</param>
        private void CreateNetwork(int neuronsCount, NeuronProperties neuronProperties)
        {

            // set network type
            this.NetworkType = NeuralNetworkType.HOPFIELD;

            // createLayer neurons in layer
            Layer layer = LayerFactory.createLayer(neuronsCount, neuronProperties);

            // createLayer full connectivity in layer
            ConnectionFactory.FullConnect(layer, 0.1);

            // add layer to network
            this.AddLayer(layer);

            // set input and output cells for this network
            NeuralNetworkFactory.SetDefaultIO(this);

            // set Hopfield learning rule for this network
            //this.setLearningRule(new HopfieldLearning(this));	
            this.LearningRule = new BinaryHebbianLearning(this);
        }

    }
}
