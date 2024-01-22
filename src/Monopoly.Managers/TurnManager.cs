using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Monopoly.Accessors.Interfaces;
using Monopoly.Accessors.Models;
using Monopoly.Engines.Interfaces;
using Monopoly.Managers.Interfaces;

namespace Monopoly.Managers
{
    public class TurnManager: ITurnManager
    {
        private readonly ILogger<TurnManager> _logger;
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
            var boardState = await _monopolyAccessor.GetBoardState(gameId);

            var diceRoll = _rollEngine.RollDice();
            
            var currentPlayer = _turnEngine.GetCurrentPlayer(boardState);
            currentPlayer.CurrentLocation = _turnEngine.GetPlayerNewLocation(currentPlayer, diceRoll);

            //Take location action
            //todo: determine action

            boardState.PlayerTurn = _turnEngine.GetNextPlayerTurn(boardState, diceRoll);
            
            var stateRequest = new SaveBoardStateRequest
            {
                GameId = gameId,
                BoardState = boardState
            };
            await _monopolyAccessor.SaveBoardState(stateRequest);

            return boardState;
        }
    }
}
