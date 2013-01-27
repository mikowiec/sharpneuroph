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
using Neuroph.Core.Learning;
using Neuroph.Core;

namespace Neuroph.NNet.Learning
{
    /// <summary>
    /// LMS learning rule for neural networks.
    /// 
    /// Author: Zoran Sevarac <sevarac@gmail.com>
    /// C# Translation: Miley Thomas (http://www.heatonresearch.com)
    /// </summary>
    [Serializable]
    public class LMS : SupervisedLearning
    {
        /// <summary>
        /// Creates new LMS learning rule
        /// </summary>
        public LMS()
            : base()
        {

        }

        /// <summary>
        /// Creates new LMS learning rule for specified neural network 
        /// </summary>
        /// <param name="neuralNetwork">neural network to train</param>
        public LMS(NeuralNetwork neuralNetwork)
            : base(neuralNetwork)
        {

        }

        /// <summary>
        /// Updates total network error with specified pattern error vector
        /// </summary>
        /// <param name="patternError">single pattern error vector</param>
        protected override void UpdateTotalNetworkError(double[] patternError)
        {
            double sqrErrorSum = 0;
            foreach (double error in patternError)
            {
                sqrErrorSum += (error * error);
            }
            this.totalNetworkError += sqrErrorSum / (2 * patternError.Length);
        }


        /// <summary>
        /// This method implements weight update procedure for the whole network for
        /// this learning rule 
        /// </summary>
        /// <param name="patternError">single pattern error vector</param>
        protected override void UpdateNetworkWeights(double[] patternError)
        {
            int i = 0;
            foreach (Neuron neuron in this.NeuralNetwork.OutputNeurons)
            {
                neuron.Error = patternError[i];
                this.UpdateNeuronWeights(neuron);
                i++;
            }
        }


        /// <summary>
        /// This method implements weights update procedure for the single neuron 
        /// </summary>
        /// <param name="neuron">neuron to update weights</param>
        protected virtual void UpdateNeuronWeights(Neuron neuron)
        {
            // get the error for specified neuron
            double neuronError = neuron.Error;
            // iterate through all neuron's input connections
            foreach (Connection connection in neuron.InputConnections)
            {
                // get the input from current connection
                double input = connection.Input;
                // calculate the weight change
                double deltaWeight = this.LearningRate * neuronError * input;
                // apply the weight change
                connection.ConnectionWeight.Inc(deltaWeight);
            }
        }

    }
}
