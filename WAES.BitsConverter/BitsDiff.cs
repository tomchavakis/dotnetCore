using System.Collections.Generic;
using System.Linq;
using WAES.Model;

namespace WAES.BitsConverter
{
    public class BitsDiff
    {
        public BitsDiff()
        {
        }

        public static ComparisonResult CompareByteArrays(byte[] a, byte[] b)
        {
            List<int> positions = new List<int>();
            ComparisonResult result = new ComparisonResult();
            if (a.Length == b.Length)
            {
                for (int i = 0; i < a.Length; i++)
                {
                    if (a[i] != b[i])
                    {
                        positions.Add(i);
                    }
                }

                result.AreEqual = ComparisonResultEnum.Equal;
                result.Offsets = positions.ToArray();
                result.OffsetsLength = positions.Count;
                return result;
            }
            else
            {
                result.AreEqual = ComparisonResultEnum.NotEqual;
                return result;
            }
        }
    }
}