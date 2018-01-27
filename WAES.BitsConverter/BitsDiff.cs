using System.Collections.Generic;
using System.Linq;

namespace WAES.BitsConverter
{
    public class BitsDiff
    {
        public static List<int> CompareByteArrays(byte[] a, byte[] b)
        {
            List<int> positions = new List<int>();
            
            if (a.Length == b.Length)
            {
              
                for (int i = 0; i < a.Length; i++)
                {
                    if (a[i] != b[i])
                    {
                        positions.Add(i);
                    }
                }

                return positions;
            }
            else
            {
                return positions;
            }
        }
    }
}