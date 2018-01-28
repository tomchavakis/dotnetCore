using System.Threading.Tasks;
using NUnit.Framework;
using WAES.Client;
using WAES.Model;

namespace WAES.Test.IntegrationTests
{
    [TestFixture]
    public class DifferencesFunctionalityTests
    {
        //const string basePath = "http://localhost:5000/api";
        const string basePath = "http://localhost:9000/api";


        /// <summary>
        /// Add base64 data to the database at specidic id
        /// </summary>
        /// <returns></returns>
        [SetUp]
        public async Task Setup()
        {
            MessageBinding bindingModel = new MessageBinding();
            bindingModel.Payload = "ZG9jdG9yV2hv"; //doctorWho
            await DifferencesClient.LeftTask(basePath, 1, 57, bindingModel); 
        }
        
        /// <summary>
        /// Create a POST Action to Test Left Method
        /// </summary>
        [Test]
        public void DifferencesLeft_AreEqualWithDifferentPayload_ReturnComparisonResult()
        {
            MessageBinding bindingModel = new MessageBinding();
            bindingModel.Payload = "ZG9jdG9yV2hp"; //doctorWhi
            ComparisonResult result = DifferencesClient.LeftTask(basePath, 1, 57, bindingModel).Result;
            Assert.AreEqual(result.AreEqual,ComparisonResultEnum.Equal);
            Assert.AreEqual(result.Offsets, new int[] {8});
            Assert.AreEqual(result.OffsetsLength, 1);
        }

        [Test]
        public void DifferencesLeft_AreNotEqual_ReturnComparisonResult()
        {
            MessageBinding bindingModel = new MessageBinding();
            bindingModel.Payload = "ZG9jdG9yU3RyYW5nZQ=="; //doctorStrange
            ComparisonResult result = DifferencesClient.LeftTask(basePath, 1, 57, bindingModel).Result;
            Assert.AreEqual(result.AreEqual,ComparisonResultEnum.NotEqual);
        }
       
        [Test]
        public void DifferencesRight_AreEqualWithDifferentPayload_ReturnComparisonResult()
        {
            MessageBinding bindingModel = new MessageBinding();
            bindingModel.Payload = "ZG9jdG9yV2hp"; //doctorWhi
            ComparisonResult result = DifferencesClient.RightTask(basePath, 1, 57, bindingModel).Result;
            Assert.AreEqual(result.AreEqual,ComparisonResultEnum.Equal);
            Assert.AreEqual(result.Offsets, new int[] {8});
            Assert.AreEqual(result.OffsetsLength, 1);
        }
        
        [Test]
        public void DifferencesRight_AreNotEqual_ReturnComparisonResult()
        {
            MessageBinding bindingModel = new MessageBinding();
            bindingModel.Payload = "ZG9jdG9yU3RyYW5nZQ=="; //doctorStrange
            ComparisonResult result = DifferencesClient.RightTask(basePath, 1, 57, bindingModel).Result;
            Assert.AreEqual(result.AreEqual,ComparisonResultEnum.NotEqual);
        }
    }
}