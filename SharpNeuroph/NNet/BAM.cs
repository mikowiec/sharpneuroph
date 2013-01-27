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
    /// Bidirectional Associative Memory
    /// 
    /// Author: Zoran Sevarac <sevarac@gmail.com>
    /// C# Translation: Miley Thomas (http://www.heatonresearch.com)
    /// </summary>
    [Serializable]
    public class BAM : NeuralNetwork
    {
        /// <summary>
        /// Creates an instance of BAM network with specified number of neurons
        /// in input and output layers.
        /// </summary>
        /// <param name="inputNeuronsCount">number of neurons in input layer</param>
        /// <param name="outputNeuronsCount">number of neurons in output layer</param>
        public BAM(int inputNeuronsCount, int outputNeuronsCount)
        {

            // init neuron settings for BAM network
            NeuronProperties neuronProperties = new NeuronProperties();
            neuronProperties.SetProperty("neuronType", typeof(InputOutputNeuron));
            neuronProperties.SetProperty("bias", 0);
            neuronProperties.SetProperty("transferFunction", TransferFunctionType.STEP);
            neuronProperties.SetProperty("transferFunction.yHigh", 1);
            neuronProperties.SetProperty("transferFunction.yLow", 0);

            this.CreateNetwork(inputNeuronsCount, outputNeuronsCount, neuronProperties);
        }


        /// <summary>
        /// Creates BAM network architecture 
        /// </summary>
        /// <param name="inputNeuronsCount">number of neurons in input layer</param>
        /// <param name="outputNeuronsCount">number of neurons in output layer</param>
        /// <param name="neuronProperties">neuron properties</param>
        private void CreateNetwork(int inputNeuronsCount, int outputNeuronsCount, NeuronProperties neuronProperties)
        {
            // set network type
            this.NetworkType = NeuralNetworkType.BAM;

            // create input layer
            Layer inputLayer = LayerFactory.createLayer(inputNeuronsCount, neuronProperties);
            // add input layer to network
            this.AddLayer(inputLayer);

            // create output layer
            Layer outputLayer = LayerFactory.createLayer(outputNeuronsCount, neuronProperties);
            // add output layer to network
            this.AddLayer(outputLayer);

            // create full connectivity from in to out layer	
            ConnectionFactory.FullConnect(inputLayer, outputLayer);
            // create full connectivity from out to in layer
            ConnectionFactory.FullConnect(outputLayer, inputLayer);

            // set input and output cells for this network
            NeuralNetworkFactory.SetDefaultIO(this);

            // set Hebbian learning rule for this network
            this.LearningRule = new BinaryHebbianLearning(this);
        }
    }
}
