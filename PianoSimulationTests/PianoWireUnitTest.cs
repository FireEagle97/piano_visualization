using Microsoft.VisualStudio.TestTools.UnitTesting;
using PianoSimulation;
namespace PianoSimulationTests
{
    [TestClass]
    public class PianoWireUnitTest
    {
        [TestMethod]
        public void TestSample(){
            PianoWire aWire = new PianoWire(2,10);
            double[] numsArr = new double[]{1,2,3,4,5};
            aWire.SamplesArr.Fill(numsArr);
            Assert.AreEqual(1,aWire.Sample());

            
        }
    }
}