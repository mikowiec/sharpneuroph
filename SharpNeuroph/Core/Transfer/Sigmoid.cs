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

namespace Neuroph.Core.Transfer
{
    /// <summary>
    /// Sigmoid neuron transfer function.
    /// 
    /// output = 1/(1+ e^(-slope*input))
    /// 
    /// Author: Zoran Sevarac <sevarac@gmail.com>
    /// C# Translation: Miley Thomas (http://www.heatonresearch.com)
    /// </summary>
    [Serializable]
    public class Sigmoid : TransferFunction
    {
        /// <summary>
        /// The slope parametetar of the sigmoid function
        /// </summary>
        public double Slope { get; set; }

        /// <summary>
        /// Creates an instance of Sigmoid neuron transfer function with default
        /// slope=1.
        /// </summary>
        public Sigmoid()
        {
            Slope = 1d;
        }

        /// <summary>
        /// Creates an instance of Sigmoid neuron transfer function with specified
        /// value for slope parametar.
        /// </summary>
        /// <param name="slope">the slope parametar for the sigmoid function</param>
        public Sigmoid(double slope)
        {
            this.Slope = slope;
        }


        /// <summary>
        /// Creates an instance of Sigmoid neuron transfer function with the
        /// specified properties.
        /// </summary>
        /// <param name="properties">properties of the sigmoid function</param>
        public Sigmoid(Properties properties)
        {

            this.Slope = (Double)properties.GetProperty("transferFunction.slope");

        }


        public override double CalculateOutput(double net)
        {
            double den = 1d + Math.Exp(-this.Slope * net);
            return (1d / den);
        }


        public override double CalculateDerivative(double net)
        {
            double o = CalculateOutput(net);
            double derivative = this.Slope * o * (1d - o);
            return derivative;
        }

    }
}
