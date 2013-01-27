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
using Neuroph.Core.Learning;
using Neuroph.NNet.Comp;

namespace Neuroph.NNet.Learning
{
    /// <summary>
    /// Competitive learning rule.
    /// 
    /// Author: Zoran Sevarac <sevarac@gmail.com>
    /// C# Translation: Miley Thomas (http://www.heatonresearch.com)
    /// </summary>
    [Serializable]
    public class CompetitiveLearning : UnsupervisedLearning
    {

        /// <summary>
        /// Creates new instance of CompetitiveLearning
        /// </summary>
        public CompetitiveLearning()
            : base()
        {

        }

        /// <summary>
        /// Creates new instance of CompetitiveLearning for the specified neural network 
        /// </summary>
        /// <param name="neuralNetwork"></param>
        public CompetitiveLearning(NeuralNetwork neuralNetwork)
            : base(neuralNetwork)
        {

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
        /// Adjusts weights for the winning neuron
        /// </summary>
        protected override void AdjustWeights()
        {
            // find active neuron in output layer
            // TODO : change idx, in general case not 1
            CompetitiveNeuron winningNeuron = ((CompetitiveLayer)this.NeuralNetwork
                    .GetLayerAt(1)).Winner;

            IList<Connection> inputConnections = winningNeuron
                    .ConnectionsFromOtherLayers;

            foreach (Connection connection in inputConnections)
            {
                double weight = connection.ConnectionWeight.Value;
                double input = connection.Input;
                double deltaWeight = this.LearningRate * (input - weight);
                connection.ConnectionWeight.Inc(deltaWeight);
            }
        }

    }
}
