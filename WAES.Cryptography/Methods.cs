using System;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using WAES.BitsConverter;

namespace WAYS.Cryptography
{
    public class Methods
    {
        /// <summary>
        /// Checks if the string is a valid base64 string
        /// </summary>
        /// <param name="message">input string</param>
        /// <returns>Boolean Result</returns>
        public static bool IsValidBase64(string message)
        {
            string pattern = "^([A-Za-z0-9+/]{4})*([A-Za-z0-9+/]{4}|[A-Za-z0-9+/]{3}=|[A-Za-z0-9+/]{2}==)$";
            if (Regex.IsMatch(message, pattern) && !string.IsNullOrEmpty(message))
                return true;
            else
                return false;
        }

        /// <summary>
        /// Encodes the message using base64 encoding
        /// </summary>
        /// <param name="encodingMessage">string of the message</param>
        /// <returns>base64 encoded string message</returns>
        /// <exception cref="ArgumentException"></exception>
        public static string Base64EncodeText(string encodingMessage)
        {
            if (!string.IsNullOrEmpty(encodingMessage))
            {
                byte[] encodedBytes = System.Text.Encoding.UTF8.GetBytes(encodingMessage);
                return System.Convert.ToBase64String(encodedBytes);
            }

            throw new ArgumentException("input cannot be null or empty string");
        }


        /// <summary>
        /// Decodes the base64 string message to the original string message
        /// </summary>
        /// <param name="base64EncodedData">base64 string message</param>
        /// <returns>Original String Message</returns>
        /// <exception cref="ArgumentException"></exception>
        public static string Base64DecodeText(string base64EncodedData)
        {
            if (!string.IsNullOrEmpty(base64EncodedData))
            {
                byte[] base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
                return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
            }

            throw new ArgumentException("input cannot be null or empty string");
        }

        /// <summary>
        /// Converts a base64 message to byte Array
        /// </summary>
        /// <param name="message">Base64 Input Message</param>
        /// <returns>Byte Array</returns>
        /// <exception cref="ArgumentException">input cannot be null or empty string</exception>
        public static string Base64DecodeToBinaryDataString(string message)
        {
            if (IsValidBase64(message))
            {
                BitsConverter bitsConverter = new BitsConverter();
                byte[] base64EncodedBytes = System.Convert.FromBase64String(message);
                string result = bitsConverter.BitStringResult(base64EncodedBytes);

                return result;
            }

            throw new ArgumentException("input cannot be null or empty string");
        }

        /// <summary>
        /// Decodes the base64 input string to a representative byte array
        /// </summary>
        /// <param name="input">base64 message</param>
        /// <returns>Byte Array of the base64 message</returns>
        /// <exception cref="ArgumentException"></exception>
        public static byte[] DecodeBase64ToByteArray(string input)
        {
            if (IsValidBase64(input))
            {
                byte[] result = System.Convert.FromBase64String(input);
                return result;
            }

            throw new ArgumentException("input cannot be null or empty string");
        }
    }
}