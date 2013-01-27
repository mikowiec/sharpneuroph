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
    /// Provides behaviour for neurons with delayed output.
    /// 
    /// Author: Zoran Sevarac <sevarac@gmail.com>
    /// C# Translation: Miley Thomas (http://www.heatonresearch.com)
    /// </summary>
    [Serializable]
    public class DelayedNeuron : Neuron
    {
        /// <summary>
        /// Output history for this neuron
        /// </summary>
        protected IList<Double> outputHistory;


        /// <summary>
        /// Creates an instance of neuron which can delay output 
        /// </summary>
        /// <param name="inputFunction">neuron input function</param>
        /// <param name="transferFunction">neuron transfer function</param>
        public DelayedNeuron(InputFunction inputFunction,
                TransferFunction transferFunction)
            : base(inputFunction, transferFunction)
        {

            outputHistory = new List<Double>(5); // default delay buffer size is 5
            outputHistory.Add(0);
        }

        public override void Calculate()
        {
            base.Calculate();
            outputHistory.Insert(0, this.output);
            if (outputHistory.Count > 5)
                outputHistory.RemoveAt(5);
        }
        
        /// <summary>
        /// Returns neuron output with the specified delay 
        /// </summary>
        /// <param name="delay">output delay</param>
        /// <returns>output with the specified delay </returns>
        public double CalculateOutput(int delay)
        {
            return outputHistory[delay];
        }
    }
}
