using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Monopoly.Accessors.Helpers;
using Monopoly.Accessors.Interfaces;
using Monopoly.Accessors.Models;
using Newtonsoft.Json;

namespace Monopoly.Accessors
{
    public class MonopolyAccessor : IMonopolyAccessor
    {
        private readonly ILogger<MonopolyAccessor> _logger;
        private readonly HttpClient _client;
        
        public MonopolyAccessor(IHttpClientFactory httpClientFactory, ILogger<MonopolyAccessor> logger)
        {
            _logger = logger;
            _client = httpClientFactory.CreateClient("MONOPOLY_API");
        }

        public async Task<BoardState> GetBoardState(int gameId)
        {
            var uriString = $"game?gameId={gameId}";
            var response = await _client.GetAsync(uriString);

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogError($"API Call Failed: {uriString}\n{JsonConvert.SerializeObject(response)}");
                return null;
            }

            return JsonConvert.DeserializeObject<BoardState>(await response.Content.ReadAsStringAsync());   
        }

        public async Task<bool> SaveBoardState(SaveBoardStateRequest stateRequest)
        {
            var uriString = "game";
            var response = await _client.PostAsync(uriString, stateRequest.SerializeRequest());

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogError($"Failed to Save Board State: {uriString}\n{JsonConvert.SerializeObject(response)}");
                return false;
            }

            return true;
        }
    }
}
