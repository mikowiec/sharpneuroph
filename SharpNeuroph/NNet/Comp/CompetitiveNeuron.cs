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
using Neuroph.Core.Input;
using Neuroph.Core.Transfer;

namespace Neuroph.NNet.Comp
{
    /// <summary>
    /// Provides neuron behaviour specific for competitive neurons which are used in
    /// competitive layers, and networks with competitive learning.
    /// 
    /// Author: Zoran Sevarac <sevarac@gmail.com>
    /// C# Translation: Miley Thomas (http://www.heatonresearch.com)
    /// </summary>
    [Serializable]
    public class CompetitiveNeuron : DelayedNeuron
    {
        /// <summary>
        /// Flag indicates if this neuron is in competing mode
        /// </summary>
        public bool IsCompeting { get; set; }

        /// <summary>
        /// Collection of conections from neurons in other layers
        /// </summary>
        private IList<Connection> connectionsFromOtherLayers;

        /// <summary>
        /// Collection of connections from neurons in the same layer as this neuron
        /// (lateral connections used for competition)
        /// </summary>
        private IList<Connection> connectionsFromThisLayer;

        /// <summary>
        /// Creates an instance of CompetitiveNeuron with specified input and transfer functions 
        /// </summary>
        /// <param name="inputFunction">neuron input function</param>
        /// <param name="transferFunction">neuron ransfer function</param>
        public CompetitiveNeuron(InputFunction inputFunction, TransferFunction transferFunction)
            : base(inputFunction, transferFunction)
        {

            connectionsFromOtherLayers = new List<Connection>();
            connectionsFromThisLayer = new List<Connection>();
            this.AddInputConnection(this, 1);
        }

        public override void Calculate()
        {
            if (this.IsCompeting)
            {
                // get input only from neurons in this layer
                this.netInput = this.InputFunction
                        .CalculateOutput(this.connectionsFromThisLayer);
            }
            else
            {
                // get input from other layers
                this.netInput = this.InputFunction
                        .CalculateOutput(this.connectionsFromOtherLayers);
                this.IsCompeting = true;
            }

            this.output = this.TransferFunction.CalculateOutput(this.netInput);
            outputHistory.Insert(0, this.output);
        }

        /// <summary>
        /// Adds input connection for this competitive neuron 
        /// </summary>
        /// <param name="connection">input connection</param>
        public override void AddInputConnection(Connection connection)
        {
            base.AddInputConnection(connection);
            if (connection.FromNeuron.ParentLayer == this
                    .ParentLayer)
            {
                connectionsFromThisLayer.Add(connection);
            }
            else
            {
                connectionsFromOtherLayers.Add(connection);
            }
        }

        /// <summary>
        /// Returns collection of connections from other layers
        /// </summary>
        public IList<Connection> ConnectionsFromOtherLayers
        {
            get
            {
                return connectionsFromOtherLayers;
            }
        }

        /// <summary>
        /// Resets the input, output and mode for this neuron
        /// </summary>
        public override void Reset()
        {
            base.Reset();
            this.IsCompeting = false;
        }

    }
}
