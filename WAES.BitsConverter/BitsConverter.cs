using System;
using System.Collections.Generic;
using System.Text;

namespace WAES.BitsConverter
{
    public class BitsConverter
    {
        public BitsConverter()
        {
            
        }
        
        /// <summary>
        /// Convert the hexadecimal string format to byte array
        /// </summary>
        /// <param name="hex">hexadecimal string</param>
        /// <returns>Byte Array Representation</returns>
        public byte[] FromHex(string hex)
        {
            byte[] raw = new byte[hex.Length / 2];
            for (int i = 0; i < raw.Length; i++)
            {
                raw[i] = Convert.ToByte(hex.Substring(i * 2, 2), 16);
            }
            return raw;
        }

        /// <summary>
        /// Convert the byte array to string of bit series
        /// </summary>
        /// <param name="input">Byte Array</param>
        /// <returns>string of bit representation</returns>
        public string BitStringResult(byte[] input)
        {
            StringBuilder builder = new StringBuilder();

            foreach (var item in input)
            {
                var ar = GetBits(item);
                string bitString = GetStringFromBitCollection(ar);
                builder.Append(bitString);
            }
            return builder.ToString();
        }


        IEnumerable<bool> GetBits(byte b)
        {
            for (int i = 0; i < 8; i++)
            {
                yield return (b & 0x80) != 0;
                b *= 2;
            }
        }
        
        /// <summary>
        /// Convert a List of bit (Boolean) to string
        /// </summary>
        /// <param name="input">bit list</param>
        /// <returns>String of bit list</returns>
        string GetStringFromBitCollection(IEnumerable<bool> input)
        {
            StringBuilder output = new StringBuilder();

            foreach (bool item in input)
            {
                string bitValue = (item.Equals(true) ? "1" : "0");
                output.Append(bitValue);
            }
            return output.ToString();
        }


    }
}