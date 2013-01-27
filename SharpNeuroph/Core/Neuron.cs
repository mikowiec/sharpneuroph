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
using Neuroph.Core.Input;
using Neuroph.Core.Transfer;

namespace Neuroph.Core
{
    /// <summary>
    /// Basic general neuron model according to McCulloch-Pitts neuron model.
    /// Different neuron models can be created by using different input and transfer functions for instances of this class,
    /// or by deriving from this class. The neuron is basic processing element of neural network.
    /// This class implements the following behaviour:
    /// 
    /// output = transferFunction( inputFunction(inputConnections) )
    /// 
    /// Author: Zoran Sevarac <sevarac@gmail.com>
    /// C# Translation: Miley Thomas (http://www.heatonresearch.com)
    /// </summary>
    [Serializable]
    public class Neuron
    {
        /// <summary>
        /// Parent layer for this neuron
        /// </summary>
        public virtual Layer ParentLayer { get; set; }

        /// <summary>
        /// Collection of neuron's input connections (connections to this neuron)
        /// </summary>
        protected IList<Connection> inputConnections = new List<Connection>();


        /// <summary>
        /// Collection of neuron's output connections (connections from this to other
        /// neurons)
        /// </summary>
        protected IList<Connection> outConnections = new List<Connection>();

        /// <summary>
        /// Total net input for this neuron. Represents total input for this neuron
        /// received from input function.
        /// </summary>
        protected double netInput;

        /// <summary>
        /// Neuron output
        /// </summary>
        protected double output;

        /// <summary>
        /// Local error for this neuron
        /// </summary>
        protected double error;

        /// <summary>
        /// Input function for this neuron
        /// </summary>
        public virtual InputFunction InputFunction { get; set; }

        /// <summary>
        /// Transfer function for this neuron
        /// </summary>
        public virtual TransferFunction TransferFunction { get; set; }

        /// <summary>
        /// Creates an instance of Neuron with the weighted sum, input function 
        /// and Step transfer function. This is the original McCulloch-Pitts 
        /// neuron model.
        /// </summary>
        public Neuron()
        {
            this.InputFunction = new InputFunction();
            this.TransferFunction = new Step();
        }

        /// <summary>
        /// Creates an instance of Neuron with the specified input and transfer functions. 
        /// </summary>
        /// <param name="inputFunction">input function for this neuron</param>
        /// <param name="transferFunction">transfer function for this neuron</param>
        public Neuron(InputFunction inputFunction, TransferFunction transferFunction)
        {
            this.InputFunction = inputFunction;
            this.TransferFunction = transferFunction;
        }

        /// <summary>
        /// Calculates neuron's output
        /// </summary>
        public virtual void Calculate()
        {
            //why fo we need this? for input neurons!!!
            if (this.HasInputConnections)
            {
                this.NetInput = this.InputFunction.CalculateOutput(this.inputConnections);
            }

            this.Output = this.TransferFunction.CalculateOutput(this.NetInput);
        }

        /// <summary>
        /// Sets input and output activation levels to zero
        /// </summary>
        public virtual void Reset()
        {
            this.NetInput = 0d;
            this.Output = 0d;
        }

        /// <summary>
        /// Returns true if there are input connections for this neuron, false
        /// otherwise
        /// </summary>
        public virtual bool HasInputConnections
        {
            get
            {
                return (this.inputConnections.Count > 0);
            }
        }

        /// <summary>
        /// Returns Iterator interface for accessing input connections 
        /// </summary>
        /// <returns>interface for accessing input connections</returns>
        public virtual IEnumerator<Connection> GetInputsEnumerator()
        {
            return this.inputConnections.GetEnumerator();
        }

        /// <summary>
        /// Adds the specified input connection 
        /// </summary>
        /// <param name="connection">input connection to add</param>
        public virtual void AddInputConnection(Connection connection)
        {
            this.inputConnections.Add(connection);
            Neuron fromNeuron = connection.FromNeuron;
            fromNeuron.AddOutputConnection(connection);
        }

        /// <summary>
        /// Adds input connection from specified neuron 
        /// </summary>
        /// <param name="fromNeuron">neuron to connect from</param>
        public virtual void AddInputConnection(Neuron fromNeuron)
        {
            Connection connection = new Connection(fromNeuron, this);
            this.AddInputConnection(connection);
        }

