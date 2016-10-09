using System;

namespace Waes.Model
{
    /// <summary>
    /// This class simple holds the charaters the differs in postion of two strings
    /// </summary>
    public class Values
    {
        public Values(char current, char other)
        {
            if (current == other) throw new ArgumentException("Values current and other must be different");

            this.Current = current;
            this.Other = other;
        }

        public char Current { get; }
        public char Other { get; }
    }
}