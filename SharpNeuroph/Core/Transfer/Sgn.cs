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
    /// Sgn neuron transfer function.
    /// 
    /// Author: Zoran Sevarac <sevarac@gmail.com>
    /// C# Translation: Miley Thomas (http://www.heatonresearch.com)
    /// </summary>
    [Serializable]
    public class Sgn: TransferFunction
    {

        /// <summary>
        /// y = 1, x > 0  
        ///  y = -1, x <= 0
        /// </summary>
        /// <param name="net"></param>
        /// <returns></returns>
        public override double CalculateOutput(double net)
        {
            if (net > 0d)
                return 1d;
            else
                return -1d;
        }

        /// <summary>
        /// Returns the properties of this function 
        /// </summary>
        /// <returns>properties of this function</returns>
        public Properties GetProperties()
        {
            Properties properties = new Properties();
            properties.SetProperty("transferFunction", TransferFunctionType.SGN.GetTypeCode());
            return properties;
        }

    }
}