        /// <summary>
        /// Adds input connection with the given weight, from given neuron 
        /// </summary>
        /// <param name="fromNeuron">neuron to connect from</param>
        /// <param name="weightVal">connection weight value</param>
        public virtual void AddInputConnection(Neuron fromNeuron, double weightVal)
        {
            Connection connection = new Connection(fromNeuron, this, weightVal);
            this.AddInputConnection(connection);
        }

        /// <summary>
        /// Adds the specified output connection 
        /// </summary>
        /// <param name="connection">connection output connection to add</param>
        protected virtual void AddOutputConnection(Connection connection)
        {
            this.outConnections.Add(connection);
        }

        /// <summary>
        /// Returns input connections for this neuron as Vector collection
        /// </summary>
        /// <returns>input connections of this neuron </returns>
        public virtual IList<Connection> InputConnections
        {
            get
            {
                return inputConnections;
            }
        }

        /**
         * Returns output connections from this neuron
         * 
         * @return output connections from this neuron
         */
        public virtual IList<Connection> OutConnections
        {
            get
            {
                return outConnections;
            }
        }

        /// <summary>
        /// Removes input connection which is connected to specified neuron 
        /// </summary>
        /// <param name="fromNeuron">neuron which is connected as input</param>
        public virtual void RemoveInputConnectionFrom(Neuron fromNeuron)
        {
            foreach (Connection connection in this.inputConnections)
            {
                if (connection.FromNeuron == fromNeuron)
                {
                    this.inputConnections.Remove(connection);
                    return;
                }
            }
        }

        /// <summary>
        /// Gets input connection from the specified neuron * @param fromNeuron
        /// neuron connected to this neuron as input 
        /// </summary>
        /// <param name="fromNeuron"></param>
        /// <returns></returns>
        public virtual Connection GetConnectionFrom(Neuron fromNeuron)
        {
            foreach (Connection connection in this.inputConnections)
            {
                if (connection.FromNeuron == fromNeuron)
                    return connection;
            }
            return null;
        }


        /// <summary>
        /// Returns weights vector of input connections 
        /// </summary>
        public virtual IList<Weight> WeightsVector
        {
            get
            {
                IList<Weight> weights = new List<Weight>();
                foreach (Connection connection in this.inputConnections)
                {
                    Weight weight = connection.ConnectionWeight;
                    weights.Add(weight);
                }
                return weights;
            }
        }

        /// <summary>
        /// Randomize all input weights
        /// </summary>
        public virtual void RandomizeInputWeights()
        {
            foreach (Connection connection in this.inputConnections)
            {
                connection.ConnectionWeight.Randomize();
            }
        }

        /// <summary>
        /// Initialize weights for all input connections to specified value 
        /// </summary>
        /// <param name="value">the weight value</param>
        public virtual void InitializeWeights(double value)
        {
            foreach (Connection connection in this.inputConnections)
            {
                connection.ConnectionWeight.Value = value;
            }
        }

        /// <summary>
        /// Initialize weights for all input connections to using random number generator 
        /// </summary>
        /// <param name="generator">the random number generator</param>
        public virtual void InitializeWeights(Random generator)
        {
            foreach (Connection connection in this.inputConnections)
            {
                connection.ConnectionWeight.Value = generator.NextDouble();
            }
        }

        /// <summary>
        /// Initialize weights for all input connections with random value within specified interval
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        public virtual void InitializeWeights(double min, double max)
        {
            foreach (Connection connection in this.inputConnections)
            {
                connection.ConnectionWeight.Randomize(min, max);
            }
        }

        /// <summary>
        /// Total net input for this neuron. Represents total input for this neuron
        /// received from input function.
        /// </summary>
        public virtual double NetInput
        {
            get
            {
                return this.netInput;
            }
            set
            {
                this.netInput = value;
            }
        }

        /// <summary>
        /// Neuron output
        /// </summary>
        public virtual double Output
        {
            get
            {
                return this.output;
            }
            set
            {
                this.output = value;
            }
        }

        /// <summary>
        /// Local error for this neuron
        /// </summary>
        public virtual double Error
        {
            get
            {
                return this.error;
            }
            set
            {
                this.error = value;
            }
        }

    }
}
