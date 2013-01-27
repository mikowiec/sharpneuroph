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

namespace Neuroph.Util.Plugins
{
    /// <summary>
    /// Plugin to support labels.
    /// 
    /// Author: Zoran Sevarac <sevarac@gmail.com>
    /// C# Translation: Miley Thomas (http://www.heatonresearch.com)
    /// </summary>
    [Serializable]
    public class LabelsPlugin : PluginBase
    {
        public const String LABELS_PLUGIN_NAME = "LabelsPlugin";


        /// <summary>
        /// Collection of labels for the neural network components
        /// </summary>
        private IDictionary<Object, String> labels = new Dictionary<Object, String>();

        /// <summary>
        /// Field for neural network label
        /// This field is required to solve the java bug described at
        /// http://bugs.sun.com/view_bug.do?bug_id=4957674
        /// </summary>
        private String neuralNetworkLabel = "";

        public LabelsPlugin()
            : base(LABELS_PLUGIN_NAME)
        {
        }

        /// <summary>
        /// Returns label for the specified object 
        /// </summary>
        /// <param name="obj">object for which label should be returned</param>
        /// <returns>label for the specified object</returns>
        public String GetLabel(Object obj)
        {
            if (obj != ParentNetwork )
            {
                return labels[obj];
            }
            else
            {
                return neuralNetworkLabel;
            }
        }

        /// <summary>
        /// Sets label for the specified object 
        /// </summary>
        /// <param name="obj">object to set label</param>
        /// <param name="label">label to set</param>
        public void SetLabel(Object obj, String label)
        {
            if (obj == ParentNetwork )
            {
                neuralNetworkLabel = label;
            }
            else
            {
                labels[obj] = label;
            }
        }
    }
}
