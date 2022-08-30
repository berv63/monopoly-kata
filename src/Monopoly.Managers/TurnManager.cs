using System;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Monopoly.Accessors.Interfaces;
using Monopoly.Accessors.Models;
using Monopoly.Managers.Interfaces;

namespace Monopoly.Managers
{
    public class TurnManager: ITurnManager
    {
        private readonly ILogger<TurnManager> _logger;
        private readonly IMonopolyAccessor _monopolyAccessor;

        public TurnManager(ILogger<TurnManager> logger, IMonopolyAccessor monopolyAccessor)
        {
            _logger = logger;
            _monopolyAccessor = monopolyAccessor;
        }

        public async Task TakePlayerTurn(int gameId)
        {
            var boardState = await _monopolyAccessor.GetBoardState(gameId);

            var rand = new Random();
            var dieRoll1 = rand.Next(1, 7);
            var dieRoll2 = rand.Next(1, 7);
            
            var currentPlayer = boardState.Players.First(x => x.PlayerNumber == boardState.PlayerTurn);
            currentPlayer.CurrentLocation = (LocationEnum)(((int)currentPlayer.CurrentLocation + dieRoll1 + dieRoll2) % 40);

            //todo: determine action
            
            boardState.PlayerTurn = (boardState.PlayerTurn) % (boardState.Players.Count) + 1;
            
            var stateRequest = new SaveBoardStateRequest
            {
                GameId = gameId,
                BoardState = boardState
            };
            
            await _monopolyAccessor.SaveBoardState(stateRequest);
        }
    }
}
