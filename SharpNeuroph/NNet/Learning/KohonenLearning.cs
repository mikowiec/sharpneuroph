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
using Neuroph.Core.Learning;
using Neuroph.Core;

namespace Neuroph.NNet.Learning
{
    /// <summary>
    /// Learning algorithm for Kohonen network.
    /// 
    /// Author: Zoran Sevarac <sevarac@gmail.com>
    /// C# Translation: Miley Thomas (http://www.heatonresearch.com)
    /// </summary>
    [Serializable]
    public class KohonenLearning : LearningRule
    {
        public double LearningRate  { get; set; }
        private int[] iterations = { 100, 0 };
        private double[] decStep = new double[2];
        private int mapSize = 0;
        private int[] nR = { 1, 1 }; // neighborhood radius
        private int currentIteration;


        public KohonenLearning(Kohonen nnet)
            : base(nnet)
        {
            LearningRate = 0.9d;
            int neuronsNum = this.NeuralNetwork.GetLayerAt(1).NeuronsCount;
            mapSize = (int)Math.Sqrt(neuronsNum);
        }

        public override void Learn(TrainingSet trainingSet)
        {
            for (int phase = 0; phase < 2; phase++)
            {
                for (int k = 0; k < iterations[phase]; k++)
                {
                    IEnumerator<TrainingElement> e = trainingSet.GetEnumerator();
                    while (e.MoveNext() && !IsStopped)
                    {
                        TrainingElement tE = e.Current;
                        LearnPattern(tE, nR[phase]);
                    } // while
                    currentIteration = k;
                    this.NotifyChange();
                    if (IsStopped) return;
                } // for k
                LearningRate = LearningRate * 0.5;
            } // for phase
        }

        private void LearnPattern(TrainingElement tE, int neighborhood)
        {
            this.NeuralNetwork.SetInput(tE.Input);
            this.NeuralNetwork.Calculate();
            Neuron winner = GetClosest();
            if (winner.Output == 0)
                return; // ako je vec istrenirana jedna celija, izadji

            Layer mapLayer = this.NeuralNetwork.GetLayerAt(1);
            int winnerIdx = mapLayer.IndexOf(winner);
            AdjustCellWeights(winner, 0);

            int cellNum = mapLayer.NeuronsCount;
            for (int p = 0; p < cellNum; p++)
            {
                if (p == winnerIdx)
                    continue;
                if (IsNeighbor(winnerIdx, p, neighborhood))
                {
                    Neuron cell = mapLayer.GetNeuronAt(p);
                    AdjustCellWeights(cell, 1);
                } // if
            } // for

        }

        // get unit with closetst weight vector
        private Neuron GetClosest()
        {
            IEnumerator<Neuron> i = this.NeuralNetwork.GetLayerAt(1)
                    .GetNeuronsEnumerator();
            Neuron winner = new Neuron();
            double minOutput = 100;
            while (i.MoveNext())
            {
                Neuron n = i.Current;
                double o = n.Output;
                if (o < minOutput)
                {
                    minOutput = o;
                    winner = n;
                } // if
            } // while
            return winner;
        }

        private void AdjustCellWeights(Neuron cell, int r)
        {
            IEnumerator<Connection> i = cell.GetInputsEnumerator();
            while (i.MoveNext())
            {
                Connection conn = i.Current;
                double dWeight = (LearningRate / (r + 1))
                        * (conn.Input - conn.ConnectionWeight.Value);
                conn.ConnectionWeight.Inc(dWeight);
            }// while
        }

        private bool IsNeighbor(int i, int j, int n)
        {
            // i - centralna celija
            // n - velicina susedstva
            // j - celija za proveru
            n = 1;
            int d = mapSize;

            // if (j<(i-n*d-n)||(j>(i+n*d+n))) return false;

            int rt = n; // broj celija ka gore
            while ((i - rt * d) < 0)
                rt--;

            int rb = n; // broj celija ka dole
            while ((i + rb * d) > (d * d - 1))
                rb--;

            for (int g = -rt; g <= rb; g++)
            {
                int rl = n; // broj celija u levu stranu
                int rl_mod = (i - rl) % d;
                int i_mod = i % d;
                while (rl_mod > i_mod)
                {
                    rl--;
                    rl_mod = (i - rl) % d;
                }

                int rd = n; // broj celija u desnu stranu
                int rd_mod = (i + rd) % d;
                while (rd_mod < i_mod)
                {
                    rd--;
                    rd_mod = (i + rd) % d;
                }

                if ((j >= (i + g * d - rl)) && (j <= (i + g * d + rd)))
                    return true;
                // else if (j<(i+g*d-rl)) return false;
            } // for
            return false;
        }

        public void SetIterations(int Iphase, int IIphase)
        {
            this.iterations[0] = Iphase;
            this.iterations[1] = IIphase;
        }

        public int Iteration
        {
            get
            {
                return currentIteration;
            }
        }

        public int MapSize
        {
            get
            {
                return mapSize;
            }
        }

    }
}
