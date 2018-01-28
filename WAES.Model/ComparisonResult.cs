using System;

namespace WAES.Model
{
    public class ComparisonResult
    {
        public ComparisonResultEnum AreEqual { get; set; }

        public int[] Offsets { get; set; }
        public int OffsetsLength { get; set; }
    }

    public enum ComparisonResultEnum
    {
        /// <summary>
        /// Comparison between 2 byte arrays are not equal of size
        /// </summary>
        NotEqual,

        /// <summary>
        /// Comparison between 2 byte arrays are equal of size
        /// </summary>
        Equal
    }
}