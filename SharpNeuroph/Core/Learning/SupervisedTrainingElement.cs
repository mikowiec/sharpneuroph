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

namespace Neuroph.Core.Learning
{
    /// <summary>
    /// Represents training element for supervised learning algorithms. Each
    /// supervised training element contains network input and desired network
    /// output.
    /// 
    /// Author: Zoran Sevarac <sevarac@gmail.com>
    /// C# Translation: Miley Thomas (http://www.heatonresearch.com)
    /// </summary>
    [Serializable]
    public class SupervisedTrainingElement : TrainingElement
    {
        /// <summary>
        /// Desired output for this training element
        /// </summary>
        public double[] DesiredOutput { get; set; }


        /// <summary>
        /// Creates new training element with specified input and desired output
        /// vectors 
        /// </summary>
        /// <param name="input">input vector</param>
        /// <param name="desiredOutput">desired output vector</param>
        public SupervisedTrainingElement(List<Double> input,
                List<Double> desiredOutput)
            : base(input)
        {
            this.DesiredOutput = VectorParser.ToDoubleArray(desiredOutput);
        }

        /// <summary>
        /// Creates new training element with specified input and desired output
        /// vectors specifed as strings 
        /// </summary>
        /// <param name="input">input vector as space separated string</param>
        /// <param name="desiredOutput">desired output vector as space separated string</param>
        public SupervisedTrainingElement(String input, String desiredOutput)
            : base(input)
        {
            this.DesiredOutput = VectorParser.ParseDoubleArray(desiredOutput);
        }


        /// <summary>
        /// Creates new training element with specified input and desired output
        /// vectors 
        /// </summary>
        /// <param name="input">input array</param>
        /// <param name="desiredOutput">desired output array</param>
        public SupervisedTrainingElement(double[] input, double[] desiredOutput)
            : base(input)
        {
            this.DesiredOutput = desiredOutput;
        }
       
        public override double[] IdealArray
        {
            get
            {
                return DesiredOutput;
            }
            set
            {
                this.DesiredOutput = value;
            }
        }

        public override bool IsSupervised
        {
            get
            {
                return true;
            }
        }


    }
}
