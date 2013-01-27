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
    /// Delta rule learning algorithm for perceptrons with sigmoid (or any other diferentiable continuous) functions.
    /// 
    /// Author: Zoran Sevarac <sevarac@gmail.com>
    /// C# Translation: Miley Thomas (http://www.heatonresearch.com)
    /// </summary>
    [Serializable]
    public class SigmoidDeltaRule : LMS
    {
        /// <summary>
        /// Creates new SigmoidDeltaRule
        /// </summary>
        public SigmoidDeltaRule()
            : base()
        {

        }

        /// <summary>
        /// Creates new SigmoidDeltaRule for the specified neural network 
        /// </summary>
        /// <param name="neuralNetwork">neural network to train</param>
        public SigmoidDeltaRule(NeuralNetwork neuralNetwork)
            : base(neuralNetwork)
        {

        }

        /// <summary>
        /// This method implements weight update procedure for the whole network for
        /// this learning rule
        /// </summary>
        /// <param name="patternError">single pattern error vector</param>
        protected override void UpdateNetworkWeights(double[] patternError)
        {
            this.AdjustOutputNeurons(patternError);
        }

        
        /// <summary>
        /// This method implements weights update procedure for the output neurons 
        /// </summary>
        /// <param name="patternError">single pattern error vector</param>
        protected void AdjustOutputNeurons(double[] patternError)
        {
            int i = 0;
            foreach (Neuron neuron in this.NeuralNetwork.OutputNeurons)
            {
                double outputError = patternError[i];
                if (outputError == 0)
                {
                    neuron.Error = 0;
                    i++;
                    continue;
                }

                TransferFunction transferFunction = neuron.TransferFunction;
                double neuronInput = neuron.NetInput;
                double delta = outputError * transferFunction.CalculateDerivative(neuronInput);
                neuron.Error = delta;
                this.UpdateNeuronWeights(neuron);
                i++;
            } // for				
        }
    }
}
