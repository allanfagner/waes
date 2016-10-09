using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace Waes.Model
{
    //I first thought this class as an immutable tiny type. However, Entity Framework is not very friendly with custom types.
    //So, I had to change my mind and make this type mutable. I also created the object graph that best suits the problem and let EF create the tables
    //Giving the code is a demo I believe that strategy works well enough and can be easily change if needed

    /// <summary>
    /// This class encapsulates the concept of a Base64 string.
    /// It only works with valid Base64 string both in length an content
    /// </summary>
    public class Base64 : IEquatable<Base64>
    {
        private string value;
        private byte[] bytes;

        protected Base64() { }

        public Base64(string value)
        {
            ValidateBase64String(value);
            this.Value = value;          
        }

        private void ValidateBase64String(string value)
        {
            if (value.Length % 4 != 0)
            {
                throw new ArgumentException("The length of the provided string is not valid");
            }
 
            if (value != "" & !Regex.IsMatch(value, @"^[a-zA-Z0-9+/]+={0,2}$"))
            {
                throw new ArgumentException("The providade value is not a valid Base64 string");
            }
        }

        public virtual int Id { get; private set; }

        //I am using this property strategy here so that EF can load the type properly
        public virtual string Value
        {
            get { return this.value; }
            private set
            {
                this.value = value;
                this.bytes = Convert.FromBase64String(value);
            }
        }

        /// <summary>
        /// Changes the value of the underlying string
        /// </summary>
        /// <param name="value">The new value</param>
        public void ChangeValue(string value)
        {
            ValidateBase64String(value);
            this.Value = value;
        }

        public override string ToString()
        {
            return this.value;
        }

        #region Those methods were implemented to keep consistency among various comparisons possibilities
        public bool Equals(Base64 other)
        {
            return this.value == other?.value;
        }

        public override bool Equals(object other)
        {
            if (other == null) return false;
            var otherBase64 = other as Base64;
            if (otherBase64 == null) return false;

            return this.value == otherBase64.value;
        }

        public override int GetHashCode()
        {
            return this.value.GetHashCode();
        }

        public static bool operator ==(Base64 left, Base64 right)
        {
            var objLeft = left as object;
            var objRight = right as object;

            if ((objLeft == null && objRight != null) ||
                (objLeft != null && objRight == null))
            {
                return false;
            }

            if (objLeft == null && objRight == null)
            {
                return true;
            }

            return left.value == right.value;
        }

        public static bool operator !=(Base64 left, Base64 right)
        {
            return !(left == right);
        } 
        #endregion

        /// <summary>
        /// Returns a string that represents the underlying string of the Base64
        /// </summary>
        /// <returns></returns>
        public string ToOriginalString()
        {
            return Encoding.Default.GetString(this.bytes);
        }

        /// <summary>
        /// Compares the length of the underlying string
        /// </summary>
        /// <param name="other">The other instance of Base64 that will be compared against</param>
        /// <returns>True if both underlying strings have the same length</returns>
        public bool SameLengthOf(Base64 other)
        {
            if (other == null) throw new ArgumentNullException("other");

            return this.bytes.Length == other.bytes.Length;
        }

        /// <summary>
        /// Compares the underlying string of this instance against the underlying string of the provided argument
        /// </summary>
        /// <param name="other">The other instance of Base64 that will be compared against </param>
        /// <returns>An enumerable containing all differences found. The difference contains the offset, length and not matching characters.</returns>        
        public IEnumerable<Diff> Diff(Base64 other)
        {
            if (other == null) throw new ArgumentNullException("other");
            if (!this.SameLengthOf(other)) throw new ArgumentException("Both strings must have the same length in order to be diffed");

            var diffList = new List<Diff>();
            bool diff;

            for (int index = 0; index < this.bytes.Length; index++)
            {
                diff = !(this.bytes[index].Equals(other.bytes[index]));

                if (diff)
                {
                    var values = new List<Values>();
                    var length = 0;
                    var offset = index;

                    while (diff)
                    {
                        values.Add(new Values(Convert.ToChar(this.bytes[index]), Convert.ToChar(other.bytes[index])));
                        length++;
                        index++;

                        if (index >= this.bytes.Length) break;

                        diff = !(this.bytes[index].Equals(other.bytes[index]));
                    }

                    diffList.Add(new Diff(offset, length, values.ToArray()));
                }
            }

            return diffList;
        }

        //When converting from custom type to base type (Base64 -> string) the implicit conversion is allowed.
        public static implicit operator string(Base64 b)
        {
            return b.value;
        }

        //When converting from base type to custom type (string -> Base64) an explicit conversion is needed.
        public static explicit operator Base64(string s)
        {
            return new Base64(s);
        }
    }    
}