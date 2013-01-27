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
    /// Hebbian-like learning algorithm used for Hopfield network. Works with [0, 1] values
    /// 
    /// Author: Zoran Sevarac <sevarac@gmail.com>
    /// C# Translation: Miley Thomas (http://www.heatonresearch.com)
    /// </summary>
    [Serializable]
    public class BinaryHebbianLearning : UnsupervisedHebbianLearning
    {
        /// <summary>
        /// Creates new instance of BinaryHebbianLearning
        /// </summary>
        public BinaryHebbianLearning()
            : base()
        {

        }
        
        /// <summary>
        /// Creates new instance of BinaryHebbianLearning for the specified neural network 
        /// </summary>
        /// <param name="neuralNetwork"></param>
        public BinaryHebbianLearning(NeuralNetwork neuralNetwork)
            : base(neuralNetwork)
        {

        }

        /// <summary>
        /// This method implements weights update procedure for the single neuron 
        /// </summary>
        /// <param name="neuron">neuron to update weights</param>
        protected override void UpdateNeuronWeights(Neuron neuron)
        {
            double output = neuron.Output;
            foreach (Connection connection in neuron.InputConnections)
            {
                double input = connection.Input;

                if (((input > 0) && (output > 0)) || ((input <= 0) && (output <= 0)))
                {
                    connection.ConnectionWeight.Inc(this.LearningRate);
                }
                else
                {
                    connection.ConnectionWeight.Dec(this.LearningRate);
                }
            }
        }

    }
}
