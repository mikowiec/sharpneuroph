﻿//
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
using Neuroph.Core;

namespace Neuroph.Util.Plugins
{
    /// <summary>
    /// Base class for plugins.
    /// 
    /// Author: Zoran Sevarac <sevarac@gmail.com>
    /// C# Translation: Miley Thomas (http://www.heatonresearch.com)
    /// </summary>
    [Serializable]
    public class PluginBase
    {
        /// <summary>
        /// Name for this plugin
        /// </summary>
        public virtual String Name { get; set; }

        /// <summary>
        /// Reference to parent neural network
        /// </summary>
        public NeuralNetwork ParentNetwork { get; set; }

        /// <summary>
        /// Creates an instance of plugin for neural network
        /// </summary>
        /// <param name="name"></param>
        public PluginBase(String name)
        {
            this.Name = name;
        }

    }
}
