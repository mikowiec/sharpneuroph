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

namespace Neuroph.Core.Learning
{
    /// <summary>
    /// Base class for all unsupervised learning algorithms.
    /// 
    /// Author: Zoran Sevarac <sevarac@gmail.com>
    /// C# Translation: Miley Thomas (http://www.heatonresearch.com)
    /// </summary>
    [Serializable]
    public abstract class UnsupervisedLearning : IterativeLearning
    {
        /// <summary>
        /// Creates new unsupervised learning rule
        /// </summary>
        public UnsupervisedLearning()
            : base()
        {
        }
        
        /// <summary>
        /// Creates new unsupervised learning rule and sets the neural network to train 
        /// </summary>
        /// <param name="neuralNetwork">neural network to train</param>
        public UnsupervisedLearning(NeuralNetwork neuralNetwork)
            : base(neuralNetwork)
        {

        }

        
        /// <summary>
        /// This method does one learning epoch for the unsupervised learning rules.
        /// It iterates through the training set and trains network weights for each
        /// element 
        /// </summary>
        /// <param name="trainingSet">training set for training network</param>
        public override void DoLearningEpoch(TrainingSet trainingSet)
        {
            IEnumerator<TrainingElement> iterator = trainingSet.GetEnumerator();
            while (iterator.MoveNext() && !IsStopped)
            {
                TrainingElement trainingElement = iterator.Current;
                LearnPattern(trainingElement);
            }
        }
        
        /// <summary>
        /// Trains network with the pattern from the specified training element 
        /// </summary>
        /// <param name="trainingElement">unsupervised training element which contains network input</param>
        protected void LearnPattern(TrainingElement trainingElement)
        {
            double[] input = trainingElement.Input;
            this.NeuralNetwork.SetInput(input);
            this.NeuralNetwork.Calculate();
            this.AdjustWeights();
        }



        /// <summary>
        /// This method implements the weight adjustment
        /// </summary>
        abstract protected void AdjustWeights();

    }
}
