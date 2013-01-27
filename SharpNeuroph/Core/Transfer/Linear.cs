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
    /// Linear neuron transfer function.
    /// 
    /// Author: Zoran Sevarac <sevarac@gmail.com>
    /// C# Translation: Miley Thomas (http://www.heatonresearch.com)
    /// </summary>
    [Serializable]
    public class Linear : TransferFunction
    {
        /// <summary>
        /// The slope parametetar of the linear function
        /// </summary>
        public double Slope { get; set; }

        /// <summary>
        /// Creates an instance of Linear transfer function
        /// </summary>
        public Linear()
        {
            Slope = 1d;
        }

        /// <summary>
        /// Creates an instance of Linear transfer function with specified value
        /// for getSlope parametar.
        /// </summary>
        /// <param name="slope">The slope.</param>
        public Linear(double slope)
        {
            this.Slope = slope;
        }

        /// <summary>
        /// Creates an instance of Linear transfer function with specified properties
        /// </summary>
        /// <param name="properties">The properties</param>
        public Linear(Properties properties)
        {
            this.Slope = (Double)properties.GetProperty("transferFunction.slope");
        }


        public override double CalculateOutput(double net)
        {
            return Slope * net;
        }

        public override double CalculateDerivative(double net)
        {
            return this.Slope;
        }
    }
}
