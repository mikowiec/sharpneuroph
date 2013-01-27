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
using Neuroph.NNet.Comp;
using Neuroph.NNet.Learning;

namespace Neuroph.NNet
{
    /// <summary>
    /// Multi Layer Perceptron neural network with Back propagation learning algorithm.
    /// 
    /// Author: Zoran Sevarac <sevarac@gmail.com>
    /// C# Translation: Miley Thomas (http://www.heatonresearch.com)
    /// </summary>
    [Serializable]
    public class MultiLayerPerceptron : NeuralNetwork
    {
        /// <summary>
        /// Creates new MultiLayerPerceptron with specified number of neurons in layers 
        /// </summary>
        /// <param name="neuronsInLayers">collection of neuron number in layers</param>
        public MultiLayerPerceptron(IList<int> neuronsInLayers)
        {
            // init neuron settings
            NeuronProperties neuronProperties = new NeuronProperties();
            neuronProperties.SetProperty("useBias", true);
            neuronProperties.SetProperty("transferFunction", TransferFunctionType.SIGMOID);

            this.CreateNetwork(neuronsInLayers, neuronProperties);
        }

        public MultiLayerPerceptron(params int[] neuronsInLayers)
        {
            // init neuron settings
            NeuronProperties neuronProperties = new NeuronProperties();
            neuronProperties.SetProperty("useBias", true);
            neuronProperties.SetProperty("transferFunction",
                    TransferFunctionType.SIGMOID);
            neuronProperties.SetProperty("inputFunction", typeof(WeightedSum));

            IList<int> neuronsInLayersVector = new List<int>();
            for (int i = 0; i < neuronsInLayers.Length; i++)
                neuronsInLayersVector.Add(neuronsInLayers[i]);

            this.CreateNetwork(neuronsInLayersVector, neuronProperties);
        }

        public MultiLayerPerceptron(TransferFunctionType transferFunctionType, params int[] neuronsInLayers)
        {
            // init neuron settings
            NeuronProperties neuronProperties = new NeuronProperties();
            neuronProperties.SetProperty("useBias", true);
            neuronProperties.SetProperty("transferFunction", transferFunctionType);
            neuronProperties.SetProperty("inputFunction", typeof(WeightedSum));


            IList<int> neuronsInLayersVector = new List<int>();
            for (int i = 0; i < neuronsInLayers.Length; i++)
                neuronsInLayersVector.Add(neuronsInLayers[i]);

            this.CreateNetwork(neuronsInLayersVector, neuronProperties);
        }

        public MultiLayerPerceptron(IList<int> neuronsInLayers, TransferFunctionType transferFunctionType)
        {
            // init neuron settings
            NeuronProperties neuronProperties = new NeuronProperties();
            neuronProperties.SetProperty("useBias", true);
            neuronProperties.SetProperty("transferFunction", transferFunctionType);

            this.CreateNetwork(neuronsInLayers, neuronProperties);
        }

        
        /// <summary>
        /// Creates new MultiLayerPerceptron net with specified number neurons in
        /// getLayersIterator
        /// </summary>
        /// <param name="neuronsInLayers">collection of neuron numbers in layers</param>
        /// <param name="neuronProperties">neuron propreties</param>
        public MultiLayerPerceptron(IList<int> neuronsInLayers, NeuronProperties neuronProperties)
        {
            this.CreateNetwork(neuronsInLayers, neuronProperties);
        }


        /// <summary>
        /// Creates MultiLayerPerceptron Network architecture - fully connected
        /// feedforward with specified number of neurons in each layer
        /// </summary>
        /// <param name="neuronsInLayers">collection of neuron numbers in getLayersIterator</param>
        /// <param name="neuronProperties">neuron propreties</param>
        protected void CreateNetwork(IList<int> neuronsInLayers, NeuronProperties neuronProperties)
        {

            // set network type
            this.NetworkType = NeuralNetworkType.MULTI_LAYER_PERCEPTRON;

            // create input layer
            NeuronProperties inputNeuronProperties = new NeuronProperties(TransferFunctionType.LINEAR);
            Layer layer = LayerFactory.createLayer(neuronsInLayers[0], inputNeuronProperties);

            bool useBias = true; // use bias neurons by default
            if (neuronProperties.HasProperty("useBias"))
            {
                useBias = (Boolean)neuronProperties.GetProperty("useBias");
            }

            if (useBias)
            {
                layer.AddNeuron(new BiasNeuron());
            }

            this.AddLayer(layer);

            // create layers
            Layer prevLayer = layer;

            //for(Integer neuronsNum : neuronsInLayers)
            for (int layerIdx = 1; layerIdx < neuronsInLayers.Count; layerIdx++)
            {
                int neuronsNum = neuronsInLayers[layerIdx];
                // createLayer layer
                layer = LayerFactory.createLayer(neuronsNum, neuronProperties);

                if (useBias && (layerIdx < (neuronsInLayers.Count - 1)))
                {
                    layer.AddNeuron(new BiasNeuron());
                }

                // add created layer to network
                this.AddLayer(layer);
                // createLayer full connectivity between previous and this layer
                if (prevLayer != null)
                    ConnectionFactory.FullConnect(prevLayer, layer);

                prevLayer = layer;
            }

            // set input and output cells for network
            NeuralNetworkFactory.SetDefaultIO(this);

            // set learnng rule
            //this.setLearningRule(new BackPropagation(this));
            this.LearningRule = new MomentumBackpropagation();
            // this.setLearningRule(new DynamicBackPropagation());

        }

        public void ConnectInputsToOutputs()
        {
            // connect first and last layer
            ConnectionFactory.FullConnect(Layers[0], Layers[Layers.Count - 1], false);
        }

    }
}
