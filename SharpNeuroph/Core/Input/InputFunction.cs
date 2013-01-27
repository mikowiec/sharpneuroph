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
    /// Neuron's input function. It has two subcomponents:
    /// 
    /// weightsFunction - which performs operation with input and weight vector
    /// summingFunction - which performs operation with the resulting vector from weightsFunction
    /// 
    /// InputFunction implements the following behaviour:
    /// output = summingFunction(weightsFunction(inputs))
    /// 
    /// Different neuron input functions can be created by setting different weights and summing functions.
    /// 
    /// Author: Zoran Sevarac <sevarac@gmail.com>
    /// C# Translation: Miley Thomas (http://www.heatonresearch.com)
    /// </summary>
    [Serializable]
    public class InputFunction
    {
        /// <summary>
        /// Weights function component of the input function. It performs some
        /// operation with weights and input vector, and ouputs vector.
        /// </summary>
        private WeightsFunction weightsFunction;

        /// <summary>
        /// Summing function component of the input function. It performs some 
        /// summing operation on output vector from weightsFunction and outputs scalar.
        /// </summary>
        private SummingFunction summingFunction;

        /// <summary>
        /// Creates an instance of WeightedSum input function by default.
        /// </summary>
        public InputFunction()
        {
            this.weightsFunction = new WeightedInput();
            this.summingFunction = new Sum();
        }

        /// <summary>
        /// Creates an instance of input function with specified weights and summing function 
        /// </summary>
        /// <param name="weightsFunction">function performs some operation on input and weight vector</param>
        /// <param name="summingFunction">function transforms output from VectorFunction to scalar</param>
        public InputFunction(WeightsFunction weightsFunction, SummingFunction summingFunction)
        {
            this.weightsFunction = weightsFunction;
            this.summingFunction = summingFunction;
        }

        /// <summary>
        /// Returns ouput value of this input function for the given neuron inputs 
        /// </summary>
        /// <param name="inputConnections">neuron's input connections</param>
        /// <returns>input total net input</returns>
        public virtual double CalculateOutput(IList<Connection> inputConnections)
        {
            double[] inputVector = this.weightsFunction.CalculateOutput(inputConnections);
            double output = this.summingFunction.CalculateOutput(inputVector);

            return output;
        }

        /// <summary>
        /// Returns summing function component of this InputFunction
        /// </summary>
        public SummingFunction SummingFunction
        {
            get
            {
                return summingFunction;
            }
        }

        /// <summary>
        /// Returns weights functioncomponent of this InputFunction
        /// </summary>
        public WeightsFunction WeightsFunction
        {
            get
            {
                return weightsFunction;
            }
        }

    }
}
