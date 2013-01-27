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
    /// Gaussian neuron transfer function.
    ///             -(x^2) / (2 * sigma^2)
    ///  f(x) =    e
    ///  
    /// Author: Zoran Sevarac <sevarac@gmail.com>
    /// C# Translation: Miley Thomas (http://www.heatonresearch.com)
    /// </summary>
    [Serializable]
    public class Gaussian : TransferFunction
    {
        /// <summary>
        /// The sigma parametetar of the gaussian function
        /// </summary>
        public double Sigma { get; set; }

        /// <summary>
        /// Creates an instance of Gaussian neuron transfer
        /// </summary>

        public Gaussian()
        {
            Sigma = 0.5d;
        }
        
        /// <summary>
        /// Creates an instance of Gaussian neuron transfer function with the
        /// specified properties. 
        /// </summary>
        /// <param name="properties">properties of the Gaussian function</param>
        public Gaussian(Properties properties)
        {

            this.Sigma = (Double)properties.GetProperty("transferFunction.sigma");
        }

        public override double CalculateOutput(double net)
        {
            return Math.Exp(-0.5d * Math.Pow((net / this.Sigma), 2d));
        }

        public override double CalculateDerivative(double net)
        {
            // TODO: check if this is correct
            double o = CalculateOutput(net);
            double derivative = o * (-net / (Sigma * Sigma));
            return derivative;
        }
    }
}
