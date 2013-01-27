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

namespace Neuroph.NNet.Learning
{
    /// <summary>
    /// Supervised hebbian learning rule.
    /// 
    /// Author: Zoran Sevarac <sevarac@gmail.com>
    /// C# Translation: Miley Thomas (http://www.heatonresearch.com)
    /// </summary>
    [Serializable]
    public class SupervisedHebbianLearning : LMS
    {
        /// <summary>
        /// Creates new instance of SupervisedHebbianLearning algorithm
        /// </summary>
        public SupervisedHebbianLearning()
            : base()
        {
        }

        /// <summary>
        /// Creates new instance of SupervisedHebbianLearning algorithm  for the specified
        /// neural network. 
        /// </summary>
        /// <param name="neuralNetwork">neural network to train</param>
        public SupervisedHebbianLearning(NeuralNetwork neuralNetwork)
            : base(neuralNetwork)
        {

        }
        
        /// <summary>
        /// Learn method override without network error and iteration limit
        /// Implements just one pass through the training set Used for testing -
        /// debuging algorithm 
        /// 
        /// Trains network with the pattern from the specified training element
        /// </summary>
        /// <param name="trainingElement">supervised training element which contains input and desired
        /// output</param>
        protected override void LearnPattern(SupervisedTrainingElement trainingElement)
        {
            double[] input = trainingElement.Input;
            this.NeuralNetwork.SetInput(input);
            this.NeuralNetwork.Calculate();
            double[] output = this.NeuralNetwork.Output;
            double[] desiredOutput = trainingElement.DesiredOutput;
            double[] patternError = this.GetPatternError(output, desiredOutput);
            this.UpdateTotalNetworkError(patternError);
            this.UpdateNetworkWeights(desiredOutput);
        }

        /// <summary>
        /// This method implements weight update procedure for the whole network for
        /// this learning rule 
        /// </summary>
        /// <param name="desiredOutput">desired network output</param>
        protected override void UpdateNetworkWeights(double[] desiredOutput)
        {
            int i = 0;
            foreach (Neuron neuron in this.NeuralNetwork.OutputNeurons)
            {
                this.UpdateNeuronWeights(neuron, desiredOutput[i]);
                i++;
            }

        }


        /// <summary>
        /// This method implements weights update procedure for the single neuron
        /// </summary>
        /// <param name="neuron">neuron to update weights</param>
        /// <param name="desiredOutput"></param>
        protected void UpdateNeuronWeights(Neuron neuron, double desiredOutput)
        {
            foreach (Connection connection in neuron.InputConnections)
            {
                double input = connection.Input;
                double deltaWeight = input * desiredOutput * this.LearningRate;
                connection.ConnectionWeight.Inc(deltaWeight);
            }
        }
    }
}
