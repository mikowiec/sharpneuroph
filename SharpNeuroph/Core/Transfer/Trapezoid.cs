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
    /// Fuzzy trapezoid neuron tranfer function.
    /// 
    /// Author: Zoran Sevarac <sevarac@gmail.com>
    /// C# Translation: Miley Thomas (http://www.heatonresearch.com)
    /// </summary>
    [Serializable]
    public class Trapezoid : TransferFunction
    {
        /// <summary>
        /// The left-low point of trapezoid function.
        /// </summary>
        double LeftLow { get; set; }

        /// <summary>
        /// The left-high point of trapezoid function.
        /// </summary>
        double LeftHigh { get; set; }

        /// <summary>
        /// The right-low point of trapezoid function.
        /// </summary>
        double RightLow { get; set; }

        /// <summary>
        /// The right-high point of trapezoid function.
        /// </summary>
        double RightHigh { get; set; }


        /// <summary>
        /// Creates an instance of Trapezoid transfer function
        /// </summary>
        public Trapezoid()
        {
            this.LeftLow = 0d;
            this.LeftHigh = 1d;
            this.RightLow = 3d;
            this.RightHigh = 2d;
        }

        /// <summary>
        /// Creates an instance of Trapezoid transfer function with the specified
        /// setting.
        /// </summary>
        /// <param name="leftLow"></param>
        /// <param name="leftHigh"></param>
        /// <param name="rightLow"></param>
        /// <param name="rightHigh"></param>
        public Trapezoid(double leftLow, double leftHigh, double rightLow, double rightHigh)
        {
            this.LeftLow = leftLow;
            this.LeftHigh = leftHigh;
            this.RightLow = rightLow;
            this.RightHigh = rightHigh;
        }

        /// <summary>
        /// Creates an instance of Trapezoid transfer function with the specified
        /// properties.
        /// </summary>
        /// <param name="properties"></param>
        public Trapezoid(Properties properties)
        {

            this.LeftLow = (Double)properties.GetProperty("transferFunction.leftLow");
            this.LeftHigh = (Double)properties.GetProperty("transferFunction.leftHigh");
            this.RightLow = (Double)properties.GetProperty("transferFunction.rightLow");
            this.RightHigh = (Double)properties.GetProperty("transferFunction.rightHigh");
        }


        public override double CalculateOutput(double net)
        {
            if ((net >= LeftHigh) && (net <= RightHigh))
            {
                return 1d;
            }
            else if ((net > LeftLow) && (net < LeftHigh))
            {
                return (net - LeftLow) / (LeftHigh - LeftLow);
            }
            else if ((net > RightHigh) && (net < RightLow))
            {
                return (RightLow - net) / (RightLow - RightHigh);
            }

            return 0d;
        }


    }
}
