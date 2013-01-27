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

namespace Neuroph.Core
{
    /// <summary>
    /// Weighted connection to another neuron.
    /// 
    /// Author: Zoran Sevarac <sevarac@gmail.com>
    /// C# Translation: Miley Thomas (http://www.heatonresearch.com)
    /// </summary>
    [Serializable]
    public class Connection
    {

        /// <summary>
        /// From neuron this connection
        /// </summary>
        protected Neuron fromNeuron;

        /// <summary>
        /// To neuron this connection
        /// </summary>
        protected Neuron toNeuron;

        /// <summary>
        /// Weight for this connection
        /// </summary>
        protected Weight weight;


        /// <summary>
        /// Creates a new connection to specified neuron with specified weight object
        /// </summary>
        /// <param name="connectTo">neuron to connect to</param>
        /// <param name="weight">weight for this connection</param>
        public Connection(Neuron fromNeuron, Neuron toNeuron, Weight weight)
        {
            this.fromNeuron = fromNeuron;
            this.toNeuron = toNeuron;
            this.weight = weight;
        }



        /// <summary>
        /// Creates a new connection between specified neurons with random weight value
        /// </summary>
        /// <param name="fromNeuron">neuron to connect</param>
        /// <param name="toNeuron">neuron to connect to</param>
        public Connection(Neuron fromNeuron, Neuron toNeuron)
        {
            this.fromNeuron = fromNeuron;
            this.toNeuron = toNeuron;
            this.weight = new Weight();
        }

        /// <summary>
        /// Creates a new connection to specified neuron with specified weight object
        /// </summary>
        /// <param name="connectTo">neuron to connect to</param>
        /// <param name="weight">weight for this connection</param>
        public Connection(Neuron fromNeuron, Neuron toNeuron, double weightValue)
        {
            this.fromNeuron = fromNeuron;
            this.toNeuron = toNeuron;
            this.weight = new Weight(weightValue);
        }


        /// <summary>
        /// Returns weight for this connection
        /// </summary>
        public virtual Weight ConnectionWeight
        {
            get
            {
                return this.weight;
            }
            set
            {
                this.weight = value;
            }
        }


        /// <summary>
        /// Returns input received through this connection - the activation that
        /// comes from the output of the cell on the other end of connection
        /// </summary>
        public virtual double Input
        {
            get
            {
                return this.fromNeuron.Output;
            }
        }

        /// <summary>
        /// Returns the weighted input received through this connection
        /// </summary>
        public virtual double WeightedInput
        {
            get
            {
                return Input * weight.Value;
            }
        }

        /// <summary>
        /// Returns from neuron for this connection
        /// </summary>
        public virtual Neuron FromNeuron
        {
            get
            {
                return this.fromNeuron;
            }
            set
            {
                this.fromNeuron = value;
            }
        }

        /// <summary>
        /// Returns from neuron for this connection
        /// </summary>
        public virtual Neuron ToNeuron
        {
            get
            {
                return this.toNeuron;
            }
            set
            {
                this.toNeuron = value;
            }
        }


    }
}
