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
using Neuroph.NNet.Comp;

namespace Neuroph.NNet.Learning
{
    /// <summary>
    /// Delta rule learning algorithm for perceptrons with step functions.
    ///
    /// The difference to Perceptronlearning is that Delta Rule calculates error
    /// before the non-lnear step transfer function
    /// 
    /// Author: Zoran Sevarac <sevarac@gmail.com>
    /// C# Translation: Miley Thomas (http://www.heatonresearch.com)
    /// </summary>
    [Serializable]
    public class BinaryDeltaRule : PerceptronLearning
    {
        /// <summary>
        /// The errorCorrection parametar of this learning algorithm
        /// </summary>
        private double errorCorrection = 0.1;

        /// <summary>
        /// Creates new BinaryDeltaRule learning
        /// </summary>
        public BinaryDeltaRule()
            : base()
        {

        }

        /// <summary>
        /// Creates new BinaryDeltaRule learning for the specified neural network 
        /// </summary>
        /// <param name="neuralNetwork"></param>
        public BinaryDeltaRule(NeuralNetwork neuralNetwork)
            : base(neuralNetwork)
        {
        }

        /// <summary>
        /// This method implements weight update procedure for the whole network for
        /// this learning rule 
        /// </summary>
        /// <param name="patternError">single pattern error vector
        /// if the output is 0 and required value is 1, increase rthe weights
        /// if the output is 1 and required value is 0, decrease the weights
        /// otherwice leave weights unchanged
        /// </param>
        protected override void UpdateNetworkWeights(double[] patternError)
        {
            int i = 0;
            foreach (Neuron outputNeuron in this.NeuralNetwork.OutputNeurons)
            {
                ThresholdNeuron neuron = (ThresholdNeuron)outputNeuron;
                double outputError = patternError[i];
                double thresh = neuron.Thresh;
                double netInput = neuron.NetInput;
                double threshError = thresh - netInput; // distance from zero
                // use output error to decide weathet to inrease, decrase or leave unchanged weights
                // add errorCorrection to threshError to move above or below zero
                double neuronError = outputError * (Math.Abs(threshError) + errorCorrection);

                // use same adjustment principle as PerceptronLearning,
                // just with different neuronError
                neuron.Error = neuronError;
                UpdateNeuronWeights(neuron);

                i++;
            } // for
        }
    }
}
