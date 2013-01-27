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
    /// Provides behaviour specific for neurons which act as input and the output
    /// neurons within the same layer. For example in Hopfield network and BAM.
    /// 
    /// Author: Zoran Sevarac <sevarac@gmail.com>
    /// C# Translation: Miley Thomas (http://www.heatonresearch.com)
    /// </summary>
    [Serializable]
    public class InputOutputNeuron : Neuron
    {
        /// <summary>
        /// Flag which is set true if neuron external input is set
        /// </summary>
        private bool externalInputSet;

        /// <summary>
        /// Bias value for this neuron
        /// </summary>
        public double Bias { get; set; }

        /// <summary>
        /// Creates an instance of neuron for Hopfield network
        /// </summary>
        public InputOutputNeuron()
            : base()
        {
        }

        /// <summary>
        /// Creates an instance of neuron for Hopfield network with specified input
        /// and transfer functions 
        /// </summary>
        /// <param name="inFunc">neuron input function</param>
        /// <param name="transFunc">neuron transfer function</param>
        public InputOutputNeuron(InputFunction inFunc, TransferFunction transFunc)
            : base(inFunc, transFunc)
        {

        }

        /// <summary>
        /// Sets total net input for this cell
        /// </summary>
        public override double NetInput
        {
            set
            {
                this.netInput = value;
                this.externalInputSet = true;
            }
        }        

        /// <summary>
        /// Calculates neuron output
        /// </summary>
        public override void Calculate()
        {

            if (!externalInputSet)
            { // ako ulaz nije setovan spolja
                if (this.HasInputConnections) // bias neuroni ne racunaju ulaz iz mreze jer
                    // nemaju ulaze
                    netInput = this.InputFunction.CalculateOutput(this.inputConnections);
            }

            // calculqate cell output
            this.output = this.TransferFunction.CalculateOutput(this.netInput + Bias); // izracunaj
            // izlaz

            if (externalInputSet)
            { // ulaz setovan 'spolja' vazi samo za jedno izracunavanje
                externalInputSet = false;
                netInput = 0;
            }
        }

    }
}
