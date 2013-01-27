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
    /// Base class for all supervised learning algorithms.
    /// It extends IterativeLearning, and provides general supervised learning principles.
    /// 
    /// Author: Zoran Sevarac <sevarac@gmail.com>
    /// C# Translation: Miley Thomas (http://www.heatonresearch.com)
    /// </summary>
    [Serializable]
    public abstract class SupervisedLearning : IterativeLearning
    {
        /// <summary>
        /// Total network error
        /// </summary>
        [NonSerialized]
        protected double totalNetworkError;

        /// <summary>
        /// Total network error in previous epoch
        /// </summary>
        [NonSerialized]
        protected double previousEpochError;

        /// <summary>
        /// Max allowed network error (condition to stop learning)
        /// </summary>
        protected double maxError = 0.01d;

        /// <summary>
        /// Stopping condition: training stops if total network error change is smaller than minErrorChange
        /// for minErrorChangeIterationsLimit number of iterations
        /// TODO: this field might not be transient in future but that will break backward compatibility
        /// </summary>
        private double minErrorChange = Double.PositiveInfinity;

        /// <summary>
        /// Stopping condition: training stops if total network error change is smaller than minErrorChange
        /// for minErrorChangeStopIterations number of iterations
        /// TODO: this field might not be transient in future but that will break backward compatibility
        /// </summary>
        private int minErrorChangeIterationsLimit = int.MaxValue;

        /// <summary>
        /// Count iterations where error change is smaller then minErrorChange
        /// </summary>
        [NonSerialized]
        private int minErrorChangeIterationsCount;


        /// <summary>
        /// Creates new supervised learning rule
        /// </summary>
        public SupervisedLearning()
            : base()
        {

        }

        /// <summary>
        /// Creates new supervised learning rule and sets the neural network to train 
        /// </summary>
        /// <param name="network">network to train</param>
        public SupervisedLearning(NeuralNetwork network)
            : base(network)
        {

        }

        /// <summary>
        /// Trains network for the specified training set and number of iterations 
        /// </summary>
        /// <param name="trainingSet">training set to learn</param>
        /// <param name="maxError">maximum numberof iterations to learn</param>
        public virtual void Learn(TrainingSet trainingSet, double maxError)
        {
            this.maxError = maxError;
            this.Learn(trainingSet);
        }


        /// <summary>
        /// Trains network for the specified training set and number of iterations 
        /// </summary>
        /// <param name="trainingSet">training set to learn</param>
        /// <param name="maxError"></param>
        /// <param name="maxIterations">maximum numberof iterations to learn</param>
        public virtual void Learn(TrainingSet trainingSet, double maxError, int maxIterations)
        {
            this.maxError = maxError;
            this.MaxIterations = maxIterations;
            this.Learn(trainingSet);
        }

        protected override void Reset()
        {
            base.Reset();
            this.minErrorChangeIterationsCount = 0;
            this.totalNetworkError = 0d;
            this.previousEpochError = 0d;
        }



        /// <summary>
        /// This method implements basic logic for one learning epoch for the
        /// supervised learning algorithms. Epoch is the one pass through the
        /// training set. This method  iterates through the training set
        /// and trains network for each element. It also sets flag if conditions 
        /// to stop learning has been reached: network error below some allowed
        /// value, or maximum iteration count 
        /// </summary>
        /// <param name="trainingSet">training set for training network</param>
        public override void DoLearningEpoch(TrainingSet trainingSet)
        {

            this.previousEpochError = this.totalNetworkError;
            this.totalNetworkError = 0d;

            IEnumerator<TrainingElement> iterator = trainingSet.GetEnumerator();
            while (iterator.MoveNext() && !IsStopped)
            {
                SupervisedTrainingElement supervisedTrainingElement = (SupervisedTrainingElement)iterator.Current;
                this.LearnPattern(supervisedTrainingElement);
            }

            // moved stopping condition to separate method hasReachedStopCondition() so it can be overriden / customized in subclasses
            if (HasReachedStopCondition())
            {
                StopLearning();
            }


        }

        /// <summary>
        /// Returns true if stop condition has been reached, false otherwise.
        /// Override this method in derived classes to implement custom stop criteria. 
        /// </summary>
        /// <returns>true if stop condition is reached, false otherwise</returns>
        protected virtual bool HasReachedStopCondition()
        {
            // da li ovd etreba staviti da proverava i da li se koristi ovaj uslov??? ili staviti da uslov bude automatski samo s ajaako malom vrednoscu za errorChange Doule.minvalue
            return (this.totalNetworkError < this.maxError) || this.ErrorChangeStalled();
        }
        
        /// <summary>
        /// Returns true if absolute error change is sufficently small (<=minErrorChange) for minErrorChangeStopIterations number of iterations 
        /// </summary>
        /// <returns>true if absolute error change is stalled (error is sufficently small for some number of iterations)</returns>
        protected virtual bool ErrorChangeStalled()
        {
            double absErrorChange = Math.Abs(previousEpochError - totalNetworkError);

            if (absErrorChange <= this.minErrorChange)
            {
                this.minErrorChangeIterationsCount++;

                if (this.minErrorChangeIterationsCount >= this.minErrorChangeIterationsLimit)
                {
                    return true;
                }
            }
            else
            {
                this.minErrorChangeIterationsCount = 0;
            }

            return false;
        }
        /**
         * 
         * 
         * @param trainingElement
         *            
         */


        /// <summary>
        /// Trains network with the pattern from the specified training element 
        /// </summary>
        /// <param name="trainingElement">supervised training element which contains input and desired
        /// output</param>
        protected virtual void LearnPattern(SupervisedTrainingElement trainingElement)
        {
            double[] input = trainingElement.Input;
            this.NeuralNetwork.SetInput(input);
            this.NeuralNetwork.Calculate();
            double[] output = this.NeuralNetwork.Output;
            double[] desiredOutput = trainingElement.DesiredOutput;
            double[] patternError = this.GetPatternError(output, desiredOutput);
            this.UpdateTotalNetworkError(patternError);
            this.UpdateNetworkWeights(patternError);
        }

        /// <summary>
        /// Calculates the network error for the current pattern - diference between
        /// desired and actual output 
        /// </summary>
        /// <param name="output">actual network output</param>
        /// <param name="desiredOutput">desired network output</param>
        /// <returns>pattern error</returns>
        protected virtual double[] GetPatternError(double[] output, double[] desiredOutput)
        {
            double[] patternError = new double[output.Length];

            for (int i = 0; i < output.Length; i++)
            {
                patternError[i] = desiredOutput[i] - output[i];
            }

            return patternError;
        }


        /// <summary>
        /// Returns learning error tolerance - the value of total network error to stop learning.
        /// </summary>
        public virtual double MaxError
        {
            get
            {
                return maxError;
            }
            set
            {
                this.maxError = value;
            }
        }

        /// <summary>
        /// Returns total network error in current learning epoch
        /// </summary>
        public virtual double TotalNetworkError
        {
            get
            {
                lock (this)
                {
                    return this.totalNetworkError;
                }
            }
        }

        /// <summary>
        /// Returns total network error in previous learning epoch
        /// </summary>
        public double PreviousEpochError
        {
            get
            {
                return previousEpochError;
            }
        }

        /// <summary>
        /// Returns min error change stopping criteria
        /// </summary>
        public virtual double MinErrorChange
        {
            get
            {
                return minErrorChange;
            }
            set
            {
                this.minErrorChange = value;
            }
        }

        /// <summary>
        /// Returns number of iterations for min error change stopping criteria
        /// </summary>
        public virtual int MinErrorChangeIterationsLimit
        {
            get
            {
                return minErrorChangeIterationsLimit;
            }
            set
            {
                this.minErrorChangeIterationsLimit = value;
            }
        }

        /// <summary>
        /// Returns number of iterations count for for min error change stopping criteria
        /// </summary>
        public virtual int MinErrorChangeIterationsCount
        {
            get
            {
                return minErrorChangeIterationsCount;
            }
        }


        /// <summary>
        /// Subclasses update total network error for each training pattern with this
        /// method. Error update formula is learning rule specific.
        /// </summary>
        /// <param name="patternError">pattern error vector</param>
        abstract protected void UpdateTotalNetworkError(double[] patternError);

        /// <summary>
        /// This method should implement the weights update procedure 
        /// </summary>
        /// <param name="patternError">pattern error vector</param>
        abstract protected void UpdateNetworkWeights(double[] patternError);

    }
}
