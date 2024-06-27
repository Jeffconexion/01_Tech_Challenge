using LocalFriendzApi.Core.IIntegration;
using LocalFriendzApi.Core.Responses;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace LocalFriendzApi.Infrastructure.Integration
{
    public class InfoDDDIntegration : IInfoDDDIntegration
    {
        public async Task<bool> GetDDDInfo(int ddd)
        {
            using (HttpClient client = new HttpClient())
            {
                string url = $"https://brasilapi.com.br/api/ddd/v1/{ddd}";
                HttpResponseMessage response = await client.GetAsync(url);
                return response.IsSuccessStatusCode;
              
            }
        }

     
    }
}
