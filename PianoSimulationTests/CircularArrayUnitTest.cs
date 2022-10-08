using Microsoft.VisualStudio.TestTools.UnitTesting;
using InteractivePiano;
namespace PianoSimulationTests
{
    [TestClass]
    public class CircularArrayUnitTest
    {
        [TestMethod]
        public void TestLength()
        {   double expected = 5;
            CircularArray arr = new CircularArray(5);
            Assert.AreEqual(arr.Length, expected);

        }
        [TestMethod]
        public void TestShiftTopBuffer()
        {   
            double expected = 2;
            CircularArray arr = new CircularArray(5);
            double[] numsarr = new double[]{1,2,3,4,5};
            arr.Fill(numsarr);
            double returnedValue = arr.Shift(6);
            Assert.AreEqual(arr[0],expected);

        }
        [TestMethod]
        public void TestShiftEndBuffer()
        {   
            double expected = 6;
            CircularArray arr = new CircularArray(5);
            double[] numsarr = new double[]{1,2,3,4,5};
            arr.Fill(numsarr);
            double returnedValue = arr.Shift(6);
            Assert.AreEqual(arr[arr.Length-1],expected);

        }
        [TestMethod]
        public void TestFill()
        {
            double[] numbers =new double[]{1,2,3,4,5};
            CircularArray arr = new CircularArray(5);
            arr.Fill(numbers);
            Assert.AreEqual(arr[0], numbers[0]);

        }
  
    }
}

