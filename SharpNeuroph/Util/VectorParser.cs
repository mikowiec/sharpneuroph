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

namespace Neuroph.Util
{
    /// <summary>
    /// Provides methods to parse strings as Integer or Double vectors.
    /// 
    /// Author: Zoran Sevarac <sevarac@gmail.com>
    /// C# Translation: Miley Thomas (http://www.heatonresearch.com)
    /// </summary>
    public class VectorParser
    {
        /**
 * This method parses input String and returns Integer vector
 * 
 * @param str
 *            input String
 * @return Integer vector
 */
        static public IList<int> ParseInteger(String str)
        {
            String[] tok = str.Split(',');
            IList<int> ret = new List<int>();
            for (int i = 0; i < tok.Length; i++)
            {
                int d = int.Parse(tok[i]);
                ret.Add(d);
            }
            return ret;
        }

        /**
         * This method parses input String and returns Double vector
         * 
         * @param inputStr
         *            input String
         * @return double array
         */
        static public double[] ParseDoubleArray(String inputStr)
        {
            String[] inputsArrStr = inputStr.Split(',');

            double[] ret = new double[inputsArrStr.Length];
            for (int i = 0; i < inputsArrStr.Length; i++)
            {
                ret[i] = double.Parse(inputsArrStr[i]);
            }

            return ret;
        }

        public static double[] ToDoubleArray(IList<Double> list)
        {
            double[] ret = new double[list.Count];
            for (int i = 0; i < list.Count; i++)
            {
                ret[i] = list[i];
            }
            return ret;
        }

        public static IList<Double> ConvertToVector(double[] array)
        {
            IList<Double> vector = new List<Double>(array.Length);

            foreach (double val in array)
            {
                vector.Add(val);
            }

            return vector;
        }

        public static double[] ConvertToArray(List<Double> list)
        {
            double[] array = new double[list.Count];

            int i = 0;
            foreach (double d in list)
            {
                array[i++] = d;
            }

            return array;
        }
    }
}
