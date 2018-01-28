using System.Net.Http;
using System.Threading.Tasks;
using WAES.Model;

namespace WAES.Client
{
    public class DifferencesClient
    {
        public static async Task<ComparisonResult> LeftTask(string basePath, int version,int id,  MessageBinding model)
        {
            string apiPath = "{0}/v{1}/diff/{2}/left";

            string methodUrl = string.Format(apiPath, basePath, version, id);

            using (HttpClient client = new HttpClient())
            {
                return await client.MethodPost<ComparisonResult, MessageBinding>(basePath, methodUrl, model);
            }
        }
        
        public static async Task<ComparisonResult> RightTask(string basePath, int version,int id,  MessageBinding model)
        {
            string apiPath = "{0}/v{1}/diff/{2}/right";

            string methodUrl = string.Format(apiPath, basePath, version, id);

            using (HttpClient client = new HttpClient())
            {
                return await client.MethodPost<ComparisonResult, MessageBinding>(basePath, methodUrl, model);
            }
        }
        
        public static async Task<ComparisonResult> Middle(string basePath, int version,int id,  ComparisonResult model)
        {
            string apiPath = "{0}/v{1}/diff/{2}";

            string methodUrl = string.Format(apiPath, basePath, version, id);

            using (HttpClient client = new HttpClient())
            {
                return await client.MethodPost<ComparisonResult, ComparisonResult>(basePath, methodUrl, model);
            }
        }
    }
}