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
using Neuroph.NNet.Comp;

namespace Neuroph.Util
{
    /// <summary>
    /// Provides methods to connect neurons by creating Connection objects.
    /// 
    /// Author: Zoran Sevarac <sevarac@gmail.com>
    /// C# Translation: Miley Thomas (http://www.heatonresearch.com)
    /// </summary>
    public class ConnectionFactory
    {
        /// <summary>
        /// Creates connection between two specified neurons
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        public static void CreateConnection(Neuron from, Neuron to)
        {
            Connection connection = new Connection(from, to);
            to.AddInputConnection(connection);
        }

        /// <summary>
        /// Creates connection between two specified neurons
        /// </summary>
        /// <param name="from">output neuron</param>
        /// <param name="to">input neuron</param>
        /// <param name="weightVal">connection weight value</param>
        public static void CreateConnection(Neuron from, Neuron to, double weightVal)
        {
            Connection connection = new Connection(from, to, weightVal);
            to.AddInputConnection(connection);
        }

        public static void CreateConnection(Neuron from, Neuron to, double weightVal, int delay)
        {
            DelayedConnection connection = new DelayedConnection(from, to, weightVal, delay);
            to.AddInputConnection(connection);
        }

        /// <summary>
        /// Creates connection between two specified neurons
        /// </summary>
        /// <param name="from">output neuron</param>
        /// <param name="to">input neuron</param>
        /// <param name="weight">connection weight</param>
        public static void CreateConnection(Neuron from, Neuron to, Weight weight)
        {
            Connection connection = new Connection(from, to, weight);
            to.AddInputConnection(connection);
        }

        /// <summary>
        /// Creates full connectivity between the two specified layers
        /// </summary>
        /// <param name="fromLayer">layer to connect</param>
        /// <param name="toLayer">layer to connect to</param>
        public static void FullConnect(Layer fromLayer, Layer toLayer)
        {
            foreach (Neuron fromNeuron in fromLayer.Neurons)
            {
                foreach (Neuron toNeuron in toLayer.Neurons)
                {
                    CreateConnection(fromNeuron, toNeuron);
                }
            }
        }

        /// <summary>
        /// Creates full connectivity between the two specified layers
        /// </summary>
        /// <param name="fromLayer">layer to connect</param>
        /// <param name="toLayer">layer to connect to</param>
        /// <param name="connectBiasNeuron"></param>
        public static void FullConnect(Layer fromLayer, Layer toLayer, bool connectBiasNeuron)
        {
            foreach (Neuron fromNeuron in fromLayer.Neurons)
            {
                if (fromNeuron is BiasNeuron) continue;
                foreach (Neuron toNeuron in toLayer.Neurons)
                {
                    CreateConnection(fromNeuron, toNeuron);
                }
            }
        }


        /// <summary>
        /// Creates full connectivity between two specified layers with specified
        /// weight for all connections 
        /// </summary>
        /// <param name="fromLayer">output layer</param>
        /// <param name="toLayer">input layer</param>
        /// <param name="weightVal">connection weight value</param>
        public static void FullConnect(Layer fromLayer, Layer toLayer, double weightVal)
        {
            foreach (Neuron fromNeuron in fromLayer.Neurons)
            {
                foreach (Neuron toNeuron in toLayer.Neurons)
                {
                    CreateConnection(fromNeuron, toNeuron, weightVal);
                }
            }
        }

        /// <summary>
        /// Creates full connectivity within layer - each neuron with all other
        /// within the same layer
        /// </summary>
        /// <param name="layer"></param>
        public static void FullConnect(Layer layer)
        {
            int neuronNum = layer.NeuronsCount;
            for (int i = 0; i < neuronNum; i++)
            {
                for (int j = 0; j < neuronNum; j++)
                {
                    if (j == i)
                        continue;
                    Neuron from = layer.GetNeuronAt(i);
                    Neuron to = layer.GetNeuronAt(j);
                    CreateConnection(from, to);
                } // j
            } // i
        }

        /// <summary>
        /// Creates full connectivity within layer - each neuron with all other
        /// within the same layer with the specified weight values for all
        /// conections.
        /// </summary>
        /// <param name="layer"></param>
        /// <param name="weightVal"></param>
        public static void FullConnect(Layer layer, double weightVal)
        {
            int neuronNum = layer.NeuronsCount;
            for (int i = 0; i < neuronNum; i++)
            {
                for (int j = 0; j < neuronNum; j++)
                {
                    if (j == i)
                        continue;
                    Neuron from = layer.GetNeuronAt(i);
                    Neuron to = layer.GetNeuronAt(j);
                    CreateConnection(from, to, weightVal);
                } // j
            } // i
        }

        /// <summary>
        /// Creates full connectivity within layer - each neuron with all other
        /// within the same layer with the specified weight and delay values for all
        /// conections.
        /// </summary>
        /// <param name="layer"></param>
        /// <param name="weightVal"></param>
        /// <param name="delay"></param>
        public static void FullConnect(Layer layer, double weightVal, int delay)
        {
            int neuronNum = layer.NeuronsCount;
            for (int i = 0; i < neuronNum; i++)
            {
                for (int j = 0; j < neuronNum; j++)
                {
                    if (j == i)
                        continue;
                    Neuron from = layer.GetNeuronAt(i);
                    Neuron to = layer.GetNeuronAt(j);
                    CreateConnection(from, to, weightVal, delay);
                } // j
            } // i
        }


        /// <summary>
        /// Creates forward connectivity pattern between the specified layers
        /// </summary>
        /// <param name="fromLayer">layer to connect</param>
        /// <param name="toLayer">layer to connect to</param>
        /// <param name="weightVal"></param>
        public static void ForwardConnect(Layer fromLayer, Layer toLayer, double weightVal)
        {
            for (int i = 0; i < fromLayer.NeuronsCount; i++)
            {
                Neuron fromNeuron = fromLayer.GetNeuronAt(i);
                Neuron toNeuron = toLayer.GetNeuronAt(i);
                CreateConnection(fromNeuron, toNeuron, weightVal);
            }
        }


        /// <summary>
        /// Creates forward connection pattern between specified layers
        /// </summary>
        /// <param name="fromLayer">layer to connect</param>
        /// <param name="toLayer">layer to connect to</param>
        public static void ForwardConnect(Layer fromLayer, Layer toLayer)
        {
            for (int i = 0; i < fromLayer.NeuronsCount; i++)
            {
                Neuron fromNeuron = fromLayer.GetNeuronAt(i);
                Neuron toNeuron = toLayer.GetNeuronAt(i);
                CreateConnection(fromNeuron, toNeuron, 1);
            }
        }

    }
}
