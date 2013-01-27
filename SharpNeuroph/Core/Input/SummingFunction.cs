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
    /// Abstract base class for all summing functions, which perform some summing
    /// operation on weighted input vector and return scalar.
    /// SummingFunctions is subcomponents of InputFunction.
    /// @see org.neuroph.core.input.InputFunction
    /// 
    /// Author: Zoran Sevarac <sevarac@gmail.com>
    /// C# Translation: Miley Thomas (http://www.heatonresearch.com)
    /// </summary>
    [Serializable]
    public abstract class SummingFunction
    {
        /// <summary>
        /// Returns summing function output
        /// </summary>
        /// <param name="inputVector">input vector for summing function.</param>
        /// <returns>Summing function output.</returns>
        abstract public double CalculateOutput(double[] inputVector);

        /// <summary>
        /// Get the object as a string.
        /// </summary>
        /// <returns>The object as a string.</returns>
        public override String ToString()
        {
            return this.GetType().Name;
        }

    }
}
