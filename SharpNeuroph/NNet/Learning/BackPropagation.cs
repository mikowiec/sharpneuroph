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
using Neuroph.Core.Transfer;

namespace Neuroph.NNet.Learning
{
    /// <summary>
    /// Back Propagation learning rule for Multi Layer Perceptron neural networks.
    /// 
    /// Author: Zoran Sevarac <sevarac@gmail.com>
    /// C# Translation: Miley Thomas (http://www.heatonresearch.com)
    /// </summary>
    [Serializable]
    public class BackPropagation : SigmoidDeltaRule
    {
        /// <summary>
        /// Creates new instance of BackPropagation learning
        /// </summary>
        public BackPropagation()
            : base()
        {
        }
        
        /// <summary>
        /// Creates new instance of BackPropagation learning for the specified neural network 
        /// </summary>
        /// <param name="neuralNetwork"></param>
        public BackPropagation(NeuralNetwork neuralNetwork)
            : base(neuralNetwork)
        {

        }

        /// <summary>
        /// This method implements weight update procedure for the whole network
        /// for the specified  error vector 
        /// </summary>
        /// <param name="patternError">single pattern error vector</param>
        protected override void UpdateNetworkWeights(double[] patternError)
        {
            this.AdjustOutputNeurons(patternError);
            this.AdjustHiddenLayers();
        }

        /// <summary>
        /// This method implements weights adjustment for the hidden layers
        /// </summary>
        protected void AdjustHiddenLayers()
        {
            int layerNum = this.NeuralNetwork.LayersCount;

            for (int i = layerNum - 2; i > 0; i--)
            {
                Layer layer = this.NeuralNetwork.GetLayerAt(i);

                foreach (Neuron neuron in layer.Neurons)
                {
                    double delta = this.CalculateDelta(neuron);
                    neuron.Error = delta;
                    this.UpdateNeuronWeights(neuron);
                } // for
            } // for
        }
        
        /// <summary>
        /// Calculates and returns delta parameter (neuron error) for the specified
        /// neuron 
        /// </summary>
        /// <param name="neuron">neuron to calculate error for</param>
        /// <returns>(neuron error) for the specified neuron</returns>
        private double CalculateDelta(Neuron neuron)
        {
            IList<Connection> connectedTo = ((Neuron)neuron).OutConnections;

            double delta_sum = 0d;
            foreach (Connection connection in connectedTo)
            {
                double d = connection.ToNeuron.Error
                        * connection.ConnectionWeight.Value;
                delta_sum += d; // weighted sum from the next layer
            } // for

            TransferFunction transferFunction = neuron.TransferFunction;
            double netInput = neuron.NetInput;
            double f1 = transferFunction.CalculateDerivative(netInput);
            double delta = f1 * delta_sum;
            return delta;
        }
    }
}
