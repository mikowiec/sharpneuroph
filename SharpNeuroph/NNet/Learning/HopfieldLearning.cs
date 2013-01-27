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
    /// Learning algorithm for the Hopfield neural network.
    /// 
    /// Author: Zoran Sevarac <sevarac@gmail.com>
    /// C# Translation: Miley Thomas (http://www.heatonresearch.com)
    /// </summary>
    [Serializable]
    public class HopfieldLearning : LearningRule
    {
        /// <summary>
        /// Creates new HopfieldLearning
        /// </summary>
        public HopfieldLearning()
            : base()
        {
        }

        /// <summary>
        /// Creates new HopfieldLearning for the specified neural network 
        /// </summary>
        /// <param name="neuralNetwork"></param>
        public HopfieldLearning(NeuralNetwork neuralNetwork)
            : base(neuralNetwork)
        {

        }

        /// <summary>
        /// Calculates weights for the hopfield net to learn the specified training
        /// set 
        /// </summary>
        /// <param name="trainingSet">training set to learn</param>
        public override void Learn(TrainingSet trainingSet)
        {
            int M = trainingSet.Count;
            int N = this.NeuralNetwork.GetLayerAt(0).NeuronsCount;
            Layer hopfieldLayer = this.NeuralNetwork.GetLayerAt(0);

            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    if (j == i)
                        continue;
                    Neuron ni = hopfieldLayer.GetNeuronAt(i);
                    Neuron nj = hopfieldLayer.GetNeuronAt(j);
                    Connection cij = nj.GetConnectionFrom(ni);
                    Connection cji = ni.GetConnectionFrom(nj);
                    double w = 0;
                    for (int k = 0; k < M; k++)
                    {
                        TrainingElement trainingElement = trainingSet.ElementAt(k);
                        double pki = trainingElement.Input[i];
                        double pkj = trainingElement.Input[j];
                        w = w + pki * pkj;
                    } // k
                    cij.ConnectionWeight.Value = w;
                    cji.ConnectionWeight.Value = w;
                } // j
            } // i

        }

    }
}
