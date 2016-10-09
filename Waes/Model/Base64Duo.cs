using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Waes.Model
{
    /// <summary>
    /// This class encapsulates a left and right Base64 strings for comparison
    /// </summary>    
    public class Base64Duo
    {
        public Base64Duo(int id, Base64 left, Base64 right)
        {
            if (id <= 0) throw new ArgumentException("Invalid id");
            if (left == null && right == null) throw new ArgumentException("Either left or right should be informed");

            this.Id = id;
            //If either left or right is not providade an empty object is created. That way, there is no need to worry about null references
            this.Left = left ?? new Base64("");
            this.Right = right ?? new Base64("");
        }

        protected Base64Duo() { }

        //Entity Framework requires an explicity pk to work. Given the name "Id" is used to identify the base64 pair, I am changing the
        //pk name to Key. That way I can use the Id concept
        [Key]
        public virtual int Key { get; private set; }

        public virtual int Id { get; private set; }        
        public virtual Base64 Left { get; private set; }
        public virtual Base64 Right { get; private set; }

        /// <summary>
        /// Changes the value of the left Base64 string
        /// </summary>
        /// <param name="left">The new value</param>
        public void ChangeLeftValue(Base64 left)
        {
            if (left == null) throw new ArgumentNullException("left");

            this.Left.ChangeValue(left.Value);
        }

        /// <summary>
        /// Changes the value of the right Base64 string
        /// </summary>
        /// <param name="left">The new value</param>
        public void ChangeRightValue(Base64 right)
        {
            if (right == null) throw new ArgumentNullException("right");

            this.Right.ChangeValue(right.Value);
        }

        /// <summary>
        /// Compares if the underlying strings of both left and right Base64 string are equal
        /// </summary>
        /// <returns>True if both underlying strings are equal. Otherwise false.</returns>
        public bool UnderlyingStringsAreEqual()
        {
            return this.Left == this.Right;
        }

        /// <summary>
        /// Compares if the underlying stings of both left and right Base64 string have the same length
        /// </summary>
        /// <returns>True if both underlying strings have the same size. Otherwise false.</returns>
        public bool UnderlyingStringsHasSameLenth()
        {
            return this.Left.SameLengthOf(this.Right);
        }

        /// <summary>
        /// Finds the differences between left and right Base64 string. Both underlying strings must have the same length.
        /// </summary>
        /// <returns>A list with the differences found</returns>
        public IEnumerable<Diff> Diff()
        {
            return this.Left.Diff(this.Right);
        }
    }
}