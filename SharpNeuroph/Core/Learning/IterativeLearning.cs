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
    /// Base class for all iterative learning algorithms.
    /// It provides the iterative learning procedure for all of its subclasses.
    /// 
    /// Author: Zoran Sevarac <sevarac@gmail.com>
    /// C# Translation: Miley Thomas (http://www.heatonresearch.com)
    /// </summary>
    [Serializable]
    public abstract class IterativeLearning : LearningRule
    {
        /// <summary>
        /// Learning rate parametar
        /// </summary>
        public double LearningRate { get; set; }

        /// <summary>
        /// Current iteration counter
        /// </summary>
        protected int currentIteration = 0;

        /// <summary>
        /// Max training iterations (when to stopLearning training)
        /// </summary>
        private int maxIterations = int.MaxValue;

        /// <summary>
        /// Flag for indicating if the training iteration number is limited
        /// </summary>
        protected bool iterationsLimited = false;

        /// <summary>
        /// Flag for indicating if learning thread is paused
        /// </summary>
        [NonSerialized]
        private bool pausedLearning = false;

        /// <summary>
        /// Creates new instannce of IterativeLearning learning algorithm
        /// </summary>
        public IterativeLearning()
            : base()
        {
            this.LearningRate = 0.1d;
        }

        /// <summary>
        /// Creates new instannce of IterativeLearning learning algorithm for the
        /// specified neural network.
        /// </summary>
        /// <param name="network">neural network to train</param>
        public IterativeLearning(NeuralNetwork network)
            : base(network)
        {

        }


        /// <summary>
        /// Sets iteration limit for this learning algorithm
        /// </summary>
        public int MaxIterations
        {
            set
            {
                this.maxIterations = value;
                this.iterationsLimited = true;
            }
        }

        /// <summary>
        /// Returns current iteration of this learning algorithm
        /// </summary>
        public int CurrentIteration
        {
            get
            {
                return this.currentIteration;
            }
        }

        /// <summary>
        /// Returns true if learning thread is paused, false otherwise
        /// </summary>
        public bool IsPausedLearning
        {
            get
            {
                return pausedLearning;
            }
        }

        /// <summary>
        /// Pause the learning
        /// </summary>
        public void Pause()
        {
            this.pausedLearning = true;
        }

        /// <summary>
        /// Resumes the paused learning
        /// </summary>
        public void Resume()
        {
            //this.pausedLearning = false;
            //lock(this) {
            //     this.notify();
            //}
        }

        /// <summary>
        /// Reset the iteration counter
        /// </summary>
        protected virtual void Reset()
        {
            this.currentIteration = 0;
        }

        public override void Learn(TrainingSet trainingSet)
        {
            this.Reset();

            while (!IsStopped)
            {
                DoLearningEpoch(trainingSet);
                this.currentIteration++;
                if (iterationsLimited && (currentIteration == maxIterations))
                {
                    StopLearning();
                }
                else if (!iterationsLimited && (currentIteration == int.MaxValue))
                {
                    // restart iteration counter since it has reached max value and iteration numer is not limited
                    this.currentIteration = 1;
                }

                this.NotifyChange(); // notify observers

                // Thread safe pause
                if (this.pausedLearning)
                    lock (this)
                    {
                        while (this.pausedLearning)
                        {
                            try
                            {
                                //this.wait();
                            }
                            catch (Exception) { }
                        }
                    }

            }
        }

        /// <summary>
        /// Trains network for the specified training set and number of iterations 
        /// </summary>
        /// <param name="trainingSet">training set to learn</param>
        /// <param name="maxIterations">maximum numberof iterations to learn</param>
        public void Learn(TrainingSet trainingSet, int maxIterations)
        {
            this.MaxIterations = maxIterations;
            this.Learn(trainingSet);
        }

        /// <summary>
        /// Runs one learning iteration for the specified training set and notfies observers.
        /// This method does the the doLearningEpoch() and in addtion notifes observrs when iteration is done. 
        /// </summary>
        /// <param name="trainingSet">training set to learn</param>
        public void DoOneLearningIteration(TrainingSet trainingSet)
        {
            this.DoLearningEpoch(trainingSet);
            this.NotifyChange(); // notify observers
        }

        /// <summary>
        /// Override this method to implement specific learning epoch - one learning iteration, one pass through whole training set 
        /// </summary>
        /// <param name="trainingSet">training set</param>
        abstract public void DoLearningEpoch(TrainingSet trainingSet);

    }
}
