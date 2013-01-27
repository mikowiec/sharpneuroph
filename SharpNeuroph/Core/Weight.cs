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

namespace Neuroph.Core
{
    /// <summary>
    /// Neuron connection weight.
    /// 
    /// Author: Zoran Sevarac <sevarac@gmail.com>
    /// C# Translation: Miley Thomas (http://www.heatonresearch.com)
    /// </summary>
    [Serializable]
    public class Weight
    {
        /// <summary>
        /// Weight value
        /// </summary>
        private double value;

        /// <summary>
        /// Previous weight value (used by some learning rules like momentum for backpropagation)
        /// </summary>
        [NonSerialized]
        private double previousValue;

        /// <summary>
        /// Creates an instance of connection weight with random weight value in range [0..1]
        /// </summary>
        public Weight()
        {
            this.value = ThreadSafeRandom.NextDouble() - 0.5d;
            this.previousValue = this.value;
        }

        /// <summary>
        /// Creates an instance of connection weight with the specified weight value
        /// </summary>
        /// <param name="value">weight value</param>
        public Weight(double value)
        {
            this.value = value;
            this.previousValue = this.value;
        }
        
        /// <summary>
        /// Increases the weight for the specified amount 
        /// </summary>
        /// <param name="amount">amount to add to current weight value</param>
        public void Inc(double amount)
        {
            this.value += amount;
        }
        
        /// <summary>
        /// Decreases the weight for specified amount 
        /// </summary>
        /// <param name="amount">amount to subtract from the current weight value</param>
        public void Dec(double amount)
        {
            this.value -= amount;
        }

        /// <summary>
        /// Returns weight value
        /// </summary>
        public double Value
        {
            get
            {
                return this.value;
            }
            set
            {
                this.value = value;
            }
        }

        /// <summary>
        /// Returns previous weight value
        /// </summary>
        public double PreviousValue
        {
            get
            {
                return this.previousValue;
            }
            set
            {
                this.previousValue = value;
            }
        }



        /// <summary>
        /// Returns weight value as String
        /// </summary>
        /// <returns></returns>
        public override String ToString()
        {
            return "" + value;
        }

        /// <summary>
        /// Sets random weight value
        /// </summary>
        public void Randomize()
        {
            this.value = ThreadSafeRandom.NextDouble() - 0.5d;
            this.previousValue = this.value;
        }

        /// <summary>
        /// Sets random weight value within specified interval
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        public void Randomize(double min, double max)
        {
            this.value = min + ThreadSafeRandom.NextDouble() * (max - min);
            this.previousValue = this.value;
        }

    }
}
