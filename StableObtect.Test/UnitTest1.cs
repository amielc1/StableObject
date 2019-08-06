using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StableObject;

namespace StableObtect.Test
{
    [TestClass]
    public class StableObtectTest
    {
        [TestMethod]
        public void StableObjectExactStableCounterSet()
        {
            var stableCounter = 5;
            StableObject<bool> myObject = new StableObject<bool>(stableCounter, false);
            for (int i = 0; i < stableCounter; i++)
                myObject.SetValue(true);
            var actualValue = myObject.ActualValue;
            Assert.AreEqual(true, actualValue);
        }
        [TestMethod]
        public void StableObjectMoreStableCounterSet()
        {
            var stableCounter = 5;
            StableObject<bool> myObject = new StableObject<bool>(stableCounter, false);
            for (int i = 0; i < 2 * stableCounter; i++)
                myObject.SetValue(true);
            var actualValue = myObject.ActualValue;
            Assert.AreEqual(true, actualValue);
        }
        [TestMethod]
        public void StableObjectLessThanStableCounterNotSet()
        {
            var stableCounter = 5;
            StableObject<bool> myObject = new StableObject<bool>(stableCounter, false);
            for (int i = 0; i < stableCounter - 1; i++)
                myObject.SetValue(true);
            var actualValue = myObject.ActualValue;
            Assert.AreEqual(false, actualValue);
        }
    }
}
