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
    /// Backpropagation learning rule with dynamic learning rate and momentum
    /// 
    /// Author: Zoran Sevarac <sevarac@gmail.com>
    /// C# Translation: Miley Thomas (http://www.heatonresearch.com)
    /// </summary>
    [Serializable]
    public class DynamicBackPropagation : MomentumBackpropagation
    {
        public double MaxLearningRate { get; set; }
        public double MinLearningRate { get; set; }
        public double LearningRateChange { get; set; }
        public bool UseDynamicLearningRate { get; set; }

        public double MaxMomentum { get; set; }
        public double MinMomentum { get; set; }
        public double MomentumChange { get; set; }
        public bool UseDynamicMomentum { get; set; }

        // private double previousNetworkError;


        public DynamicBackPropagation()
            : base()
        {
            MaxLearningRate = 0.9d;
            MinLearningRate = 0.1d;
            LearningRateChange = 0.99926d;
            UseDynamicLearningRate = true;

            MaxMomentum = 0.9d;
            MinMomentum = 0.1d;
            MomentumChange = 0.99926d;
            UseDynamicMomentum = true;
        }

        public DynamicBackPropagation(NeuralNetwork neuralNetwork)
            : base(neuralNetwork)
        {

        }

        /// <summary>
        /// Adjusting learning rate dynamically
        /// If network error of current epoch is higher than the network error of the previous
        /// epoch the learning rate is adjusted by minus 1 per cent of current learning rate.
        /// Otherwise the learning rate is adjusted by plus 1 per cent of current learning
        /// rate. So, learning rate increases faster than decreasing does. But if learning rate
        /// reaches 0.9 it switches back to 0.5 to avoid endless training. The lowest learning
        /// rate is 0.5 also to avoid endless training. 
        /// </summary>
        protected void AdjustLearningRate()
        {
            // 1. First approach - probably the best
            // bigger error -> smaller learning rate; minimize the error growth
            // smaller error -> bigger learning rate; converege faster
            // the amount of earning rate change is proportional to error change - by using errorChange

            double errorChange = this.previousEpochError - this.totalNetworkError;
            this.LearningRate = this.LearningRate + (errorChange * LearningRateChange);

            if (this.LearningRate > this.MaxLearningRate)
                this.LearningRate = this.MaxLearningRate;

            if (this.LearningRate < this.MinLearningRate)
                this.LearningRate = this.MinLearningRate;

        }

        protected void AdjustMomentum()
        {
            double errorChange = this.previousEpochError - this.totalNetworkError;
            this.Momentum = this.Momentum + (errorChange * MomentumChange);

            if (this.Momentum > this.MaxMomentum)
                this.Momentum = this.MaxMomentum;

            if (this.Momentum < this.MinMomentum)
                this.Momentum = this.MinMomentum;
        }

        public override void DoLearningEpoch(TrainingSet trainingSet)
        {
            base.DoLearningEpoch(trainingSet);

            if (currentIteration > 0)
            {
                if (UseDynamicLearningRate) AdjustLearningRate();
                if (UseDynamicMomentum) AdjustMomentum();
            }
        }
    }
}
