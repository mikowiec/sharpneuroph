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
using Neuroph.Util;
using Neuroph.Core;

namespace Neuroph.NNet.Comp
{
    /// <summary>
    /// Represents layer of competitive neurons, and provides methods for competition.
    /// 
    /// Author: Zoran Sevarac <sevarac@gmail.com>
    /// C# Translation: Miley Thomas (http://www.heatonresearch.com)
    /// </summary>
    [Serializable]
    public class CompetitiveLayer : Layer
    {
        /// <summary>
        /// Max iterations for neurons to compete
        /// This is neccesery to limit, otherwise if there is no winner there will
        /// be endless loop.
        /// </summary>
        public int MaxIterations { get; set; }

        /// <summary>
        /// The competition winner for this layer
        /// </summary>
        private CompetitiveNeuron winner;

        /// <summary>
        /// Create an instance of CompetitiveLayer with the specified number of
        /// neurons with neuron properties 
        /// </summary>
        /// <param name="neuronNum">neuron number in this layer</param>
        /// <param name="neuronProperties">properties for the nurons in this layer</param>
        public CompetitiveLayer(int neuronNum, NeuronProperties neuronProperties)
            : base(neuronNum, neuronProperties)
        {
            MaxIterations = 100;
        }

        /// <summary>
        /// Performs calculaton for all neurons in this layer
        /// </summary>
        public override void Calculate()
        {
            bool hasWinner = false;

            int iterationsCount = 0;

            while (!hasWinner)
            {
                int fireingNeurons = 0;
                foreach (Neuron neuron in this.neurons)
                {
                    neuron.Calculate();
                    if (neuron.Output > 0)
                        fireingNeurons += 1;
                } // for

                if (iterationsCount > this.MaxIterations) break;

                if (fireingNeurons == 1)
                    hasWinner = true;
                iterationsCount++;

            } // while !done

            if (hasWinner)
            {
                // now set reference to winner
                double maxOutput = Double.MinValue;

                foreach (Neuron neuron in this.neurons)
                {
                    CompetitiveNeuron cNeuron = (CompetitiveNeuron)neuron;
                    cNeuron.IsCompeting = false; // turn off competing mode
                    if (cNeuron.Output > maxOutput)
                    {
                        maxOutput = cNeuron.Output;
                        this.winner = cNeuron;
                    }
                }
            }

        }

        /// <summary>
        /// Returns the winning neuron for this layer
        /// </summary>
        public CompetitiveNeuron Winner
        {
            get
            {
                return this.winner;
            }
        }       
    }
}
