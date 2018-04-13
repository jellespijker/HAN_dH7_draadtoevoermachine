using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using JelleMath;

namespace SimTFTestUnit
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            SimTF SimTFclass = new SimTF();
            SimTFclass.num = new Double[] {0, 1, 2};
            SimTFclass.den = new Double[] { 3, 4, 5 };
            SimTFclass.setTFtoMatlabEnviroment();

            Double[] SimResult = SimTFclass.Simulate(new Double[] { 0.2d, 0.4d }, new Double[] { 1, 2 });

            Assert.AreEqual(new Double[] {0, 0.067106287937639689d}, SimResult);
        }
    }
}
