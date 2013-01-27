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
    /// The types of neural network.
    /// 
    /// Author: Zoran Sevarac <sevarac@gmail.com>
    /// C# Translation: Miley Thomas (http://www.heatonresearch.com)
    /// </summary>
    public enum NeuralNetworkType
    {
        ADALINE,
        PERCEPTRON,
        MULTI_LAYER_PERCEPTRON,
        HOPFIELD,
        KOHONEN,
        NEURO_FUZZY_REASONER,
        SUPERVISED_HEBBIAN_NET,
        UNSUPERVISED_HEBBIAN_NET,
        COMPETITIVE,
        MAXNET,
        INSTAR,
        OUTSTAR,
        RBF_NETWORK,
        BAM,
        RECOMMENDER

    }
}
