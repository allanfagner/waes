using System;
using System.Collections.Generic;
using System.Linq;

namespace Waes.Model
{
    public class Diff
    {
        public Diff(int offset, int length, IEnumerable<Values> diffs)
        {
            if (offset < 0) throw new ArgumentException("Invalid offset");
            if (length <= 0) throw new ArgumentException("Invalid length");
            if (diffs == null) throw new ArgumentNullException("diffs");
            if (diffs.Count() != length) throw new ArgumentException("The number of diffs and the length do not match");

            this.Offest = offset;
            this.Length = length;
            this.Diffs = diffs;
        }

        public IEnumerable<Values> Diffs { get; }
        public int Length { get; }
        public int Offest { get; }
    }
}