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
    /// A thread safe random number generator.
    /// 
    /// Author: Jeff Heaton(http://www.jeffheaton.com)
    /// </summary>
    public class ThreadSafeRandom
    {
        /// <summary>
        /// A non-thread-safe random number generator that uses a time-based seed.
        /// </summary>
        private static Random random = new Random();

        /// <summary>
        /// Generate a random number between 0 and 1.
        /// </summary>
        /// <returns></returns>
        public static double NextDouble()
        {
            lock (random)
            {
                return random.NextDouble();
            }
        }
    }
}
