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
    ///  Step neuron transfer function.
    /// y = yHigh, x > 0
    /// y = yLow, x <= 0
    /// 
    /// Author: Zoran Sevarac <sevarac@gmail.com>
    /// C# Translation: Miley Thomas (http://www.heatonresearch.com)
    /// </summary>
    [Serializable]
    public class Step : TransferFunction
    {
        /// <summary>
        /// Output value for high output level
        /// </summary>
        public double YHigh { get; set; }

        /// <summary>
        /// Output value for low output level
        /// </summary>
        public double YLow { get; set; }

        /// <summary>
        /// Creates an instance of Step transfer function
        /// </summary>
        public Step()
        {
            this.YHigh = 1d;
            this.YLow = 0d;
        }

        /// <summary>
        /// Creates an instance of Step transfer function with specified properties
        /// </summary>
        /// <param name="properties"></param>
        public Step(Properties properties)
        {
            this.YHigh = (Double)properties.GetProperty("transferFunction.yHigh");
            this.YLow = (Double)properties.GetProperty("transferFunction.yLow");

        }

        public override double CalculateOutput(double net)
        {
            if (net > 0d)
                return YHigh;
            else
                return YLow;
        }

        /// <summary>
        /// Returns the properties of this function 
        /// </summary>
        /// <returns>properties of this function</returns>
        public Properties GetProperties()
        {
            Properties properties = new Properties();
            properties.SetProperty("transferFunction.yHigh", YHigh);
            properties.SetProperty("transferFunction.yLow", YLow);
            return properties;
        }

    }
}
