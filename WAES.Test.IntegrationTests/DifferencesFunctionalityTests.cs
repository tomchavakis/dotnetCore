using System.Threading.Tasks;
using NUnit.Framework;
using WAES.Client;
using WAES.Model;

namespace WAES.Test.IntegrationTests
{
    [TestFixture]
    public class DifferencesFunctionalityTests
    {
        const string basePath = "http://localhost:5000/api";
        //private string basePath = "http://localhost:9000/";


        /// <summary>
        /// Add base64 data to the database at specidic id
        /// </summary>
        /// <returns></returns>
        [SetUp]
        public async Task Setup()
        {
            MessageBinding bindingModel = new MessageBinding();
            bindingModel.Payload = "ZG9jdG9yV2hv"; //doctorWho
            await DifferencesClient.LefTask(basePath, 1, 15, bindingModel); 
        }
        
        /// <summary>
        /// Create a POST Action to Test Left Method
        /// </summary>
        [Test]
        public void DifferencesLeftTest()
        {
            MessageBinding bindingModel = new MessageBinding();
            bindingModel.Payload = "ZG9jdG9yV2hp"; //doctorWhi
            ComparisonResult result = DifferencesClient.LefTask(basePath, 1, 15, bindingModel).Result;
            Assert.AreEqual(result.AreEqual,ComparisonResultEnum.Equal);
            Assert.AreEqual(result.Offsets, new int[] {8});
            Assert.AreEqual(result.OffsetsLength, 1);
        }
    }
}