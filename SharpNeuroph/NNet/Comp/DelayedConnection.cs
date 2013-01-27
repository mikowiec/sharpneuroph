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

namespace Neuroph.NNet.Comp
{
    /// <summary>
    /// Represents the connection between neurons which can delay signal.
    /// 
    /// Author: Zoran Sevarac <sevarac@gmail.com>
    /// C# Translation: Miley Thomas (http://www.heatonresearch.com)
    /// </summary>
    [Serializable]
    public class DelayedConnection : Connection
    {
        /// <summary>
        /// Delay factor for this conection
        /// </summary>
        private int delay = 0;


        
        /// <summary>
        /// Creates an instance of delayed connection to cpecified neuron and
        /// with specified weight 
        /// </summary>
        /// <param name="connectTo">neuron to connect ti</param>
        /// <param name="weightVal">weight value for the connection</param>
        /// <param name="delay">delay for the connection</param>
        public DelayedConnection(Neuron fromNeuron, Neuron toNeuron, double weightVal, int delay)
            : base(fromNeuron, toNeuron, weightVal)
        {

            this.delay = delay;
        }

        /// <summary>
        /// Gets delayed input through this connection
        /// </summary>
        public override double Input
        {
            get
            {
                if (this.FromNeuron is DelayedNeuron)
                    return ((DelayedNeuron)this.FromNeuron).CalculateOutput(delay);
                else
                    return this.FromNeuron.Output;
            }
        }

    }
}
