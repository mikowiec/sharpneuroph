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
using System.Collections;
using Neuroph.Core.Exceptions;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Neuroph.Core.Learning
{
    /// <summary>
    /// A set of training elements for training neural network.
    /// 
    /// Author: Zoran Sevarac <sevarac@gmail.com>
    /// C# Translation: Miley Thomas (http://www.heatonresearch.com)
    /// </summary>
    [Serializable]
    public class TrainingSet : IEnumerable<TrainingElement>
    {
        /// <summary>
        /// Collection of training elements
        /// </summary>
        private List<TrainingElement> elements;

        private int inputVectorSize = 0;
        private int outputVectorSize = 0;

        /// <summary>
        /// Label for this training set
        /// </summary>
        private String label;

        /// <summary>
        /// Full file path incuding file name
        /// </summary>
        [NonSerialized]
        private String filePath;

        /// <summary>
        /// Creates an instance of new empty training set
        /// </summary>
        public TrainingSet()
        {
            this.elements = new List<TrainingElement>();
        }

        /// <summary>
        /// Creates an instance of new empty training set with given label
        /// </summary>
        /// <param name="label">training set label</param>
        public TrainingSet(String label)
        {
            this.label = label;
            this.elements = new List<TrainingElement>();
        }


        /// <summary>
        /// Creates an instance of new empty training set 
        /// </summary>
        /// <param name="inputVectorSize"></param>
        public TrainingSet(int inputVectorSize)
        {
            this.elements = new List<TrainingElement>();
            this.inputVectorSize = inputVectorSize;
        }


        /// <summary>
        /// Creates an instance of new empty training set 
        /// </summary>
        /// <param name="inputVectorSize"></param>
        /// <param name="outputVectorSize"></param>
        public TrainingSet(int inputVectorSize, int outputVectorSize)
        {
            this.elements = new List<TrainingElement>();
            this.inputVectorSize = inputVectorSize;
            this.outputVectorSize = outputVectorSize;
        }

        /// <summary>
        /// Adds new training element to this training set
        /// </summary>
        /// <param name="el">training element to add</param>
        public void Add(TrainingElement el)
        {
            // check input vector size if it is predefined
            if ((this.inputVectorSize != 0)
                    && (el.Input.Length != this.inputVectorSize))
            {
                throw new VectorSizeMismatchException(
                        "Input vector size does not match training set!");
            }
            // check output vector size if it is predefined
            if (el is SupervisedTrainingElement)
            {
                SupervisedTrainingElement sel = (SupervisedTrainingElement)el;
                if ((this.outputVectorSize != 0)
                        && (sel.DesiredOutput.Length != this.outputVectorSize))
                {
                    throw new VectorSizeMismatchException(
                            "Output vector size does not match training set!");
                }
            }
            // if everything went ok add training element
            this.elements.Add(el);
        }


        /// <summary>
        /// Removes training element at specified index position 
        /// </summary>
        /// <param name="idx">position of element to remove</param>
        public void RemoveAt(int idx)
        {
            this.elements.RemoveAt(idx);
        }

        /// <summary>
        /// Returns training elements collection
        /// </summary>
        /// <returns>training elements collection</returns>
        public IList<TrainingElement> TrainingElements
        {
            get
            {
                return this.elements;
            }
        }


        /// <summary>
        /// Returns training element at specified index position
        /// </summary>
        /// <param name="idx">index position of training element to return</param>
        /// <returns>training element at specified index position</returns>
        public TrainingElement ElementAt(int idx)
        {
            return this.elements.ElementAt(idx);
        }

        public TrainingElement this[int index]
        {
            get
            {
                return this.elements[index];
            }
        }

        /// <summary>
        /// Removes all alements from training set
        /// </summary>
        public void Clear()
        {
            this.elements.Clear();
        }

        /// <summary>
        /// Returns true if training set is empty, false otherwise
        /// </summary>
        public bool IsEmpty
        {
            get
            {
                return this.elements.Count == 0;
            }
        }

        /// <summary>
        /// Returns number of training elements in this training set set
        /// </summary>
        public int Count
        {
            get
            {
                return this.elements.Count;
            }
        }

        /// <summary>
        /// Returns label for this training set
        /// </summary>
        public String Label
        {
            get
            {
                return label;
            }
            set
            {
                this.label = value;
            }
        }


        /// <summary>
        /// Sets full file path for this training set
        /// </summary>
        public String FilePath
        {
            get
            {
                return this.filePath;
            }
            set
            {
                this.filePath = value;
            }
        }


        /// <summary>
        /// Returns label of this training set 
        /// </summary>
        /// <returns>label of this training set</returns>
        public override String ToString()
        {
            return this.label;
        }

        /// <summary>
        /// Saves this training set to the specified file
        /// </summary>
        /// <param name="filePath">path</param>
        public void Save(String filePath)
        {
            this.filePath = filePath;
            this.Save();
        }

        /// <summary>
        /// Saves this training set to file specified in its filePath field
        /// </summary>
        public void Save()
        {
            Stream s = new FileStream(this.filePath, FileMode.Create, FileAccess.Write, FileShare.None);
            BinaryFormatter b = new BinaryFormatter();
            b.Serialize(s, this);
            s.Close();
        }

        /// <summary>
        /// Loads training set from the specified file 
        /// </summary>
        /// <param name="filePath">training set file</param>
        /// <returns>loded training set</returns>
        public static TrainingSet Load(String filePath)
        {
            Stream s = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.None);
            BinaryFormatter b = new BinaryFormatter();
            object obj = b.Deserialize(s);
            s.Close();
            return (TrainingSet)obj;
        }

        public IEnumerator CreateEnumerator()
        {
            return this.elements.GetEnumerator();
        }

        public int IdealSize
        {
            get
            {
            return this.outputVectorSize;
            }
        }

        public int InputSize
        {
            get
            {
            return this.inputVectorSize;
            }
        }

        public bool IsSupervised
        {
            get
            {
            return this.outputVectorSize > 0;
            }
        }

        /*public void getRecord(long index, EngineData pair) {
            EngineData item = this.elements.get((int)index);
            pair.setInput(item.getInput());
            pair.setIdeal(item.getIdeal());
        }*/

        public long RecordCount
        {
            get
            {
                return this.elements.Count;
            }
        }

        /*public EngineIndexableSet openAdditional() {
            return this;
        }*/

        public IEnumerator<TrainingElement> GetEnumerator()
        {
            return this.elements.GetEnumerator();
        }


        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.elements.GetEnumerator();
        }


    }
}
