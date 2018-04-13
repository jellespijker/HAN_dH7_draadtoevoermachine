/*
The MIT License (MIT)

Copyright (c) 2013 Jelle Spijker

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.
*/

using System;
using System.Collections.Generic;
using System.Linq;

namespace JelleMath
{
    public class SimTF
    {
        #region Matlab Commands
        /// <summary>
        /// The Matlab command to create the Transfer Function
        /// </summary>
        private const string STR_SysTfnumDen = "sys = tf(num, den);";
        /// <summary>
        /// The Matlab simulate command
        /// </summary>
        private const string STR_SresLsimsysUvectorTvector = "Yarray = lsim(sys, Uvector, Tvector)";
        #endregion

        #region Fields

        private MLApp.MLApp MWinstance;

        private string _result;

        private Double[] _num;

        private Double[] _den;

        #endregion

        #region Properties
        /// <summary>
        /// Last Matlab results as a string
        /// </summary>
        public string Result
        {
            get
            {
                return _result;
            }
            set
            {
                _result = value;
            }
        }

        /// <summary>
        /// Numorator cooefficients of the transfer function polynomial
        /// </summary>
        public Double[] num
        {
            get { return _num; }
            set { _num = value; }
        }

        /// <summary>
        /// Denominator cooefficients of the transfer function polynomial
        /// </summary>
        public Double[] den
        {
            get { return _den; }
            set { _den = value; }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Standard Constructor to initiate the class
        /// </summary>
        public SimTF()
        {
            // Inits the Matlab app
            MWinstance = new MLApp.MLApp();
        }

        /// <summary>
        /// Overload constructor that takes the transfer function as input and initiate The start variables in Matlab
        /// </summary>
        /// <param name="num">Numorator cooefficients of the transfer function polynomial</param>
        /// <param name="den">Denominator cooefficients of the transfer function polynomial</param>
        public SimTF(Double[] num, Double[] den)
        {
            MWinstance = new MLApp.MLApp();

            // Puts the num en den in the Matlab enviroment
            MWinstance.PutWorkspaceData("num", "base", num);
            MWinstance.PutWorkspaceData("den", "base", den);

            //Create the transfer function
            MWinstance.Execute(STR_SysTfnumDen);
        }
        #endregion

        /// <summary>
        /// Sets the Transfer Function in Matlab
        /// </summary>
        public void setTFtoMatlabEnviroment()
        {
            // Puts the num en den in the Matlab enviroment
            MWinstance.PutWorkspaceData("num", "base", num);
            MWinstance.PutWorkspaceData("den", "base", den);

            //Create the transfer function
            MWinstance.Execute(STR_SysTfnumDen);
        }

        /// <summary>
        /// Sets the Transfer Function in Matlab
        /// </summary>
        /// <param name="num">Numorator cooefficients of the transfer function polynomial</param>
        /// <param name="den">Denominator cooefficients of the transfer function polynomial</param>
        public void setTFtoMatlabEnviroment(Double[] num, Double[] den)
        {
            // Puts the num en den in the Matlab enviroment
            MWinstance.PutWorkspaceData("num", "base", num);
            MWinstance.PutWorkspaceData("den", "base", den);

            //Create the transfer function
            MWinstance.Execute(STR_SysTfnumDen);
        }

        ///// <summary>
        ///// Simulates the transfer function in matlab and outputs the results a a Double array
        ///// </summary>
        ///// <param name="Uvector">The Input vector, these input should have the same size as the Time Vector at regualar intervals</param>
        ///// <param name="Tvector">The Output vector, this vector should have a regualor non repeating sequence of real numbers t0:dt:t1</param>
        //public double[] Simulate(Double[] Uvector, Double[] Tvector)
        //{

        //    // Put the variables in de matlab workspace
        //    MWinstance.PutWorkspaceData("Uvector", "base", Uvector);
        //    MWinstance.PutWorkspaceData("Tvector", "base", Tvector);

        //    // Execute the Transfer Function
        //    Result = MWinstance.Execute(STR_SresLsimsysUvectorTvector);

        //    // Get the Yvector from the Matalb instance
        //    Double[,] Yarray = MWinstance.GetVariable("Yarray", "base");

        //    
        //    // Gets a 1-dimensional Yvector from the 2-dimenisional Yarray
        //    Double[] Yvector = new Double[Yarray.Length];
        //    int i = 0;

        //    foreach (var row in Yarray)
        //    {
        //        Yvector[i++] = row;
        //    }

        //    return Yvector;
        //}

        /// <param name="Uvector">Overload constructor that takes the transfer function as input and initiate The start variables in Matlab</param>
        /// <param name="Tvector">The Output vector, this vector should have a regualor non repeating sequence of real numbers t0:dt:t1 Beacause of Labview shitty timing this will be normalised by the dt variable</param>
        /// <param name="dt">Used to normalise the Tvector</param>
        public double[] Simulate(Double[] Uvector, Double[] Tvector, Double dt)
        {

            //Makes the Tvector vector workable in Matlab 
            Double[] tvec = new Double[Tvector.Length];

            for (int i = 0; i < Tvector.Length; i++)
            {
                tvec[i] = Math.Round(Math.Round(Tvector[0], 0) + i * dt, 3);
            }

            // Sort the U vector using the T vector as key 
            Double[,] UT_Matrix = new Double[Tvector.Length, 2];

            // Put the variables in de matlab workspace
            MWinstance.PutWorkspaceData("Uvector", "base", Uvector);
            MWinstance.PutWorkspaceData("Tvector", "base", tvec);

            // Execute the Transfer Function
            Result = MWinstance.Execute(STR_SresLsimsysUvectorTvector);

            // Get the Yvector from the Matalb instance
            Double[,] Yarray = MWinstance.GetVariable("Yarray", "base");


            // Gets a 1-dimensional Yvector from the 2-dimenisional Yarray
            Double[] Yvector = new Double[Yarray.Length];

            for (int i = 0; i < Yarray.Length; i++)
            {
                Yvector[i] = Yarray[i, 0];
            }

            return Yvector;
        }


        #region Destructors
        ~SimTF()
        {
            MWinstance.Quit();
        }
        #endregion
    }
}
