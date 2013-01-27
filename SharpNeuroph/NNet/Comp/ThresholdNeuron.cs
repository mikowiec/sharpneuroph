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
using Neuroph.Util;

namespace Neuroph.NNet.Comp
{
    /// <summary>
    /// Provides behaviour for neurons with threshold.
    /// 
    /// Author: Zoran Sevarac <sevarac@gmail.com>
    /// C# Translation: Miley Thomas (http://www.heatonresearch.com)
    /// </summary>
    [Serializable]
    public class ThresholdNeuron : Neuron
    {
        /// <summary>
        /// Threshold value for this neuron
        /// </summary>
        public double Thresh { get; set; }

        /// <summary>
        /// Creates a neuron with threshold behaviour, and with the specified input
        /// and transfer functions. 
        /// </summary>
        /// <param name="inputFunction">input function for this neuron</param>
        /// <param name="transferFunction">transfer function for this neuron</param>
        public ThresholdNeuron(InputFunction inputFunction, TransferFunction transferFunction)
        {
            this.InputFunction = inputFunction;
            this.TransferFunction = transferFunction;
            this.Thresh = ThreadSafeRandom.NextDouble();
        }

        /// <summary>
        /// Calculates neuron's output
        /// </summary>
        public override void Calculate()
        {
            if (this.HasInputConnections)
            {
                this.netInput = this.InputFunction.CalculateOutput(this.inputConnections);
            }

            this.output = this.TransferFunction.CalculateOutput(this.netInput - this.Thresh);
        }
    }
}
