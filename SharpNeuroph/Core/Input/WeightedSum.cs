﻿//
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

namespace Neuroph.Core.Input
{
    /// <summary>
    /// Optimized version of weighted input function
    /// 
    /// Author: Zoran Sevarac <sevarac@gmail.com>
    /// C# Translation: Miley Thomas (http://www.heatonresearch.com)
    /// </summary>
    [Serializable]
    public class WeightedSum : InputFunction
    {
        public override double CalculateOutput(IList<Connection> inputConnections)
        {
            double output = 0d;

            foreach (Connection connection in inputConnections)
            {
                output += connection.WeightedInput;
            }

            return output;
        }

        public static double[] CalculateOutput(double[] inputs, double[] weights)
        {
            double[] output = new double[inputs.Length];

            for (int i = 0; i < inputs.Length; i++)
            {
                output[i] += inputs[i] * weights[i];
            }

            return output;
        }
    }
}
