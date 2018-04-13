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
using System.Text;
using System.Threading.Tasks;

namespace JelleMath
{
    public class SimTF
    {
        private MLApp.MLApp MWinstance;
        private string result;
        private string Mlocation;
        private double resultD;
        private object resultobj;

        public SimTF()
        {
            MWinstance = new MLApp.MLApp();
            result = "";
            resultD = 0;
            resultobj = new object();
            Mlocation = "D:/SimTF";
            result = MWinstance.Execute("cd('" + Mlocation + "');");
        }

        /// <summary>
        /// Simulate a transfer function in the S-domain
        /// </summary>
        /// <param name="num">Numerator of the transfer function. Array of doubles. Polynomial ordered from nth... 2nd... 1th... Constant</param>
        /// <param name="den">Denominator of the transfer function. Array of doubles. Polynomial ordered from nth... 2nd... 1th... Constant</param>
        /// <param name="PreviousValue">Value from the previous itteration. Use the shift register to obtain this value</param>
        /// <param name="CurrentValue">Value from the current itteration</param>
        public double Simulate(Double[] num, Double[] den, Double PreviousValue, Double CurrentValue)
        {
            // Put the variables in de matlab workspace
            MWinstance.PutWorkspaceData("PreviousValue", "base", PreviousValue);
            MWinstance.PutWorkspaceData("CurrentValue", "base", CurrentValue);
            MWinstance.PutWorkspaceData("num", "base", num);
            MWinstance.PutWorkspaceData("den", "base", den);

            /*
             * Matlab script for SimulateTF
             * Writen by Jelle Spijker
             * 
             * function  CurrentOutput = SimulateTF( num, den, PreviousValue, CurrentValue )
             * %UNTITLED Summary of this function goes here
             * %   Detailed explanation goes here
             * TransferFunction = tf(num, den);
             * TimeVector = 0:1;
             * if PreviousValue ~= CurrentValue 
             *     InputVector = ones(1,2) * PreviousValue;
             *     InputVector(2) = CurrentValue;
             * else
             *     InputVector = ones(1,2) * PreviousValue;
             * end
             * OutputVector = lsim(TransferFunction, InputVector, TimeVector);
             * CurrentOutput = OutputVector(end);
             * end
             * 
             */

            result = MWinstance.Execute("Sres = SimulateTF(num, den, PreviousValue, CurrentValue)");

            // Get the result from the Matalb instance
            var test = MWinstance.GetVariable("Sres", "base");

            //Casts the result in a double
            resultD = (Double)test;

            return resultD;
        }

        ~SimTF()
        {
            MWinstance.Quit();
        }
    }
}
