using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JelleMath;

namespace ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            SimTF SimTFclass = new SimTF();
            SimTFclass.num = new Double[] { 0, 1, 2 };
            SimTFclass.den = new Double[] { 3, 4, 5 };
            SimTFclass.setTFtoMatlabEnviroment();

            Double[] SimResult = SimTFclass.Simulate(new Double[] {0, 0, 3000, 2200 }, new Double[] { 0, 0, 0.4193d, 0.6534d}, 0.2d);

        }
    }
}
