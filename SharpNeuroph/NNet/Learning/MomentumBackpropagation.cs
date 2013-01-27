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

namespace Neuroph.NNet.Learning
{
    /// <summary>
    /// Backpropagation learning rule with momentum.
    /// 
    /// Author: Zoran Sevarac <sevarac@gmail.com>
    /// C# Translation: Miley Thomas (http://www.heatonresearch.com)
    /// </summary>
    [Serializable]
    public class MomentumBackpropagation : BackPropagation
    {
        /// <summary>
        /// Momentum factor
        /// </summary>
        protected double Momentum { get; set; }


        /// <summary>
        /// Creates new instance of MomentumBackpropagation learning
        /// </summary>
        public MomentumBackpropagation()
            : base()
        {

        }


        /// <summary>
        /// Creates new instance of MomentumBackpropagation learning for the specified neural network 
        /// </summary>
        /// <param name="neuralNetwork">neural network to train</param>
        public MomentumBackpropagation(NeuralNetwork neuralNetwork)
            : base(neuralNetwork)
        {
            Momentum = 0.25d;
        }
        
        /// <summary>
        /// This method implements weights update procedure for the single neuron
        /// for the backpropagation with momentum factor 
        /// </summary>
        /// <param name="neuron">neuron to update weights</param>
        protected override void UpdateNeuronWeights(Neuron neuron)
        {
            foreach (Connection connection in neuron.InputConnections)
            {
                double input = connection.Input;
                if (input == 0)
                {
                    continue;
                }
                double neuronError = neuron.Error;

                Weight weight = connection.ConnectionWeight;

                double currentWeighValue = weight.Value;
                double previousWeightValue = weight.PreviousValue;
                double deltaWeight = this.LearningRate * neuronError * input +
                    Momentum * (currentWeighValue - previousWeightValue);

                weight.PreviousValue = currentWeighValue;
                weight.Inc(deltaWeight);
            }
        }

    }
}
