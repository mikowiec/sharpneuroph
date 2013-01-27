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
    /// Represents single training element for neural network learning. This class
    /// contains only network input and it is used for unsupervised learning
    /// algorithms. It is also the base class for SupervisedTrainingElement.
    /// 
    /// Author: Zoran Sevarac <sevarac@gmail.com>
    /// C# Translation: Miley Thomas (http://www.heatonresearch.com)
    /// </summary>
    [Serializable]
    public class TrainingElement
    {
        /// <summary>
        /// Single training element.
        /// </summary>
        public double[] Input { get; set; }

        /// <summary>
        /// Label for this training element
        /// </summary>
        public String Label { get; set; }

        /// <summary>
        /// Creates new empty training element
        /// </summary>
        public TrainingElement()
        {

        }


        /// <summary>
        /// Creates new training element with specified input vector 
        /// </summary>
        /// <param name="inputVector"></param>
        public TrainingElement(IList<Double> inputVector)
        {
            this.Input = VectorParser.ToDoubleArray(inputVector);
        }

        /// <summary>
        /// Creates new training element with specified input vector
        /// </summary>
        /// <param name="input"></param>
        public TrainingElement(String input)
        {
            this.Input = VectorParser.ParseDoubleArray(input);
        }

        /// <summary>
        /// Creates new training element with input array 
        /// </summary>
        /// <param name="input">input array</param>
        public TrainingElement(params double[] input)
        {
            this.Input = input;
        }


        public virtual bool[] Defined
        {
            get
            {
                return null;
            }
            set
            {
            }
        }

        public virtual double[] IdealArray
        {
            get
            {
                return null;
            }
            set
            {
            }
        }

        public virtual bool IsSupervised
        {
            get
            {
                return false;
            }
        }
    }
}
