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

namespace Neuroph.Core.Input
{
    /// <summary>
    /// Performs logic AND operation on input vector.
    /// 
    /// Author: Zoran Sevarac <sevarac@gmail.com>
    /// C# Translation: Miley Thomas (http://www.heatonresearch.com)
    /// </summary>
    [Serializable]
    public class And : SummingFunction
    {
        /// <summary>
        /// Calculate the output.
        /// </summary>
        /// <param name="inputVector">Input values >= 0.5d are considered true, otherwise false.</param>
        /// <returns>The output from the calculation.</returns>
        public override double CalculateOutput(double[] inputVector)
        {
            bool res = true;

            foreach (double input in inputVector)
            {
                res = res && (input >= 0.5d);
            }

            return res ? 1d : 0d;
        }
    }
}
