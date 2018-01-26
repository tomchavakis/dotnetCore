using System;
using System.Diagnostics;
using NUnit.Framework;
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
        public void Base64Decode_DecodeEncodingString_ReturnDecodedMessage(string encodingMessage, string expectedResult)
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
        public void Base64DecodeToBinaryDataString_DecodeBase64Message_ReturnBinaryStringData()
        {
            
        }
        
    }
}