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
using Neuroph.NNet.Comp;

namespace Neuroph.NNet.Learning
{
    /// <summary>
    /// Perceptron learning rule for perceptron neural networks.
    /// 
    /// Author: Zoran Sevarac <sevarac@gmail.com>
    /// C# Translation: Miley Thomas (http://www.heatonresearch.com)
    /// </summary>
    [Serializable]
    public class PerceptronLearning : LMS
    {
        /// <summary>
        /// Creates new PerceptronLearning instance
        /// </summary>
        public PerceptronLearning()
            : base()
        {

        }

        /// <summary>
        /// Creates new PerceptronLearning learning for the specified neural network 
        /// </summary>
        /// <param name="neuralNetwork"></param>
        public PerceptronLearning(NeuralNetwork neuralNetwork)
            : base(neuralNetwork)
        {

        }

        /// <summary>
        /// This method implements weights update procedure for the single neuron
        /// In addition to weights change in LMS it applies change to neuron's threshold 
        /// </summary>
        /// <param name="neuron">neuron to update weights</param>
        protected override void UpdateNeuronWeights(Neuron neuron)
        {
            // adjust the input connection weights with method from superclass
            base.UpdateNeuronWeights(neuron);

            // and adjust the neurons threshold
            ThresholdNeuron thresholdNeuron = (ThresholdNeuron)neuron;
            // get neurons error
            double neuronError = thresholdNeuron.Error;
            // get the neurons threshold
            double thresh = thresholdNeuron.Thresh;
            // calculate new threshold value
            thresh = thresh - this.LearningRate * neuronError;
            // apply the new threshold
            thresholdNeuron.Thresh = thresh;
        }
    }
}
