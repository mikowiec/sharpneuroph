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
    /// Oja learning rule wich is a modification of unsupervised hebbian learning.
    /// 
    /// Author: Zoran Sevarac <sevarac@gmail.com>
    /// C# Translation: Miley Thomas (http://www.heatonresearch.com)
    /// </summary>
    [Serializable]
    public class OjaLearning : UnsupervisedHebbianLearning
    {
        /// <summary>
        /// Creates an instance of OjaLearning algorithm
        /// </summary>
        public OjaLearning()
            : base()
        {
        }

        /// <summary>
        /// Creates an instance of OjaLearning algorithm  for the specified 
        /// neural network
        /// </summary>
        /// <param name="neuralNetwork">neural network to train</param>
        public OjaLearning(NeuralNetwork neuralNetwork)
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
                double weight = connection.ConnectionWeight.Value;
                double deltaWeight = (input - output * weight) * output * this.LearningRate;
                connection.ConnectionWeight.Inc(deltaWeight);
            }
        }
    }
}
