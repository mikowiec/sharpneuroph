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
    /// Simple class to emulate the Java properties class.
    /// 
    /// Author: Zoran Sevarac <sevarac@gmail.com>
    /// C# Translation: Miley Thomas (http://www.heatonresearch.com)
    /// </summary>
    public class Properties
    {
        private IDictionary<String, Object> data = new Dictionary<string, object>();

        public ICollection<String> Keys
        {
            get
            {
                return this.data.Keys;
            }
        }

        public virtual void SetProperty(String key, Object value)
        {
            this.data[key] = value;
        }

        public Object Get(String key)
        {
            return this.data[key];
        }

        public Object GetProperty(String key)
        {
            return this.data[key];
        }

        public void Put(String key, Object obj)
        {
            this.data[key] = obj;
        }

        public bool HasProperty(String p)
        {
            return this.data.ContainsKey(p);
        }
    }
}
