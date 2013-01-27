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
    /// Unsupervised hebbian learning rule.
    /// 
    /// Author: Zoran Sevarac <sevarac@gmail.com>
    /// C# Translation: Miley Thomas (http://www.heatonresearch.com)
    /// </summary>
    [Serializable]
    public class UnsupervisedHebbianLearning : UnsupervisedLearning
    {
        /// <summary>
        /// Creates new instance of UnsupervisedHebbianLearning algorithm
        /// </summary>
        public UnsupervisedHebbianLearning()
            : base()
        {
            this.LearningRate = 0.1d;
        }


        /// <summary>
        /// Creates an instance of UnsupervisedHebbianLearning algorithm  for the specified 
        /// neural network 
        /// </summary>
        /// <param name="neuralNetwork">neural network to train</param>
        public UnsupervisedHebbianLearning(NeuralNetwork neuralNetwork)
            : base(neuralNetwork)
        {

            this.LearningRate = 0.1;
        }


        /// <summary>
        /// This method does one learning epoch for the unsupervised learning rules.
        /// It iterates through the training set and trains network weights for each
        /// element. Stops learning after one epoch.
        /// </summary>
        /// <param name="trainingSet">training set for training network</param>
        public override void DoLearningEpoch(TrainingSet trainingSet)
        {
            base.DoLearningEpoch(trainingSet);
            StopLearning(); // stop learning ahter one learning epoch
        }

        /// <summary>
        /// Adjusts weights for the output neurons
        /// </summary>
        protected override void AdjustWeights()
        {
            foreach (Neuron neuron in this.NeuralNetwork.OutputNeurons)
            {
                this.UpdateNeuronWeights(neuron);
            }
        }

        
        /// <summary>
        /// This method implements weights update procedure for the single neuron 
        /// </summary>
        /// <param name="neuron">neuron to update weights</param>
        protected virtual void UpdateNeuronWeights(Neuron neuron)
        {
            double output = neuron.Output;

            foreach (Connection connection in neuron.InputConnections)
            {
                double input = connection.Input;
                double deltaWeight = input * output * this.LearningRate;
                connection.ConnectionWeight.Inc(deltaWeight);
            }
        }
    }
}
