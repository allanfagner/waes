using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Waes.Model;

namespace Waes.Tests.Unity
{
    [TestClass]
    public class ValuesTest
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Values current and other must be different")]
        public void Create_WithSameValue_ThrowsArgumentException()
        {
            new Values('a', 'a');
        }
    }
}
