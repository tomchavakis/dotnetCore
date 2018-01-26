using System;
using System.Text.RegularExpressions;
using WAES.BitsConverter;

namespace WAYS.Cryptography
{
    public class Methods
    {

        public static bool IsValidBase64(string message)
        {
            string pattern = "^([A-Za-z0-9+/]{4})*([A-Za-z0-9+/]{4}|[A-Za-z0-9+/]{3}=|[A-Za-z0-9+/]{2}==)$";
            if (Regex.IsMatch(message, pattern))
                return true;
            else
                return false;
        }
        
        public static string Base64EncodeText(string encodingMessage) {
            
            if (!string.IsNullOrEmpty(encodingMessage))
            {
                byte[] encodedBytes = System.Text.Encoding.UTF8.GetBytes(encodingMessage);
                return System.Convert.ToBase64String(encodedBytes);
            }

            throw new ArgumentException("input cannot be null or empty string");
        }
        
        
        public static string Base64DecodeText(string base64EncodedData) {
            
            if (!string.IsNullOrEmpty(base64EncodedData))
            {
                byte[] base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
                return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
            }

            throw new ArgumentException("input cannot be null or empty string");
        }

        /// <summary>
        /// Converts a base64 message to string of binary data
        /// </summary>
        /// <param name="message">Base64 Input Message</param>
        /// <returns>String of the Binary Data</returns>
        /// <exception cref="ArgumentException"></exception>
        public static string Base64DecodeToBinaryDataString(string message)
        {
            if (!string.IsNullOrEmpty(message))
            {
                BitsConverter bitsConverter = new BitsConverter();
                byte[] base64EncodedBytes = System.Convert.FromBase64String(message);            
                string result = bitsConverter.BitStringResult(base64EncodedBytes);
                
                return result;    
            }
            
            throw new ArgumentException("input cannot be null or empty string");
        }
        
    }
}