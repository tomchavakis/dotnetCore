using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using NUnit.Framework;
using WAES.BitsConverter;
using WAYS.Cryptography;

namespace WAES.WebApp.UnitTests
{
    [TestFixture]
    public class CryptographyTests
    {
        [Test]
        [TestCase("U3RyaW5nVG9CYXNlNjRFbmNvZGluZw==")]
        public void IsValidBase64Test_ValidBase64_ReturnTrue(string message)
        {
            bool result = Methods.IsValidBase64(message);
            Assert.IsTrue(result);
        }

        [Test]
        [TestCase("U3RyaW5nVG9CYXNlNjRFbmNvZGluZ==")]
        public void IsValidBase64Test_NotValidBase64_ReturnFalse(string message)
        {
            bool result = Methods.IsValidBase64(message);
            Assert.IsFalse(result);
        }


        [Test]
        [TestCase("StringToBase64Encoding", "U3RyaW5nVG9CYXNlNjRFbmNvZGluZw==")]
        [TestCase("ThomasChavakis", "VGhvbWFzQ2hhdmFraXM=")]
        public void Base64Encode_StringHasValue_ReturnBase64(string encodingMessage, string expectedResult)
        {
            string result = Methods.Base64EncodeText(encodingMessage);

            Assert.That(result, Is.EqualTo(expectedResult));
            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [Test]
        [TestCase("")]
        [TestCase(null)]
        public void Base64Encode_StringIsEmptyOrNull_ThrowsArguementNullException(string encodingMessage)
        {
            Assert.That(() => Methods.Base64EncodeText(encodingMessage), Throws.Exception.TypeOf<ArgumentException>());
        }

        [Test]
        [TestCase("U3RyaW5nVG9CYXNlNjRFbmNvZGluZw==", "StringToBase64Encoding")]
        [TestCase("VGhvbWFzQ2hhdmFraXM=", "ThomasChavakis")]
        public void Base64Decode_DecodeEncodingString_ReturnDecodedMessage(string encodingMessage,
            string expectedResult)
        {
            string result = Methods.Base64DecodeText(encodingMessage);

            Assert.That(result, Is.EqualTo(expectedResult));
            Assert.That(result, Is.EqualTo(expectedResult));
        }


        [Test]
        [TestCase("")]
        [TestCase(null)]
        public void Base64Decode_StringIsEmptyOrNull_ThrowsArguementNullException(string encodingMessage)
        {
            Assert.That(() => Methods.Base64DecodeText(encodingMessage), Throws.Exception.TypeOf<ArgumentException>());
        }

        [Test]
        [TestCase("VGhvbWFzQ2hhdmFraXM=",
            "0101010001101000011011110110110101100001011100110100001101101000011000010111011001100001011010110110100101110011")]
        public void Base64DecodeToBinaryDataString_DecodeBase64Message_ReturnBinaryStringData(string encodingMessage,
            string expextedResult)
        {
            string result = Methods.Base64DecodeToBinaryDataString(encodingMessage);
            Assert.That(result, Is.EqualTo(expextedResult));
        }

        [Test]
        [TestCase("VGhvbWFzQ2hhdmFraXM=", new byte[] {84, 104, 111, 109, 97, 115, 67, 104, 97, 118, 97, 107, 105, 115})]
        public void DecodeBase64ToByteArray_DecodeBase64Message_ReturnByteArray(string encodingMessage,
            byte[] expextedResult)
        {
            byte[] result = Methods.DecodeBase64ToByteArray(encodingMessage);
            Assert.That(result, Is.EqualTo(expextedResult));
        }

        [Test]
        [TestCase(new byte[] {84, 104, 111, 109, 97, 115, 67, 104, 97, 118, 97, 107, 105, 115}, new byte[]
            {84, 104, 111, 109, 97, 115, 67, 104, 97, 118, 97, 107, 105, 115})]
        public void CompareByteArrays_AreEqual_ReturnZeroDiffs(byte[] a, byte[] b)
        {
            List<int> result = new List<int>(); 
            result = BitsDiff.CompareByteArrays(a, b);
            Assert.AreEqual(result.Count, 0);
        }
        
        [Test]
        [TestCase(new byte[] {84, 104, 111, 109, 97, 115, 67, 104, 97, 118, 97, 107, 105, 115}, new byte[]
            {84, 104, 111, 109, 97, 115, 67, 104, 97, 118, 97, 107, 104, 114})]
        public void CompareByteArrays_AreEqual_Return2Diffs(byte[] a, byte[] b)
        {
            List<int> result = new List<int>(); 
            result = BitsDiff.CompareByteArrays(a, b);
            Assert.AreEqual(result.Count, 2);
        }
    }
}