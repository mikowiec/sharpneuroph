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
using Neuroph.Core;

namespace Neuroph.Util
{
    /// <summary>
    /// Contains weights functions types and labels.
    /// 
    /// Author: Zoran Sevarac <sevarac@gmail.com>
    /// C# Translation: Miley Thomas (http://www.heatonresearch.com)
    /// </summary>
    public class LayerFactory
    {
        public static Layer createLayer(int neuronsCount, NeuronProperties neuronProperties)
        {
            Layer layer = new Layer(neuronsCount, neuronProperties);
            return layer;
        }

        public static Layer createLayer(int neuronsCount, TransferFunctionType transferFunctionType)
        {
            NeuronProperties neuronProperties = new NeuronProperties();
            neuronProperties.SetProperty("transferFunction", transferFunctionType);
            Layer layer = createLayer(neuronsCount, neuronProperties);
            return layer;
        }

        public static Layer createLayer(IList<NeuronProperties> neuronPropertiesVector) {
		Layer layer = new Layer();
		
		foreach(NeuronProperties neuronProperties in neuronPropertiesVector) {
			Neuron neuron = NeuronFactory.CreateNeuron(neuronProperties);
			layer.AddNeuron(neuron);
		}
		
		return layer;
	}

    }
}
