using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Monopoly.Accessors.Helpers;
using Monopoly.Accessors.Interfaces;
using Monopoly.Accessors.Models;
using Monopoly.Engines.Interfaces;
using Monopoly.Managers.Interfaces;
using Newtonsoft.Json;

namespace Monopoly.Managers
{
    public class TurnManager: ITurnManager
    {
        private readonly ILogger<TurnManager> _logger;
        private readonly HttpClient _client;
        private readonly IMonopolyAccessor _monopolyAccessor;
        private readonly IRollEngine _rollEngine;
        private readonly ITurnEngine _turnEngine;

        public TurnManager(ILogger<TurnManager> logger,
            IRollEngine rollEngine,
            ITurnEngine turnEngine,
            IMonopolyAccessor monopolyAccessor)
        {
            _logger = logger;
            _rollEngine = rollEngine;
            _turnEngine = turnEngine;
            _monopolyAccessor = monopolyAccessor;
        }

        public async Task<BoardState> TakePlayerTurn(int gameId)
        {
            //Get state
            var getUri = $"game?gameId={gameId}";
            var getResponse = await _client.GetAsync(getUri);

            if (!getResponse.IsSuccessStatusCode)
            {
                _logger.LogError($"API Call Failed: {getUri}\n{JsonConvert.SerializeObject(getResponse)}");
                return new BoardState();
            }

            var boardState = JsonConvert.DeserializeObject<BoardState>(await getResponse.Content.ReadAsStringAsync());

            //Roll Dice
            var rand = new Random();
            var dieRoll1 = rand.Next(1, 7);
            var dieRoll2 = rand.Next(1, 7);
            
            //Move Player
            var currentPlayer = boardState.Players.First(x => x.PlayerNumber == boardState.PlayerTurn);
            currentPlayer.CurrentLocation = (LocationEnum)(((int)currentPlayer.CurrentLocation + dieRoll1 + dieRoll2) % 40);

            //Take location action
            //todo: determine action
            
            //Determine next player
            boardState.PlayerTurn = (boardState.PlayerTurn) % (boardState.Players.Count) + 1;
            
            //Save state
            var stateRequest = new SaveBoardStateRequest
            {
                GameId = gameId,
                BoardState = boardState
            };
            
            var saveUri = "game";
            var saveResponse = await _client.PostAsync(saveUri, stateRequest.SerializeRequest());

            if (!saveResponse.IsSuccessStatusCode)
            {
                _logger.LogError($"Failed to Save Board State: {saveUri}\n{JsonConvert.SerializeObject(saveResponse)}");
                return new BoardState();
            }

            return boardState;
        }
    }
}
