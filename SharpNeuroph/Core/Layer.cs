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

namespace Neuroph.Core
{
    /// <summary>
    /// Layer of neurons in a neural network. The Layer is basic neuron container (a collection of neurons),
    /// and it provides methods for manipulating neurons (add, remove, get, set, calculate, randomize).
    /// 
    /// Author: Zoran Sevarac <sevarac@gmail.com>
    /// C# Translation: Miley Thomas (http://www.heatonresearch.com)
    /// </summary>
    [Serializable]
    public class Layer
    {
        /// <summary>
        /// Reference to parent neural network
        /// </summary>
        public NeuralNetwork ParentNetwork { get; set; }

        /// <summary>
        /// Neurons collection
        /// </summary>
        protected IList<Neuron> neurons;

        /// <summary>
        /// Creates an instance of empty Layer
        /// </summary>
        public Layer()
        {
            this.neurons = new List<Neuron>();
        }

        /// <summary>
        /// Creates an instance of Layer with the specified number of neurons with
        /// specified neuron properties
        /// </summary>
        /// <param name="neuronsCount">number of neurons in layer</param>
        /// <param name="neuronProperties">properties of neurons in layer</param>
        public Layer(int neuronsCount, NeuronProperties neuronProperties)
            : this()
        {
            for (int i = 0; i < neuronsCount; i++)
            {
                Neuron neuron = NeuronFactory.CreateNeuron(neuronProperties);
                this.AddNeuron(neuron);
            }
        }

        /// <summary>
        /// Returns interface for iterating neurons in this layer 
        /// </summary>
        /// <returns>interface for iterating neurons in this layer</returns>
        public IEnumerator<Neuron> GetNeuronsEnumerator()
        {
            return this.neurons.GetEnumerator();
        }

        /// <summary>
        /// Returns collection of neurons in this layer
        /// </summary>
        public IList<Neuron> Neurons
        {
            get
            {
                return this.neurons;
            }
        }

        /// <summary>
        /// Adds specified neuron to this layer 
        /// </summary>
        /// <param name="neuron">neuron to add</param>
        public void AddNeuron(Neuron neuron)
        {
            neuron.ParentLayer = this;
            this.neurons.Add(neuron);
        }

        /// <summary>
        /// Adds specified neuron to this layer,at specified index position
        /// </summary>
        /// <param name="idx">neuron to add</param>
        /// <param name="neuron">index position at which neuron should be added</param>
        public void AddNeuron(int idx, Neuron neuron)
        {
            neuron.ParentLayer = this;
            this.neurons.Insert(idx, neuron);
        }


        /// <summary>
        /// Sets (replace) the neuron at specified position in layer 
        /// </summary>
        /// <param name="idx">index position to set/replace</param>
        /// <param name="neuron">new Neuron object to set</param>
        public void SetNeuron(int idx, Neuron neuron)
        {
            neuron.ParentLayer = this;
            this.neurons[idx] = neuron;
        }

        /// <summary>
        /// Removes neuron from layer
        /// </summary>
        /// <param name="neuron">neuron to remove</param>
        public void RemoveNeuron(Neuron neuron)
        {
            this.neurons.Remove(neuron);
        }

        /// <summary>
        /// Removes neuron at specified index position in this layer
        /// </summary>
        /// <param name="idx">position of neuron to remove</param>
        public void RemoveNeuronAt(int idx)
        {
            this.neurons.RemoveAt(idx);
        }


        /// <summary>
        /// Returns neuron at specified index position in this layer
        /// </summary>
        /// <param name="idx">index position</param>
        /// <returns>neuron at specified index position</returns>
        public Neuron GetNeuronAt(int idx)
        {
            return this.neurons[idx];
        }

        /// <summary>
        /// Returns the index position in layer for the specified neuron 
        /// </summary>
        /// <param name="neuron">neuron object</param>
        /// <returns>index position of specified neuron</returns>
        public int IndexOf(Neuron neuron)
        {
            return this.neurons.IndexOf(neuron);
        }

        /// <summary>
        /// Returns number of neurons in this layer
        /// </summary>
        public int NeuronsCount
        {
            get
            {
                return this.neurons.Count;
            }
        }

        /// <summary>
        /// Performs calculaton for all neurons in this layer
        /// </summary>
        public virtual void Calculate()
        {
            foreach (Neuron neuron in this.neurons)
            {
                neuron.Calculate();
            }
        }

        /// <summary>
        /// Resets the activation and input levels for all neurons in this layer
        /// </summary>
        public void Reset()
        {
            foreach (Neuron neuron in this.neurons)
            {
                neuron.Reset();
            }
        }

        /// <summary>
        /// Randomize input connection weights for all neurons in this layer
        /// </summary>
        public void RandomizeWeights()
        {
            foreach (Neuron neuron in this.neurons)
            {
                neuron.RandomizeInputWeights();
            }
        }

        /// <summary>
        /// Initialize connection weights for the whole layer to to specified value
        /// </summary>
        /// <param name="value">the weight value</param>
        public void InitializeWeights(double value)
        {
            foreach (Neuron neuron in this.neurons)
            {
                neuron.InitializeWeights(value);
            }
        }


        /// <summary>
        /// Initialize connection weights for the whole layer using a
        /// random number generator
        /// </summary>
        /// <param name="generator">generator the random number generator</param>
        public void InitializeWeights(Random generator)
        {
            foreach (Neuron neuron in this.neurons)
            {
                neuron.InitializeWeights(generator);
            }
        }

        public void InitializeWeights(double min, double max)
        {
            foreach (Neuron neuron in this.neurons)
            {
                neuron.InitializeWeights(min, max);
            }
        }

    }
}
