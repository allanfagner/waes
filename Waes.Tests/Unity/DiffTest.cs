using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using Waes.Model;

namespace Waes.Tests.Unity
{
    [TestClass]
    public class DiffTest
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Invalid offset")]
        public void Create_WithOffsetLessThenZero_ThrowsArgumentException()
        {
            new Diff(-1, 1, new System.Collections.Generic.List<Values> { new Values('a', 'x') });
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Invalid length")]
        public void Create_WithLengthLessThenZero_ThrowsArgumentException()
        {
            new Diff(0,-11, new System.Collections.Generic.List<Values> { new Values('a', 'x') });
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Invalid length")]
        public void Create_WithLengthZero_ThrowsArgumentException()
        {
            new Diff(3, 0, new System.Collections.Generic.List<Values> { new Values('a', 'x') });
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Create_WithNullDiff_ThrowsArgumentNullException()
        {
            new Diff(3, 2, null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "The number of diffs and the length do not match")]
        public void Create_LengthAndNumberOfDiffDoNotMatch_ThrowsArgumentException()
        {
            new Diff(3, 2, new System.Collections.Generic.List<Values> { new Values('a', 'x') });
        }

        [TestMethod]
        public void Create_WithProperValues_InstanceIsCreatedProperly()
        {
            var diff = new Diff(3, 1, new System.Collections.Generic.List<Values> { new Values('a', 'x') });

            Assert.AreEqual(3, diff.Offest);
            Assert.AreEqual(1, diff.Length);
            Assert.AreEqual(1, diff.Diffs.Count());

        }
    }
}
