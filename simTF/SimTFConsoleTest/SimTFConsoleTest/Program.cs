using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimTFConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            JelleMath.SimTF stf = new JelleMath.SimTF();
            double[] num = new double[] {1, 2};
            double[] den = new double[] {3, 4, 6};
            double prev = 10;
            double cur = 11;

            Console.WriteLine(stf.Simulate(num, den, prev, cur));
            Console.ReadLine();

        }
    }
}
