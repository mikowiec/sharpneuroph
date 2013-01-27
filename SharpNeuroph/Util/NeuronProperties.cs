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

namespace Neuroph.Util
{
    /// <summary>
    /// Represents properties of a neuron.
    /// 
    /// Author: Zoran Sevarac <sevarac@gmail.com>
    /// C# Translation: Miley Thomas (http://www.heatonresearch.com)
    /// </summary>
    public class NeuronProperties : Properties
    {
        public NeuronProperties()
        {

            //		this.setProperty("weightsFunction", WeightedInput.class);
            //		this.setProperty("summingFunction", Sum.class);
            this.SetProperty("inputFunction", typeof(WeightedSum));
            this.SetProperty("transferFunction", typeof(Linear));
            this.SetProperty("neuronType", typeof(Neuron));
        }

        public NeuronProperties(TransferFunctionType transferFunctionType)
        {
            //		this.setProperty("weightsFunction", WeightedInput.class);
            //		this.setProperty("summingFunction", Sum.class);
            this.SetProperty("inputFunction", typeof(WeightedSum));
            this.SetProperty("transferFunction", transferFunctionType.getTypeClass());
            this.SetProperty("neuronType", typeof(Neuron));
        }

        public NeuronProperties(Type neuronClass, TransferFunctionType transferFunctionType)
        {            
            //		this.setProperty("weightsFunction", WeightedInput.class);
            //		this.setProperty("summingFunction", Sum.class);
            this.SetProperty("inputFunction", typeof(WeightedSum));
            this.SetProperty("transferFunction", transferFunctionType.getTypeClass());
            this.SetProperty("neuronType", neuronClass);
        }

        public NeuronProperties(Type neuronClass, Type transferFunctionClass)
        {
            //		this.setProperty("weightsFunction", WeightedInput.class);
            //		this.setProperty("summingFunction", Sum.class);
            this.SetProperty("inputFunction", typeof(WeightedSum));
            this.SetProperty("transferFunction", transferFunctionClass);
            this.SetProperty("neuronType", neuronClass);
        }

        public NeuronProperties(Type neuronClass, Type inputFunctionClass, Type transferFunctionClass)
        {
            this.SetProperty("inputFunction", inputFunctionClass);
            this.SetProperty("transferFunction", transferFunctionClass);
            this.SetProperty("neuronType", neuronClass);
        }

        public NeuronProperties(TransferFunctionType transferFunctionType, bool useBias)
        {
            //		this.setProperty("weightsFunction", WeightedInput.class);
            //		this.setProperty("summingFunction", Sum.class);
            this.SetProperty("inputFunction", typeof(WeightedSum));
            this.SetProperty("transferFunction", transferFunctionType.getTypeClass());
            this.SetProperty("useBias", useBias);
            this.SetProperty("neuronType", typeof(Neuron));
        }

        public NeuronProperties(WeightsFunctionType weightsFunctionType,
                                    SummingFunctionType summingFunctionType,
                                    TransferFunctionType transferFunctionType)
        {            
            this.SetProperty("weightsFunction", weightsFunctionType.getTypeClass());
            this.SetProperty("summingFunction", summingFunctionType.getTypeClass());
            this.SetProperty("transferFunction", transferFunctionType.getTypeClass());
            this.SetProperty("neuronType", typeof(Neuron));
        }

        public Type WeightsFunction
        {
            get
            {
                return (Type)this.Get("weightsFunction");
            }
        }

        public Type SummingFunction
        {
            get
            {
                return (Type)this.Get("summingFunction");
            }
        }

        public Type InputFunction
        {
            get
            {
                return (Type)this.Get("inputFunction");
            }

        }

        public Type TransferFunction
        {
            get
            {
                return (Type)this.Get("transferFunction");
            }
        }

        public Type NeuronType
        {
            get
            {
                return (Type)this.Get("neuronType");
            }
        }


        public Properties GetTransferFunctionProperties()
        {
            Properties tfProperties = new Properties();
            foreach (String name in this.Keys)
            {
                if (name.IndexOf("transferFunction") != -1)
                {
                    tfProperties.SetProperty(name, this.Get(name));
                }
            }
            return tfProperties;
        }

        public override void SetProperty(String key, Object value)
        {
            if (value is TransferFunctionType) value = ((TransferFunctionType)value).getTypeClass();
            if (value is WeightsFunctionType) value = ((WeightsFunctionType)value).getTypeClass();
            if (value is SummingFunctionType) value = ((SummingFunctionType)value).getTypeClass();

            this.Put(key, value);
        }

    }
}
