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
    
    public class Base64DuoTest
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Invalid id")]
        public void New_WithInvalidId_ThrowsArgumentException()
        {
            new Base64Duo(0, new Base64(""), new Base64(""));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Either left or right should be informed")]
        public void New_WithBothLeftAndRigthNull_ThrowsArgumentException()
        {
            new Base64Duo(1, null, null);
           // VGV4dCB0byBiZSBjb252ZXJ0ZWQ=
        }

        [TestMethod]
        public void New_WithLeftPassed_CreatesTheInstanceCorretly()
        {
            var base64Duo = new Base64Duo(1, new Base64("VGV4dCB0byBiZSBjb252ZXJ0ZWQ="), null);

            Assert.AreEqual(1, base64Duo.Id);
            Assert.AreEqual("VGV4dCB0byBiZSBjb252ZXJ0ZWQ=", base64Duo.Left.ToString());
            Assert.AreEqual("", base64Duo.Right.ToString());
        }

        [TestMethod]
        public void New_WithRightPassed_CreatesTheInstanceCorretly()
        {
            var base64Duo = new Base64Duo(1, null, new Base64("VGV4dCB0byBiZSBjb252ZXJ0ZWQ="));

            Assert.AreEqual(1, base64Duo.Id);
            Assert.AreEqual("VGV4dCB0byBiZSBjb252ZXJ0ZWQ=", base64Duo.Right.ToString());
            Assert.AreEqual("", base64Duo.Left.ToString());
        }

        [TestMethod]
        public void New_WithLeftDifferentFromRight_CreatesTheInstanceCorretly()
        {
            var base64Duo = new Base64Duo(1, new Base64("VGV4dCB0byBiZSBjb21wYXJlZA=="), new Base64("VGV4dCB0byBiZSBjb252ZXJ0ZWQ="));

            Assert.AreEqual(1, base64Duo.Id);
            Assert.AreEqual("VGV4dCB0byBiZSBjb21wYXJlZA==", base64Duo.Left.ToString());
            Assert.AreEqual("VGV4dCB0byBiZSBjb252ZXJ0ZWQ=", base64Duo.Right.ToString());
        }

        [TestMethod]
        public void ChangeLeftValue_WithANewValidBase64_ChangesTheValueCorrectly()
        {
            var base64Duo = new Base64Duo(1, null, new Base64("VGV4dCB0byBiZSBjb252ZXJ0ZWQ="));
            base64Duo.ChangeLeftValue(new Base64("VGV4dCB0byBiZSBjb21wYXJlZA=="));
            
            Assert.AreEqual("VGV4dCB0byBiZSBjb21wYXJlZA==", base64Duo.Left.ToString());
            Assert.AreEqual("VGV4dCB0byBiZSBjb252ZXJ0ZWQ=", base64Duo.Right.ToString());
        }

        [TestMethod]
        public void ChangeRightValue_WithANewValidBase64_ChangesTheValueCorrectly()
        {
            var base64Duo = new Base64Duo(1, new Base64("VGV4dCB0byBiZSBjb252ZXJ0ZWQ="), null);
            base64Duo.ChangeRightValue(new Base64("VGV4dCB0byBiZSBjb21wYXJlZA=="));

            Assert.AreEqual("VGV4dCB0byBiZSBjb252ZXJ0ZWQ=", base64Duo.Left.ToString());
            Assert.AreEqual("VGV4dCB0byBiZSBjb21wYXJlZA==", base64Duo.Right.ToString());
        }

        [TestMethod]
        public void UnderlyinStringsAreEqual_LeftAndRightHaveTheSameUnderlyingString_True()
        {
            var base64Duo = new Base64Duo(1, new Base64("VGV4dCB0byBiZSBjb252ZXJ0ZWQ="), new Base64("VGV4dCB0byBiZSBjb252ZXJ0ZWQ="));

            Assert.IsTrue(base64Duo.UnderlyingStringsAreEqual());
        }

        [TestMethod]
        public void UnderlyinStringsAreEqual_LeftAndRightHaveDifferentStringsWithSameLength_False()
        {
            var left = new Base64("dGVzdCBzdHJpbmc="); //test string
            var right = new Base64("VGVTdCBTdFJpTmc="); //TeSt StRiNg

            var base64Duo = new Base64Duo(1, left, right);

            Assert.IsFalse(base64Duo.UnderlyingStringsAreEqual());
        }

        [TestMethod]
        public void UnderlyingStringsHasSameLenth_LeftAndRightHaveDifferentStringsWithSameLength_True()
        {
            var left = new Base64("dGVzdCBzdHJpbmc="); //test string
            var right = new Base64("VGVTdCBTdFJpTmc="); //TeSt StRiNg

            var base64Duo = new Base64Duo(1, left, right);

            Assert.IsTrue(base64Duo.UnderlyingStringsHasSameLenth());
        }

        [TestMethod]
        public void UnderlyingStringsHasSameLenth_LeftHasAValueAndRightDont_Fase()
        {
            var left = new Base64("dGVzdCBzdHJpbmc="); //test string
            Base64 right = null;

            var base64Duo = new Base64Duo(1, left, right);

            Assert.IsFalse(base64Duo.UnderlyingStringsHasSameLenth());
        }

        [TestMethod]
        public void UnderlyingStringsHasSameLenth_RighttHasAValueAndLeftDont_Fase()
        {            
            Base64 left = null;
            var right = new Base64("dGVzdCBzdHJpbmc="); //test string

            var base64Duo = new Base64Duo(1, left, right);

            Assert.IsFalse(base64Duo.UnderlyingStringsHasSameLenth());
        }

        [TestMethod]
        public void UnderlyingStringsHasSameLenth_LeftAndRightHaveDifferentStringsWithDifferentLengths_False()
        {
            var left = new Base64("dGVzdCBzdHJpbmc=");
            var right = new Base64("VGV4dCB0byBiZSBjb21wYXJlZA==");

            var base64Duo = new Base64Duo(1, left, right);

            Assert.IsFalse(base64Duo.UnderlyingStringsHasSameLenth());
        }

        [TestMethod]
        public void Diff_TwoDifferentBase64InstanceWithSameUnderlyingString_RerturnsZeroErrors()
        {
            var left = new Base64("VGV4dCB0byBiZSBjb21wYXJlZA==");
            var right = new Base64("VGV4dCB0byBiZSBjb21wYXJlZA==");

            var base64Duo = new Base64Duo(1, left, right);

            var errors = base64Duo.Diff();

            Assert.AreEqual(0, errors.Count());
        }

        [TestMethod]
        public void Diff_TwoDifferentBase64InstanceWithDifferentUnderlyingString_RerturnsCorrectNumberOfErrors()
        {
            var left = new Base64("dGVzdCBzdHJpbmc="); //test string
            var right = new Base64("VGVTdCBTdFJpTmc="); //TeSt StRiNg

            var base64Duo = new Base64Duo(1, left, right);

            var errors = base64Duo.Diff();

            Assert.AreEqual(5, errors.Count());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Diff_LeftHasValueAndRightDont_ThrowsException()
        {
            var left = new Base64("dGVzdCBzdHJpbmc="); //test string
            Base64 right = null;

            var base64Duo = new Base64Duo(1, left, right);

            var errors = base64Duo.Diff();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Diff_RightHasValueAndLeftDont_ThrowsException()
        {
            Base64 left = null;
            var right = new Base64("dGVzdCBzdHJpbmc="); //test string
            

            var base64Duo = new Base64Duo(1, left, right);

            var errors = base64Duo.Diff();
        }
    }
}
