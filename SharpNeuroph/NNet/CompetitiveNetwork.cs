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
using Neuroph.Core.Input;
using Neuroph.NNet.Learning;

namespace Neuroph.NNet
{
    /// <summary>
    /// Two layer neural network with competitive learning rule.
    /// 
    /// Author: Zoran Sevarac <sevarac@gmail.com>
    /// C# Translation: Miley Thomas (http://www.heatonresearch.com)
    /// </summary>
    [Serializable]
    public class CompetitiveNetwork : NeuralNetwork
    {
        /// <summary>
        /// Creates new competitive network with specified neuron number
        /// </summary>
        /// <param name="inputNeuronsCount">number of input neurons</param>
        /// <param name="outputNeuronsCount">number of output neurons</param>
        public CompetitiveNetwork(int inputNeuronsCount, int outputNeuronsCount)
        {
            this.CreateNetwork(inputNeuronsCount, outputNeuronsCount);
        }


        /// <summary>
        /// Creates Competitive network architecture
        /// </summary>
        /// <param name="inputNeuronsCount">input neurons number</param>
        /// <param name="outputNeuronsCount">neuron properties</param>
        private void CreateNetwork(int inputNeuronsCount, int outputNeuronsCount)
        {
            // set network type
            this.NetworkType = NeuralNetworkType.COMPETITIVE;

            // createLayer input layer
            Layer inputLayer = LayerFactory.createLayer(inputNeuronsCount, new NeuronProperties());
            this.AddLayer(inputLayer);

            // createLayer properties for neurons in output layer
            NeuronProperties neuronProperties = new NeuronProperties();
            neuronProperties.SetProperty("neuronType", typeof(CompetitiveNeuron));
            neuronProperties.SetProperty("weightsFunction", typeof(WeightedInput));
            neuronProperties.SetProperty("summingFunction", typeof(Sum));
            neuronProperties.SetProperty("transferFunction", TransferFunctionType.RAMP);

            // createLayer full connectivity in competitive layer
            CompetitiveLayer competitiveLayer = new CompetitiveLayer(outputNeuronsCount, neuronProperties);

            // add competitive layer to network
            this.AddLayer(competitiveLayer);

            double competitiveWeight = -(1 / (double)outputNeuronsCount);
            // createLayer full connectivity within competitive layer
            ConnectionFactory.FullConnect(competitiveLayer, competitiveWeight, 1);

            // createLayer full connectivity from input to competitive layer
            ConnectionFactory.FullConnect(inputLayer, competitiveLayer);

            // set input and output cells for this network
            NeuralNetworkFactory.SetDefaultIO(this);

            this.LearningRule = new CompetitiveLearning(this);
        }

    }
}
