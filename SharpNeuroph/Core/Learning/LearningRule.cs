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

namespace Neuroph.Core.Learning
{
    /// <summary>
    /// Base class for all neural network learning algorithms.
    /// It provides the general principles for training neural network.
    /// 
    /// Author: Zoran Sevarac <sevarac@gmail.com>
    /// C# Translation: Miley Thomas (http://www.heatonresearch.com)
    /// </summary>
    [Serializable]
    public abstract class LearningRule: IObservable
    {
        /// <summary>
        /// Neural network to train
        /// </summary>
        public NeuralNetwork NeuralNetwork { get; set; }

        /// <summary>
        /// Collection of training elements
        /// </summary>
        [NonSerialized]
        private TrainingSet trainingSet;

        /// <summary>
        /// Flag to stop learning
        /// </summary>
        [NonSerialized]
        private bool shouldStopLearning = false;


        [NonSerialized]
        private IList<IObserver> observers = new List<IObserver>();

        /// <summary>
        /// Creates new instance of learning rule
        /// </summary>
        public LearningRule() { }

        
        /// <summary>
        /// Sets neural network for this learning rule 
        /// </summary>
        /// <param name="network">neural network to train</param>
        public LearningRule(NeuralNetwork network)
        {
            this.NeuralNetwork = network;
        }

        /// <summary>
        /// Gets training set
        /// </summary>
        public TrainingSet TrainingSet
        {
            get
            {
                return this.trainingSet;
            }
            set
            {
                this.trainingSet = value;
            }
        }

        /// <summary>
        /// Method from Runnable interface for running learning procedure in separate
        /// thread.
        /// </summary>
        public void Run()
        {
            this.Learn(this.trainingSet);
        }

        /// <summary>
        /// Prepares the learning rule to run by setting stop flag to false
        /// </summary>
        public void SetStarted()
        {
            lock (this)
            {
                // note: as long as all this method does is assign stopLearning, it doesn't need to be synchronized if stopLearning is a VOLATILE field. - Jon Tait 6-19-2010
                this.shouldStopLearning = false;
            }
        }

        /// <summary>
        /// Stops learning
        /// </summary>
        public virtual void StopLearning()
        {
            lock (this)
            {
                // note: as long as all this method does is assign stopLearning, it doesn't need to be synchronized if stopLearning is a VOLATILE field. - Jon Tait 6-19-2010
                this.shouldStopLearning = true;
            }
        }

        /// <summary>
        /// Returns true if learning has stopped, false otherwise 
        /// </summary>
        public virtual bool IsStopped
        {
            get
            {
                lock (this)
                {
                    // note: as long as all this method does is return stopLearning, it doesn't need to be synchronized if stopLearning is a VOLATILE field. - Jon Tait 6-19-2010
                    return this.shouldStopLearning;
                }
            }
        }

        /// <summary>
        /// Notify observers about change
        /// </summary>
        protected void NotifyChange()
        {
            foreach (IObserver obs in this.observers)
            {
                obs.Update(this, null);
            }
        }

        /// <summary>
        /// Add an observer.
        /// </summary>
        /// <param name="obs">The observer.</param>
        public void AddObserver(IObserver obs)
        {
            this.observers.Add(obs);
        }
        
        /// <summary>
        /// Override this method to implement specific learning procedures 
        /// </summary>
        /// <param name="trainingSet">training set</param>
        abstract public void Learn(TrainingSet trainingSet);

    }
}
