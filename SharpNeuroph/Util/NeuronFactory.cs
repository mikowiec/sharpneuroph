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
using Neuroph.Core.Input;
using Neuroph.Core;
using Neuroph.Core.Transfer;
using System.Reflection;
using Neuroph.NNet;
using Neuroph.NNet.Comp;

namespace Neuroph.Util
{
    /// <summary>
    /// Provides methods to create customized instances of Neurons.
    /// 
    /// Author: Zoran Sevarac <sevarac@gmail.com>
    /// C# Translation: Miley Thomas (http://www.heatonresearch.com)
    /// </summary>
    public class NeuronFactory
    {
        /// <summary>
        /// Creates and returns neuron instance according to the given specification in neuronProperties.
        /// </summary>
        /// <param name="neuronProperties">specification of neuron properties</param>
        /// <returns>returns instance of neuron with specified properties</returns>
        public static Neuron CreateNeuron(NeuronProperties neuronProperties)
        {

            InputFunction inputFunction = null;
            Type inputFunctionClass = neuronProperties.InputFunction;

            if (!inputFunctionClass.Equals(" "))
            {
                inputFunction = CreateInputFunction(inputFunctionClass);
            }
            else
            {
                WeightsFunction weightsFunction = CreateWeightsFunction(neuronProperties.WeightsFunction);
                SummingFunction summingFunction = CreateSummingFunction(neuronProperties.SummingFunction);

                inputFunction = new InputFunction(weightsFunction, summingFunction);
            }



            TransferFunction transferFunction = CreateTransferFunction(neuronProperties.GetTransferFunctionProperties());

            Neuron neuron = null;
            Type neuronClass = neuronProperties.NeuronType;

            // use two param constructor to create neuron
            Type[] paramTypes = {
                typeof(InputFunction), 
                typeof(TransferFunction) };

            ConstructorInfo con = neuronClass.GetConstructor(paramTypes);

            if (con != null)
            {
                Object[] paramList = new Object[2];
                paramList[0] = inputFunction;
                paramList[1] = transferFunction;

                object o = con.Invoke(paramList);
                neuron = (Neuron)o;
            }

            if (neuron == null)
            {
                neuron = (Neuron)Activator.CreateInstance(neuronClass);
            }

            if (neuronProperties.HasProperty("thresh"))
            {
                ((ThresholdNeuron)neuron).Thresh = ((Double)neuronProperties.GetProperty("thresh"));
            }
            else if (neuronProperties.HasProperty("bias"))
            {
                ((InputOutputNeuron)neuron).Bias = ((Double)neuronProperties.GetProperty("bias"));
            }

            return neuron;

        }



        private static InputFunction CreateInputFunction(Type inputFunctionClass)
        {
            InputFunction inputFunction = null;

            inputFunction = (InputFunction)Activator.CreateInstance(inputFunctionClass);

            return inputFunction;
        }


        /// <summary>
        /// Creates and returns instance of transfer function 
        /// </summary>
        /// <param name="tfProperties">transfer function properties</param>
        /// <returns>returns transfer function</returns>
        private static TransferFunction CreateTransferFunction(Properties tfProperties)
        {
            TransferFunction transferFunction = null;

            Type tfClass = (Type)tfProperties.GetProperty("transferFunction");

            ParameterInfo[] paramTypes = null;

            ConstructorInfo[] cons = tfClass.GetConstructors();
            for (int i = 0; i < cons.Length; i++)
            {
                paramTypes = cons[i].GetParameters();

                // use constructor with one parameter of Properties type
                if ((paramTypes.Length == 1) && (paramTypes[0].GetType() == typeof(Properties)))
                {
                    Type[] argTypes = new Type[1];
                    argTypes[0] = typeof(Properties);
                    ConstructorInfo ct = tfClass.GetConstructor(argTypes);

                    Object[] argList = new Object[1];
                    argList[0] = tfProperties;
                    transferFunction = (TransferFunction)ct.Invoke(argList);
                    break;
                }
                else if (paramTypes.Length == 0)
                { // use constructor without params
                    transferFunction = (TransferFunction)Activator.CreateInstance(tfClass);
                    break;
                }
            }

            return transferFunction;

        }



        /// <summary>
        /// Creates and returns instance of specified weights function. 
        /// </summary>
        /// <param name="weightsFunctionClass">weights function class</param>
        /// <returns>returns instance of weights function.</returns>
        private static WeightsFunction CreateWeightsFunction(Type weightsFunctionClass)
        {
            WeightsFunction weightsFunction = null;
            weightsFunction = (WeightsFunction)Activator.CreateInstance(weightsFunctionClass);

            return weightsFunction;
        }


        /// <summary>
        /// Creates and returns instance of specified summing function. 
        /// </summary>
        /// <param name="summingFunctionClass">summing function class</param>
        /// <returns>returns instance of summing function</returns>
        private static SummingFunction CreateSummingFunction(Type summingFunctionClass)
        {
            SummingFunction summingFunction = null;

            summingFunction = (SummingFunction)Activator.CreateInstance(summingFunctionClass);

            return summingFunction;
        }

    }
}
