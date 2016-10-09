using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using Waes.Model;

namespace Waes.Tests.Unity
{
    [TestClass]    
    public class Base64Test
    {
        //VGV4dCB0byBiZSBjb252ZXJ0ZWQ= - Text to be converted
        //VGV4dCB0byBiZSBjb21wYXJlZA== - Text to be compared
        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "The length of the provided string is not valid")]
        public void Create_WithInvalidStringLenght_ThrowsArgumentException()
        {
            new Base64("VGhpcyBpcyBhI/HNp");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "The providade value is not a valid Base64 string")]
        public void Create_StringEndingWithThreeEqualSign_ThrowsArgumentException()
        {
            new Base64("VGhpcyBpcyBhI/HNp===");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "The providade value is not a valid Base64 string")]
        public void Create_StringEndingWithFourEqualSign_ThrowsArgumentException()
        {
            new Base64("VGhpcyBpcyBI/HNp====");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "The providade value is not a valid Base64 string")]
        public void Create_StringWithInvalidCharacter_ThrowsArgumentException()
        {
            new Base64(@"VGV4dCB0byBi\SBjb252ZXJ0ZWQ=");
        }

        [TestMethod]        
        public void ExplicitStrigConversion_ValidString_CreatesBase64ClassCorrectly()
        {
            Base64 b = (Base64)"VGV4dCB0byBiZSBjb252ZXJ0ZWQ=";
        }

        [TestMethod]
        public void ImplicitBase64Conversion_ValidString_ReturnsStringCorrectly()
        {
            var base64String = "VGV4dCB0byBiZSBjb252ZXJ0ZWQ=";
            Base64 b = new Base64(base64String);
            string s = b;

            Assert.AreEqual(base64String, s);
        }

        [TestMethod]        
        public void Create_ValidStringBase64_ClassIsCreatedCorrected()
        {
            var base64String = "VGV4dCB0byBiZSBjb252ZXJ0ZWQ=";
            var base64 = new Base64(base64String);

            Assert.AreEqual(base64.ToString(), base64String);
        }

        [TestMethod]
        public void Create_EmptyString_ClassIsCreatedCorrected()
        {
            var base64 = new Base64("");

            Assert.AreEqual(base64.ToString(), "");
        }

        [TestMethod]
        public void ToString_ReturnsBase64StringUsedToCreateTheObject()
        {
            var base64String = "VGV4dCB0byBiZSBjb252ZXJ0ZWQ=";
            var base64 = new Base64(base64String);

            Assert.AreEqual(base64String, base64.ToString());
        }

        [TestMethod]
        public void Equals_TwoDifferentBase64_ReturnsFalse()
        {
            var base64 = new Base64("VGV4dCB0byBiZSBjb252ZXJ0ZWQ=");
            var other = new Base64("VGV4dCB0byBiZSBjb21wYXJlZA==");

            Assert.IsFalse(base64.Equals(other));
        }

        [TestMethod]
        public void Equals_SameBase64_ReturnsTrue()
        {
            var base64 = new Base64("VGV4dCB0byBiZSBjb252ZXJ0ZWQ=");
            var other = new Base64("VGV4dCB0byBiZSBjb252ZXJ0ZWQ=");

            Assert.IsTrue(base64.Equals(other));
        }

        [TestMethod]
        public void Equals_CompareAgainstNull_ReturnFalse()
        {
            var base64 = new Base64("VGV4dCB0byBiZSBjb252ZXJ0ZWQ=");

            Assert.IsFalse(base64.Equals(null));
        }

        [TestMethod]
        public void Equals_Base64AgainsNullObject_ReturnsFalse()
        {
            var base64 = new Base64("VGV4dCB0byBiZSBjb252ZXJ0ZWQ=");
            object other = null;

            Assert.IsFalse(base64.Equals(other));
        }

        [TestMethod]
        public void Equals_Base64AgainstAnObjectWhichIsNotBase64_ReturnsFalse()
        {
            var base64 = new Base64("VGV4dCB0byBiZSBjb252ZXJ0ZWQ=");
            object other = "VGV4dCB0byBiZSBjb252ZXJ0ZWQ=";

            Assert.IsFalse(base64.Equals(other));
        }

        [TestMethod]
        public void Equals_Base64AgainstAnObjectWhichIsTheSameBase64_ReturnsTrue()
        {
            var base64 = new Base64("VGV4dCB0byBiZSBjb252ZXJ0ZWQ=");
            object other = new Base64("VGV4dCB0byBiZSBjb252ZXJ0ZWQ=");

            Assert.IsTrue(base64.Equals(other));
        }

        [TestMethod]
        public void Equals_Base64AgainstAnObjectWhichIsADifferentBase64_ReturnsTrue()
        {
            var base64 = new Base64("VGV4dCB0byBiZSBjb252ZXJ0ZWQ=");
            object other = new Base64("VGV4dCB0byBiZSBjb21wYXJlZA==");

            Assert.IsFalse(base64.Equals(other));
        }

        [TestMethod]
        public void ToOriginalJson_ReturnsTheStringThatOriginatedTheBase64String()
        {
            var base64 = new Base64("VGV4dCB0byBiZSBjb252ZXJ0ZWQ=");

            Assert.AreEqual("Text to be converted", base64.ToOriginalString());
        }

        [TestMethod]
        public void EqualOperador_SameInstanceOfBase64_ReturnsTrue()
        {
            var base64 = new Base64("VGV4dCB0byBiZSBjb252ZXJ0ZWQ=");

            Assert.IsTrue(base64 == base64);
        }

        [TestMethod]
        public void NotEqualOperador_SameInstanceOfBase64_ReturnsFalse()
        {
            var base64 = new Base64("VGV4dCB0byBiZSBjb252ZXJ0ZWQ=");

            Assert.IsFalse(base64 != base64);
        }

        [TestMethod]
        public void EqualOperador_TwoDifferentInsancesWithSameValue_ReturnsTrue()
        {
            var base64 = new Base64("VGV4dCB0byBiZSBjb252ZXJ0ZWQ=");
            var otherBase64 = new Base64("VGV4dCB0byBiZSBjb252ZXJ0ZWQ=");

            Assert.IsTrue(base64 == otherBase64);
        }

        [TestMethod]
        public void NotEqualOperador_TwoDifferentInsancesWithSameValue_ReturnsFalse()
        {
            var base64 = new Base64("VGV4dCB0byBiZSBjb252ZXJ0ZWQ=");
            var otherBase64 = new Base64("VGV4dCB0byBiZSBjb252ZXJ0ZWQ=");

            Assert.IsFalse(base64 != otherBase64);
        }

        [TestMethod]
        public void EqualOperador_TwoNullBase64Instances_ReturnsTrue()
        {
            Base64 base64 = null;
            Base64 otherBase64 = null;

            Assert.IsTrue(base64 == otherBase64);
        }

        [TestMethod]
        public void NotEqualOperador_TwoNullBase64Instances_ReturnsFalse()
        {
            Base64 base64 = null;
            Base64 otherBase64 = null;

            Assert.IsFalse(base64 != otherBase64);
        }

        [TestMethod]
        public void EqualOperador_TwoDifferentInsancesWithDifferentValues_ReturnsFalse()
        {
            var base64 = new Base64("VGV4dCB0byBiZSBjb252ZXJ0ZWQ=");
            var otherBase64 = new Base64("VGV4dCB0byBiZSBjb21wYXJlZA==");

            Assert.IsFalse(base64 == otherBase64);
        }

        [TestMethod]
        public void NotEqualOperador_TwoDifferentInsancesWithDifferentValues_ReturnsTrue()
        {
            var base64 = new Base64("VGV4dCB0byBiZSBjb252ZXJ0ZWQ=");
            var otherBase64 = new Base64("VGV4dCB0byBiZSBjb21wYXJlZA==");

            Assert.IsTrue(base64 != otherBase64);
        }

        [TestMethod]
        public void EqualOperador_OneBase64InstanceAndNull_ReturnsFalse()
        {
            Base64 base64 = new Base64("VGV4dCB0byBiZSBjb252ZXJ0ZWQ=");
            Base64 otherBase64 = null;

            Assert.IsFalse(base64 == otherBase64);
        }

        [TestMethod]
        public void NotEqualOperador_OneBase64InstanceAndNull_ReturnsTrue()
        {
            Base64 base64 = new Base64("VGV4dCB0byBiZSBjb252ZXJ0ZWQ=");
            Base64 otherBase64 = null;

            Assert.IsTrue(base64 != otherBase64);
        }

        [TestMethod]
        public void SameLength_SameInstance_ReturnsTrue()
        {
            Base64 base64 = new Base64("VGV4dCB0byBiZSBjb252ZXJ0ZWQ=");

            Assert.IsTrue(base64.SameLengthOf(base64));
        }

        [TestMethod]
        public void SameLength_TwoDifferentInstancesWithSameLength_ReturnsTrue()
        {
            Base64 base64 = new Base64("VGV4dCB0byBiZSBjb252ZXJ0ZWQ=");
            Base64 otherBase64 = new Base64("VGV4dCB0byB3edg7ujk2ZXJ0ZWQ=");

            Assert.IsTrue(base64.SameLengthOf(otherBase64));
        }

        [TestMethod]
        public void SameLength_TwoDifferentInstancesWithDifferentLength_ReturnsFalse()
        {
            Base64 base64 = new Base64("YQ=="); //letter a
            Base64 otherBase64 = new Base64("YWI="); //letters ab

            Assert.IsFalse(base64.SameLengthOf(otherBase64));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void SameLength_CompareAgainstNull_ThrowsArgumentNullException()
        {
            Base64 base64 = new Base64("VGV4dCB0byBiZSBjb252ZXJ0ZWQ=");

            base64.SameLengthOf(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Diff_DiffAgainstNull_ThrowsArgumentNullException()
        {
            Base64 base64 = new Base64("VGV4dCB0byBiZSBjb252ZXJ0ZWQ=");

            base64.Diff(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Both strings must have the same length in order to be diffed")]
        public void Diff_TwoDifferentInstancesWithDifferentLength_ThrowsArgumentException()
        {
            Base64 base64 = new Base64("YQ=="); //letter a
            Base64 otherBase64 = new Base64("YWI="); //letters ab

            base64.Diff(otherBase64);
        }

        [TestMethod]
        public void Diff_SameInstance_ReturnsNoErrors()
        {
            Base64 base64 = new Base64("VGV4dCB0byBiZSBjb252ZXJ0ZWQ=");

            var result = base64.Diff(base64);

            Assert.AreEqual(0, result.Count());
        }

        [TestMethod]
        public void Diff_DifferentInstancesWithSameUnderlayingString_ReturnsNoErrors()
        {
            Base64 base64 = new Base64("VGV4dCB0byBiZSBjb252ZXJ0ZWQ=");
            Base64 otherBase64 = new Base64("VGV4dCB0byBiZSBjb252ZXJ0ZWQ=");

            var result = base64.Diff(otherBase64);

            Assert.AreEqual(0, result.Count());
        }

        [TestMethod]
        public void Diff_TwoDifferentBase64InstancesWithOneCharaterDifferent_ReturnsOneError()
        {
            Base64 base64 = new Base64("YQ=="); //a
            Base64 otherBase64 = new Base64("QQ=="); //A

            var result = base64.Diff(otherBase64);            

            Assert.AreEqual(1, result.Count());            
        }

        [TestMethod]
        public void Diff_TwoDifferentBase64InstancesWithOneCharaterDifferent_ReturnsOneErrorWithProperValues()
        {
            Base64 base64 = new Base64("YQ=="); //a
            Base64 otherBase64 = new Base64("QQ=="); //A

            var result = base64.Diff(otherBase64).First();

            //All of those are one logical assert
            Assert.AreEqual(0, result.Offest);
            Assert.AreEqual(1, result.Length);
            Assert.AreEqual('a', result.Diffs.First().Current);
            Assert.AreEqual('A', result.Diffs.First().Other);
        }

        [TestMethod]
        public void ChangeValue_ChangeBase64ValueToAnotherBase64ValidValue_TheValueIsChangedCorrectly()
        {
            var base64 = new Base64("VGV4dCB0byBiZSBjb252ZXJ0ZWQ=");

            base64.ChangeValue("VGV4dCB0byBiZSBjb21wYXJlZA==");

            Assert.AreEqual("VGV4dCB0byBiZSBjb21wYXJlZA==", base64.ToString());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException),"The providade value is not a valid Base64 string")]
        public void ChangeValue_ChangeBase64ValueToAnotherBase64InvalidValue_Throws()
        {
            var base64 = new Base64("VGV4dCB0byBiZSBjb252ZXJ0ZWQ=");

            base64.ChangeValue("VGV4dCB0byBiZSBjb21wYXlZA==");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "The length of the provided string is not valid")]
        public void ChangeValue_ChangeBase64ValueToAnotherBase64OfInvalidLength_Throws()
        {
            var base64 = new Base64("VGV4dCB0byBiZSBjb252ZXJ0ZWQ=");

            base64.ChangeValue("VGV4dCB0byBiZSBjb252ZXJ0ZWQ");
        }

        [TestMethod]
        public void Diff_TwoDifferentBase64InstancesWithOneTwoSetsOfDifferentCharacters_ReturnsTwoError()
        {
            Base64 base64 = new Base64("eyBuYW1lOiAnYWxsYW4nLCBsYXN0TmFtZTogJ2ZlcnJlaXJhJyB9"); //{ name: 'allan', lastName: 'ferreira' }
            Base64 otherBase64 = new Base64("eyBuYW1lOiAnQUxMYW4nLCBsYXN0TmFtZTogJ2ZlUlJFSVJBJyB9"); //{ name: 'ALLan', lastName: 'feRREIRA' }

            var result = base64.Diff(otherBase64);

            Assert.AreEqual(2, result.Count());
        }

        [TestMethod]
        public void Diff_TwoDifferentBase64InstancesWithOneTwoSetsOfDifferentCharacters_ReturnsTwoErrorsWithProperValues()
        {
            Base64 base64 = new Base64("eyBuYW1lOiAnYWxsYW4nLCBsYXN0TmFtZTogJ2ZlcnJlaXJhJyB9"); //{ name: 'allan', lastName: 'ferreira' }
            Base64 otherBase64 = new Base64("eyBuYW1lOiAnQUxMYW4nLCBsYXN0TmFtZTogJ2ZlUlJFSVJBJyB9"); //{ name: 'ALLan', lastName: 'feRREIRA' }

            var result = base64.Diff(otherBase64);

            var error1 = result.First();
            var error2 = result.Last();

            Assert.AreEqual(9, error1.Offest);
            Assert.AreEqual(3, error1.Length);


            Assert.AreEqual(30, error2.Offest);
            Assert.AreEqual(6, error2.Length);
        }
    }    
}
