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

namespace Neuroph.NNet
{
    /// <summary>
    /// Max Net neural network with competitive learning rule.
    /// 
    /// Author: Zoran Sevarac <sevarac@gmail.com>
    /// C# Translation: Miley Thomas (http://www.heatonresearch.com)
    /// </summary>
    [Serializable]
    public class MaxNet : NeuralNetwork
    {
        /// <summary>
        /// Creates new Maxnet network with specified neuron number
        /// </summary>
        /// <param name="neuronsCount">number of neurons in MaxNet network (same number in input and output layer)</param>
        public MaxNet(int neuronsCount)
        {
            this.CreateNetwork(neuronsCount);
        }

       
        /// <summary>
        /// Creates MaxNet network architecture
        /// </summary>
        /// <param name="neuronsCount">neuron number in network</param>
        private void CreateNetwork(int neuronsCount)
        {

            // set network type
            this.NetworkType = NeuralNetworkType.MAXNET;

            // createLayer input layer in layer
            Layer inputLayer = LayerFactory.createLayer(neuronsCount,
                    new NeuronProperties());
            this.AddLayer(inputLayer);

            // createLayer properties for neurons in output layer
            NeuronProperties neuronProperties = new NeuronProperties();
            neuronProperties.SetProperty("neuronType", typeof(CompetitiveNeuron));
            neuronProperties.SetProperty("transferFunction", TransferFunctionType.RAMP);

            // createLayer full connectivity in competitive layer
            CompetitiveLayer competitiveLayer = new CompetitiveLayer(neuronsCount, neuronProperties);

            // add competitive layer to network
            this.AddLayer(competitiveLayer);

            double competitiveWeight = -(1 / (double)neuronsCount);
            // createLayer full connectivity within competitive layer
            ConnectionFactory.FullConnect(competitiveLayer, competitiveWeight, 1);

            // createLayer forward connectivity from input to competitive layer
            ConnectionFactory.ForwardConnect(inputLayer, competitiveLayer, 1);

            // set input and output cells for this network
            NeuralNetworkFactory.SetDefaultIO(this);
        }

    }
}
