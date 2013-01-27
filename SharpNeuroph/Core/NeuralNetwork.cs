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
using System.Threading;
using Neuroph.Util;
using Neuroph.Core.Learning;
using Neuroph.Util.Plugins;
using Neuroph.Core.Exceptions;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Neuroph.Core
{
    /// <summary>
    /// Base class for artificial neural networks. It provides generic structure and functionality
    /// for the neural networks. Neural network contains a collection of neuron layers and learning rule.
    /// Custom neural networks are created by deriving from this class, creating layers of interconnected network specific neurons,
    /// and setting network specific learning rule.
    /// 
    /// Author: Zoran Sevarac <sevarac@gmail.com>
    /// C# Translation: Miley Thomas (http://www.heatonresearch.com)
    /// </summary>
    [Serializable]
    public class NeuralNetwork
    {
        /// <summary>
        /// Network type id (see neuroph.util.NeuralNetworkType)
        /// </summary>
        public NeuralNetworkType NetworkType { get; set; }

        /// <summary>
        /// Neural network
        /// </summary>
        private IList<Layer> layers;

        /// <summary>
        /// Reference to network input neurons
        /// </summary>
        public IList<Neuron> InputNeurons { get; set; }

        /// <summary>
        /// Reference to newtwork output neurons
        /// </summary>
        public IList<Neuron> OutputNeurons { get; set; }

        /// <summary>
        /// Learning rule for this network
        /// </summary>
        private LearningRule learningRule;

        /// <summary>
        /// Separate thread for learning rule
        /// </summary>
        [NonSerialized]
        private Thread learningThread;

        /// <summary>
        /// Plugins collection
        /// </summary>
        private IDictionary<String, PluginBase> plugins;

        /// <summary>
        /// Creates an instance of empty neural network.
        /// </summary>
        public NeuralNetwork()
        {
            this.layers = new List<Layer>();
            this.plugins = new Dictionary<String, PluginBase>();
            this.AddPlugin(new LabelsPlugin());
        }

        /// <summary>
        /// Adds layer to neural network 
        /// </summary>
        /// <param name="layer">layer to add</param>
        public void AddLayer(Layer layer)
        {
            layer.ParentNetwork = this;
            this.layers.Add(layer);
        }

        /// <summary>
        /// Adds layer to specified index position in network 
        /// </summary>
        /// <param name="idx">index position to add layer</param>
        /// <param name="layer">layer to add</param>
        public void AddLayer(int idx, Layer layer)
        {
            layer.ParentNetwork = this;
            this.layers.Insert(idx, layer);
        }

        /// <summary>
        /// Removes specified layer from network 
        /// </summary>
        /// <param name="layer">layer to remove</param>
        public void RemoveLayer(Layer layer)
        {
            this.layers.Remove(layer);
        }

        /// <summary>
        /// Removes layer at specified index position from net 
        /// </summary>
        /// <param name="idx">int value represents index postion of layer which should be
        /// removed</param>
        public void RemoveLayerAt(int idx)
        {
            this.layers.RemoveAt(idx);
        }
        
        /// <summary>
        /// Returns interface for iterating layers 
        /// </summary>
        /// <returns>interface for network getLayersIterator</returns>
        public IEnumerator<Layer> GetLayersEnumerator()
        {
            return this.layers.GetEnumerator();
        }

        /// <summary>
        /// Returns layers collection
        /// </summary>
        public IList<Layer> Layers
        {
            get
            {
                return this.layers;
            }
        }
        
        /// <summary>
        /// Returns layer at specified index 
        /// </summary>
        /// <param name="idx">layer index position</param>
        /// <returns>layer at specified index position</returns>
        public Layer GetLayerAt(int idx)
        {
            return this.layers[idx];
        }

        /// <summary>
        /// Returns index position of the specified layer
        /// </summary>
        /// <param name="layer">requested Layer object</param>
        /// <returns>layer position index</returns>
        public int IndexOf(Layer layer)
        {
            return this.layers.IndexOf(layer);
        }

        /// <summary>
        /// Returns number of layers in network
        /// </summary>
        public int LayersCount
        {
            get
            {
                return this.layers.Count;
            }
        }

        /// <summary>
        /// Sets network input. Input is array of double values. 
        /// </summary>
        /// <param name="inputVector">network input as double array</param>
        public void SetInput(params double[] inputVector)
        {
            if (inputVector.Length != InputNeurons.Count)
                throw new VectorSizeMismatchException("Input vector size does not match network input dimension!");

            int i = 0;
            foreach (Neuron neuron in this.InputNeurons)
            {
                neuron.NetInput = inputVector[i]; // set input to the coresponding neuron
                i++;
            }

        }

        /// <summary>
        /// Returns network output Vector. Output Vector is a collection of Double
        /// values.
        /// </summary>
        public double[] Output
        {
            get
            {
                double[] outputVector = new double[OutputNeurons.Count];

                int i = 0;
                foreach (Neuron neuron in this.OutputNeurons)
                {
                    outputVector[i] = neuron.Output;
                    i++;
                }

                return outputVector;
            }
        }

        /// <summary>
        /// Performs calculation on whole network
        /// </summary>
        public void Calculate()
        {
            foreach (Layer layer in this.layers)
            {
                layer.Calculate();
            }
        }

        /// <summary>
        /// Resets the activation levels for whole network
        /// </summary>
        public void Reset()
        {
            foreach (Layer layer in this.layers)
            {
                layer.Reset();
            }
        }

        /// <summary>
        /// Implementation of Runnable interface for calculating network in the
        /// separate thread.
        /// </summary>
        public void Run()
        {
            this.Calculate();
        }

        /// <summary>
        /// Starts learning in a new thread to learn the specified training set,
        /// and immediately returns from method to the current thread execution 
        /// </summary>
        /// <param name="trainingSetToLearn">set of training elements to learn</param>
        public void LearnInNewThread(TrainingSet trainingSetToLearn)
        {
            learningRule.TrainingSet = trainingSetToLearn;
            learningThread = new Thread(new ThreadStart(learningRule.Run));
            learningRule.SetStarted();
            learningThread.Start();
        }

        /// <summary>
        /// Starts learning with specified learning rule in new thread to learn the
        /// specified training set, and immediately returns from method to the current thread execution 
        /// </summary>
        /// <param name="trainingSetToLearn">set of training elements to learn</param>
        /// <param name="learningRule">learning algorithm</param>
        public void LearnInNewThread(TrainingSet trainingSetToLearn, LearningRule learningRule)
        {
            LearningRule = learningRule;
            learningRule.TrainingSet = trainingSetToLearn;
            learningThread = new Thread(new ThreadStart(learningRule.Run));
            learningRule.SetStarted();
            learningThread.Start();
        }

        /// <summary>
        /// Starts the learning in the current running thread to learn the specified
        /// training set, and returns from method when network is done learning 
        /// </summary>
        /// <param name="trainingSetToLearn">set of training elements to learn</param>
        public void LearnInSameThread(TrainingSet trainingSetToLearn)
        {
            learningRule.TrainingSet = trainingSetToLearn;
            learningRule.SetStarted();
            learningRule.Run();
        }


        /// <summary>
        /// Starts the learning with specified learning rule in the current running
        /// thread to learn the specified training set, and returns from method when network is done learning 
        /// </summary>
        /// <param name="trainingSetToLearn">set of training elements to learn</param>
        /// <param name="learningRule">learning algorithm</param>
        public void LearnInSameThread(TrainingSet trainingSetToLearn, LearningRule learningRule)
        {
            LearningRule = learningRule;
            learningRule.TrainingSet = trainingSetToLearn;
            learningRule.SetStarted();
            learningRule.Run();
        }

        /// <summary>
        /// Stops learning
        /// </summary>
        public void StopLearning()
        {
            learningRule.StopLearning();
        }

        /// <summary>
        /// Pause the learning - puts learning thread in wait state.
        /// Makes sense only wen learning is done in new thread with learnInNewThread() method
        /// </summary>
        public void PauseLearning()
        {
            if (learningRule is IterativeLearning)
                ((IterativeLearning)learningRule).Pause();
        }

        /// <summary>
        /// Resumes paused learning - notifies the learning thread to continue
        /// </summary>
        public void ResumeLearning()
        {
            if (learningRule is IterativeLearning)
                ((IterativeLearning)learningRule).Resume();
        }

        /// <summary>
        /// Randomizes connection weights for the whole network
        /// </summary>
        public void RandomizeWeights()
        {
            foreach (Layer layer in this.layers)
            {
                layer.RandomizeWeights();
            }
        }

        /// <summary>
        /// Initialize connection weights for the whole network to a value 
        /// </summary>
        /// <param name="value">the weight value</param>
        public void InitializeWeights(double value)
        {
            foreach (Layer layer in this.layers)
            {
                layer.InitializeWeights(value);
            }
        }
        
        /// <summary>
        /// Initialize connection weights for the whole network using a
        /// random number generator 
        /// </summary>
        /// <param name="generator">the random number generator</param>
        public void InitializeWeights(Random generator)
        {
            foreach (Layer layer in this.layers)
            {
                layer.InitializeWeights(generator);
            }
        }

        public void InitializeWeights(double min, double max)
        {
            foreach (Layer layer in this.layers)
            {
                layer.InitializeWeights(min, max);
            }
        }

        /// <summary>
        /// Returns the learning algorithm of this network
        /// </summary>
        public LearningRule LearningRule
        {
            get
            {
                return this.learningRule;
            }
            set
            {
                this.learningRule = value;
                learningRule.NeuralNetwork = this; 
            }
        }

        /// <summary>
        /// Returns the current learning thread (if it is learning in the new thread
        /// Check what happens if it learns in the same thread)
        /// </summary>
        public Thread LearningThread
        {
            get
            {
                return learningThread;
            }
        }

        /// <summary>
        /// Notifies observers about some change
        /// </summary>
        public void NotifyChange()
        {
            //setChanged();
            //notifyObservers();
            //clearChanged();
        }

        /// <summary>
        /// Creates connection with specified weight value between specified neurons
        /// </summary>
        /// <param name="fromNeuron">neuron to connect</param>
        /// <param name="toNeuron">neuron to connect to</param>
        /// <param name="weightVal">connection weight value</param>
        public void CreateConnection(Neuron fromNeuron, Neuron toNeuron, double weightVal)
        {
            Connection connection = new Connection(fromNeuron, toNeuron, weightVal);
            toNeuron.AddInputConnection(connection);
        }


        public override String ToString()
        {
            if (plugins.ContainsKey("LabelsPlugin"))
            {
                LabelsPlugin labelsPlugin = ((LabelsPlugin)this.GetPlugin("LabelsPlugin"));
                String label = labelsPlugin.GetLabel(this);
                if (label != null) return label;
            }

            return base.ToString();
        }

        /// <summary>
        /// Saves neural network into the specified file. 
        /// </summary>
        /// <param name="filePath">file path to save network into</param>
        public void Save(String filePath)
        {
            Stream s = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None);
            BinaryFormatter b = new BinaryFormatter();
            b.Serialize(s, this);
            s.Close();
        }



        /// <summary>
        /// Loads neural network from the specified file.
        /// </summary>
        /// <param name="filePath">file path to load network from</param>
        /// <returns>loaded neural network as NeuralNetwork object</returns>
        public static NeuralNetwork Load(String filePath)
        {
            Stream s = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.None);
            BinaryFormatter b = new BinaryFormatter();
            object obj = b.Deserialize(s);
            s.Close();
            return (NeuralNetwork)obj;
        }



        /// <summary>
        /// Adds plugin to neural network 
        /// </summary>
        /// <param name="plugin">neural network plugin to add</param>
        public void AddPlugin(PluginBase plugin)
        {
            plugin.ParentNetwork = this;
            this.plugins[plugin.Name] = plugin;
        }


        /// <summary>
        /// Returns the requested plugin
        /// </summary>
        /// <param name="pluginName">name of the plugin to get</param>
        /// <returns>plugin with specified name</returns>
        public PluginBase GetPlugin(String pluginName)
        {
            return this.plugins[pluginName];
        }

        /// <summary>
        /// Removes the plugin with specified name 
        /// </summary>
        /// <param name="pluginName">name of the plugin to remove</param>
        public void RemovePlugin(String pluginName)
        {
            this.plugins.Remove(pluginName);
        }

    }
}
