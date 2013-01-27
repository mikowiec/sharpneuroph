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
using Neuroph.Core.Transfer;
using Neuroph.Core.Input;

namespace Neuroph.Util
{
    /// <summary>
    /// Various extension methods, used by Neuroph.  Mostly to resolve enum names to types.
    /// 
    /// Author: Zoran Sevarac <sevarac@gmail.com>
    /// C# Translation: Miley Thomas (http://www.heatonresearch.com)
    /// </summary>
    public static class NeurophExtensionMethods
    {
        public static Type getTypeClass(this TransferFunctionType s)
        {
            switch (s)
            {
                case TransferFunctionType.LINEAR:
                    return typeof(Linear);
                case TransferFunctionType.STEP:
                    return typeof(Step);
                case TransferFunctionType.RAMP:
                    return typeof(Ramp);
                case TransferFunctionType.SIGMOID:
                    return typeof(Sigmoid);
                case TransferFunctionType.TANH:
                    return typeof(Tanh);
                case TransferFunctionType.TRAPEZOID:
                    return typeof(Trapezoid);
                case TransferFunctionType.GAUSSIAN:
                    return typeof(Gaussian);
                case TransferFunctionType.SGN:
                    return typeof(Sgn);
            } // switch

            return null;

        }

        public static Type getTypeClass(this WeightsFunctionType s)
        {
            switch (s)
            {
                case WeightsFunctionType.WEIGHTED_INPUT:
                    return typeof(WeightedInput);
                case WeightsFunctionType.DIFERENCE:
                    return typeof(Diference);
            }
            return null;
        }

        public static Type getTypeClass(this SummingFunctionType s)
        {
            switch (s)
            {
                case SummingFunctionType.SUM:
                    return typeof(Sum);
                case SummingFunctionType.INTENSITY:
                    return typeof(Intensity);
                case SummingFunctionType.AND:
                    return typeof(And);
                case SummingFunctionType.OR:
                    return typeof(Or);
                case SummingFunctionType.SUMSQR:
                    return typeof(SumSqr);
                case SummingFunctionType.MIN:
                    return typeof(Min);
                case SummingFunctionType.MAX:
                    return typeof(Max);
                case SummingFunctionType.PRODUCT:
                    return typeof(Product);
            }
            return null;
        }

        public static String ArrayString(this double[] d)
        {
            StringBuilder result = new StringBuilder();
            result.Append("[");
            for (int i = 0; i < d.Length; i++)
            {
                if (i != 0)
                    result.Append(',');
                result.Append(d[i]);
            }
            result.Append("]");
            return result.ToString();
        }

    }
}
