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
    /// Ramp neuron transfer function.
    /// 
    /// Author: Zoran Sevarac <sevarac@gmail.com>
    /// C# Translation: Miley Thomas (http://www.heatonresearch.com)
    /// </summary>
    [Serializable]
    public class Ramp : TransferFunction
    {
        /// <summary>
        /// The slope parametetar of the ramp function
        /// </summary>
        public double Slope { get; set; }

        /// <summary>
        /// Threshold for the high output level
        /// </summary>
        public double XHigh { get; set; }

        /// <summary>
        /// Threshold for the low output level
        /// </summary>
        public double XLow { get; set; }

        /// <summary>
        /// Output value for the high output level
        /// </summary>
        public double YHigh { get; set; }

        /// <summary>
        /// Output value for the low output level
        /// </summary>
        public double YLow { get; set; }

        /// <summary>
        /// Creates an instance of Ramp transfer function with default settings
        /// </summary>
        public Ramp()
        {
            Slope = 1d;
            XHigh = 1d;
            XLow = 0d;
            YHigh = 1d;
            YLow = 0d;
        }

        /// <summary>
        /// Creates an instance of Ramp transfer function with specified settings
        /// </summary>
        /// <param name="slope"></param>
        /// <param name="xLow"></param>
        /// <param name="xHigh"></param>
        /// <param name="yLow"></param>
        /// <param name="yHigh"></param>
        public Ramp(double slope, double xLow, double xHigh, double yLow,
                double yHigh)
        {
            this.Slope = slope;
            this.XLow = xLow;
            this.XHigh = xHigh;
            this.YLow = yLow;
            this.YHigh = yHigh;
        }

        /// <summary>
        /// Creates an instance of Ramp transfer function with specified properties.
        /// </summary>
        /// <param name="properties"></param>
        public Ramp(Properties properties)
        {

            this.Slope = (Double)properties.GetProperty("transferFunction.slope");
            this.YHigh = (Double)properties.GetProperty("transferFunction.yHigh");
            this.YLow = (Double)properties.GetProperty("transferFunction.yLow");
            this.XHigh = (Double)properties.GetProperty("transferFunction.xHigh");
            this.XLow = (Double)properties.GetProperty("transferFunction.xLow");
        }

        public override double CalculateOutput(double net)
        {
            if (net < this.XLow)
                return this.YLow;
            else if (net > this.XHigh)
                return this.YHigh;
            else
                return (double)(Slope * net);
        }

    }
}
